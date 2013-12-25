<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAjaxLoad.aspx.cs" Inherits="MyTest.AjaxLoding.ShowAjaxLoad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>页面加载前显示加载进度条</title>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
       //此处显示的进度条是假进度条，是利用图片来显示的

        function loading()
        {
            $("#showBody").hide();
            $("#ImgLoading").css("display", "block");
            setTimeout("hideLoading()", 5000);
        }

        function hideLoading()
        {
            $("#showBody").show();
            $("#ImgLoading").css("display", "none");
        }

        $(function ()
        {
            //test(0);
            loading();
        });
    </script>



 <style type="text/css">

     div#ImgLoading{ 
      position:absolute; 
      top: 50%; 
      left: 50%; 
   } 

</style>

</head>
<body >
    <form id="form1" runat="server">
        
     <div id="showBody">
          <div id="forspider"></div>
          <div id="loadingbox"><div id="progressbar"></div></div>   
          <div id="main"></div>    
     </div> 
    

     <div id="ImgLoading" style="display: none"><img src="../images/progressbar.gif" alt=""/></div>
    </form>

</body>

   
</html>
