/// <reference path="jquery-1.7.1.min.js" />
/// <reference path="jquery.paginate.js" />

$(document).ready(function ()
{

    /*
    公用方法
    showPaginateData(data) 显示分页数据
    getPaginateData(pSize, curPage) 取得分页数据
    getTotalPage(pSize) 取得总页数
    setPaginate() 分页页码显示
    */

    var begPage = 1; //开始页码
    var displayPage = 5; //显示页码数
    var pageSize = 5; //每面显示记录数

    //显示分页数据
    function showPaginateData(data)
    {

        $("#pagetxt").html("");
        var v_str = "<table id='stk_position_tab' class='pos_tab'>";
        v_str += "<thead><tr>";
        v_str += "<td class='pos_tab_td_header'>证券代码</td><td class='pos_tab_td_header'>证券名称</td>";
        v_str += "<td class='pos_tab_td_header'>买卖时间</td><td class='pos_tab_td_header'>业务类型</td>";
        v_str += "<td class='pos_tab_td_header'>投顾</td><td class='pos_tab_td_header'>成交价格</td>";
        v_str += "<td class='pos_tab_td_header'>成交数量</td><td class='pos_tab_td_header'>发生金额</td>";
        v_str += "<td class='pos_tab_td_header'>持仓</td><td class='pos_tab_td_header'>统一编码</td>";

        v_str += "</tr></thead><tbody>";

        try
        {
            $(data).each(function (i, item)
            {
                v_str += "<tr id='show_stk_tr_" + i + "'";

                //盈亏颜色变化，-为绿，+为红
                if (item.Bs_Mark_Name == "买")
                {
                    v_str += " class='show_stk_tr_green'>";

                } else if (item.Bs_Mark_Name == "卖")
                {
                    v_str += " class='show_stk_tr_red'>";
                }

                v_str += "<td id='s_code_td' >" + item.Stk_Code + "</td><td>" + item.Stk_Name + "</td>";
                v_str += "<td class='alignRight'>" + item.Bs_Time + "</td><td class='alignRight'>" + item.Bs_Mark_Name + "</td>";
                v_str += "<td class='alignRight'>" + item.Invs_Name + "</td><td class='alignRight'>" + item.Bs_Price + "</td>";
                v_str += "<td class='alignRight'>" + item.Bs_Count + "</td><td class='alignRight'>" + item.Occur_Amount + "</td>";
                v_str += "<td class='alignRight'>" + item.Is_Finished_Name + "</td><td class='alignRight'>" + item.Stk_Unicode + "</td>";
                v_str += "</tr>";

            });
            v_str += "</tbody></table>";
            $("#pagetxt").html(v_str);


        } catch (e)
        {
            alert(e);
            return;
        }
    }
    //showPaginateData(data):end

    //取得分页数据
    //pageSize:每页记录条数
    //currentPage:当前是第几页
    function getPaginateData(pSize, curPage)
    {
        var paras = "{pageSize:" + pSize + ",currentPage:" + curPage + "}";
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "jPaginate.asmx/getPaginateData",
            data: paras,
            dataType: 'json',
            beforeSend: function ()
            {
                $("#pagetxt").html("");
                $("#load_gif").attr('style', 'display:block');
            },

            success: function (json)
            {
                var data = eval('(' + json.d + ')');
                showPaginateData(data);
            },

            complete: function ()
            {
                $("#load_gif").attr('style', 'display:none');
            },
            error: function (xhr)
            { //如果没有上面的捕获出错会执行这里的回调函数
                alert('页出错\n\r' + xhr.responseText);
            }
        });
    }
    //getPaginateData(pSize, curPage):end

    //取得总页数
    function getTotalPage(pSize)
    {
        var paras = "{pageSize:" + pSize + "}";
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "jPaginate.asmx/getTotalPage",
            data: paras,
            dataType: 'json',
            beforeSend: function ()
            {
                $("#load_gif").attr('style', 'display:block');
            },

            success: function (json)
            {
                var data = eval('(' + json.d + ')');
                $("#Hidden1").val(data.pages);

                ////////////////////////////
                //放置分页:
                //因为ajax的执行,是在所有js代码中非服务器请求执行完成后,才执行ajax代码,
                //所以为了取得总记录行数,必须要等到ajax执行完成.
                setPaginate();
                //////////////////////////////

            },

            complete: function ()
            {
                $("#load_gif").attr('style', 'display:none');
            },
            error: function (xhr)
            { //如果没有上面的捕获出错会执行这里的回调函数
                alert('页出错\n\r' + xhr.responseText);
            }
        });
    }
    //getTotalPage(pSize):end

    //分页页码显示 setPaginate()
    function setPaginate()
    {
        var pages = $("#Hidden1").val(); //总页面数

        $("#demo").paginate({
            count: pages,
            start: begPage,
            display: displayPage,
            border: true,
            border_color: '#BEF8B8',
            text_color: '#79B5E3',
            background_color: '#E3F2E1',
            border_hover_color: '#68BA64',
            text_hover_color: '#2573AF',
            background_hover_color: '#CAE6C6',
            images: false,
            mouse: 'press',
            onChange: function (currentPage)
            {
                //点击页码时,执行的函数(作ajax异步请求数据)
                //alert(currentPage);
                //取得分页数据
                getPaginateData(pageSize, currentPage);

            }
        });
    }
    //setPaginate():end

    ///////////////::正文开始:://///////////////
    //////////////////////////////////////////
    //pageSize:每页显示记录数
    getTotalPage(pageSize);
    //首次进入页面,取出第一页数据:currentPage=1
    getPaginateData(pageSize, 1);

});