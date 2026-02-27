using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SmartphoneWeb.Models;
using SmartphoneWeb.Requests;
using SmartphoneWeb.Service;
using System.Security.Claims;

namespace SmartphoneWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Nếu đã đăng nhập rồi thì không cho vào trang login nữa
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin")) return RedirectToAction("Admin", "Home");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.LoginAsync(request.Email, request.Password);

                if (user != null)
                {
                    // 1. Tạo danh sách thông tin người dùng (Claims) để lưu vào Cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.Email, user.Email),
                        // Lưu Role dưới dạng string ("Admin" hoặc "User")
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = false };

                    // 2. Thực hiện ghi Cookie đăng nhập
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    if (user.Role == Role.Admin)
                    {
                        
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                       
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác.");
            }
            return View(request);
        }

        // Chức năng Đăng xuất
        // Chức năng Đăng xuất
        [HttpGet] // Đảm bảo nhận yêu cầu từ thẻ <a> hoặc URL
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}