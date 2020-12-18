using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication5
{
    public class BookContext:DbContext
    {
        public BookContext() : base()
        {
            string dbName = "BookStore";
            this.Database.Connection.ConnectionString = "Data Source=DESKTOP-74IKDSE\\;Initial Catalog=" + dbName + ";Trusted_Connection=Yes";
        }
        public DbSet<Models.ChuDe> ChuDe { get; set; }
        public DbSet<Models.NhaXuatBan> NhaXuaBan { get; set; }
        public DbSet<Models.TacGia> TacGia { get; set; }
        public DbSet<Models.KhachHang> KhachHang { get; set; }
        public DbSet<Models.Sach> Sach { get; set; }
        public DbSet<Models.HoaDon> HoaDon { get; set; }
        public DbSet<Models.ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<Models.Log> Log { get; set; }
    }
}