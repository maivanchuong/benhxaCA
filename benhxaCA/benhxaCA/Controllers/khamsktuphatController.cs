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
    public class khamsktuphatController : Controller
    {
        // GET: khamsktuphat
        public ActionResult Index()
        {
            return View();
        }
        //Khởi tạo các tham số cho báo cáo và lưu vào Model Session ReportParams
        public JsonResult danhsachcanbo()
        {
            //Lấy dữ liệu được gửi từ ajax sang
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
        //Khởi tạo các tham số cho báo cáo và lưu vào Model Session ReportParams
        public JsonResult GetKhamBenhReport()
        {
            string macb = Request.Form["macb"];
            string ngay = Request.Form["ngay"];
            if (macb != "")
            {
                ReportParams objectReportParams = new ReportParams();
                DataSet data = GetInfo(macb,ngay);
                objectReportParams.DataSource = data.Tables[0];
                objectReportParams.ReportTitle = "Phiếu khám sức khỏe";
                objectReportParams.ReportType = "KhamBenhReport";
                objectReportParams.RptFileName = "KhamBenhReport.rdlc";
                objectReportParams.DataSetName = "KhamBenhReport";
                this.HttpContext.Session["ReportParam"] = objectReportParams;
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Không có dữ liệu", JsonRequestBehavior.AllowGet); ;
            }
        }
        //Lấy data cho báo cáo đổ vào Dataset
        public DataSet GetInfo(string macb,string ngaykham)
        {
            string loaikham = "Khám sức khỏe tự phát";
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            DataSet ds = new DataSet();
            ds = db.Get_tong_hop(macb,loaikham,ngaykham);
            db.CloseConnection();
            return ds;
        }
        //Khởi tạo các tham số cho báo cáo và lưu vào Model Session ReportParams
        public JsonResult GetThongTinReport()
        {
            string macb = Request.Form["macb"];
            if (macb != "")
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
        //Lấy data cho báo cáo đổ vào Dataset
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