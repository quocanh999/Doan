using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ChuDe
    {
        [Key]
        public int MaChuDe { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public string TenChuDe { get; set; }
        public bool flag { get; set; }
        public ICollection<Sach> Sach { get; set; }
    }
}