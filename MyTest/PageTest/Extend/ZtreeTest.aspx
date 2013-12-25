<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZtreeTest.aspx.cs" Inherits="MyTest.Extend.ZtreeTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="ztree/css/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="ztree/jquery.ztree-2.6.js" type="text/javascript"></script>

    <script type="text/javascript">
        var zTree1;
        var setting;

        setting = {
            isSimpleData: true,
            treeNodeKey: "id",
            treeNodeParentKey: "pId",
            fontCss: setFont,
            callback: {
                beforeExpand: function () { return false; },
                beforeCollapse: function () { return false; },
                beforeClick: zTreeOnBeforeClick,
                click: zTreeOnClick
            }

        };

        var zNodes = [
  	 	{ id: 1, pId: 0, name: "北京" },
  	 	{ id: 2, pId: 0, name: "天津" },
  	 	{ id: 3, pId: 0, name: "上海" },
  	 	{ id: 6, pId: 0, name: "重庆" },
  	 	{ id: 4, pId: 0, name: "河北省", open: true },
  	 	{ id: 41, pId: 4, name: "石家庄" },
  	 	{ id: 42, pId: 4, name: "保定" },
  	 	{ id: 43, pId: 4, name: "邯郸" },
  	 	{ id: 44, pId: 4, name: "承德" },
  	 	{ id: 5, pId: 0, name: "广东省", open: true },
  	 	{ id: 51, pId: 5, name: "广州" },
  	 	{ id: 52, pId: 5, name: "深圳" },
  	 	{ id: 53, pId: 5, name: "东莞" },
  	 	{ id: 54, pId: 5, name: "佛山" },
  	 	{ id: 6, pId: 0, name: "福建省", open: true },
  	 	{ id: 61, pId: 6, name: "福州" },
  	 	{ id: 62, pId: 6, name: "厦门" },
  	 	{ id: 63, pId: 6, name: "泉州" },
  	 	{ id: 64, pId: 6, name: "三明" }

  	 ];

        $(document).ready(function () {
            reloadTree();

            $("body").bind("mousedown",
			function (event) {
			    if (!(event.target.id == "menuBtn" || event.target.id == "DropdownMenuBackground" || $(event.target).parents("#DropdownMenuBackground").length > 0)) {
			        hideMenu();
			    }
			});
        });

        function setFont(treeId, treeNode) {
            if (treeNode && treeNode.isParent) {
                return { color: "blue" };
            } else {
                return null;
            }
        }

        function showMenu() {
            var cityObj = $("#citySel");
            var cityOffset = $("#citySel").offset();
            $("#DropdownMenuBackground").css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");

        }
        function hideMenu() {
            $("#DropdownMenuBackground").fadeOut("fast");
        }

        function zTreeOnBeforeClick(treeId, treeNode) {
            var check = (treeNode && !treeNode.isParent);
            if (!check) alert("只能选择城市...");
            return check;
        }

        function zTreeOnClick(event, treeId, treeNode) {
            if (treeNode) {
                var cityObj = $("#citySel");
                cityObj.attr("value", treeNode.name);
                hideMenu();
            }
        }

        function reloadTree() {
            hideMenu();
            zTree1 = $("#dropdownMenu").zTree(setting, clone(zNodes));
        }

        function clone(jsonObj, newName) {
            var buf;
            if (jsonObj instanceof Array) {
                buf = [];
                var i = jsonObj.length;
                while (i--) {
                    buf[i] = clone(jsonObj[i], newName);
                }
                return buf;
            } else if (typeof jsonObj == "function") {
                return jsonObj;
            } else if (jsonObj instanceof Object) {
                buf = {};
                for (var k in jsonObj) {
                    if (k != "parentNode") {
                        buf[k] = clone(jsonObj[k], newName);
                        if (newName && k == "name") buf[k] += newName;
                    }
                }
                return buf;
            } else {
                return jsonObj;
            }
        }

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" width="700" class="tb1">
        <tr>
            <td width="340px" align="center" valign="top">
                <div class="zTreeDemoBackground">
                    <br />
                    &nbsp;&nbsp;城市：<input id="citySel" type="text" readonly value="" style="width: 80px;" />
                    &nbsp;<a id="menuBtn" href="#" onclick="showMenu(); return false;">选择</a>
                </div>
            </td>
        </tr>
    </table>
    <div id="DropdownMenuBackground" style="display: none; position: absolute; height: 200px;
        min-width: 150px; background-color: white; border: 1px solid; overflow-y: auto;
        overflow-x: auto;">
        <ul id="dropdownMenu" class="tree">
        </ul>
    </div>
    </form>
</body>
</html>
