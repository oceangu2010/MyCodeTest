<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageCache.aspx.cs" Inherits="MyTest.CacheTest.PageCache" %>
<%@ OutputCache Duration="1000"  VaryByParam="id;langid"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0 auto;">
    <form id="form1" runat="server">
    <div style="text-align: center; vertical-align: middle">
        
    <div  style="text-align: center; vertical-align: middle"> 

       <a href="PageCache.aspx">No Query String</a> |  

       <a href="PageCache.aspx?id=1">ID 1</a> |  

       <a href="PageCache.aspx?id=2">ID 2</a> |  

       <a href="PageCache.aspx?id=3">ID 3</a> | 

       <a href="PageCache.aspx?id=3&langid=1">ID 3</a> ｜

        <a href="PageCache.aspx?id=4">缓存失效</a> 

    </div>
    
    <div id="cacheDiv" runat="server" style="text-align: center; vertical-align: middle">默认显示页面</div>

    </div>
    </form>
</body>
</html>
