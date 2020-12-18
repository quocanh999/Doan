using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaCTDH { get; set; }
        public int MaSach { get; set; }
        public int MaDonHang { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
        public bool flag { get; set; }
        public Sach Sach { get; set; }
        public HoaDon HoaDon { get; set; }
    }
}