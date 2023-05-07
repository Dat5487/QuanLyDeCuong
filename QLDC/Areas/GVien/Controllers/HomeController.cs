using Microsoft.AspNetCore.Mvc;
using QLDC.Models;
using System.Diagnostics;

namespace QLDC.Areas.GVien.Controllers
{
    [Area("GVien")]
    public class HomeController : Controller
    {
        private readonly Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["sldecuongnhap"] = _context.DanhSachDeCuong.Where(x=>x.TrangThai==false).Count();
            ViewData["sldecuong"] = _context.DanhSachDeCuong.Where(x => x.TrangThai == true).Count();


            return View();
        }
        public ActionResult Error(string error)
        {
            ViewBag.error = error;
            return View();
        }
    }
}