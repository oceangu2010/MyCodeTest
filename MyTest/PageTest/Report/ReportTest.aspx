<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportTest.aspx.cs" Inherits="MyTest.PageTest.Report.ReportTest" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Width="893px" ShowPrintButton="true" >
        <LocalReport ReportPath="PageTest\Report\Report1.rdlc" 
            EnableExternalImages="True" EnableHyperlinks="True">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="ReportPrintTest" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ASCX12ConnectionString %>" 
           SelectCommand="SELECT * FROM [ConfigElements]"></asp:SqlDataSource>
    </form>
</body>
</html>
