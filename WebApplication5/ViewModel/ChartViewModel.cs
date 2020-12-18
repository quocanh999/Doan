using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;
using WebApplication5.scripts;

namespace WebApplication5.ViewModel
{
    public class ChartViewModel
    {
        //Sach nhap ve
        public static List<int> Months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public static List<int> GetSaches()
        {
            var db = new BookContext();
            List<int> saches = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                int sl = db.Sach.Where(x => x.NgayCapNhat.Month == i && x.NgayCapNhat.Year == DateTime.Now.Year).Count();
                saches.Add(sl);
            }
            return saches;
        }
        
        //Doanh thu
        public static List<double> GetDoanhThu()
        {
            var db = new BookContext();
            List<double> dt = new List<double>();
            for (int i = 1; i < 13; i++)
            {
                var sl = db.HoaDon.Where(x => x.NgayTao.Month == i && x.NgayTao.Year == DateTime.Now.Year).ToList();
                double tongTien = 0;
                foreach (var item in sl)
                {
                    tongTien += item.TongTien;
                }
                dt.Add(tongTien);
            }
            return dt;
        }

        //Sach noi bat
        public static List<string> Saches()
        {
            var list = GetListNoiBat();
            List<string> rs = new List<string>();
            foreach (var item in GetListNoiBat())
            {
                rs.Add(StringExtension.opimizeName(item.TenSach, 17));
            }
            return rs;
        }
        public static List<int> SoLuong()
        {
            var db = new BookContext();
            List<int> soLuong = new List<int>();
            foreach (var item in GetListNoiBat())
            {
                var cthd = db.ChiTietHoaDon.Where(x => x.MaSach == item.MaSach && x.flag == true);
                int tmp = 0;
                foreach (var item2 in cthd)
                {
                    tmp += item2.SoLuong;
                }
                soLuong.Add(tmp);
            }
            return soLuong;
        }
        static List<Models.Sach> GetListNoiBat()
        {
            var db = new BookContext();

            //lấy hết sách chưa xoá
            List<Sach> listSach = db.Sach.Where(x => x.flag == false).ToList();

            //list sách kết quả trả về
            List<Sach> listSachResult = new List<Sach>();

            //Ma trận 2 chiều để lấy sách và số lượng sách trong bill
            //Có dạng (ví dụ):
            //      0   1   2   3   4   5   6   7   8 
            //  0   s0  s1  s2  s3  s4  s5  s6  s7  s8 (MaSach)
            //  1   12  8   56  22  1   20  3   9   10  (SoLuong sách trong bill)
            int[,] saches = new int[listSach.Count, 2];
            var listCTHD = db.ChiTietHoaDon.Where(x => x.flag == false).ToList();

            //lấy mã sách bỏ vào hàng MaSach
            //cho SoLuong ban đầu là 0
            //Có dạng:
            //      0   1   2   3   4   5   6   7   8 
            //  0   s0  s1  s2  s3  s4  s5  s6  s7  s8 (MaSach)
            //  1   0   0   0   0   0   0   0   0   0  (SoLuong sách trong bill)
            int i = 0;
            foreach (var item in listSach)
            {
                saches[i, 0] = item.MaSach;
                saches[i, 1] = 0;
                i += 1;
            }

            //duyệt chi tiết hoá đơn để lấy số lượng
            //Có dạng (ví dụ):
            //      0   1   2   3   4   5   6   7   8 
            //  0   s0  s1  s2  s3  s4  s5  s6  s7  s8 (MaSach)
            //  1   12  8   56  22  1   20  3   9   10  (SoLuong sách trong bill)
            if (listCTHD.Count() != 0)
            {
                for (int j = 0; j < listSach.Count; j++)
                {
                    foreach (var item in listCTHD)
                    {
                        if (item.MaSach == saches[j, 0])
                            saches[j, 1] = saches[j,1]+ 1;

                    }
                }
            }

            //sắp xếp số lại mảng theo chiều giảm dần của số lượng
            //Có dạng (ví dụ):
            //      0   1   2   3   4   5   6   7   8 
            //  0   s0  s1  s2  s3  s4  s5  s6  s7  s8 (MaSach)
            //  1   33  28  26  22  19  15  9   5   1  (SoLuong sách trong bill)
            var a = saches;
            for (int j = 0; j < saches.GetLength(0) - 1; j++)
            {
                if (saches[j, 1] < saches[j + 1, 1])
                {
                    //đảo MaSach
                    saches[j, 0] += saches[j + 1, 0];
                    saches[j + 1, 0] = saches[j, 0] - saches[j + 1, 0];
                    saches[j, 0] = saches[j, 0] - saches[j + 1, 0];
                    //đảo SoLuong
                    saches[j, 1] += saches[j + 1, 1];
                    saches[j + 1, 1] = saches[j, 1] - saches[j + 1, 1];
                    saches[j, 1] = saches[j, 0] - saches[j + 1, 1];
                }
            }
            for (int j = 0; j < saches.GetLength(0); j++)
            {
                listSachResult.Add(db.Sach.Find(saches[j, 0]));
            }

            return listSachResult;
        }

        //Tac gia noi bat
        public static List<SachBanChay> GetTacGias()
        {
            var db = new BookContext();
            var listTacGia = new List<SachBanChay>();
            foreach (var item in db.TacGia.Where(x=>x.flag==false).ToList())
            {
                SachBanChay s = new SachBanChay();
                s.MaSach = item.MaTacGia;
                s.SoLuong = 0;
                foreach (var item2 in db.ChiTietHoaDon.Where(x=>x.flag==true).ToList())
                {
                    if (db.Sach.Find(item2.MaSach).MaTacGia == item.MaTacGia)
                        s.SoLuong += item2.SoLuong;
                }
                if (s.SoLuong > 0)
                    listTacGia.Add(s);
            }
            return listTacGia.OrderByDescending(x => x.SoLuong).Take(6).ToList();
        }
        public static List<string> TenTacGias()
        {
            var db= new BookContext();
            List<string> rs = new List<string>();
            foreach (var item in GetTacGias())
            {
                rs.Add(db.TacGia.Find(item.MaSach).TenTacGia);
            }
            return rs;
        }
        public static List<int> SoLuongSach_tacGia()
        {
            var db = new BookContext();
            List<int> rs = new List<int>();
            foreach (var item in GetTacGias())
            {
                rs.Add(item.SoLuong);
            }
            return rs;
        }
        //NXB noi bat
    }
}