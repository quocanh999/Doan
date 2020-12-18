using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.ViewModel
{
    public static class ChiTietSach
    {
        public static string GetTenTacGia(int id)
        {
            var db = new BookContext();
            return db.TacGia.Find(id).TenTacGia.ToString();
        }
        public static string GetTenNXB(int id)
        {
            var db = new BookContext();
            return db.NhaXuaBan.Find(id).TenNXB.ToString();
        }
    }
}