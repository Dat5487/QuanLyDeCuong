using Microsoft.AspNetCore.Mvc;
using QLDC.Models;
using System.Diagnostics;

namespace QLDC.Areas.VPKhoa.Controllers
{
    [Area("VPKhoa")]
    public class HomeController : Controller
    {
        private readonly Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["sldecuongnhap"] = _context.DanhSachDeCuong.Where(x=>x.TrangThai == false).Count();
            ViewData["sltaikhoan"] = _context.DanhSachTaiKhoan.Count();
            ViewData["slgiangvien"] = _context.DanhSachGiangVien.Count();
            ViewData["sldecuong"] = _context.DanhSachDeCuong.Where(x => x.TrangThai == true).Count();
            ViewData["slmonhoc"] = _context.DanhSachMonHoc.Count();
            ViewData["slnhanvien"] = _context.DanhSachVanPhongKhoa.Count();

            return View();
        }
        public ActionResult Error(string error)
        {
            ViewBag.error = error;
            return View();
        }
    }
}