using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ThongTinTaiKhoanController : Controller
    {
        // GET: ThongTinTaiKhoan
        static int id;
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            if (Session["KhachHangId"] == null)
                return View("tmp");
            int tmpAcc = int.Parse(Session["KhachHangId"].ToString());
            id = tmpAcc;
            var db = new BookContext();
            return View(db.KhachHang.Find(tmpAcc));
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult CapNhat(string tenkh, string matkhaumoi,string matkhauxn)
        {
            KhachHang kh = Session["KhachHang"] as KhachHang;
            if (kh == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else
            {
                if (matkhaumoi == ""||matkhaumoi==null)
                {
                    var db = new BookContext();
                    var kh1 = db.KhachHang.Find(id);
                    kh1.HoTen = tenkh;
                    db.SaveChanges();
                    Session["KhachHang"] = kh1;
                    Session["KhachHangId"] = kh1.MaKH;
                    Session["TenKhachHang"] = kh1.HoTen;
                    Session["Loimk"] = "z";
                    return RedirectToAction("Index", "ThongTinTaiKhoan");

                }
                else
                {
                    if (matkhaumoi != matkhauxn)
                    {
                        Session["Loimk"] = "Mật khẩu xác nhận không đúng";
                        return RedirectToAction("Index", "ThongTinTaiKhoan");
                    }
                    else
                    {
                        var db = new BookContext();
                        var kh1 = db.KhachHang.Find(id);
                        kh1.HoTen = tenkh;
                        kh1.MatKhau = matkhaumoi;
                        db.SaveChanges();
                        Session["KhachHang"] = kh1;
                        Session["KhachHangId"] = kh1.MaKH;
                        Session["TenKhachHang"] = kh1.HoTen;
                        Session["Loimk"] = "z";
                        return RedirectToAction("Index", "ThongTinTaiKhoan");
                    }
                }
                
            }
        }
    }
}