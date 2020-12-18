using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class HoaDon
    {
        [Key]
        public int MaDonHang { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaThanhToan { get; set; }
        public int MaKH { get; set; }
        public double TongTien { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public bool flag { get; set; }
        public bool DaGiaoHang { get; set; }    
        public ICollection<ChiTietHoaDon> ChiTietDonHang { get; set; }
        public KhachHang KhachHang { get; set; }
    }
}