using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
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
                return View();
            }
        }
    }
}