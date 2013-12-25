using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.PageTest.Paging
{
    /// <summary>
    /// 投顾模拟交易
    /// csk
    /// </summary>
    [Serializable]
    public class jPaginateModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 股票统一编码
        /// </summary>
        public int Stk_Unicode { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        public string Stk_Code { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        public string Stk_Name { get; set; }

        /// <summary>
        /// 买卖标志(买1 | 卖 0)
        /// </summary>
        public int Bs_Mark { get; set; }

        /// <summary>
        /// 买卖标志名称
        /// </summary>
        public string Bs_Mark_Name { get; set; }

        /// <summary>
        /// 买卖时间
        /// </summary>
        public DateTime Bs_Time { get; set; }

        /// <summary>
        /// 买卖价格
        /// </summary>
        public Double Bs_Price { get; set; }

        /// <summary>
        /// 买卖数量
        /// </summary>
        public int Bs_Count { get; set; }

        /// <summary>
        /// 发生金额
        /// </summary>
        public Double Occur_Amount { get; set; }

        /// <summary>
        /// 手续费(%)
        /// </summary>
        public Double Cc { get; set; }

        /// <summary>
        /// 投顾ID
        /// </summary>
        public int Invsadv_Id { get; set; }

        /// <summary>
        /// 投顾名字
        /// </summary>
        public string Invs_Name { get; set; }

        /// <summary>
        /// 是否卖完(0否 | 1 是)
        /// </summary>
        public int Is_Finished { get; set; }

        /// <summary>
        /// 是否卖完名称
        /// </summary>
        public string Is_Finished_Name { get; set; }
    }

}