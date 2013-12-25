<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsonUrl.aspx.cs" Inherits="MyTest.PageTest.Json.JsonUrl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        var str1 = '{ "Name": "cxh", "Sex": "man" }';
        var str2 =' { "billid": 72, "totalamountbalance": "466.8", "closedbyuser": "false", "billsource": 3, "closedbyprovider": "false", "accountnumber": "acc#123" }'
        function GoPage()
        {
            var objJson = eval('('+str2+')');
            alert(objJson.totalamountbalance);
           // window.location= "JsonUrl2.aspx?json=" + window.encodeURIComponent(str1);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="Button1" type="button" value="click button" onclick="GoPage();" />
    </div>
    </form>
</body>
</html>
