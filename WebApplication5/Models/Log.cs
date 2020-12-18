using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Log
    {
        [Key]
        public int MaLog { get; set; }
        public DateTime ThoiGian { get; set; }
        public string HanhDong { get; set; }
        public string TaiKhoan { get; set; }
        public string TacVu { get; set; }
        public bool flag { get; set; }

    }
}