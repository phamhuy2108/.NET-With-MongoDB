using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHQTCSDL.Models;

namespace DoAnHQTCSDL.Controllers
{
    public class QLSVController : Controller
    {
        // GET: QLSV
        public ActionResult Index()
        {
            Lop lop = new Lop();
            Nganh nganh = new Nganh();
            SinhVien sinhvien = new SinhVien();
            ViewBag.ListLop = lop.listlop();
            ViewBag.ListNganh = nganh.listnganh();
            ViewBag.SinhVien = sinhvien.listSinhVien();
            return View();
        }
        [HttpPost]
        public ActionResult BackUp(string duongdan)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C mongodump --db=qlsv --out="+duongdan);
            TempData["BackUp"] = "Bạn đã backup thành công";
            return Redirect("~/QLSV/Index");
        }
        [HttpPost]
        public ActionResult ThemSinhVien(string mssv, string hoten, DateTime ngaysinh, string gioitinh, double diemchuan, double diemthi, string truongTHPT, string manganh, string malop)
        {
            int khoa = DateTime.Now.Year;
            SinhVien sv = new SinhVien(mssv, hoten, ngaysinh, gioitinh, diemchuan, diemthi, truongTHPT, manganh, malop, khoa);
            if (sv.timsinhvien(mssv) != null)
            {
                TempData["DaCoSv"] = "MSSV này đã tồn tại";
                return Redirect("~/QLSV/Index");
            }
            else
            {
                sv.themSinhVien(sv);
                return Redirect("~/QLSV/Index");
            }
        }
        [HttpGet]
        public ActionResult SuaSinhVien(string mssv)
        {
            SinhVien sv = new SinhVien();
            var a = sv.timsinhvien(mssv);
            ViewBag.NgaySinh = (a.ngaysinh).ToString("yyyy-MM-dd");
            Lop l = new Lop();
            var lop = l.timlop(a.malop);
            ViewBag.Lop = lop;
            Nganh ngh = new Nganh();
            var nganh = ngh.timnganh(a.manganh);
            ViewBag.Nganh = nganh;
            ViewBag.Lop1 = l.timlop2(a.malop);
            ViewBag.Nganh1 = ngh.timnganh2(a.manganh);
            ViewBag.SuaSinhVien = a;
            return View();
        }
        [HttpPost]
        public ActionResult SuaSinhVien(string mssv, string hoten, DateTime ngaysinh, string gioitinh, double diemchuan, double diemthi, string truongTHPT, string manganh, string malop)
        {
            SinhVien sv = new SinhVien();
            sv.suasinhvien(mssv, hoten, ngaysinh, gioitinh, diemchuan, diemthi, truongTHPT, manganh, malop);
            return Redirect("~/QLSV/Index");
        }
        public ActionResult XoaSinhVien(string mssv)
        {
            SinhVien sv = new SinhVien();
            Diem diem = new Diem();
            if (diem.timdiem3(mssv) == null)
            {
                sv.xoasinhvien(mssv);
                return Redirect("~/QLSV/Index");
            }
            else
            {
                TempData["ErrorSV"] = "Vẫn còn tồn tại bảng điểm của sinh viên";
                return Redirect("~/QLSV/Index");
            }
        }
        public ActionResult TimSinhVien(string hotentim)
        {
            SinhVien sv = new SinhVien();
            var timkiem = sv.timsinhvien1(hotentim);
            //var timkiem = db.Sach.Where(s => ConvertToUnSign(s.tensach).StartsWith(tensach)).ToList();
            Lop l = new Lop();
            ViewBag.ListLop = l.listlop();
            Nganh ngh = new Nganh();
            ViewBag.ListNganh = ngh.listnganh();
            ViewBag.TimKiem = timkiem;
            return View();
        }
        public ActionResult QLNganh()
        {
            Nganh ng = new Nganh();
            ViewBag.Nganh = ng.listnganh();
            return View();
        }
        [HttpPost]
        public ActionResult ThemNganh(string manganh, string tennganh)
        {
            Nganh ng = new Nganh();
            Nganh ng1 = new Nganh(manganh, tennganh);
            if (ng.timnganh(manganh) == null)
            {
                ng.themnganh(ng1);
                return Redirect("~/QLSV/QLNganh");
            }
            else
            {
                TempData["DaCoNG"] = "Mã ngành này đã tồn tại";
                return Redirect("~/QLSV/QLNganh");
            }
        }
        [HttpPost]
        public ActionResult SuaNganh(string manganh, string tennganh)
        {
            Nganh ngh = new Nganh();
            ngh.suanganh(manganh, tennganh);
            return Redirect("~/QLSV/QLNganh");
        }
        public ActionResult XoaNganh(string manganh)
        {
            Nganh ngh = new Nganh();
            SinhVien sv = new SinhVien();
            if (sv.timsinhvien3(manganh) == null)
            {
                ngh.xoanganh(manganh);
                return Redirect("~/QLSV/QLNganh");
            }
            else
            {
                TempData["ErrorNGH"] = "Vẫn còn tồn tại sinh viên";
                return Redirect("~/QLSV/QLNganh");
            }
        }
        public ActionResult QLMon()
        {
            Mon mon = new Mon();
            ViewBag.Mon = mon.listmon();
            return View();
        }
        [HttpPost]
        public ActionResult ThemMon(string mamon, string tenmon)
        {
            Mon mon = new Mon(mamon, tenmon);
            if (mon.timmon(mamon) == null)
            {
                mon.themMon(mon);
                return Redirect("~/QLSV/QLMon");
            }
            else
            {
                TempData["DaCoMon"] = "Mã môn này đã tồn tại";
                return Redirect("~/QLSV/QLMon");
            }
        }
        [HttpPost]
        public ActionResult SuaMon(string mamon, string tenmon)
        {
            Mon mon = new Mon();
            mon.suamon(mamon, tenmon);
            return Redirect("~/QLSV/QLMon");
        }
        public ActionResult XoaMon(string mamon)
        {
            Mon mon = new Mon();
            Diem d = new Diem();
            if (d.timdiem4(mamon) == null)
            {
                mon.xoamon(mamon);
                return Redirect("~/QLSV/QLMon");
            }
            else
            {
                TempData["ErrorMon"] = "Vẫn còn tồn tại bảng điểm của sinh viên";
                return Redirect("~/QLSV/QLMon");
            }
        }
        public ActionResult TimMon(string montim)
        {
            Mon mon = new Mon();
            var timkiem = mon.timmon1(montim);
            ViewBag.TimKiem = timkiem;
            return View();
        }
        public ActionResult QLLop()
        {
            Lop lop = new Lop();
            ViewBag.Lop = lop.listlop();
            return View();
        }
        [HttpPost]
        public ActionResult ThemLop(string malop, string gvphutrach, string tenlop, int siso)
        {
            Lop lop = new Lop(malop, gvphutrach, tenlop, siso);
            if (lop.timlop(malop) == null)
            {
                lop.themLop(lop);
                return Redirect("~/QLSV/QLLop");
            }
            else
            {
                TempData["DaCoLop"] = "Mã lớp này đã tồn tại";
                return Redirect("~/QLSV/QLLop");
            }
        }
        [HttpPost]
        public ActionResult SuaLop(string malop, string gvphutrach, string tenlop, int siso)
        {
            Lop lop = new Lop();
            lop.sualop(malop, gvphutrach, tenlop, siso);
            return Redirect("~/QLSV/QLLop");
        }
        public ActionResult XoaLop(string malop)
        {
            Lop lop = new Lop();
            SinhVien sv = new SinhVien();
            if (sv.timsinhvien4(malop) == null)
            {
                lop.xoalop(malop);
                return Redirect("~/QLSV/QLLop");
            }
            else
            {
                TempData["ErrorLop"] = "Vẫn còn tồn tại sinh viên";
                return Redirect("~/QLSV/QLLop");
            }
        }
        public ActionResult QLDiem()
        {
            Mon m = new Mon();
            Nganh ngh = new Nganh();
            SinhVien sv = new SinhVien();
            Diem d = new Diem();
            ViewBag.ListMon = m.listmon();
            ViewBag.ListNganh = ngh.listnganh();
            ViewBag.SinhVien = sv.listSinhVien();
            ViewBag.Diem = d.listdiem();
            return View();
        }
        [HttpPost]
        public ActionResult NhapDiem(int mabangdiem, string mamon, string mssv, int lanthi, double diem, string ghichu)
        {
            Diem d = new Diem(mabangdiem, mamon, mssv, lanthi, diem, ghichu);
            if (d.timdiem(mabangdiem) == null)
            {
                d.themDiem(d);
                return Redirect("~/QLSV/QLDiem");
            }
            else
            {
                TempData["DaCoBD"] = "Bảng điểm này đã tồn tại";
                return Redirect("~/QLSV/QLDiem");
            }
        }
        [HttpGet]
        public ActionResult SuaDiem(int mabangdiem)
        {
            Diem d = new Diem();
            var a = d.timdiem(mabangdiem);
            Mon m = new Mon();
            var mamon = m.timmon(a.mamon);
            ViewBag.Mon = mamon;
            SinhVien sv = new SinhVien();
            var masv = sv.timsinhvien(a.mssv);
            ViewBag.SinhVien = masv;
            ViewBag.Mon1 = m.listmon2(a.mamon);
            ViewBag.SinhVien1 = sv.listsinhvien2(a.mssv);
            ViewBag.SuaDiem = a;
            return View();
        }
        [HttpPost]
        public ActionResult SuaDiem(int mabangdiem, string mamon, string mssv, int lanthi, double diem, string ghichu)
        {
            Diem d = new Diem();
            d.suadiem(mabangdiem, mamon, mssv, lanthi, diem, ghichu);
            return Redirect("~/QLSV/QLDiem");
        }
        public ActionResult XoaDiem(int mabangdiem)
        {
            Diem d = new Diem();
            d.xoadiem(mabangdiem);
            return Redirect("~/QLSV/QLDiem");
        }
        public ActionResult TimDiem(double diemtim)
        {
            Diem d = new Diem();
            var timkiem = d.timdiem2(diemtim);
            //var timkiem = db.Sach.Where(s => ConvertToUnSign(s.tensach).StartsWith(tensach)).ToList();
            Mon m = new Mon();
            ViewBag.ListMon = m.listmon();
            Nganh ngh = new Nganh();
            ViewBag.ListNganh = ngh.listnganh();
            SinhVien sv = new SinhVien();
            ViewBag.SinhVien = sv.listSinhVien();
            ViewBag.TimKiem = timkiem;
            return View();
        }
    }
}