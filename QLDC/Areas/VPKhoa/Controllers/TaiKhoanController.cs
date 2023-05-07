using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDC.Models;
using System.Linq.Dynamic.Core;

namespace QLDC.Areas.VPKhoa.Controllers
{
    [Area("VPKhoa")]
    public class TaiKhoanController : Controller
    {
        private readonly Context _context;
        public TaiKhoanController(Context context)
        {
            _context = context;
        }
        public (TaiKhoan[] DSTK, int pages, int page) Paging(int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachTaiKhoan.Count() / size);
            var DSTK = _context.DanhSachTaiKhoan.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSTK, pages, page);
        }

        public IActionResult Index(int page = 1, string orderBy = "Username", bool dsc = false)
        {
            var model = Paging(page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["Username"] = false;
            ViewData["Password"] = false;
            ViewData["PhanQuyen"] = false;

            ViewData[orderBy] = !dsc;
            return View(model.DSTK);
        }

        public IActionResult Details(int id)
        {
            var model = _context.DanhSachTaiKhoan;
            if (id == null)
                return NotFound();
            else
                return View(model.FirstOrDefault(x => x.MaTaiKhoan == id));

        }

        public IActionResult Edit(int id)
        {
            var model = _context.DanhSachTaiKhoan.Where(x => x.MaTaiKhoan == id).FirstOrDefault();
            if (model == null)
                return NotFound();
            ViewBag.PhanQuyen = model.PhanQuyen;
            return View(model);
        }

        public IActionResult Update(TaiKhoan taikhoan)
        {
            if (taikhoan.Username == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên đăng nhập" });
            if (taikhoan.Password == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập mật khẩu" });
            if (taikhoan.PhanQuyen == "0")
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn phân quyền" });

            _context.DanhSachTaiKhoan.Update(taikhoan);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var model = _context.DanhSachTaiKhoan.FirstOrDefault(x => x.MaTaiKhoan == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.DanhSachTaiKhoan.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateNew(TaiKhoan taikhoan)
        {
            if (taikhoan.Username == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên đăng nhập" });
            if (taikhoan.Password == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập mật khẩu" });
            if (taikhoan.PhanQuyen == "0")
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn phân quyền" });

            _context.DanhSachTaiKhoan.Add(taikhoan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Search(string term)
        {
            var model = Paging(1, "Username", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            return View("Index", TimKiemTaiKhoan(term));
        }
        public TaiKhoan[] TimKiemTaiKhoan(string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            return _context.DanhSachTaiKhoan.Where(b =>
                b.Username.ToLower().Contains(s) ||
             b.PhanQuyen.ToLower().Contains(s)).ToArray();
        }

    }
}
