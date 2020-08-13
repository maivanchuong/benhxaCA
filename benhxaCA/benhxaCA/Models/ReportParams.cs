using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace benhxaCA.Models
{
    public class ReportParams
    {
        public string RptFileName { get; set; }
        public string ReportTitle { get; set; }
        public string ReportType { get; set; }
        public DataTable DataSource { get; set; }
        public bool IsHasParam { get; set; }
        public string DataSetName { get; internal set; }
    }
}