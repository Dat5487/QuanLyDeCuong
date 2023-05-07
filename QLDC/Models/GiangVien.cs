using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLDC.Models
{
    public class GiangVien
    {
        [Key]
        [DisplayName("Mã giảng viên")]
        public int MaGiangVien { get; set; }

        [StringLength(50, ErrorMessage = "Tên giảng viên phải dưới 50 ký tự")]
        [Required(ErrorMessage = "Bắt buộc phải nhập Tên giảng viên")]
        [DisplayName("Tên giảng viên")]
        public string TenGiangVien { get; set; }

        [DisplayName("Giới tính")]
        public bool GioiTinh { get; set; }

        [Range(0, 10, ErrorMessage = "Số điện thoại dưới 10 chữ số")]
        [DisplayName("Số điện thoại")]
        public int SoDienThoai { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ phải dưới 50 ký tự")]
        [Required(ErrorMessage = "Bắt buộc phải nhập Địa chỉ")]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }


    }
}
