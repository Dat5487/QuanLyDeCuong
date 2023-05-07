using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLDC.Models
{
    public class TaiKhoan
    {
        [Key]
        [DisplayName("Mã tài khoản")]
        public int MaTaiKhoan { get; set; }

        [StringLength(20, ErrorMessage = "Tên đăng nhập phải dưới 20 ký tự")]
        [Required(ErrorMessage = "Bắt buộc phải nhập tên đăng nhập")]
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }

        [DisplayName("Phân quyền")]
        public string PhanQuyen { get; set; }

        [StringLength(20, ErrorMessage = "Mật khẩu phải dưới 20 ký tự")]
        [Required(ErrorMessage = "Bắt buộc phải nhập mật khẩu")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
    }
}
