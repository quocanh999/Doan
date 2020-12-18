using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
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
                    return View();
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Xem(ThongKeHD tk)
        {
            using (var db = new BookContext())
            {
                List<HoaDon> listHD = new List<HoaDon>();
                listHD = db.HoaDon.Where(i => i.NgayTao.Year == tk.Year).ToList();
                if (listHD.Count() == 0)
                {
                    
                    return RedirectToAction("Index", "ThongKe");
                }
                else
                {
                    double Tong = 0;
                    List<ThongKeHD> listTK = new List<ThongKeHD>();
                    int month = 1;
                    while (month <= 12)
                    {
                        ThongKeHD hd = new ThongKeHD();
                        hd.Year = tk.Year;
                        hd.Month = month;
                        hd.SoLuongBill = listHD.Where(i => i.NgayTao.Month == month).Count();
                        foreach (var item in listHD.Where(i => i.NgayTao.Month == month).ToList())
                        {
                            hd.TongTien += item.TongTien;
                        }
                        Tong = Tong + hd.TongTien;
                        listTK.Add(hd);
                        month++;
                    }
                    ViewBag.Tong = Tong;
                    Session["ThongKe"] = listTK;
                    return View("Index");
                }
            }
        }
    }
}