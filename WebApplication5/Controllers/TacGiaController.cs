using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: AdminArea/TacGia
        BookContext db = new BookContext();
        public static int pb = 1;
        // GET: TacGia
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
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
                    Session["ListTG"] = db.TacGia.Where(i => i.flag == false).ToList();
                }
                else
                {
                    pb = 1;
                }
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TimKiem(string name)
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
                if (string.IsNullOrEmpty(name))
                {
                    Session["ListTG"] = db.TacGia.Where(i => i.flag == false).ToList();
                }
                else
                {
                    Session["ListTG"] = db.TacGia.Where(i => i.flag == false && i.TenTacGia.Contains(name)).ToList();   
                }
                return RedirectToAction("Index", "TacGia");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Add(TacGia tg)
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
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.TaiKhoan = kh.TaiKhoan;
                    log.ThoiGian = DateTime.Now;
                    log.HanhDong = "Đã thêm tác giả có tên " + tg.TenTacGia;
                    db.Log.Add(log);


                    TacGia tacGia = new TacGia();
                    tacGia.TenTacGia = tg.TenTacGia;
                    db.TacGia.Add(tg);
                    db.SaveChanges();
                    Session["ListTG"] = db.TacGia.Where(i => i.flag == false).ToList();
                    return RedirectToAction("Index", "TacGia");
                }
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
                Session["TG"] = db.TacGia.Find(id);
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Update(TacGia tg)
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
                TacGia tacGia = db.TacGia.Find((Session["TG"] as TacGia).MaTacGia);
                if (tacGia.TenTacGia != tg.TenTacGia)
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.TaiKhoan = kh.TaiKhoan;
                    log.ThoiGian = DateTime.Now;
                    log.HanhDong = "Đã cập nhật tác giả có tên " + tacGia.TenTacGia + " thành " + tg.TenTacGia;
                    db.Log.Add(log);
                }
                tacGia.TenTacGia = tg.TenTacGia;
                db.SaveChanges();
                return RedirectToAction("Index", "TacGia");
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
            else if (kh.LoaiKH == "Nhập Liệu")
            {
                return RedirectToAction("Index","TacGia");
            }
            else
            {
                if (db.Sach.Where(i => i.MaTacGia == id && i.flag == false).Count() == 0)
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.TaiKhoan = kh.TaiKhoan;
                    log.ThoiGian = DateTime.Now;
                   

                    db.TacGia.Find(id).flag = true;
                    log.HanhDong = "Đã xóa tác giả có tên " + db.TacGia.Find(id).TenTacGia ;
                    db.Log.Add(log);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "TacGia");
            }
        }
    }
}