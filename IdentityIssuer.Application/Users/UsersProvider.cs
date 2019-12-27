using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Exceptions;

namespace IdentityIssuer.Application.Users
{
    public interface IUsersProvider
    {
        Task<TenantUser> GetUser(string guid);
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

        public async Task<TenantUser> GetUser(string guid)
        {
            var result = await cache.GetOrCreateAsync($"{CacheConstants.UserCachePrefix}{guid}",
                async (ce) =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var user = await userRepository.GetUser(guid);
                    if (user == null)
                        throw new UserNotFoundException(guid);

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
                        throw new UserNotFoundException(guid);

                    return user;
                });
            return result;
        }
    }
}