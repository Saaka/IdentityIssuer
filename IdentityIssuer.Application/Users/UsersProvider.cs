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
        Task<TenantUser> GetUserAsync(string guid);
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

        public async Task<TenantUser> GetUserAsync(string guid)
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
    }
}