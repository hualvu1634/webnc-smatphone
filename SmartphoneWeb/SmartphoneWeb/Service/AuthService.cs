using SmartphoneWeb.Models;

namespace SmartphoneWeb.Service
{
    public interface AuthService
    {
        Task<User> LoginAsync(string email, string password);
    }
}