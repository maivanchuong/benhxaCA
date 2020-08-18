using benhxaCA.Database;
using benhxaCA.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace benhxaCA.Controllers
{
    public class themdoankskController : Controller
    {
        // GET: themdoanksk
        //Lấy danh sách tên các đơn vị và chuyển sang View
        public ActionResult Index()
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            List<string> tendonvi = new List<string>();
            tendonvi = db.Get_donvi();
            db.CloseConnection();
            return View(tendonvi);
        }
        //Thực hiện thêm đợt khám sức khỏe
        [HttpPost]
        public ActionResult themdotkham()
        {
            //Lấy dữ liệu được gửi từ ajax sang
            string dvk = Request.Form["dvkham"];
            string nk = Request.Form["ngay"];
            string loai = Request.Form["loaiksk"];
            string ghichu = Request.Form["note"];
            string tencb = Request.Form["tencanbo"];
            DatabaseHelper db = new DatabaseHelper();
            if (loai == "Khám sức khỏe định kỳ" && nk != "")
            {
                db.Insert_dotkhamsuckhoe_dinhky(dvk, nk, loai, ghichu);
                return Json("Thêm thành công", JsonRequestBehavior.AllowGet);
            }
            else if (tencb != "" && loai == "Khám sức khỏe tự phát" && nk != "")
            {
                string macb = db.Get_macb(tencb);
                db.Insert_dotkhamsuckhoe_tuphat(dvk, nk, loai, macb, ghichu);
                return Json("Thêm thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Thêm không thành công", JsonRequestBehavior.AllowGet);
            }


        }
        //Hiển thị ra danh sách đơn khám và gửi sang PartiView
        public ActionResult danhsachdotkham()
        {
            DatabaseHelper db = new DatabaseHelper();
            db.Get_dotkhamsuckhoe();
            List<dotkhamsuckhoe> dksk = new List<dotkhamsuckhoe>();
            dksk = db.Get_dotkhamsuckhoe();
            return PartialView("_danhsachdotkham", dksk);
        }
        //Lấy danh sách cán bộ dựa vào 2 tham số là đơn vị và loại khám sức khỏe là "Khám sức khỏe tự phát"
        public JsonResult get_canbo()
        {
            //Lấy dữ liệu được gửi từ ajax sang
            string dvk = Request.Form["dvkham"];
            string loai = Request.Form["loaiksk"];
            DatabaseHelper db = new DatabaseHelper();
            List<thongtincanbo> ttcb = new List<thongtincanbo>();
            string madv = db.Get_madonvi(dvk);
            if (loai == "Khám sức khỏe tự phát")
            {
                ttcb = db.get_canbo(madv);
                return Json(ttcb, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Không có dữ liệu", JsonRequestBehavior.AllowGet);
            }
        }
    }

}