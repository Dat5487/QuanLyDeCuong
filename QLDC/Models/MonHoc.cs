using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLDC.Models
{
    public class MonHoc
    {
        [Key]
        [DisplayName("Mã môn")]
        public int MaMon { get; set; }

        [StringLength(50, ErrorMessage = "Tên môn phải dưới 50 ký tự")]
        //[Required(ErrorMessage = "Bắt buộc phải nhập Tên môn")]
        [Required,DisplayName("Tên môn")]
        public string TenMon { get; set; }

        [Required(ErrorMessage = "Bắt buộc phải nhập Số tín chỉ")]
        [DisplayName("Số tín chỉ")]
        public int SoTinChi { get; set; }

        [Required(ErrorMessage = "Bắt buộc phải nhập Hình thức thi")]
        [DisplayName("Hình thức thi")]
        public string HinhThucThi{ get; set; }



    }
}
