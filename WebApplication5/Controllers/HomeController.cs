using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [OutputCache(NoStore =true,Duration =0,VaryByParam ="None")]
        public ActionResult Index()
        {
            Session["ListSachMoi"] = SachGetListMoi();
            Session["ListSachBanChay"]=SachGetListBanChay();
            Session["ListSachXemNhieu"] = SachGetListXemNhieu();
            return View();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult KetQua()
        {
            return View();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public List<Sach> SachGetListBanChay()
        {
            var db = new BookContext();
            List<Sach> sach = new List<Sach>();
            List<SachBanChay> banChay = new List<SachBanChay>();
            List<HoaDon> listHD = db.HoaDon.Where(i => i.DaThanhToan == true).ToList();
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            foreach (var item in listHD)
            {
                foreach (var item1 in db.ChiTietHoaDon.ToList())
                {
                    if (item.MaDonHang == item1.MaDonHang)
                    {
                        listCTHD.Add(item1);
                    }
                }
            }
            foreach (var item in db.Sach.Where(i => i.flag == false).ToList())
            {
                SachBanChay a = new SachBanChay();
                a.MaSach = item.MaSach;
                foreach (var item1 in listCTHD.Where(i => i.MaSach == item.MaSach).ToList())
                {
                    a.SoLuong = a.SoLuong + item1.SoLuong;
                }
                if (a.SoLuong != 0)
                {
                    banChay.Add(a);
                }

            }
            banChay = banChay.OrderByDescending(i => i.SoLuong).Take(4).ToList();
            if (banChay.Count() != 0)
            {
                foreach (var item in banChay)
                {

                    sach.Add(db.Sach.Find(item.MaSach));
                }
            }


            return sach;
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public List<Sach> SachGetListMoi()
        {
            var db = new BookContext();
            return db.Sach.Where(i => i.flag == false).OrderByDescending(i=>i.NgayCapNhat).Take(4).ToList();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public List<Sach> SachGetListXemNhieu()
        {
            var db = new BookContext();
            return db.Sach.Where(i => i.flag == false).OrderByDescending(i => i.LuotXem).Take(4).ToList();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult TimKiemSach(string tenSach)
        {
            var db = new BookContext();
            Session["Products"] = db.Sach.Where(i => i.flag == false && i.TenSach.Contains(tenSach)).ToList();
            return View("KetQua");
        }
    }
}