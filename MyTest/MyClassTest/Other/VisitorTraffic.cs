using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// VisitorTraffic 的摘要说明
/// </summary>
public class VisitorTraffic
{
    private string _website_name;
    private string _visitor_traffic;

    public static List<VisitorTraffic> find_all()
    {
        List<VisitorTraffic> result = new List<VisitorTraffic>();
        result.Add(new VisitorTraffic("1. 使用分治法实现的全排列算法", "50"));
        result.Add(new VisitorTraffic("2. 让Ruby的数组支持任意起始下标", "16"));
        result.Add(new VisitorTraffic("3. 输出二叉树的方法", "14"));

        return result;
    }

    public VisitorTraffic(string website_name, string visitor_traffic)
    {
        _website_name = website_name;
        _visitor_traffic = visitor_traffic;
    }

    public string website_name
    {
        get { return _website_name; }
        set { _website_name = value; }
    }

    public string visitor_traffic
    {
        get { return _visitor_traffic; }
        set { _visitor_traffic = value; }
    }
}
