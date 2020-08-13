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
    public class baocaoksktheoloaikhamController : Controller
    {
        // GET: baocaoksktheoloaikham
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRPKSKTheoLoaiKhamReport()
        {
            string tn = Request.Form["tungay"];
            string dn = Request.Form["denngay"];
            if (tn != "" && dn != "")
            {
                ReportParams objectReportParams = new ReportParams();
                DataSet data = GetInfo(tn, dn);
                objectReportParams.DataSource = data.Tables[0];
                objectReportParams.ReportTitle = "Báo cáo khám sức khỏe theo loại khám";
                objectReportParams.ReportType = "RPKSKTheoLoaiKhamReport";
                objectReportParams.RptFileName = "RPKSKTheoLoaiKhamReport.rdlc";
                objectReportParams.DataSetName = "RPKSKTheoLoaiKhamReport";
                this.HttpContext.Session["ReportParam"] = objectReportParams;
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Xin chọn ngày", JsonRequestBehavior.AllowGet);
            }

        }
        public DataSet GetInfo(string tn, string dn)
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            DataSet ds = new DataSet();
            ds = db.Get_baocaoksk_theoloaikham(tn, dn);
            db.CloseConnection();
            return ds;
        }
    }
}