using System.ComponentModel.DataAnnotations;

namespace SmartphoneWeb.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản (Email)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}