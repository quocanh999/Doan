using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.ViewModel
{
    public static class NoiBat
    {
        static BookContext db;
        public static List<Sach> GetListNoiBat()
        {
            db = new BookContext();
            var listNoiBat = db.ChiTietHoaDon.ToList();
            var rs = new List<Sach>();
            foreach (var item in db.Sach.Where(i=>i.flag==false).ToList())
            {
                
                int soLuong = 0;
                foreach (var item1 in listNoiBat.Where(i => i.MaSach == item.MaSach).ToList())
                {
                    soLuong = soLuong + item1.SoLuong;
                }
                if (soLuong > 3)
                {
                    rs.Add(db.Sach.Find(item.MaSach));
                }
            }
            return rs;
            //foreach (var item in listNoiBat)
            //{
            //    rs.Add(db.Sach.Find(item.MaSach));
            //}
            //return rs;
        }
    }
}