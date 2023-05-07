using Microsoft.AspNetCore.Mvc;
using QLDC.Models;
using System.Linq.Dynamic.Core;

namespace QLDC.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;
        public LoginController(Context context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(TaiKhoan model)
        {
            if (_context.DanhSachTaiKhoan.FirstOrDefault(x=>x.Username == model.Username) == null)
            {
                return RedirectToAction("Index", "Error", new { error = "Tên đăng nhập không đúng" });
            }
            else if(_context.DanhSachTaiKhoan.FirstOrDefault(x => x.Password == model.Password) == null)
            {
                return RedirectToAction("Index", "Error", new { error = "Mật khẩu không đúng" });
            }
            else
            {
                var tk = _context.DanhSachTaiKhoan.FirstOrDefault(x=> x.Username== model.Username);
                if (tk.PhanQuyen.TrimEnd().Equals("Quản lý"))
                    return RedirectToAction("Index", "Home", new { area = "VPKhoa" });
                if (tk.PhanQuyen.TrimEnd() == "Giảng viên")
                    return RedirectToAction("Index", "Home", new { area = "GVien" });
            }
            return RedirectToAction("Index", "Error", new { error = "Đăng nhập không đúng" });
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}
