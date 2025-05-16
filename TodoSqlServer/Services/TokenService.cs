﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoSqlServer.Models;

namespace TodoSqlServer.Services
{
    public static class TokenService
    {
        public static object GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }

        public static Guid GetUserToken(ClaimsPrincipal principal)
        {
            
            var userId = principal.FindFirst("userId")?.Value;
            if (userId == null)
            {
                throw new InvalidOperationException(nameof(principal));
            }

            return Guid.Parse(userId);
        }
    }
}
