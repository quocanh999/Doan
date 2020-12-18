using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class DangNhapController : Controller
    {
        BookContext db = new BookContext();
        // GET: DangNhap
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
           
                KhachHang khachHang = new KhachHang();
                khachHang.HoTen = "Admin";
                khachHang.NgaySinh = DateTime.Today;
                khachHang.GioiTinh = "Nam";
                khachHang.TaiKhoan = "admin";
                khachHang.MatKhau = "admin";
                khachHang.LoaiKH = "Admin";
                khachHang.flag = false;
            if (db.KhachHang.Where(i=>i.TaiKhoan==khachHang.TaiKhoan).FirstOrDefault() == null)
            {
                using (db)
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = khachHang.TaiKhoan;
                    log.HanhDong = khachHang.HoTen + " đã được đăng kí ";
                    db.Log.Add(log);
                    db.KhachHang.Add(khachHang);
                    db.SaveChanges();
                }
            }
            if (Session["KhachHang"]!=null)
            {
                return RedirectToAction("Index", "Home");
            }
            else {  return View();}
           
            
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Index(string username,string password)
        {
            if (Session["KhachHang"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var rs = db.KhachHang.Where(i => i.flag == false && i.TaiKhoan == username && i.MatKhau == password).FirstOrDefault();

                if (rs == null)
                {
                    ViewBag.Err = "Wrong User Name or Password";
                    return View("Index");
                }
                else
                {
                    Session["KhachHang"] = rs;
                    if (rs.LoaiKH == "Admin" || rs.LoaiKH == "Nhập liệu")
                    {
                        KhachHang kh1 = new KhachHang();
                        kh1 = Session["KhachHang"] as KhachHang;
                        using (var db = new BookContext())
                        {
                            Log log = new Log();
                            log.TacVu = "Admin";
                            log.ThoiGian = DateTime.Now;
                            log.TaiKhoan = kh1.TaiKhoan;
                            log.HanhDong = "Đã đăng nhập ";
                            db.Log.Add(log);
                            db.SaveChanges();
                            return View("../KhachHang/AdIndex");

                        }
                        
                    }
                    else
                    {
                        KhachHang kh1 = new KhachHang();
                        kh1 = Session["KhachHang"] as KhachHang;
                        using (var db = new BookContext())
                        {
                            Log log = new Log();
                            log.TacVu = "Admin";
                            log.ThoiGian = DateTime.Now;
                            log.TaiKhoan = kh1.TaiKhoan;
                            log.HanhDong = "Đã đăng nhập ";
                            db.Log.Add(log);
                            db.SaveChanges();
                            return View("../Home/Index");

                        }
                        
                    }
                }
            }
        }
        
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult DangXuat()
        {
            
            KhachHang kh1 = new KhachHang();
            kh1 = Session["KhachHang"] as KhachHang;
            using (var db = new BookContext())
            {
                Log log = new Log();
                log.TacVu = "Admin";
                log.ThoiGian = DateTime.Now;
                log.TaiKhoan = kh1.TaiKhoan;


                KhachHang kh = db.KhachHang.Find((Session["KhachHang"] as KhachHang).MaKH);
                log.HanhDong =  "Đã log out ";
                db.Log.Add(log);
                db.SaveChanges();
                ChiTietSachController.listCTHD = new List<Sach>();
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
          
        }
    }
}