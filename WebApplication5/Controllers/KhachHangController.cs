using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class KhachHangController : Controller
    {
        BookContext db = new BookContext();
        public static int pb = 1;
        // GET: AdminArea/KhachHang
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult AdIndex()
        {
            using (var db = new BookContext())
            {
                KhachHang kh = new KhachHang();
                kh = Session["KhachHang"] as KhachHang;
                if (kh == null)
                {
                    return RedirectToAction("Index", "DangNhap");
                }
                else if (kh.LoaiKH == "Khách Hàng")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (pb == 1)
                    {
                        Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.TaiKhoan != kh.TaiKhoan).ToList();
                    }
                    else
                    {
                        pb = 1;
                    }
                    return View();
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TimKiem(string name, string tacgia, string theloai)
        {
            KhachHang kh = new KhachHang();
            kh = Session["KhachHang"] as KhachHang;
            if (kh == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else if (kh.LoaiKH == "Khách Hàng")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                pb = 0;
                if (String.IsNullOrEmpty(name) == true && tacgia == "Khong co" && String.IsNullOrEmpty(theloai) == true)
                {
                    Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.TaiKhoan != kh.TaiKhoan).ToList();
                }
                else if (String.IsNullOrEmpty(name) == true && tacgia != "Khong co" && String.IsNullOrEmpty(theloai) == true)
                {
                   
                        Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.LoaiKH == tacgia && i.TaiKhoan != kh.TaiKhoan).ToList();
                    
                }
                else if (String.IsNullOrEmpty(name) == true && tacgia == "Khong co" && String.IsNullOrEmpty(theloai) == false)
                {
                    
                    Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.TaiKhoan.Contains(theloai) && i.TaiKhoan != kh.TaiKhoan).ToList();
                }
                else if (String.IsNullOrEmpty(name) == false && tacgia == "Khong co" && String.IsNullOrEmpty(theloai) == false)
                {
                    
                    Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.HoTen.Contains(name) && i.TaiKhoan.Contains(theloai) && i.TaiKhoan != kh.TaiKhoan).ToList();
                }
                else if (String.IsNullOrEmpty(name) == false && tacgia != "Khong co" && String.IsNullOrEmpty(theloai) == true)
                {
                    
                    Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.HoTen.Contains(name) && i.LoaiKH == tacgia && i.TaiKhoan != kh.TaiKhoan).ToList();
                }
                else if (String.IsNullOrEmpty(name) == false && tacgia == "Khong co" && String.IsNullOrEmpty(theloai) == true)
                {
                    Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.HoTen.Contains(name) && i.TaiKhoan != kh.TaiKhoan).ToList();
                }
                else if (String.IsNullOrEmpty(name) == true && tacgia != "Khong co" && String.IsNullOrEmpty(theloai) == false)
                {
                   
                    Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.LoaiKH == tacgia && i.TaiKhoan.Contains(theloai) && i.TaiKhoan != kh.TaiKhoan).ToList();
                }
                else
                {
               
                    Session["ListKH"] = db.KhachHang.Where(i => i.flag == false && i.HoTen.Contains(name) && i.LoaiKH == tacgia && i.TaiKhoan.Contains(theloai) && i.TaiKhoan != kh.TaiKhoan).ToList();
                }
                return RedirectToAction("AdIndex", "KhachHang");
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Update(int id)
        {
            KhachHang kh = new KhachHang();
            kh = Session["KhachHang"] as KhachHang;
            if (kh == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else if (kh.LoaiKH == "Khách Hàng")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                using (var db = new BookContext())
                {
                    Session["KH"] = db.KhachHang.Find(id);
                    return View();
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Update(KhachHang kh,string loaitaikhoan)
        {
            KhachHang kh1 = new KhachHang();
            kh1 = Session["KhachHang"] as KhachHang;
            if (kh1 == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else if (kh1.LoaiKH == "Khách Hàng")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                using (var db = new BookContext())
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.TaiKhoan = kh1.TaiKhoan;
                    log.ThoiGian = DateTime.Now;
                    log.HanhDong = "Đã cập nhật khách hàng có tài khoản " + kh.TaiKhoan;

                    KhachHang KH = db.KhachHang.Find((Session["KH"] as KhachHang).MaKH);
                    if (KH.HoTen != kh.HoTen)
                    {
                        log.HanhDong=log.HanhDong+" từ họ tên là "+KH.HoTen +" thành " + kh.HoTen;
                    }
                    KH.HoTen = kh.HoTen;
                    KH.NgaySinh = kh.NgaySinh;
                    KH.GioiTinh = kh.GioiTinh;
                    if (KH.LoaiKH != loaitaikhoan)
                    {
                        log.HanhDong = log.HanhDong + " từ loại tài khoản là " + KH.LoaiKH + " thành " + loaitaikhoan;
                    }
                    KH.LoaiKH = loaitaikhoan;
                    if(log.HanhDong != "Đã cập nhật khách hàng có tài khoản " + kh.TaiKhoan)
                    {
                        db.Log.Add(log);
                    }
                    db.SaveChanges();
                    return RedirectToAction("AdIndex");


                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Add(KhachHang kh,string loaitaikhoan)
        {
            KhachHang kh1 = new KhachHang();
            kh1 = Session["KhachHang"] as KhachHang;
            if (kh1 == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else if (kh1.LoaiKH == "Khách Hàng")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                using (var db = new BookContext())
                {

                    int i = db.KhachHang.Where(j => j.TaiKhoan == kh.TaiKhoan && j.flag == false).Count();
                    if (i == 0)
                    {
                        Log log = new Log();
                        log.TacVu = "Admin";
                        log.ThoiGian = DateTime.Now;
                        log.TaiKhoan = kh1.TaiKhoan;
                        log.HanhDong = "Đã thêm tài khoản " + kh.TaiKhoan;
                        db.Log.Add(log);

                        kh.LoaiKH = loaitaikhoan;
                        kh.MatKhau = "1";
                        db.KhachHang.Add(kh);
                        db.SaveChanges();
                        return RedirectToAction("AdIndex", "KhachHang");
                    }
                    else
                    {
                        ViewBag.Tk = "Tài khoản đã được sử dụng";
                        return View("AdIndex", kh);
                    }
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Reset()
        {
            KhachHang kh1 = new KhachHang();
            kh1 = Session["KhachHang"] as KhachHang;
            if (kh1 == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else if (kh1.LoaiKH == "Khách Hàng")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                using (var db = new BookContext())
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = kh1.TaiKhoan;
                   

                    KhachHang kh = db.KhachHang.Find((Session["KH"] as KhachHang).MaKH);
                    log.HanhDong = "Đã reset mật khẩu của tài khoản " + kh.TaiKhoan;
                    db.Log.Add(log);
                    kh.MatKhau = "1";
                    db.SaveChanges();
                    return RedirectToAction("AdIndex", "KhachHang");
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Xoa(int id)
        {
            KhachHang kh = new KhachHang();
            kh = Session["KhachHang"] as KhachHang;
            if (kh == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else if (kh.LoaiKH == "Khách Hàng")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (kh.LoaiKH=="Nhập Liệu")
            {
                return RedirectToAction("AdIndex");
            }
            else
            {
                using (var db = new BookContext())
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = kh.TaiKhoan;


                    KhachHang kh1 = db.KhachHang.Find(id);
                    log.HanhDong = "Đã xóa tài khoản " + kh1.TaiKhoan;
                    db.Log.Add(log);
                    kh1.flag = true;
                    db.SaveChanges();
                    return RedirectToAction("AdIndex");
                }
            }
        }
    }
}