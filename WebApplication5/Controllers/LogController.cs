using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class LogController : Controller
    {
        public static int tk = 0;
        BookContext db = new BookContext();
        // GET: Log
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            KhachHang kh = Session["KhachHang"] as KhachHang;
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
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                Session["ListLog"] = db.Log.Where(i => i.TacVu == "Admin" && i.ThoiGian.Year == year && i.ThoiGian.Month == month && i.ThoiGian.Day == day).OrderByDescending(i=>i.ThoiGian).ToList();
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TimKiemHome(string maDH, string ngayTao)
        {
            tk = 1;
            try
            {
                KhachHang kh = Session["KhachHang"] as WebApplication5.Models.KhachHang;
                if (String.IsNullOrEmpty(maDH) == true && String.IsNullOrEmpty(ngayTao) == true)
                {
                    Session["LichSu"] = db.HoaDon.Where(i =>i.MaKH==kh.MaKH &&  i.flag == true).OrderByDescending(i => i.NgayTao).ToList();

                }
                else if (String.IsNullOrEmpty(maDH) == false && String.IsNullOrEmpty(ngayTao) == true)
                {
                    int ma = Convert.ToInt32(maDH);
                    Session["LichSu"] = db.HoaDon.Where(i => i.MaKH == kh.MaKH && i.flag == true && i.MaDonHang == ma).OrderByDescending(i => i.NgayTao).ToList();

                }
                else if (String.IsNullOrEmpty(maDH) == true && String.IsNullOrEmpty(ngayTao) == false)
                {
                    string[] arr = ngayTao.Split('-');

                    int year = Convert.ToInt32(arr[0]);
                    int month = Convert.ToInt32(arr[1]);
                    int day = Convert.ToInt32(arr[2]);
                    Session["LichSu"] = db.HoaDon.Where(i => i.MaKH == kh.MaKH && i.flag == true && i.NgayTao.Year == year && i.NgayTao.Month == month && i.NgayTao.Day == day).OrderByDescending(i => i.NgayTao).ToList();

                }
                else
                {
                    string[] arr = ngayTao.Split('-');
                    int year = Convert.ToInt32(arr[0]);
                    int month = Convert.ToInt32(arr[1]);
                    int day = Convert.ToInt32(arr[2]);
                    int ma = Convert.ToInt32(maDH);
                    Session["LichSu"] = db.HoaDon.Where(i => i.MaKH == kh.MaKH && i.flag == true && i.NgayTao.Year == year && i.NgayTao.Month == month && i.NgayTao.Day == day && i.MaDonHang == ma).OrderByDescending(i => i.NgayTao).ToList();

                }

                return RedirectToAction("LichSu", "Log");

            }
            catch
            {
                Session["LichSu"] = db.HoaDon.Where(i =>  i.flag == true).ToList();
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult LichSu()
        {
            if (Session["KhachHang"] == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else
            {
                if (tk == 1)
                {
                    tk = 0;
                    return View();
                }


                KhachHang kh = Session["KhachHang"] as WebApplication5.Models.KhachHang;
                Session["LichSu"] = db.HoaDon.Where(i => i.MaKH == kh.MaKH && i.flag == true).OrderByDescending(i=>i.NgayTao).ToList();
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult CTLichSu(int id)
        {
            if (Session["KhachHang"] == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            else
            {
                Session["ListCTLichSu"] = db.ChiTietHoaDon.Where(i => i.MaDonHang == id).ToList();
                Session["LS"] = db.HoaDon.Find(id);
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TimKiem(string ngayTimKiem)
        {
            KhachHang kh = Session["KhachHang"] as KhachHang;
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
                if (string.IsNullOrEmpty(ngayTimKiem))
                {
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    int day = DateTime.Now.Day;
                    return RedirectToAction("Index", "Log");
                }
                else
                {
                    string[] temp = ngayTimKiem.Split('-');
                    int year = Convert.ToInt32(temp[0]);
                    int month = Convert.ToInt32(temp[1]);
                    int day = Convert.ToInt32(temp[2]);
                    Session["ListLog"] = db.Log.Where(i => i.TacVu == "Admin" && i.ThoiGian.Year == year && i.ThoiGian.Month == month && i.ThoiGian.Day == day).OrderByDescending(i => i.ThoiGian).ToList();
                    return View("Index");
                }
               
            }
            }
    }
}