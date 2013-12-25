<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyLinqTest.aspx.cs" Inherits="MyTest.MyClassTest.MyLinqTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>

        <br />
        <asp:Button ID="btnAddPageNum" Text="增加分页码" runat="server" 
            onclick="btnAddPageNum_Click"/>
    </div>
    </form>
</body>
</html>
