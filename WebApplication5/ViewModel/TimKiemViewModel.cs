using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.ViewModel
{
    public static class TimKiemViewModel
    {
        public static List<Sach> GetList(string tensach)
        {
            var db = new BookContext();
            return db.Sach.Where(x => x.TenSach.Contains(tensach) && x.flag==false).ToList();
        }
    }
}