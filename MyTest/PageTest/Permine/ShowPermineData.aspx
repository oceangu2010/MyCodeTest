<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPermineData.aspx.cs" Inherits="MyTest.MyClassTest.ShowPermineData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
  
     <div id="perimeDiv">
         <div id="perimeControl">
            <span>求一个范围内素数的算法：</span><asp:TextBox ID="txtPerime" runat="server"></asp:TextBox>
            <asp:Button ID="btnPerime" runat="server" Text="求素数" onclick="btnPerime_Click"/>
        </div>
        <div id="showPerime" runat="server"></div>
     </div> 
     

    </div>
    </form>
</body>
</html>
