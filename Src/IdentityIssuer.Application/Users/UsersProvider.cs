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
        Task<int> GetUserId(Guid guid);
    }

    public class UsersProvider : IUsersProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheStore _cache;

        public UsersProvider(IUserRepository userRepository,
            ICacheStore cache)
        {
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<TenantUser> GetUser(int id, int tenantId)
        {
            var result = await _cache.GetOrCreateAsync($"{CacheConstants.UserCachePrefix}{id}_{tenantId}",
                async (ce) =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var user = await _userRepository.GetUser(id, tenantId);

                    return user;
                });
            return result;
        }

        public async Task<int> GetUserId(Guid guid)
        {
            var result = await _cache.GetOrCreateAsync($"{CacheConstants.UserIdCachePrefix}{guid}",
                async (ce) =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var user = await _userRepository.GetUserId(guid);
                    if (user == 0)
                        throw new DomainException(ExceptionCode.UserNotFound, 
                            new { userGuid = guid });

                    return user;
                });
            return result;
        }
    }
}