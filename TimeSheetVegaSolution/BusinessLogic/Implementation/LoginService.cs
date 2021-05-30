using Contracts.Interface.Repository;
using Contracts.Interface.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Dtos;
using Model.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration, IEmployeeRepository employeeRepository)
        {
            _configuration = configuration;
            _employeeRepository = employeeRepository;
        }

        public UserModel AuthenticateUser(UserModel login)
        {
            Employee employee = _employeeRepository.GetByLoginInfo(login);
            return new UserModel
            {
                Id = employee.Id,
                Email = employee.Email,
                Password = employee.Password,
                Role = employee.Role.ToString(),
                Name = employee.Name
            };
        }

        public string GenerateJSONWebToken(UserModel userInfo)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            SigningCredentials credentials = new SigningCredentials
            (
                securityKey, SecurityAlgorithms.HmacSha256
            );

            Claim[] claims = new[] {
                new Claim("id", userInfo.Id.ToString()),
                new Claim("name", userInfo.Name),
                new Claim("role", userInfo.Role),
                new Claim (ClaimTypes.Role, userInfo.Role)
            };

            JwtSecurityToken token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
