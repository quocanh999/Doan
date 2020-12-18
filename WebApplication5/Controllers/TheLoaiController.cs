using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: TheLoai
        BookContext db = new BookContext();
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            
            var db = new BookContext();
            return View(db.ChuDe.Where(x=>x.flag==false).ToList());
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult KetQua(int? id, int? page)
        {
            if (id == null)
                return HttpNotFound();
            else
            {
                ViewBag.MaSach = id;
                ViewBag.TenTheLoai = db.ChuDe.Find(id).TenChuDe;
                return View(db.Sach.Where(x => x.MaChuDe == id && x.flag==false).OrderByDescending(x => x.NgayCapNhat).ToPagedList(page ?? 1, 6));
            }
        }
    }
}