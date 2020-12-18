using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    public class NoiBatController : Controller
    {
        // GET: NoiBat
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(int? page)
        {
            return View(HomViewModel.GetListNoiBat_XemThem().ToPagedList(page ?? 1, 9));
        }
    }
}