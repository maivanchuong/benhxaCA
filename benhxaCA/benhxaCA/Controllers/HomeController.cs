using benhxaCA.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace benhxaCA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //DatabaseHelper db = new DatabaseHelper();
            //db.OpenConnection();
            //db.Get_donvi();
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
    }
}