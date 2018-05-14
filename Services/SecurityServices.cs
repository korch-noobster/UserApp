using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UserApp.Models;

namespace UserApp.Services
{
    public class SecurityServices
    {
        private readonly IConfiguration _configuration;

        public SecurityServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string HashingPassword(string password)
        {
            var salt = BCryptHelper.GenerateSalt(10);
            return BCryptHelper.HashPassword(password, salt);
        }

        public bool ValidatePassword(string password, string hash)
        {
            return BCryptHelper.CheckPassword(password, hash);
        }
        public ResultToken GenerateJwtToken(string username, string securityKey)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["JwtExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
           
            var userToken = new ResultToken
            {
                AccesToken =  new JwtSecurityTokenHandler().WriteToken(token)
            };
            return userToken;
        }
    }
}
