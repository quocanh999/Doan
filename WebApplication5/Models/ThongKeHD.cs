using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ThongKeHD
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double TongTien { get; set; }
        public int SoLuongBill { get; set; }
    }
}