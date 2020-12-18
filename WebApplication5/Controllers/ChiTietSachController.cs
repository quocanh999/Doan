using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ChiTietSachController : Controller
    {
        BookContext db = new BookContext();
        public static List<Sach> listCTHD = new List<Sach>();
        // GET: ChiTietSach
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(int id)
        {

            
            var db = new BookContext();
            if (db.Sach.Find(id) == null)
            {
                return HttpNotFound();
            }
            if (id == null)
                return HttpNotFound();
            Session["ListCTS"] = db.Sach.Find(id);
            Session["ListCungChuDe"] = GetListCungChuDe(db.Sach.Find(id).MaChuDe,id);
            db.Sach.Find(id).LuotXem++;
            db.SaveChanges();
            return View("Index");
            

        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult ThemGioHang(int idSach)
        {
            if (Session["KhachHang"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                var db = new BookContext();
                var tk = Session["KhachHang"] as KhachHang;
                var hd = db.HoaDon.Where(i => i.DaThanhToan == false && i.MaKH == tk.MaKH).FirstOrDefault();
                if (hd == null)
                {
                    HoaDon newHd = new HoaDon();
                    newHd.MaKH = tk.MaKH;
                    newHd.NgayTao = DateTime.Today;
                    newHd.DaThanhToan = false;
                    newHd.flag = true;
                    newHd.TongTien = 0;
                    newHd.SDT = "";
                    newHd.DiaChi = "";
                    db.HoaDon.Add(newHd);
                    db.SaveChanges();

                    ChiTietHoaDon newCTHD = new ChiTietHoaDon();
                    newCTHD.MaSach = idSach;
                    newCTHD.MaDonHang = newHd.MaDonHang;
                    newCTHD.SoLuong = 1;
                    newCTHD.ThanhTien = db.Sach.Find(idSach).GiaBan * newCTHD.SoLuong;
                    newCTHD.flag = true;
                    db.ChiTietHoaDon.Add(newCTHD);
                    db.SaveChanges();
                }
                else
                {
                    var listCTHD = db.ChiTietHoaDon.Where(i => i.MaDonHang == hd.MaDonHang).ToList();
                    int pb = 0;
                    foreach (var item in listCTHD)
                    {
                        if (item.MaSach == idSach)
                        {
                            pb = 1;
                            var cthd = db.ChiTietHoaDon.Find(item.MaCTDH);
                            cthd.SoLuong = cthd.SoLuong + 1;
                            if (cthd.SoLuong > db.Sach.Find(idSach).SoLuongTon)
                            {
                                ViewBag.Add = "Add Unsuccessfully";
                                return View("Index");
                            }
                            else
                            {
                                cthd.ThanhTien = db.Sach.Find(idSach).GiaBan * cthd.SoLuong;
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                    if (pb == 0)
                    {
                        ChiTietHoaDon newCTHD = new ChiTietHoaDon();
                        newCTHD.MaSach = idSach;
                        newCTHD.MaDonHang = hd.MaDonHang;
                        newCTHD.SoLuong = 1;
                        newCTHD.ThanhTien = db.Sach.Find(idSach).GiaBan * newCTHD.SoLuong;
                        newCTHD.flag = true;
                        db.ChiTietHoaDon.Add(newCTHD);
                        db.SaveChanges();
                    }
                }
                ViewBag.Add = "Add successfully";
                return View("Index");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult MuaNgay(int idSach)
        {
            if (Session["KhachHang"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                var db = new BookContext();
                var tk = Session["KhachHang"] as KhachHang;
                var hd = db.HoaDon.Where(i => i.DaThanhToan == false && i.MaKH == tk.MaKH).FirstOrDefault();
                if (hd == null)
                {
                    HoaDon newHd = new HoaDon();
                    newHd.MaKH = tk.MaKH;
                    newHd.NgayTao = DateTime.Today;
                    newHd.DaThanhToan = false;
                    newHd.flag = true;
                    newHd.TongTien = 0;
                    newHd.SDT = "";
                    newHd.DiaChi = "";
                    db.HoaDon.Add(newHd);
                    db.SaveChanges();

                    ChiTietHoaDon newCTHD = new ChiTietHoaDon();
                    newCTHD.MaSach = idSach;
                    newCTHD.MaDonHang = newHd.MaDonHang;
                    newCTHD.SoLuong = 1;
                    newCTHD.ThanhTien = db.Sach.Find(idSach).GiaBan * newCTHD.SoLuong;
                    newCTHD.flag = true;
                    db.ChiTietHoaDon.Add(newCTHD);
                    db.SaveChanges();
                }
                else
                {
                    var listCTHD = db.ChiTietHoaDon.Where(i => i.MaDonHang == hd.MaDonHang).ToList();
                    int pb = 0;
                    foreach (var item in listCTHD)
                    {
                        if (item.MaSach == idSach)
                        {
                            pb = 1;
                            var cthd = db.ChiTietHoaDon.Find(item.MaCTDH);
                            cthd.SoLuong = cthd.SoLuong + 1;
                            if (cthd.SoLuong > db.Sach.Find(idSach).SoLuongTon)
                            {
                                ViewBag.Add = "Add Unsuccessfully";
                                return View("Index");
                            }
                            else
                            {
                                cthd.ThanhTien = db.Sach.Find(idSach).GiaBan * cthd.SoLuong;
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                    if (pb == 0)
                    {
                        ChiTietHoaDon newCTHD = new ChiTietHoaDon();
                        newCTHD.MaSach = idSach;
                        newCTHD.MaDonHang = hd.MaDonHang;
                        newCTHD.SoLuong = 1;
                        newCTHD.ThanhTien = db.Sach.Find(idSach).GiaBan * newCTHD.SoLuong;
                        newCTHD.flag = true;
                        db.ChiTietHoaDon.Add(newCTHD);
                        db.SaveChanges();
                    }
                }
                ViewBag.Add = "Add successfully";
                return RedirectToAction("Index","GioHang");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public List<Sach> GetListCungChuDe(int idChuDe,int idSach)
        {
            var db = new BookContext();
            return db.Sach.Where(i => i.flag == false && i.MaChuDe == idChuDe && i.MaSach != idSach).Take(4).ToList();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Dem(KhachHang kh)
        {
            HoaDon hd = db.HoaDon.Where(i => i.DaThanhToan == false && i.MaKH == kh.MaKH).FirstOrDefault();
            int soluong = 0;
            foreach (var item in db.ChiTietHoaDon.Where(i=>i.flag==false && i.MaDonHang==hd.MaDonHang).ToList())
            {
                soluong += item.SoLuong;
            }
            Session["SL"] = soluong;
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult ThemSach(int id, int soluong = 1)
        {

            KhachHang kh = new KhachHang();
            kh = Session["KhachHang"] as KhachHang;
            if (kh == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else
            {
                HoaDon hd = db.HoaDon.Where(i => i.MaKH == kh.MaKH && i.DaThanhToan == false).FirstOrDefault();
                if (hd == null)
                {
                    HoaDon newHD = new HoaDon();
                    newHD.MaKH = kh.MaKH;
                    newHD.DaThanhToan = false;
                    newHD.NgayTao = DateTime.Now;
                    newHD.TongTien = 0;
                    newHD.flag = false;
                    db.HoaDon.Add(newHD);
                    db.SaveChanges();
                    ChiTietHoaDon ctHD = new ChiTietHoaDon();
                    ctHD.MaDonHang = newHD.MaDonHang;
                    ctHD.MaSach = id;
                    ctHD.SoLuong = soluong;
                    ctHD.ThanhTien = db.Sach.Find(id).GiaBan * ctHD.SoLuong;
                    db.HoaDon.Find(newHD.MaDonHang).TongTien = ctHD.ThanhTien;
                    ctHD.flag = false;
                    db.ChiTietHoaDon.Add(ctHD);
                    db.SaveChanges();
                    Dem(kh);
                    return RedirectToAction("Index", "GioHang");
                }
                else
                {
                    ChiTietHoaDon ctHD = db.ChiTietHoaDon.Where(i => i.MaDonHang == hd.MaDonHang && i.flag == false && i.MaSach == id).FirstOrDefault();
                    if (ctHD == null)
                    {
                        ChiTietHoaDon newCtHD = new ChiTietHoaDon();
                        newCtHD.MaDonHang = hd.MaDonHang;
                        newCtHD.MaSach = id;
                        newCtHD.SoLuong = soluong;
                        newCtHD.ThanhTien = db.Sach.Find(id).GiaBan * newCtHD.SoLuong;
                        newCtHD.flag = false;
                        db.ChiTietHoaDon.Add(newCtHD);
                        db.SaveChanges();
                        hd = db.HoaDon.Find(hd.MaDonHang);
                        hd.TongTien = 0;
                        foreach (var item in db.ChiTietHoaDon.Where(i => i.MaDonHang == hd.MaDonHang && i.flag == false).ToList())
                        {
                            hd.TongTien += item.ThanhTien;
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        ChiTietHoaDon newCtHD = db.ChiTietHoaDon.Find(ctHD.MaCTDH);
                        newCtHD.SoLuong += soluong;
                        newCtHD.ThanhTien = db.Sach.Find(id).GiaBan * newCtHD.SoLuong;
                        db.SaveChanges();
                        hd = db.HoaDon.Find(hd.MaDonHang);
                        hd.TongTien = 0;
                        foreach (var item in db.ChiTietHoaDon.Where(i => i.MaDonHang == hd.MaDonHang && i.flag == false).ToList())
                        {
                            hd.TongTien += item.ThanhTien;
                        }
                        db.SaveChanges();

                    }
                    Dem(kh);
                    return RedirectToAction("Index", "GioHang");
                }

            }
        }
    }
}