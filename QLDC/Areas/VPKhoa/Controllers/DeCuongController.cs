using Microsoft.AspNetCore.Mvc;
using QLDC.Models;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;

namespace QLDC.Areas.VPKhoa.Controllers
{
    [Area("VPKhoa")]
    public class DeCuongController : Controller
    {
        private readonly Context _context;
        public DeCuongController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, string orderBy = "TenDeCuong", bool dsc = false)
        {
            ViewBagDeCuong();
            var model = Paging(page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["TenDeCuong"] = false;
            ViewData["TrangThai"] = false;
            ViewData["NgayPheDuyet"] = false;
            ViewData["MaGiangVien"] = false;
            ViewData["MaMon"] = false;
            ViewData[orderBy] = !dsc;
            return View(model.DSDC);
        }

        public IActionResult DSDeCuongNhap(int page = 1, string orderBy = "TenDeCuong", bool dsc = false)
        {
            ViewBagDeCuong();
            var model = PagingOfNhap(page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["TenDeCuong"] = false;
            ViewData["TrangThai"] = false;
            ViewData["NgayPheDuyet"] = false;
            ViewData["MaGiangVien"] = false;
            ViewData["MaMon"] = false;
            ViewData[orderBy] = !dsc;
            return View(model.DSDC);
        }

        public IActionResult DSDeCuongOfGV(int MaGiangVien, int page = 1, string orderBy = "TenDeCuong", bool dsc = false)
        {
            ViewBagDeCuong();
            var model = PagingOfGV(MaGiangVien, page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["TenDeCuong"] = false;
            ViewData["TrangThai"] = false;
            ViewData["NgayPheDuyet"] = false;
            ViewData["MaGiangVien"] = MaGiangVien;
            ViewData["TenGiangVien"] = _context.DanhSachGiangVien.FirstOrDefault(x => x.MaGiangVien == MaGiangVien).TenGiangVien;

            ViewData[orderBy] = !dsc;
            return View(model.DSDC);
        }
        public IActionResult DSDeCuongOfMH(int MaMon, int page = 1, string orderBy = "TenDeCuong", bool dsc = false)
        {
            ViewBagDeCuong();
            var model = PagingOfMH(MaMon, page, orderBy, dsc);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["TenMon"] = false;
            ViewData["SoTinChi"] = false;
            ViewData["HinhThucThi"] = false;
            ViewData["MaGiangVien"] = false;
            ViewData["MaMon"] = MaMon;
            ViewData["TenMon"] = _context.DanhSachMonHoc.FirstOrDefault(x => x.MaMon == MaMon).TenMon;

            ViewData[orderBy] = !dsc;
            return View(model.DSDC);
        }

        public IActionResult Details(int id)
        {
            var model = _context.DanhSachDeCuong;
            ViewBagDeCuong();

            if (id == null)
                return NotFound();
            else
                return View(model.FirstOrDefault(x => x.MaDeCuong == id));

        }

        public IActionResult Edit(int id)
        {
            var model = _context.DanhSachDeCuong.Where(x => x.MaDeCuong == id).FirstOrDefault();
            ViewBagDeCuong();

            if (model == null)
                return NotFound();
            return View(model);
        }

        public IActionResult Update(DeCuong deCuong, IFormFile file)
        {
            if (deCuong.TenDeCuong == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên đề cương" });
            if (deCuong.TomTat == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tóm tắt đề cương" });
            if (deCuong.MaGiangVien == 0)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn giảng viên" });
            if (deCuong.MaMon == 0)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn môn học" });
            Upload(deCuong, file);
            _context.DanhSachDeCuong.Update(deCuong);
            _context.SaveChanges();

            var model = _context.DanhSachDeCuong;
            ViewBagDeCuong();
            return RedirectToAction("DSDeCuongNhap");
        }

        public ActionResult Delete(int id)
        {
            var model = _context.DanhSachDeCuong.FirstOrDefault(x => x.MaDeCuong == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.DanhSachDeCuong.Remove(model);
            _context.SaveChanges();

            var newmodel = _context.DanhSachDeCuong;
            ViewBagDeCuong();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            ViewBagDeCuong();
            return View();
        }
        public ActionResult CreateNew(DeCuong deCuong, IFormFile file)
        {
            if (deCuong.TenDeCuong == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tên đề cương" });
            if (deCuong.TomTat == null)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải nhập tóm tắt đề cương" });
            if (deCuong.MaGiangVien == 0)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn giảng viên" });
            if (deCuong.MaMon == 0)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn môn học" });
            Upload(deCuong, file);
            _context.DanhSachDeCuong.Add(deCuong);
            _context.SaveChanges();
            var model = _context.DanhSachDeCuong;
            ViewBagDeCuong();
            return RedirectToAction("DSDeCuongNhap");
        }
        public ActionResult PheDuyet(int id)
        {
            var model = _context.DanhSachDeCuong.FirstOrDefault(x => x.MaDeCuong == id);
            ViewBagDeCuong();

            if (model == null)
                return NotFound();
            return View(model);
        }
        public IActionResult XacNhanPheDuyet(DeCuong deCuong)
        {
            if (deCuong.MaNhanVien == 0)
                return RedirectToAction("Error", "Home", new { error = "Bạn phải chọn nhân viên phê duyệt" });

            var dc = _context.DanhSachDeCuong.FirstOrDefault(x => x.MaDeCuong == deCuong.MaDeCuong);
            dc.MaNhanVien = deCuong.MaNhanVien;
            dc.NgayPheDuyet = DateTime.Now;
            dc.TrangThai = true;
            _context.DanhSachDeCuong.Update(dc);
            _context.SaveChanges();

            var model = _context.DanhSachDeCuong;
            ViewBagDeCuong();
            return RedirectToAction("Index");
        }

        public IActionResult Search(string term)
        {
            ViewBagDeCuong();
            var model = Paging(1, "TenDeCuong", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            return View("Index", TimKiemDeCuong(term));
        }
        public IActionResult SearchDcNhap(string term)
        {
            ViewBagDeCuong();
            var model = PagingOfNhap(1, "TenDeCuong", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            return View("DSDeCuongNhap", TimKiemDeCuongNhap(term));
        }
        public IActionResult SearchDcOfGv(int MaGiangVien, string term)
        {
            ViewBagDeCuong();
            var model = PagingOfGV(MaGiangVien, 1, "TenDeCuong", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["MaGiangVien"] = MaGiangVien;
            ViewData["TenGiangVien"] = _context.DanhSachGiangVien.FirstOrDefault(x => x.MaGiangVien == MaGiangVien).TenGiangVien;

            return View("DSDeCuongOfGV", TimKiemDeCuongOfGV(MaGiangVien, term));
        }
        public IActionResult SearchDcOfMh(int MaMon, string term)
        {
            ViewBagDeCuong();
            var model = PagingOfMH(MaMon, 1, "TenDeCuong", false);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["MaMon"] = MaMon;
            ViewData["TenMon"] = _context.DanhSachMonHoc.FirstOrDefault(x => x.MaMon == MaMon).TenMon;

            return View("DSDeCuongOfMH", TimKiemDeCuongOfMH(MaMon, term));
        }

        public DeCuong[] TimKiemDeCuong(string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            var model = _context.DanhSachDeCuong.Where(x => x.TrangThai == true);
            return model.Where(b =>
                b.TenDeCuong.ToLower().Contains(s) ||
                _context.DanhSachGiangVien.FirstOrDefault(x => x.MaGiangVien == b.MaGiangVien).TenGiangVien.ToLower().Contains(s) ||
                _context.DanhSachMonHoc.FirstOrDefault(x => x.MaMon == b.MaMon).TenMon.ToLower().Contains(s)
                ).ToArray();
        }
        public DeCuong[] TimKiemDeCuongNhap(string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            var model = _context.DanhSachDeCuong.Where(x => x.TrangThai == false);
            return model.Where(b =>
                b.TenDeCuong.ToLower().Contains(s) ||
                _context.DanhSachGiangVien.FirstOrDefault(x => x.MaGiangVien == b.MaGiangVien).TenGiangVien.ToLower().Contains(s) ||
                _context.DanhSachMonHoc.FirstOrDefault(x => x.MaMon == b.MaMon).TenMon.ToLower().Contains(s)
                ).ToArray();
        }

        public DeCuong[] TimKiemDeCuongOfGV(int MaGiangVien, string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            var model = _context.DanhSachDeCuong.Where(b => b.MaGiangVien == MaGiangVien);
            return model.Where(b =>
                b.TenDeCuong.ToLower().Contains(s) ||
                b.TomTat.ToLower().Contains(s) ||
                _context.DanhSachGiangVien.FirstOrDefault(x => x.MaGiangVien == b.MaGiangVien).TenGiangVien.ToLower().Contains(s) ||
                _context.DanhSachMonHoc.FirstOrDefault(x => x.MaMon == b.MaMon).TenMon.ToLower().Contains(s)
            ).ToArray();
        }
        public DeCuong[] TimKiemDeCuongOfMH(int MaMon, string search)
        {
            string s = "";
            if (search != null)
                s = search.ToLower();

            var model = _context.DanhSachDeCuong.Where(b => b.MaMon == MaMon);
            return model.Where(b =>
                b.TenDeCuong.ToLower().Contains(s) ||
                b.TomTat.ToLower().Contains(s) ||
                _context.DanhSachGiangVien.FirstOrDefault(x => x.MaGiangVien == b.MaGiangVien).TenGiangVien.ToLower().Contains(s) ||
                _context.DanhSachMonHoc.FirstOrDefault(x => x.MaMon == b.MaMon).TenMon.ToLower().Contains(s)
            ).ToArray();
        }

        public (DeCuong[] DSDC, int pages, int page) Paging(int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachDeCuong.Count() / size);
            var DSDC = _context.DanhSachDeCuong.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSDC, pages, page);
        }
        public (DeCuong[] DSDC, int pages, int page) PagingOfNhap(int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachDeCuong.Count() / size);
            var model = _context.DanhSachDeCuong.Where(x => x.TrangThai == false);
            var DSDC = model.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSDC, pages, page);
        }
        public (DeCuong[] DSDC, int pages, int page) PagingOfGV(int MaGiangVien, int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachDeCuong.Where(x => x.MaGiangVien == MaGiangVien).Count() / size);
            var model = _context.DanhSachDeCuong.Where(x => x.MaGiangVien == MaGiangVien);
            var DSDC = model.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSDC, pages, page);
        }
        public (DeCuong[] DSDC, int pages, int page) PagingOfMH(int MaMon, int page, string orderBy = "Name", bool dsc = false)
        {
            int size = 5;
            int pages = (int)Math.Ceiling((double)_context.DanhSachDeCuong.Where(x => x.MaMon == MaMon).Count() / size);
            var model = _context.DanhSachDeCuong.Where(x => x.MaMon == MaMon);
            var DSDC = model.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (DSDC, pages, page);
        }

        public string GetDataPath(string file) => $"Data\\{file}";
        public void Upload(DeCuong deCuong, IFormFile file)
        {
            if (file != null)
            {
                var path = GetDataPath(file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                deCuong.DataFile = file.FileName;
            }
        }
        public (Stream, string) Download(DeCuong deCuong)
        {
            var memory = new MemoryStream();
            using var stream = new FileStream(GetDataPath(deCuong.DataFile), FileMode.Open);
            stream.CopyTo(memory);
            memory.Position = 0;
            var type = Path.GetExtension(deCuong.DataFile) switch
            {
                "pdf" => "application/pdf",
                "docx" => "application/vnd.ms-word",
                "doc" => "application/vnd.ms-word",
                "txt" => "text/plain",
                _ => "application/pdf"
            };
            return (memory, type);
        }
        public IActionResult Read(int id)
        {
            var dc = _context.DanhSachDeCuong.FirstOrDefault(x => x.MaDeCuong == id);
            if (dc == null) return NotFound();
            if (!System.IO.File.Exists(GetDataPath(dc.DataFile))) return NotFound();
            var (stream, type) = Download(dc);
            return File(stream, type, dc.DataFile);
        }

        public void ViewBagDeCuong()
        {
            ViewBag.NV = _context.DanhSachVanPhongKhoa.ToList();
            ViewBag.GV = _context.DanhSachGiangVien.ToList();
            ViewBag.MH = _context.DanhSachMonHoc.ToList();
        }

    }
}
