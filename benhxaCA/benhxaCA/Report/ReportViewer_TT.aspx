﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer_TT.aspx.cs" Inherits="benhxaCA.Report.ReportViewer_TT" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="60%" Height="1000px" style="margin-left:250px; margin-top:10px;"></rsweb:ReportViewer>

    </div>
    </form>
</body>
</html>
