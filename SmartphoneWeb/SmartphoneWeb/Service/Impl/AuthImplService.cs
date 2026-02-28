using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmartphoneWeb.Models;
using System.Security.Claims;

namespace SmartphoneWeb.Service.Impl
{
    public class AuthImplService : AuthService
    {
        private readonly AppDbContext _context;

        public AuthImplService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
           
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }


        public ClaimsPrincipal CreateClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}