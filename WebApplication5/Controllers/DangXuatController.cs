using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class DangXuatController : Controller
    {
        // GET: DangXuat
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
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
                log.HanhDong = "Đã log out ";
                db.Log.Add(log);
                db.SaveChanges();
                ChiTietSachController.listCTHD = new List<Sach>();
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}