using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SmartphoneWeb.Models;
using SmartphoneWeb.Requests;
using SmartphoneWeb.Service;

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
            // Nếu đã đăng nhập, chuyển hướng tùy theo Role
            if (User.Identity.IsAuthenticated)
            {
                // Chuyển Admin về trang quản lý thay vì Home/Admin như trước
                if (User.IsInRole("Admin")) return RedirectToAction("Index", "Category");

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var user = await _authService.LoginAsync(request.Email, request.Password);

            if (user != null)
            {
                // Gọi Service để lấy thông tin định danh (ClaimsPrincipal)
                var principal = _authService.CreateClaimsPrincipal(user);
                var authProperties = new AuthenticationProperties { IsPersistent = false };

                // Ghi Cookie đăng nhập
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties);

                // Chuyển hướng
                return user.Role == Role.Admin
                    ? RedirectToAction("Index", "Category")
                    : RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác.");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Xóa toàn bộ cookie (bao gồm cookie session nếu có)
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}