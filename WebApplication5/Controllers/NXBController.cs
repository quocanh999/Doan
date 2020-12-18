using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class NXBController : Controller
    {
        public static int pb = 1;
        // GET: AdminArea/NXB
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
                    using (var db = new BookContext())
                    {
                        Session["ListNXB"] = db.NhaXuaBan.Where(i => i.flag == false).ToList();
                    }
                }
                else
                {
                    pb = 1;
                }
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Add(NhaXuatBan nxb)
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
                    log.HanhDong = "Đã thêm nhà xuất bản " + nxb.TenNXB;
                    db.Log.Add(log);
                    db.SaveChanges();


                    db.NhaXuaBan.Add(nxb);
                    db.SaveChanges();
                    return RedirectToAction("Index", "NXB");
                }
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
                using (var db = new BookContext())
                {
                    pb = 0;
                    if (string.IsNullOrEmpty(name))
                    {
                        Session["ListNXB"] = db.NhaXuaBan.Where(i => i.flag == false).ToList();
                    }
                    else
                    {
                        Session["ListNXB"] = db.NhaXuaBan.Where(i => i.flag == false && i.TenNXB.Contains(name)).ToList();
                    }
                    return RedirectToAction("Index", "NXB");
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
                using (var db = new BookContext())
                {
                    Session["NXB"] = db.NhaXuaBan.Find(id);
                    return View();
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Update(NhaXuatBan nxb)
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
                   


                    NhaXuatBan nNXB = db.NhaXuaBan.Find((Session["NXB"] as NhaXuatBan).MaNXB);
                    if (nNXB.TenNXB != nxb.TenNXB)
                    {
                        Log log = new Log();
                        log.TacVu = "Admin";
                        log.TaiKhoan = kh.TaiKhoan;
                        log.ThoiGian = DateTime.Now;
                        log.HanhDong = "Đã cập nhật nhà xuất bản có tên " + nNXB.TenNXB+" thành " + nxb.TenNXB;
                        db.Log.Add(log);
                    }
                    nNXB.TenNXB = nxb.TenNXB;
                    db.SaveChanges();
                    return RedirectToAction("Index", "NXB");
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
            else if (kh.LoaiKH == "Nhập Liệu")
            {
                return RedirectToAction("Index", "NXB");
            }
            else
            {
                using (var db = new BookContext())
                {
                    if (db.Sach.Where(i => i.MaNXB == id && i.flag == false).Count() == 0)
                    {
                        Log log = new Log();
                        log.ThoiGian = DateTime.Now;
                        log.TacVu = "Admin";
                        log.TaiKhoan = kh.TaiKhoan;
                        

                        NhaXuatBan nxb = db.NhaXuaBan.Find(id);
                        log.HanhDong = "Đã xóa nhà xuất bản " + nxb.TenNXB;
                        db.Log.Add(log);
                        nxb.flag = true;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "NXB");
                }
            }
        }
    }
}