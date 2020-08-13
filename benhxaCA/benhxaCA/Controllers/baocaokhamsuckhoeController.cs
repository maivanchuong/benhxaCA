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
        // GET: baocaokhamsuckhoe
        public ActionResult Index()
        {
            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            List<string> tendonvi = new List<string>();
            tendonvi = db.Get_donvi();
            db.CloseConnection();
            return View(tendonvi);
        }
        public JsonResult hienthibaocao()
        {
            string tn = Request.Form["tungay"];
            string dn = Request.Form["denngay"];
            string dv = Request.Form["donvi"];
            string loai = Request.Form["loaiksk"];
            DatabaseHelper db = new DatabaseHelper();
            string ma = db.Get_madonvi(dv);
            List<tinhhinhksk> ksk = new List<tinhhinhksk>();
            if(tn != "" && dn != "")
            {
                if (loai == "1" && ma == "")
                {
                    ksk = db.Get_tinhhinhksk_dinhky_all(tn, dn);
                }
                else if (loai == "2" && ma == "")
                {
                    ksk = db.Get_tinhhinhksk_tuphat_all(tn, dn);
                }
                else if (loai == "1" && dv != "0" && ma != "")
                {
                    ksk = db.Get_tinhhinhksk_dinhky_by_donvi(tn, dn, ma);
                }
                else if (loai == "2" && dv != "0" && ma != "")
                {
                    ksk = db.Get_tinhhinhksk_tuphat_by_donvi(tn, dn, ma);
                }
                else if (loai == "0" && ma == "")
                {
                    ksk = db.Get_tinhhinhksk_all(tn, dn);
                }
                else if (loai == "0" && ma != "")
                {
                    ksk = db.Get_tinhhinhksk_all_bydonvi(tn, dn, ma);
                }
                return Json(ksk, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json("Xin chọn ngày", JsonRequestBehavior.AllowGet);
            }
            

        }
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