<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongHopKSKReport.aspx.cs" Inherits="benhxaCA.Report.TongHopKSKReport1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager3" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer3" runat="server" Width="60%" Height="1000px" style="margin-left:250px; margin-top:10px;"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
