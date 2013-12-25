                                            <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySortTest.aspx.cs" Inherits="MyTest.MyClassTest.MySortTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据结构算法测试页面</title>
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var str = "1212333123434554455445656";

        function AlertResult(str) 
        {
           // alert(str.replace(/\B(?=(?:\d{3})+$)/g, ','));
            var ret = [];

        }


        function TestUnshifty() {
            var arr = new Array();
            arr[0] = "George";
            arr[1] = "John";
            arr[2] = "Thomas";
            arr.unshift("aaa");

            var str = "";
            for (var i = 0; i < arr.length; i++) 
            {
                str += arr[i]+";";
            }

            alert(str);
        }

        function Test2(str) {

            var ret = "";
                for (var i = 0, n = str.length; i < n; i++) {
                    ret += str.charAt(i);
                    if (i % 3 === 0) {
                        ret += ",";
                    }
                }
            alert(ret);

        }

        $(function () {
            //alert("$含意："+$);
            /*  AlertResult(str);
            TestUnshifty();
            Test2(str);*/
        });

    </script>
    <style type="text/css">
    	body 
    	{
    		font-weight: bold;
    		color: blue;
    		text-align: left;
    	  }
    	
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
