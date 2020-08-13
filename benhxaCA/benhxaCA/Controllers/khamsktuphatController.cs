using benhxaCA.Database;
using benhxaCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace benhxaCA.Controllers
{
    public class khamsktuphatController : Controller
    {
        // GET: khamsktuphat
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult danhsachcanbo()
        {
            string nk = Request.Form["ngay"];
            string status = Request.Form["status"];
            DatabaseHelper db = new DatabaseHelper();
            List<thongtincanbo> dscb = new List<thongtincanbo>();
            if (status == "3")
            {
                dscb = db.Get_dscb_dakham_tuphat(nk);
            }
            else if (status == "1" && nk != null)
            {
                dscb = db.Get_dscb_chokham_tuphat(nk);
            }
            if (dscb.Count == 0)
            {
                return Json("Không có dữ liệu", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(dscb, JsonRequestBehavior.AllowGet);
            }
        }
    }
}