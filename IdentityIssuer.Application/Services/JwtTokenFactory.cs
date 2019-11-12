using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityIssuer.Application.Configuration;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Constants;
using Microsoft.IdentityModel.Tokens;

namespace IdentityIssuer.Application.Services
{
    public class JwtTokenFactory
    {
        private readonly ITokenConfiguration tokenConfiguration;

        public JwtTokenFactory(ITokenConfiguration tokenConfiguration)
        {
            this.tokenConfiguration = tokenConfiguration;
        }

        public string Create(TenantUser user, TenantSettings settings)
        {
            var key = Encoding.ASCII.GetBytes(settings.TokenSecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserGuid),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if (user.IsAdmin)
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRoles.Admin));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddMinutes(settings.TokenExpirationInMinutes),
                Issuer = tokenConfiguration.Issuer,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}