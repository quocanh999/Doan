using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeTacGiaController : Controller
    {
        // GET: HomeTacGia
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            var db = new BookContext();
            return View(db.TacGia.Where(x => x.flag == false).ToList());
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult KetQua(int? id, int? page)
        {
            if (id == null)
                return HttpNotFound();
            else
            {
                var db = new BookContext();
                TacGia tg = db.TacGia.Find(id);
                ViewBag.MaTacGia = id;
                ViewBag.TenTacGia = tg.TenTacGia;
                return View(db.Sach.Where(x => x.MaTacGia == id && x.flag == false).OrderByDescending(x => x.NgayCapNhat).ToPagedList(page ?? 1, 6));
            }
        }
    }
}