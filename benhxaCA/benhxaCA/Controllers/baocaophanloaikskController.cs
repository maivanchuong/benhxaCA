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
    public class baocaophanloaikskController : Controller
    {
        // GET: baocaotinhhinhksktheoicd
        public ActionResult Index()
        {
            return View();
        }
        //Khởi tạo các tham số cho báo cáo và lưu vào Model Session ReportParams
        public JsonResult GetPLKSKReport()
        {
            //Lấy dữ liệu được gửi từ ajax sang
            string tn = Request.Form["tungay"];
            string dn = Request.Form["denngay"];
            if (tn != "" && dn != "")
            {
                ReportParams objectReportParams = new ReportParams();
                DataSet data = GetInfo(tn, dn);
                objectReportParams.DataSource = data.Tables[0];
                objectReportParams.ReportTitle = "Báo cáo phân loại khám sức khỏe";
                objectReportParams.ReportType = "PhanLoaiKSKReport";
                objectReportParams.RptFileName = "PhanLoaiKSKReport.rdlc";
                objectReportParams.DataSetName = "PhanLoaiKSKReport";
                this.HttpContext.Session["ReportParam"] = objectReportParams;
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Xin chọn ngày", JsonRequestBehavior.AllowGet);
            }

        }
        //Lấy data cho báo cáo đổ vào Dataset
        public DataSet GetInfo(string tn, string dn)
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            DataSet ds = new DataSet();
            ds = db.Get_tonghopphanloai_ksk(tn, dn);
            db.CloseConnection();
            return ds;
        }
    }
    
}