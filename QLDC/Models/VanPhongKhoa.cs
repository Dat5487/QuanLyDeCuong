using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLDC.Models
{
    public class VanPhongKhoa
    {
        [Key]
        [DisplayName("Mã nhân viên")]
        public int MaNhanVien { get; set; }

        [StringLength(50, ErrorMessage = "Tên nhân viên phải dưới 50 ký tự")]
        [DisplayName("Tên nhân viên")]
        [Required(ErrorMessage = "Bắt buộc phải nhập Tên nhân viên")]
        public string TenNhanVien { get; set; }

        [Range(0, 10, ErrorMessage = "Số điện thoại dưới 10 chữ số")]
        [DisplayName("Số điện thoại")]
        public int SoDienThoai  { get; set; }

        [StringLength(50, ErrorMessage = "Chức vụ phải dưới 50 ký tự")]
        [Required(ErrorMessage = "Bắt buộc phải nhập Chức vụ")]
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }

        //ICollection<DeCuong> DeCuongs { get; set; }
    }
}
