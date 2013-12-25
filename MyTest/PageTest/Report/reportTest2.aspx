<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportTest2.aspx.cs" Inherits="MyTest.PageTest.Report.reportTest2" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="727px">
    </rsweb:ReportViewer>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="btnPrint" runat="server" Text="ReportPrint"  onclick="btnPrint_Click" />
    <br />
    <br />

    <input id="btnJSPrint" type="button" value="button" onclick="javascript:window.print();"/></form>
</body>
</html>
