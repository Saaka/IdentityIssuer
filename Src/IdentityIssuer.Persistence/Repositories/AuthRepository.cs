using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityIssuer.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<TenantUserEntity> _userManager;

        public AuthRepository(AppIdentityContext context,
            IMapper mapper,
            UserManager<TenantUserEntity> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> IsEmailRegisteredForTenant(string email, int tenantId)
        {
            var normalizedEmail = email.ToUpper();
            var query = from u in _context.Users
                where u.TenantId == tenantId &&
                      u.NormalizedEmail == normalizedEmail
                select u;

            return await query.AnyAsync();
        }

        public async Task<bool> GoogleUserExists(string externalUserId, int tenantId)
        {
            var query = from u in _context.Users
                where u.TenantId == tenantId &&
                      u.GoogleId == externalUserId
                select u.Id;

            return await query.AnyAsync();
        }

        public async Task<bool> FacebookUserExists(string externalUserId, int tenantId)
        {
            var query = from u in _context.Users
                where u.TenantId == tenantId &&
                      u.FacebookId == externalUserId
                select u.Id;

            return await query.AnyAsync();
        }

        public async Task<TenantUser> AddGoogleLoginToUser(int tenantId, string email, string externalUserId)
        {
            var user = await GetUserForTenant(email, tenantId);

            user.GoogleId = externalUserId;

            return await UpdateUser(user, nameof(AddGoogleLoginToUser));
        }

        public async Task<TenantUser> AddFacebookLoginToUser(int tenantId, string email, string externalUserId)
        {
            var user = await GetUserForTenant(email, tenantId);

            user.FacebookId = externalUserId;

            return await UpdateUser(user, nameof(AddFacebookLoginToUser));
        }

        public async Task<TenantUser> UpdateExistingGoogleUser(int tenantId, string email, string imageUrl)
        {
            var user = await GetUserForTenant(email, tenantId);

            user.ImageUrl = imageUrl;
            return await UpdateUser(user, nameof(UpdateExistingGoogleUser));
        }

        public async Task<TenantUser> UpdateExistingFacebookUser(int tenantId, string email, string imageUrl)
        {
            var user = await GetUserForTenant(email, tenantId);

            user.ImageUrl = imageUrl;
            return await UpdateUser(user, nameof(UpdateExistingFacebookUser));
        }

        public async Task<TenantUser> CreateUser(CreateUserData data)
        {
            var tenantUser = new TenantUserEntity
            {
                Email = data.Email,
                UserName = data.UserGuid.ToString(),
                DisplayName = data.DisplayName,
                UserGuid = data.UserGuid,
                ImageUrl = data.ImageUrl,
                TenantId = data.TenantId,
                SelectedAvatarType = data.AvatarType
            };

            var result = await _userManager.CreateAsync(tenantUser, data.Password);
            if (!result.Succeeded)
                throw new RepositoryException(nameof(CreateUser), result.Errors.Select(x => x.Code));

            return _mapper.Map<TenantUser>(tenantUser);
        }

        public async Task<TenantUser> CreateGoogleUser(CreateUserData data)
        {
            var tenantUser = new TenantUserEntity
            {
                Email = data.Email,
                UserName = data.UserGuid.ToString(),
                DisplayName = data.DisplayName,
                UserGuid = data.UserGuid,
                ImageUrl = data.ImageUrl,
                TenantId = data.TenantId,
                GoogleId = data.ExternalUserId
            };

            var result = await _userManager.CreateAsync(tenantUser);
            if (!result.Succeeded)
                throw new RepositoryException(nameof(CreateGoogleUser), result.Errors.Select(x => x.Code));

            return _mapper.Map<TenantUser>(tenantUser);
        }

        public async Task<TenantUser> CreateFacebookUser(CreateUserData data)
        {
            var tenantUser = new TenantUserEntity
            {
                Email = data.Email,
                UserName = data.UserGuid.ToString(),
                DisplayName = data.DisplayName,
                UserGuid = data.UserGuid,
                ImageUrl = data.ImageUrl,
                TenantId = data.TenantId,
                FacebookId = data.ExternalUserId
            };

            var result = await _userManager.CreateAsync(tenantUser);
            if (!result.Succeeded)
                throw new RepositoryException(nameof(CreateFacebookUser), result.Errors.Select(x => x.Code));

            return _mapper.Map<TenantUser>(tenantUser);
        }

        public async Task<TenantUser> GetUserByCredentials(string email, string password, int tenantId)
        {
            var normalizedEmail = email.ToUpper();
            var query = from u in _context.Users
                where u.NormalizedEmail == normalizedEmail
                      && u.TenantId == tenantId
                select u;

            var user = await query.FirstOrDefaultAsync();

            if (await _userManager.CheckPasswordAsync(user, password))
                return _mapper.Map<TenantUser>(user);

            return null;
        }

        public async Task<TenantUser> GetUserByEmail(string email, int tenantId)
        {
            var user = await GetUserForTenant(email, tenantId);
            return _mapper.Map<TenantUser>(user);
        }

        private async Task<TenantUserEntity> GetUserForTenant(string email, int tenantId)
        {
            var normalizedEmail = email.ToUpper();
            var query = from u in _context.Users
                where u.NormalizedEmail == normalizedEmail
                      && u.TenantId == tenantId
                select u;

            var user = await query.FirstOrDefaultAsync();

            if (user == null)
                throw new RepositoryException(nameof(GetUserForTenant));

            return user;
        }

        private async Task<TenantUser> UpdateUser(TenantUserEntity user, string method)
        {
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return _mapper.Map<TenantUser>(user);
            else
                throw new RepositoryException(method, result.Errors.Select(x => x.Code));
        }
    }
}