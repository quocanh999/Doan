using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Sach
    {
        [Key]
        public int MaSach { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public string TenSach { get; set; }
        public string MoTa { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public DateTime NgayCapNhat { get; set; }
        public string AnhBia { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        [Range(0,100000000000000,ErrorMessage ="*Không được là số âm")]
        public int SoLuongTon { get; set; }
        public int MaChuDe { get; set; }
        public int MaNXB { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        [Range(1, 100000000000000, ErrorMessage = "*Không được là số âm")]
        public double GiaBan { get; set; }
        public int MaTacGia { get; set; }
        public bool flag { get; set; }
        public NhaXuatBan NhaXuatBan { get; set; }
        public ChuDe Chude { get; set; }
        public TacGia TacGia { get; set; }
        public int LuotXem { get; set; }
    }
}