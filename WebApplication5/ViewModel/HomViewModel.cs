using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.ViewModel
{
    public static class HomViewModel
    {
        static BookContext db = new BookContext();
        public static List<SachBanChay> GetListNoiBat()
        {
            List<SachBanChay> listBanChay = new List<SachBanChay>();
            foreach (var item in db.Sach.Where(i => i.flag == false).ToList())
            {
                SachBanChay sbc = new SachBanChay();
                sbc.MaSach = item.MaSach;
                sbc.SoLuong = 0;
                foreach (var item1 in db.ChiTietHoaDon.Where(i=>i.flag==true).ToList())
                {
                    if (item1.MaSach == sbc.MaSach)
                    {
                        sbc.SoLuong=sbc.SoLuong+item1.SoLuong;
                    }
                }
                if (sbc.SoLuong > 0)
                {
                    listBanChay.Add(sbc);
                }
            }

            listBanChay = listBanChay.OrderByDescending(i => i.SoLuong).Take(6).ToList();
            return listBanChay;

        }
        public static List<Sach> GetListNoiBat_XemThem()
        {
            List<SachBanChay> listBanChay = new List<SachBanChay>();
            foreach (var item in db.Sach.Where(i => i.flag == false).ToList())
            {
                SachBanChay sbc = new SachBanChay();
                sbc.MaSach = item.MaSach;
                sbc.SoLuong = 0;
                foreach (var item1 in db.ChiTietHoaDon.Where(i => i.flag == true).ToList())
                {
                    if (item1.MaSach == sbc.MaSach)
                    {
                        sbc.SoLuong = sbc.SoLuong + item1.SoLuong;
                    }
                }
                if (sbc.SoLuong > 0)
                {
                    listBanChay.Add(sbc);
                }
            }
            List<Sach> listSach = new List<Sach>();
            listBanChay = listBanChay.OrderByDescending(i => i.SoLuong).ToList();
            foreach (var item in listBanChay)
            {
                listSach.Add(db.Sach.Find(item.MaSach));
            }
            return listSach;

        }
        public static List<Models.Sach> GetListMoiPhatHanh()
        {
            var db = new BookContext();
            return db.Sach.Where(x => x.flag==false).OrderByDescending(i=>i.NgayCapNhat).Take(6).ToList();
        }
        public static List<Models.Sach> GetListMoiPhatHanh_XemThem()
        {
            var db = new BookContext();
            return db.Sach.Where(x => x.flag == false).OrderByDescending(i => i.NgayCapNhat).ToList();
        }
   
        public static List<Sach> GetListXemNhieu()
        {
            List<Sach> list = new List<Sach>();
            foreach (var item in db.Log.Where(i=>i.TacVu=="Log"&&i.HanhDong!="0").OrderByDescending(i=>i.HanhDong).ToList())
            {
                list.Add(db.Sach.Find(Convert.ToInt32(item.TaiKhoan)));
            }
            list = list.Take(6).ToList();
            return list;
        }
        public static List<Sach> GetListNoiBat(int maSach)
        {
            List<Tuple<Sach, int>> rank = new List<Tuple<Sach, int>>();
            foreach (var item in db.Sach.Where(i=>i.flag==false).ToList())
            {
                rank.Add(new Tuple<Sach, int>(item, db.ChiTietHoaDon.Where(x => x.MaSach == item.MaSach).Count()));
            }
            var top6 = rank.OrderByDescending(x => x.Item2).Take(6).ToList();
            var rs = new List<Sach>();
            foreach (var item in top6)
            {
                rs.Add(item.Item1);
            }
            return rs;
        }
        public static List<Sach> GetSachFromTacGia(string name)
        {
            var db = new BookContext();
            return db.Sach.Where(x => x.NgayCapNhat <= DateTime.Now && x.flag == false).OrderBy(x => x.NgayCapNhat).ToList();
        }
    }
}