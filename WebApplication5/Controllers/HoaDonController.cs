using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HoaDonController : Controller
    {
        public static DateTime? ngayBatDau;
        public static DateTime ngayKetThuc;
        // GET: HoaDon
        BookContext db = new BookContext();
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            if (Session["KhachHang"] != null)
            {
                var taiKhoan = Session["KhachHang"] as KhachHang;
                if(taiKhoan.LoaiKH=="Admin" || taiKhoan.LoaiKH=="Nhập Liệu")
                {
                    Session["ListHD"] = db.HoaDon.Where(i => i.DaThanhToan == true && i.flag == true).OrderByDescending(i => i.NgayTao).ToList();
                    return View();
                }
                else
                {
                    return View("../Home/Index");
                }
                
                
            }
            else
            {
                return RedirectToAction("Index", "DangNhap");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult TimKiem(DateTime? NgayBatDau,DateTime NgayKetThuc) {
            if (NgayBatDau!=null)
            {
                ngayBatDau = new DateTime();
                ngayBatDau = NgayBatDau;
            }
            ngayKetThuc = new DateTime();
            ngayKetThuc = NgayKetThuc;
            NgayKetThuc = NgayKetThuc.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            if (Session["KhachHang"] != null)
            {
                var taiKhoan = Session["KhachHang"] as KhachHang;
                if (taiKhoan.LoaiKH == "Admin" || taiKhoan.LoaiKH == "Nhập Liệu")
                {
                    
                    if (NgayBatDau == null)
                    {
                        Session["ListHD"] = db.HoaDon.Where(i => i.DaThanhToan == true && i.flag == true && i.NgayTao <= NgayKetThuc).OrderByDescending(i => i.NgayTao).ToList();
                        return View("Index");
                    }
                    else {
                        Session["ListHD"] = db.HoaDon.Where(i => i.DaThanhToan == true && i.flag == true && i.NgayTao >= NgayBatDau && i.NgayTao <=NgayKetThuc).OrderByDescending(i => i.NgayTao).ToList();
                        return View("Index");
                    } }
                else
                {
                    return View("../Home/Index");
                }


            }
            else
            {
                return RedirectToAction("Index", "DangNhap");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Xuat()
        {
            var tk = Session["KhachHang"] as KhachHang;
            if (tk == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                if (tk.LoaiKH == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    try
                    {
                        var db = new BookContext();
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 3;
                        double tongTien = 0;
                        if (ngayBatDau == null)
                        {
                            ws.Cells[1, 1] = "Hóa đơn từ ngày " + ngayKetThuc + " trở về";
                        }
                        else { ws.Cells[1, 1] = "Hóa đơn từ ngày " + ngayBatDau + " tới ngày " + ngayKetThuc; }

                        ws.Cells[2, 1] = "Mã Hóa Đơn";
                        ws.Cells[2, 2] = "Ngày tạo";
                        ws.Cells[2, 3] = "Số điện thoại người mua";
                        ws.Cells[2, 4] = "Địa chỉ";
                        ws.Cells[2, 5] = "Thành Tiền";
                        ws.Cells[2, 6] = "Tình Trạng giao hàng";
                        var list = Session["ListHD"] as List<HoaDon>;
                        foreach (var item in list)
                        {
                            ws.Cells[index, 1] = item.MaDonHang;
                            ws.Cells[index, 2] = item.NgayTao;
                            ws.Cells[index, 3] = item.SDT;
                            ws.Cells[index, 4] = item.DiaChi;
                            ws.Cells[index, 5] = item.TongTien;
                            ws.Cells[index, 6] = item.DaThanhToan;
                            tongTien = tongTien + item.TongTien;
                            index++;

                        }
                        ws.Cells[index, 5] = "Tổng Tiền";
                        ws.Cells[index, 6] = tongTien;
                        ws.SaveAs(@path + "/TKHD"+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second + ".xlsx",
                        XlFileFormat.xlWorkbookDefault,
                        Type.Missing,
                        Type.Missing,
                        true, false,
                        XlSaveAsAccessMode.xlNoChange,
                        XlSaveConflictResolution.xlLocalSessionChanges,
                        Type.Missing,
                        Type.Missing);
                        excel.Quit();
                    }
                    catch (Exception ex) { }
                    return View("Index");
                }
            }
        }
        
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult ThanhToan(int id)
        {
            db.HoaDon.Find(id).DaThanhToan = false;
            db.SaveChanges();
            return RedirectToAction("Index", "HoaDon");
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult GiaoHang(int id)
        {
            KhachHang kh1 = new KhachHang();
            kh1 = Session["KhachHang"] as KhachHang;
            using (var db = new BookContext())
            {
                Log log = new Log();
                log.TacVu = "Admin";
                log.ThoiGian = DateTime.Now;
                log.TaiKhoan = kh1.TaiKhoan;
                HoaDon hd = db.HoaDon.Find(id);
                log.HanhDong = "Đã xác nhận tình trạng giao hàng của hóa đơn " + hd.MaDonHang ;
                db.Log.Add(log);
                hd.DaGiaoHang = true;
                db.SaveChanges();
                return RedirectToAction("Index", "HoaDon");
            }
           
           
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Xem(int id)
        {
            if (Session["KhachHang"] != null)
            {
                Session["ListCTHD"] = db.ChiTietHoaDon.Where(i => i.MaDonHang == id).ToList();
                Session["HD"] = db.HoaDon.Find(id);
                return View("CTHD");
            }
            else
            {
                return RedirectToAction("Index", "DangNhap");
            }
        }
    }
}