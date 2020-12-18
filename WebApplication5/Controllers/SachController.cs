using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class SachController : Controller
    {
        BookContext db = new BookContext();
        public static int pb = 1;
        // GET: AdminArea/Sach
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
                        ViewBag.NXB = new SelectList(db.NhaXuaBan.Where(i => i.flag == false).ToList(), "MaNXB", "TenNXB");
                        ViewBag.ChuDe = new SelectList(db.ChuDe.Where(i => i.flag == false).ToList(), "MaChuDe", "TenChuDe");
                        ViewBag.TacGia = new SelectList(db.TacGia.Where(i => i.flag == false).ToList(), "MaTacGia", "TenTacGia");
                        Session["ListSach"] = db.Sach.Where(i => i.flag == false).ToList();
                        Session["CBTG"] = db.TacGia.Where(i => i.flag == false).ToList();
                        Session["CBCD"] = db.ChuDe.Where(i => i.flag == false).ToList();

                    }
                }
                else
                {
                    ViewBag.NXB = new SelectList(db.NhaXuaBan.Where(i => i.flag == false).ToList(), "MaNXB", "TenNXB");
                    ViewBag.ChuDe = new SelectList(db.ChuDe.Where(i => i.flag == false).ToList(), "MaChuDe", "TenChuDe");
                    ViewBag.TacGia = new SelectList(db.TacGia.Where(i => i.flag == false).ToList(), "MaTacGia", "TenTacGia");
                    pb = 1;
                }
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TimKiem(string name, string tacgia, string theloai)
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
                pb = 0;
                if (String.IsNullOrEmpty(name) == true && tacgia == "Khong co" && theloai == "Khong co")
                {
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false).ToList();
                }
                else if (String.IsNullOrEmpty(name) == true && tacgia != "Khong co" && theloai == "Khong co")
                {
                    int maTG = Convert.ToInt32(tacgia);
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false && i.MaTacGia ==maTG).ToList();
                }
                else if (String.IsNullOrEmpty(name) == true && tacgia == "Khong co" && theloai != "Khong co")
                {
                    int maTL = Convert.ToInt32(theloai);
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false && i.MaChuDe ==maTL ).ToList();
                }
                else if (String.IsNullOrEmpty(name) == false && tacgia == "Khong co" && theloai != "Khong co")
                {
                    int maTL = Convert.ToInt32(theloai);
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false && i.TenSach.Contains(name) && i.MaChuDe == maTL).ToList();
                }
                else if (String.IsNullOrEmpty(name) == false && tacgia != "Khong co" && theloai == "Khong co")
                {
                    int maTG = Convert.ToInt32(tacgia);
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false && i.TenSach.Contains(name) && i.MaTacGia == maTG).ToList();
                }
                else if (String.IsNullOrEmpty(name) == false && tacgia == "Khong co" && theloai == "Khong co")
                {
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false && i.TenSach.Contains(name)).ToList();
                }
                else if (String.IsNullOrEmpty(name) == true && tacgia != "Khong co" && theloai != "Khong co")
                {
                    int maTG = Convert.ToInt32(tacgia);
                    int maTL = Convert.ToInt32(theloai);
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false && i.MaTacGia == maTG && i.MaChuDe == maTL).ToList();
                }
                else
                {
                    int maTG = Convert.ToInt32(tacgia);
                    int maTL = Convert.ToInt32(theloai);
                    Session["ListSach"] = db.Sach.Where(i => i.flag == false && i.TenSach.Contains(name) && i.MaTacGia == maTG && i.MaChuDe == maTL).ToList();
                }
                return RedirectToAction("Index", "Sach");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Add(Sach sach, HttpPostedFileBase file)
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

                    string path = "";
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                            file.SaveAs(path);
                            //path= path.Replace("\","/");
                            sach.AnhBia = fileName;
                        }
                        else
                        {
                            sach.AnhBia = "img";
                        }
                    }
                    else
                    {
                        sach.AnhBia = "img";
                    }

                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = kh.TaiKhoan;
                    log.HanhDong = "Đã thêm sách có tên " + sach.TenSach + " có NXB: " + db.NhaXuaBan.Find(sach.MaNXB).TenNXB + ", Thể loại: " + db.ChuDe.Find(sach.MaChuDe).TenChuDe + ", Tác giả: " + db.TacGia.Find(sach.MaTacGia).TenTacGia + ", Giá: " + sach.GiaBan;
                    db.Log.Add(log);

                    sach.NgayCapNhat = DateTime.Now;
                    sach.LuotXem = 0;
                    db.Sach.Add(sach);
                    db.SaveChanges();
                    Log log1 = new Log();
                    log1.TacVu = "Log";
                    log1.ThoiGian = DateTime.Now;
                    log1.TaiKhoan = sach.MaSach.ToString();
                    int dem = 0;
                    log1.HanhDong = dem.ToString();
                    db.Log.Add(log1);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Sach");
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
                    Session["Sach"] = db.Sach.Find(id);
                    Sach sach = db.Sach.Find(id);
                    ViewBag.Hinh = sach.AnhBia;
                    ViewBag.NXB = new SelectList(db.NhaXuaBan.Where(i => i.flag == false).ToList(), "MaNXB", "TenNXB", sach.MaNXB);
                    ViewBag.ChuDe = new SelectList(db.ChuDe.Where(i => i.flag == false).ToList(), "MaChuDe", "TenChuDe", sach.MaChuDe);
                    ViewBag.TacGia = new SelectList(db.TacGia.Where(i => i.flag == false).ToList(), "MaTacGia", "TenTacGia", sach.MaTacGia);
                    return View();
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
            else if (kh.LoaiKH=="Nhập Liệu")
            {
                return RedirectToAction("Index","Sach");
            }
            else
            {
                using (var db = new BookContext())
                {
                    Log log = new Log();
                    log.ThoiGian = DateTime.Now;
                    log.TacVu = "Admin";
                    log.TaiKhoan = kh.TaiKhoan;
                  

                    Sach sach = db.Sach.Find(id);
                    log.HanhDong = "Đã xóa sách có tên " + sach.TenSach;
                    db.Log.Add(log);
                    sach.flag = true;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Sach");
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Update(Sach sach, HttpPostedFileBase file)
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
                    log.ThoiGian = DateTime.Now;
                    log.TacVu = "Admin";
                    log.TaiKhoan = kh.TaiKhoan;
                    


                    Sach S = db.Sach.Find((Session["Sach"] as Sach).MaSach);
                    log.HanhDong = "Đã cập nhật thông tin sách có tên: "+S.TenSach;
                    string path = "";
                    if (file == null)
                    {
                        if (S.TenSach != sach.TenSach)
                        {
                            log.HanhDong = log.HanhDong + " thành " + sach.TenSach;
                        }
                        S.TenSach = sach.TenSach;
                        S.MoTa = sach.MoTa;
                        S.NgayCapNhat = sach.NgayCapNhat;
                        if (S.GiaBan != sach.GiaBan)
                        {
                            log.HanhDong = log.HanhDong + ", từ giá bán: " + S.GiaBan + "VNĐ thành " + sach.GiaBan + "VNĐ";
                        }
                        S.GiaBan = sach.GiaBan;
                        if (S.SoLuongTon != sach.SoLuongTon)
                        {
                            log.HanhDong = log.HanhDong + ", từ số lượng: " + S.SoLuongTon + " thành " + sach.SoLuongTon;
                        }
                        S.SoLuongTon = sach.SoLuongTon;
                        if (S.MaChuDe != sach.MaChuDe)
                        {
                            log.HanhDong = log.HanhDong + ", từ thể loại: " + db.ChuDe.Find(S.MaChuDe).TenChuDe + " thành " + db.ChuDe.Find(sach.MaChuDe).TenChuDe;
                        }
                        S.MaChuDe = sach.MaChuDe;
                        S.MaNXB = sach.MaNXB;
                        if (S.MaTacGia != sach.MaTacGia)
                        {
                            log.HanhDong = log.HanhDong + ", từ tác giả: " + db.TacGia.Find(S.MaTacGia).TenTacGia + " thành " + db.TacGia.Find(sach.MaTacGia).TenTacGia;
                        }
                        S.MaTacGia = sach.MaTacGia;
                        if(log.HanhDong!= "Đã cập nhật thông tin sách có tên: " + S.TenSach)
                        {
                            db.Log.Add(log);
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index", "Sach");
                    }
                    else
                    {
                        if (file.ContentLength > 0)
                        {

                            string fileName = Path.GetFileName(file.FileName);
                            path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                            file.SaveAs(path);
                            S.AnhBia = fileName;
                        }
                        S.TenSach = sach.TenSach;
                        S.MoTa = sach.MoTa;
                        S.NgayCapNhat = sach.NgayCapNhat;
                        S.GiaBan = sach.GiaBan;
                        S.SoLuongTon = sach.SoLuongTon;
                        S.MaChuDe = sach.MaChuDe;
                        S.MaNXB = sach.MaNXB;
                        S.MaTacGia = sach.MaTacGia;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Sach");
                    }
                }
            }
        }
    }
}