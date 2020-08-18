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
    public class baocaokhamsuckhoeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //Thêm các thành phần báo cáo vào Session
        public JsonResult GetTongHopKSKReport()
        {
            string tn = Request.Form["tungay"];
            string dn = Request.Form["denngay"];
            if(tn!="" && dn != "")
            {
                ReportParams objectReportParams = new ReportParams();
                DataSet data = GetInfo(tn, dn);
                objectReportParams.DataSource = data.Tables[0];
                objectReportParams.ReportTitle = "Báo cáo khám sức khỏe tổng quan";
                objectReportParams.ReportType = "TongHopKSKReport";
                objectReportParams.RptFileName = "TongHopKSKReport.rdlc";
                objectReportParams.DataSetName = "TongHopKSKReport";
                this.HttpContext.Session["ReportParam"] = objectReportParams;
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }else
            {
                return Json("Xin chọn ngày", JsonRequestBehavior.AllowGet);
            }
            
        }
        //Lấy data cho báo cáo đổ vào Dataset
        public DataSet GetInfo(string tn,string dn)
        {
            DatabaseHelper db = new DatabaseHelper();
                db.OpenConnection();
                DataSet ds = new DataSet();
                ds = db.Get_tong_hop_ksk(tn, dn);
                db.CloseConnection();
                return ds;
        }
    }
}