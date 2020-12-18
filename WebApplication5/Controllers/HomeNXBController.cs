using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeNXBController : Controller
    {
        // GET: HomeNXB
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            var db = new BookContext();
            return View(db.NhaXuaBan.Where(x => x.flag == false).ToList());
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult KetQua(int? id, int? page)
        {
            if (id == null)
                return HttpNotFound();
            else
            {
                var db = new BookContext();
                NhaXuatBan nxb = db.NhaXuaBan.Find(id);
                ViewBag.MaNXB = id;
                ViewBag.TenNXB = nxb.TenNXB;
                return View(db.Sach.Where(x => x.MaNXB == id && x.flag == false).OrderByDescending(x => x.NgayCapNhat).ToPagedList(page ?? 1, 6));
            }
        }
    }
}