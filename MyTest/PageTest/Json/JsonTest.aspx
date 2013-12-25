<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsonTest.aspx.cs" Inherits="MyTest.PageTest.Json.JsonTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
        <asp:Button ID="btnRadomNumber" runat="server" Text="数字随机码" 
            onclick="btnRadomNumber_Click" />
        <br /> <br /> <br />
        <asp:TextBox ID="txtNumberLetter" runat="server"></asp:TextBox>
        <asp:Button ID="btnLetter" runat="server" Text="数字母随机码" 
            onclick="btnLetter_Click" />

         <br /> <br /> <br />
        <asp:TextBox ID="txtNew" runat="server"></asp:TextBox>
        <asp:Button ID="btnNew" runat="server" Text="新规则随机码" 
            onclick="btnNew_Click" />
    </div>
    </form>
</body>
</html>
