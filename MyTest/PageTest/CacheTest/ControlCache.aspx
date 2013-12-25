<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlCache.aspx.cs" Inherits="MyTest.MyClassTest.ControlCache" %>
<%@ OutputCache Duration="50" VaryByControl="btnCache" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>缓存控件</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblCache" runat="server"></asp:Label>
    <br/>
    <asp:Button ID="btnCache" runat="server" Text="缓存控件的值" onclick="btnCache_Click" />
    <br />
    <asp:Button ID="btnNoCache" runat="server" Text="不缓存控件的值" onclick="btnNoCache_Click" />
    </div>
    </form>
</body>
</html>
