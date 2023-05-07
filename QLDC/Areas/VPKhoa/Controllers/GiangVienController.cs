using Microsoft.AspNetCore.Mvc;
using QLDC.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq.Dynamic.Core;

namespace QLDC.Areas.VPKhoa.Controllers
{
    [Area("VPKhoa")]
    public class GiangVienController : Controller
    {
        private readonly Context _context;
        public GiangVienController(Context context)
        {
            _context = context;
        }
        public (GiangVien[] DSGV, int pages, int page) Paging(int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachGiangVien.Count() / size);
            var DSGV = _context.DanhSachGiangVien.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSGV, pages, page);
        }

        public IActionResult Index(int page = 1, string orderBy = "TenGiangVien", bool dsc = false)
        {
            var model = Paging(page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["TenGiangVien"] = false;
            ViewData["GioiTinh"] = false;
            ViewData["SoDienThoai"] = false;
            ViewData["DiaChi"] = false;
            ViewData[orderBy] = !dsc;
            return View(model.DSGV);
        }

        public IActionResult Details(int id)
        {
            var model = _context.DanhSachGiangVien;
            ViewBag.id = id;
            if (id == null)
                return NotFound();
            else
                return View(model.FirstOrDefault(x => x.MaGiangVien == id));
        }

        public IActionResult Edit(int id)
        {
            var model = _context.DanhSachGiangVien.Where(x => x.MaGiangVien == id).FirstOrDefault();
            if (model == null)
                return NotFound();
            return View(model);
        }

        public IActionResult Update(GiangVien giangVien)
        {
            if (giangVien.TenGiangVien == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên giảng viên" });
            if (giangVien.DiaChi == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập địa chỉ" });

            _context.DanhSachGiangVien.Update(giangVien);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var model = _context.DanhSachGiangVien.FirstOrDefault(x => x.MaGiangVien == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.DanhSachGiangVien.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateNew(GiangVien giangVien)
        {
            if (giangVien.TenGiangVien == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên giảng viên" });
            if (giangVien.DiaChi == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập địa chỉ" });
            _context.DanhSachGiangVien.Add(giangVien);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Search(string term)
        {
            var model = Paging(1, "TenGiangVien", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            return View("Index", TimKiemGiangVien(term));
        }
        public GiangVien[] TimKiemGiangVien(string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            return _context.DanhSachGiangVien.Where(b =>
                b.TenGiangVien.ToLower().Contains(s) ||
             b.GioiTinh.ToString().ToLower().Contains(s) ||
             b.SoDienThoai.ToString().Contains(s) ||
             b.DiaChi.ToLower().Contains(s)
            ).ToArray();
        }
    }
}
