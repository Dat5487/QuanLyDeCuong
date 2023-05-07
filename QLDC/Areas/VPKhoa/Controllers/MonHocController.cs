using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDC.Models;
using System.Linq.Dynamic.Core;

namespace QLDC.Areas.VPKhoa.Controllers
{
    [Area("VPKhoa")]
    public class MonHocController : Controller
    {
        private readonly Context _context;
        public MonHocController(Context context)
        {
            _context = context;
        }
        public (MonHoc[] DSMH, int pages, int page) Paging(int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachMonHoc.Count() / size);
            var DSMH = _context.DanhSachMonHoc.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSMH, pages, page);
        }

        public IActionResult Index(int page = 1, string orderBy = "TenMon", bool dsc = false)
        {
            var model = Paging(page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["TenMon"] = false;
            ViewData["HinhThucThi"] = false;
            ViewData["SoTinChi"] = false;

            ViewData[orderBy] = !dsc;
            return View(model.DSMH);
        }

        public IActionResult Details(int id)
        {
            var model = _context.DanhSachMonHoc;
            if (id == null)
                return NotFound();
            else
                return View(model.FirstOrDefault(x => x.MaMon == id));

        }

        public IActionResult Edit(int id)
        {
            var model = _context.DanhSachMonHoc.Where(x => x.MaMon == id).FirstOrDefault();
            if (model == null)
                return NotFound();
            ViewBag.HinhThucThi = model.HinhThucThi;
            return View(model);
        }

        public IActionResult Update(MonHoc monHoc)
        {
            if (monHoc.TenMon == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên môn" });
            if (monHoc.SoTinChi == 0)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập số tín chỉ" });
            if (monHoc.HinhThucThi == "0")
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn hình thức thi" });

            _context.DanhSachMonHoc.Update(monHoc);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var model = _context.DanhSachMonHoc.FirstOrDefault(x => x.MaMon == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.DanhSachMonHoc.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateNew(MonHoc monHoc)
        {
            if (monHoc.TenMon == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên môn" });
            if (monHoc.SoTinChi == 0)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập số tín chỉ" });
            if (monHoc.HinhThucThi == "0")
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn hình thức thi" });

            _context.DanhSachMonHoc.Add(monHoc);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Search(string term)
        {
            var model = Paging(1, "TenMon", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            return View("Index", TimKiemMonHoc(term));
        }
        public MonHoc[] TimKiemMonHoc(string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            return _context.DanhSachMonHoc.Where(b =>
                b.TenMon.ToLower().Contains(s) ||
             b.SoTinChi.ToString().ToLower().Contains(s) ||
             b.HinhThucThi.ToLower().Contains(s)
            ).ToArray();
        }
    }
}
