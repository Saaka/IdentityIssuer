using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityIssuer.Application.Configuration;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Application.Services;
using Microsoft.IdentityModel.Tokens;

namespace IdentityIssuer.Infrastructure.Security
{
    public class JwtTokenFactory : IJwtTokenFactory
    {
        private readonly ITokenConfiguration _tokenConfiguration;

        public JwtTokenFactory(ITokenConfiguration tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }

        public string Create(TenantUser user, TenantSettings settings, string tenantCode)
        {
            var key = Encoding.ASCII.GetBytes(settings.TokenSecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserGuid),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            claims.Add(new Claim(CustomClaims.Tenant, tenantCode));

            var roleClaims = GetRoleClaims(user);
            claims.AddRange(roleClaims);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddMinutes(settings.TokenExpirationInMinutes),
                Issuer = _tokenConfiguration.Issuer,
            };
            var jwt = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            jwt.Header.Add(JwtHeaderParameterNames.Kid, tenantCode);

            return tokenHandler.WriteToken(jwt);
        }

        private IEnumerable<Claim> GetRoleClaims(TenantUser user)
        {
            var roleClaims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRoles.User)
            };
            
            if (user.IsAdmin)
                roleClaims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRoles.Admin));
            if (user.IsOwner)
                roleClaims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRoles.TenantOwner));

            return roleClaims;
        }
    }
}