using benhxaCA.Database;
using benhxaCA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace benhxaCA.Controllers
{
    public class khamsuckhoetheodoanController : Controller
    {
        // GET: khamsuckhoetheodoan
        public ActionResult Index()
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            List<string> tendonvi = new List<string>();
            tendonvi = db.Get_donvi();
            db.CloseConnection();
            return View(tendonvi);
        }
       // [HttpPost]
        public JsonResult _danhsachcanbo()
        {
            string dvk = Request.Form["dvkham"];
            string nk = Request.Form["ngay"];
            string status = Request.Form["status"];
            DatabaseHelper db = new DatabaseHelper();
            List<thongtincanbo> dscb = new List<thongtincanbo>();
            if(status == "3")
            {
                dscb = db.Get_dscb_dakham(nk, dvk);
            }else if(status == "1" && nk !=null)
            {
                dscb = db.Get_dscb_chokham(nk, dvk);
            }
            if (dscb.Count==0)
            {
                return Json("Không có dữ liệu", JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(dscb, JsonRequestBehavior.AllowGet);
            }
            

        }
        public JsonResult GetKhamBenhReport()
        {
            string macb = Request.Form["macb"];
            if (macb != "")
            {
                ReportParams objectReportParams = new ReportParams();
                DataSet data = GetInfo(macb);
                objectReportParams.DataSource = data.Tables[0];
                objectReportParams.ReportTitle = "Báo cáo khám bệnh";
                objectReportParams.ReportType = "KhamBenhReport";
                objectReportParams.RptFileName = "KhamBenhReport.rdlc";
                objectReportParams.DataSetName = "KhamBenhReport";
                this.HttpContext.Session["ReportParam"] = objectReportParams;
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }else
            {
                return Json("Không có dữ liệu", JsonRequestBehavior.AllowGet); ;
            }
        }
        public DataSet GetInfo(string macb)
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            DataSet ds = new DataSet();
            ds=db.Get_tong_hop(macb);
            db.CloseConnection();
            return ds;
        }
        public JsonResult GetThongTinReport()
        {
            string macb = Request.Form["macb"];
            if(macb != "")
            {
                ReportParams objectReportParams = new ReportParams();
                DataSet data = GetInfoTT(macb);
                objectReportParams.DataSource = data.Tables[0];
                objectReportParams.ReportTitle = "Thông tin cán bộ";
                objectReportParams.ReportType = "ThongTinReport";
                objectReportParams.RptFileName = "ThongTinReport.rdlc";
                objectReportParams.DataSetName = "ThongTinReport";
                this.HttpContext.Session["ReportParam"] = objectReportParams;
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Không có dữ liệu", JsonRequestBehavior.AllowGet);
            }
          
        }
        public DataSet GetInfoTT(string macb)
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            DataSet ds = new DataSet();
            ds = db.Get_thong_tin_cb(macb);
            db.CloseConnection();
            return ds;
        }
    }
}