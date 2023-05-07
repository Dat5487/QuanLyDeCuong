using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLDC.Models
{
    public class DeCuong
    {
        [Key]
        [DisplayName("Mã đề cương")]
        public int MaDeCuong { get; set; }

        [DisplayName("Giảng viên")]
        public int MaGiangVien { get; set; }

        [DisplayName("Môn học")]
        public int MaMon { get; set; }

        [DisplayName("Mã nhân viên")]
        public int MaNhanVien { get; set; }

        [StringLength(50, ErrorMessage = "Tên đề cương phải dưới 50 ký tự")]
        [Required(ErrorMessage = "Bắt buộc phải nhập Tên đề cương")]
        [DisplayName("Tên đề cương")]
        public string TenDeCuong { get; set; }

        [DisplayName("Tóm tắt")]
        public string TomTat { get; set; }

        [DisplayName("Ngày phê duyệt")]
        public DateTime NgayPheDuyet{ get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        [DisplayName("File")]
        public string? DataFile { get; set; }

    }
}
