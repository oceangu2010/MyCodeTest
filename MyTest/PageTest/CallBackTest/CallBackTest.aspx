<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallBackTest.aspx.cs" Inherits="MyTest.MyClassTest.CallBackTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="委托测试" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Button ID="btnArray" runat="server" Text="数组测试" onclick="btnArray_Click" />
        <br />
        <br />
        <asp:Button ID="btnRevetStr" runat="server" Text="字符串倒置" 
            onclick="btnRevetStr_Click" />
        <asp:TextBox ID="txtInputStr" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnRecursive" runat="server" Text="递归测试" 
            onclick="btnRecursive_Click" />
    </div>
    </form>
</body>
</html>
