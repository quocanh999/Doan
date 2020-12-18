using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: AdminArea/ChuDe
        BookContext db = new BookContext();
        public static int pb=1;
        // GET: ChuDe
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
                    Session["ListCD"] = db.ChuDe.Where(i => i.flag == false).ToList();
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
                pb = 0;
                if (string.IsNullOrEmpty(name))
                {
                    Session["ListCD"] = db.ChuDe.Where(i => i.flag == false).ToList();
                }
                else
                {
                    Session["ListCD"] = db.ChuDe.Where(i => i.flag == false && i.TenChuDe.Contains(name)).ToList();
                 
                }
                return RedirectToAction("Index", "ChuDe");
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
                Session["ChuDe"] = db.ChuDe.Find(id);
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Update(ChuDe cd)
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
               
                

                ChuDe chuDe = db.ChuDe.Find((Session["ChuDe"] as ChuDe).MaChuDe);
                if (chuDe.TenChuDe != cd.TenChuDe)
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = kh.TaiKhoan;
                    log.HanhDong = "Đã thay đổi chủ đề có tên là " + chuDe.TenChuDe + " thành " + cd.TenChuDe;
                    db.Log.Add(log);
                }
                chuDe.TenChuDe = cd.TenChuDe;
                db.SaveChanges();
                return RedirectToAction("Index", "ChuDe");
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Add(ChuDe cd)
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
                using (var db = new BookContext())
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = kh.TaiKhoan;
                    log.HanhDong = "Đã thêm chủ đề có tên là "+cd.TenChuDe ;
                    db.Log.Add(log);

                    ChuDe chuDe = new ChuDe();
                    chuDe.TenChuDe = cd.TenChuDe;
                    db.ChuDe.Add(chuDe);
                    db.SaveChanges();
                    Session["ListCD"] = db.ChuDe.Where(i => i.flag == false).ToList();
                    return RedirectToAction("Index", "ChuDe");
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Xoa(int id)
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
            else if (kh.LoaiKH == "Nhập Liệu")
            {
                return RedirectToAction("Index", "ChuDe");
            }
            else
            {
                if (db.Sach.Where(i => i.MaChuDe == id && i.flag == false).Count() == 0)
                {
                    db.ChuDe.Find(id).flag = true;
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = kh.TaiKhoan;
                    log.HanhDong = "Đã xóa chủ đề có tên là " + db.ChuDe.Find(id).TenChuDe;
                    db.Log.Add(log);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "ChuDe");
            }
        }
    }
}