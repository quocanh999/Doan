using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.scripts;

namespace WebApplication5.Controllers
{
    public class DangKyController : Controller
    {
        // GET: DangKy
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {

            return View();

        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Index(string name,DateTime dob,string gender,string address,string username,string pass,string repass)
        {
            var db = new BookContext();
            if (db.KhachHang.Where(i => i.TaiKhoan == username).FirstOrDefault()!=null)
            {
                ViewBag.Err = "User Name is duplicate, please try another one";
                return View("Index");
            }
            else
            {
                if (pass != repass)
                {
                    ViewBag.Err = "Repeat password is not correct ";
                    return View("Index");
                }
                else
                {
                    KhachHang kh = new KhachHang();
                    kh.flag = false;
                    kh.GioiTinh = gender;
                    kh.LoaiKH = "Khách Hàng";
                    kh.HoTen = name;
                    kh.NgaySinh = dob;
                    kh.TaiKhoan = username;
                    kh.MatKhau = pass;
                    using (db)
                    {
                        Log log = new Log();
                        log.TacVu = "Admin";
                        log.ThoiGian = DateTime.Now;
                        log.TaiKhoan = kh.TaiKhoan;
                        log.HanhDong = kh.HoTen + " đã được đăng kí ";
                        db.Log.Add(log);
                        db.KhachHang.Add(kh);
                        db.SaveChanges();
                        return View("../DangNhap/Index");
                    }
                    
                }
            }
        }
       

    } }
