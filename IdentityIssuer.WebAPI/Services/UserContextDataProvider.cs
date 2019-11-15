using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IdentityIssuer.WebAPI.Services
{
    public interface IUserContextDataProvider
    {
        Task<int> GetUser(HttpContext context);
    }

    public class UserContextDataProvider
    {
        public async Task<int> GetUser(HttpContext context)
        {
            var userCode = GetUserCodeFromContext(context);

            var userId = await GetUserId(userCode);
            return userId;
        }

        private async Task<int> GetUserId(string userCode)
        {
            throw new System.NotImplementedException();
        }

        private string GetUserCodeFromContext(HttpContext context)
        {
            if (context.User?.Claims == null || !context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                throw new InvalidOperationException("Can't authenticate current user");

            var userCode = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userCode;
        }
    }
}