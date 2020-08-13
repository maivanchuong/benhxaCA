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
    public class baocaoksktheodonviController : Controller
    {
        // GET: baocaoksktheodonvi
       
        public ActionResult Index()
        {
            //Lấy danh sách tên đơn vị, gửi sang view
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            List<string> tendonvi = new List<string>();
            tendonvi = db.Get_donvi();
            db.CloseConnection();
            return View(tendonvi);
        }
            //Khởi tạo các tham số cho báo cáo
        public JsonResult GetRPKSKTheoDonViReport()
        {
            DatabaseHelper db = new DatabaseHelper();
            //Lấy dữ liệu được gửi từ ajax sang
            string tn = Request.Form["tungay"];
            string dn = Request.Form["denngay"];
            string tendv = Request.Form["dvk"];
            //Lấy mã đơn vị từ tên đơn vị
            string dv = db.Get_madonvi(tendv);
            if (tn != "" && dn != "")
            {
                ReportParams objectReportParams = new ReportParams();
                DataSet data = GetInfo(tn, dn, dv);
                objectReportParams.DataSource = data.Tables[0];
                objectReportParams.ReportTitle = "Báo cáo khám sức khỏe chi tiết theo đơn vị";
                objectReportParams.ReportType = "RPKSKTheoDonViReport";
                objectReportParams.RptFileName = "RPKSKTheoDonViReport.rdlc";
                objectReportParams.DataSetName = "RPKSKTheoDonViReport";
                this.HttpContext.Session["ReportParam"] = objectReportParams;
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Xin chọn ngày", JsonRequestBehavior.AllowGet);
            }

        }
        //Lấy data 
        public DataSet GetInfo(string tn, string dn,string dv)
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            DataSet ds = new DataSet();
            ds = db.Get_baocaoksk_theodonvi(tn, dn, dv);
            db.CloseConnection();
            return ds;
        }
    }
}