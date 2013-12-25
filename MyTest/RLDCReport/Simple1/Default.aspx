<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>

        <div>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="GridView:"></asp:Label>&nbsp;
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px">
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField HeaderText="网址">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("website_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="访问量">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/MyHandler.jxd?data=" + Eval("visitor_traffic") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
            <br />
            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="RDLC 报表："></asp:Label>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" >
            </rsweb:ReportViewer>

        </div>
    </form>
</body>
</html>
