using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace benhxaCA.Report
{
    public partial class ReportViewer_TT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }
        private void LoadReport()
        {
            var reportParam = (dynamic)HttpContext.Current.Session["ReportParam"];
            if (reportParam != null && !string.IsNullOrEmpty(reportParam.RptFileName))
            {
                Page.Title = "Báo cáo | " + reportParam.ReportTitle;
                var dt = new DataTable();
                dt = reportParam.DataSource;
                if (dt.Rows.Count > 0)
                {
                    GenerateReportDocument(reportParam, reportParam.ReportType, dt);
                }
                else
                {

                    ShowErrorMessage();
                }
            }

        }
        public void GenerateReportDocument(dynamic reportParam, string reportType, DataTable data)
        {
            string dsName = reportParam.DataSetName;
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(new ReportDataSource(dsName, data));
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/" + "Report//rpt//" + reportParam.RptFileName);
            ReportViewer2.DataBind();
            ReportViewer2.LocalReport.Refresh();
        }
        public void ShowErrorMessage()
        {
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(new ReportDataSource("", new DataTable()));
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/" + "Report//rpt//blank.rdlc");
            ReportViewer2.DataBind();
            ReportViewer2.LocalReport.Refresh();
        }
    }
}