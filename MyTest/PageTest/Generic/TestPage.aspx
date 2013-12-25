<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="MyTest.PageTest.Extend.date.TestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Extend/date/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="text" id="txtBirthday"  title='Start Date'  style='width:95px;' onclick="WdatePicker();"   />
     <img src='../Image/Calendar.gif' style='vertical-align:bottom;border:none;cursor: pointer' name='ImgCalendar' onclick="WdatePicker({el:'txtBirthday'})">
       &nbsp;<span class="ErrorMessag">*</span>
    </div>
    </form>
</body>
</html>
