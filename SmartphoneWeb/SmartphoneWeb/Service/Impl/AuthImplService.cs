using Microsoft.EntityFrameworkCore;
using SmartphoneWeb.Models;

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
    }
}