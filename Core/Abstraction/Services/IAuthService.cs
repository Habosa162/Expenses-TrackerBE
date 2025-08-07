using ExpensesTracker.Core.Models;
namespace ExpensesTracker.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> IsAuthenticated(string email); 
        public Task<string?> GenerateJwtTokenAsync(AppUser user); 
        public Task<string> Login(LoginDto loginDto);
        public Task<bool> Logout();
        public Task<string> Register(RegisterDto registerDTO, string role);
    }

}
