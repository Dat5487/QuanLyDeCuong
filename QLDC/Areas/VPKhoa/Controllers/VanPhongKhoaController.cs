using Microsoft.AspNetCore.Mvc;
using QLDC.Models;
using System.Linq.Dynamic.Core;

namespace QLDC.Areas.VPKhoa.Controllers
{
    [Area("VPKhoa")]
    public class VanPhongKhoaController : Controller
    {
        private readonly Context _context;
        public VanPhongKhoaController(Context context)
        {
            _context = context;
        }
        public (VanPhongKhoa[] DSNV, int pages, int page) Paging(int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachVanPhongKhoa.Count() / size);
            var DSNV = _context.DanhSachVanPhongKhoa.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSNV, pages, page);
        }

        public IActionResult Index(int page = 1, string orderBy = "TenNhanVien", bool dsc = false)
        {
            var model = Paging(page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["TenNhanVien"] = false;
            ViewData["SoDienThoai"] = false;
            ViewData["ChucVu"] = false;

            ViewData[orderBy] = !dsc;
            return View(model.DSNV);
        }

        public IActionResult Details(int id)
        {
            var model = _context.DanhSachVanPhongKhoa;
            if (id == null)
                return NotFound();
            else
                return View(model.FirstOrDefault(x => x.MaNhanVien == id));

        }

        public IActionResult Edit(int id)
        {
            var model = _context.DanhSachVanPhongKhoa.Where(x => x.MaNhanVien == id).FirstOrDefault();
            if (model == null)
                return NotFound();
            return View(model);
        }

        public IActionResult Update(VanPhongKhoa vanPhongKhoa)
        {
            if (vanPhongKhoa.TenNhanVien == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên nhân viên" });
            if (vanPhongKhoa.ChucVu == "0")
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn chức vụ" });

            _context.DanhSachVanPhongKhoa.Update(vanPhongKhoa);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var model = _context.DanhSachVanPhongKhoa.FirstOrDefault(x => x.MaNhanVien == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.DanhSachVanPhongKhoa.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateNew(VanPhongKhoa vanPhongKhoa)
        {
            if (vanPhongKhoa.TenNhanVien == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên nhân viên" });
            if (vanPhongKhoa.ChucVu == "0")
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn chức vụ" });
            _context.DanhSachVanPhongKhoa.Add(vanPhongKhoa);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Search(string term)
        {
            var model = Paging(1, "TenNhanVien", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            return View("Index", TimKiemNhanVien(term));
        }
        public VanPhongKhoa[] TimKiemNhanVien(string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            return _context.DanhSachVanPhongKhoa.Where(b =>
                b.TenNhanVien.ToLower().Contains(s) ||
             b.SoDienThoai.ToString().ToLower().Contains(s) ||
             b.ChucVu.ToLower().Contains(s)
            ).ToArray();
        }
    }
}
