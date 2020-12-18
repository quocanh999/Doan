using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TacGia
    {
        [Key]
        public int MaTacGia { get; set; }
        [Required(ErrorMessage = "*Không được để trống")]
        public string TenTacGia { get; set; }
        public bool flag { get; set; }
        public ICollection<Sach> Sach { get; set; }
    }
}