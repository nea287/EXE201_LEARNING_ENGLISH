using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IConfiguration _configuration;

        public RefreshTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateRefreshToken(string email, int role)
        {
            string roleName = "";
            switch (role)
            {
                case (int)AccountRole.ADMIN:
                    roleName = AccountRole.ADMIN.ToString();
                    break;

                case (int)AccountRole.STUDENT:
                    roleName = AccountRole.STUDENT.ToString();
                    break;

                case (int)AccountRole.TEACHER:
                    roleName = AccountRole.TEACHER.ToString();
                    break;
            }
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:RefreshTokenSecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var refreshToken = new JwtSecurityToken(
                _configuration["Token:Issuer"],
                _configuration["Token:Audience"],
                claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Token:RefreshTokenExpirationInDays"])),
                signingCredentials: creds
                );

            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            return refreshTokenString;
        }
    }
}
