using System.Security.Claims;
using SmartphoneWeb.Models;

namespace SmartphoneWeb.Service
{
    public interface AuthService
    {
        Task<User> LoginAsync(string email, string password);

       
        ClaimsPrincipal CreateClaimsPrincipal(User user);
    }
}