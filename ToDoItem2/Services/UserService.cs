using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToDoItem2.BL.Dtos;

namespace TodoItem2.Services
{
    public interface IUserService
    {
        UserDto AuthenticateUser(UserDto loginCredentials);
        string GenerateJWTToken(UserDto userInfo);
    }
    public class UserService: IUserService
    {
        private readonly List<UserDto> users = new List<UserDto>
            {
            new UserDto { FullName = "Vaibhav Bhapkar", UserName = "admin", Password = "1234", UserRole = "Admin" },
            new UserDto { FullName = "Test User", UserName = "user", Password = "1234", UserRole = "User" }
            };
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserDto AuthenticateUser(UserDto loginCredentials)
        {
            UserDto user = users.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }

        public string GenerateJWTToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
            new Claim("fullName", userInfo.FullName.ToString()),
            new Claim("role",userInfo.UserRole),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            var _token = new JwtSecurityTokenHandler().WriteToken(token);
            return _token;
        }
    }
}
