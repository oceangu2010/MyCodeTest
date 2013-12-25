<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jPaginateTest.aspx.cs" Inherits="MyTest.PageTest.Paging.jPaginateTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>无标题页</title>
        <script src="../../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
        <script src="../../Scripts/jPaginate/jquery.paginate.js" type="text/javascript"></script>
        <script src="jPaginate.js" type="text/javascript"> </script>
        <link href="../../Scripts/jPaginate/css/style.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            
            <div class="demo"> 
                <h4>Demo ： asp.net+jQuery(jPaginate插件)+AJAX 分页</h4> 
                <div id="pagetxt" style="width: 98%;"> 
                    <img id="load_gif" src="loading_invs.gif" style="display: none;"/>
                </div> 
                <div id="demo" style="margin: 10px 0px 0px 0px; vertical-align: middle; width: 92%; height:30px;"></div> 
            </div> 
            <input id="Hidden1" type="hidden" />
        </form>
    </body>
</html>
