using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public string TaiKhoan { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public string MatKhau { get; set; }
        public string LoaiKH { get; set; }

        public bool flag { get; set; }
        public ICollection<HoaDon> HoaDon { get; set; }
    }
}