using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class ThuatToanController : Controller
    {
        // GET: ThuatToan
        public ActionResult Index()
        {
            string xuat = "";
            ViewBag.xuat = "Xin chao";
            return View();
        }
    }
}