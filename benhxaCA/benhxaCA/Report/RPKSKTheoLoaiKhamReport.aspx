<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPKSKTheoLoaiKhamReport.aspx.cs" Inherits="benhxaCA.Report.RPKSKTheoLoaiKhamReport1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager6" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer6" runat="server" Width="60%" Height="1000px" style="margin-left:250px; margin-top:10px;"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
