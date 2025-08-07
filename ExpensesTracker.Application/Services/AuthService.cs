using ExpensesTracker.Application.Interfaces;
using ExpensesTracker.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpensesTracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration
            , RoleManager<IdentityRole> roleManager
            , UserManager<AppUser> userManager
            )
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //test
        public async Task<string> GenerateJwtTokenAsync(AppUser user)
        {
            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
               
                var claims = new List<Claim>  
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("profileImg", user.ProfileImage ?? string.Empty),
                };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(3),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return null;
            }
        }



        public async Task<string> Register(RegisterDto registerDTO, string role)
        {
            if (registerDTO == null)
            {
                return null; 
            }
            if(await IsAuthenticated(registerDTO.Email))
            {
                return "existed";
            }

            var user = new AppUser
            {
                FirstName = registerDTO.FName,
                LastName = registerDTO.LName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
            };
            var result  = await _userManager.CreateAsync(user, registerDTO.Password);
            if ((result.Succeeded) && (!string.IsNullOrEmpty(role)))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return await GenerateJwtTokenAsync(user);  
        }

        public async Task<string> Login(LoginDto LoginUser)
        {

            if(await IsAuthenticated(LoginUser.Email))
            {
                var user = await _userManager.FindByEmailAsync(LoginUser.Email);
                if (await _userManager.CheckPasswordAsync(user, LoginUser.Password))
                {
                    return await GenerateJwtTokenAsync(user);
                }
                else
                {
                    return null;
                }
            }
            else
            {
               return null;
            }
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsAuthenticated(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return !(user == null);
        }

    }
}
