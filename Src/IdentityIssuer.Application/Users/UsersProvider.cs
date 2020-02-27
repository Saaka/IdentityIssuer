using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;

namespace IdentityIssuer.Application.Users
{
    public interface IUsersProvider
    {
        Task<TenantUser> GetUser(int id, int tenantId);
        Task<int> GetUserId(string guid);
    }

    public class UsersProvider : IUsersProvider
    {
        private readonly IUserRepository userRepository;
        private readonly ICacheStore cache;

        public UsersProvider(IUserRepository userRepository,
            ICacheStore cache)
        {
            this.userRepository = userRepository;
            this.cache = cache;
        }

        public async Task<TenantUser> GetUser(int id, int tenantId)
        {
            var result = await cache.GetOrCreateAsync($"{CacheConstants.UserCachePrefix}{id}_{tenantId}",
                async (ce) =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var user = await userRepository.GetUser(id, tenantId);

                    return user;
                });
            return result;
        }

        public async Task<int> GetUserId(string guid)
        {
            var result = await cache.GetOrCreateAsync($"{CacheConstants.UserIdCachePrefix}{guid}",
                async (ce) =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var user = await userRepository.GetUserId(guid);
                    if (user == 0)
                        throw new DomainException(ExceptionCode.UserNotFound, 
                            new { userGuid = guid });

                    return user;
                });
            return result;
        }
    }
}