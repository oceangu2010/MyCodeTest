using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Windows.Forms;

namespace MyTest.MyClassTest
{
    public class bag
    {
        /// <summary>
        /// 找出和为10的所有组合
        /// </summary>
        public static void GetBag()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbNode = new StringBuilder();
            int[] source = new int[] { 1, 2, 3, 4, 5, 8, 9,7,6,0};
            string stringData = "1111111111";//表示有6个数！
            int intData = Convert.ToInt32(stringData, 2);
            int sum;

            for (int i = intData; i > 0; i--)
            {
                sum = 0;
                int buff = i;
                sbNode.Remove(0, sbNode.Length);
                for (int length = 0; buff > 0; length++)
                {
                    if ((buff & 0x00000001) != 0)
                    {
                        sum += source[length];
                        sbNode.Append(source[length].ToString() + "\t");

                    }

                    buff = buff >> 1;
                }

                //此次是不是符合条件？并记录结果
                if (sum == 10)
                {
                    sb.AppendLine(sbNode.ToString());
                }
            }

            MessageBox.Show(sb.ToString(), "结果" + Convert.ToString(intData, 2) + "系列");
        }


        /// <summary>
        /// 找零
        /// </summary>
        /// <param name="num"></param>
       public static Dictionary<decimal, int> Exchange(decimal num)
        {
            var money = GetInit();

            int i = 0;

            while (true)
            {
                if (num < 0.05M)
                {
                    return money;
                }

                var max = money.Keys.ElementAt(i);

                if (num >= max)
                {
                    num = num - max;

                    //money的张数自增
                    money[max] = money[max] + 1;
                }
                else
                {
                    //如果是小于1毛，大于5分的情况下
                    if (num < 0.1M && num >= 0.05M)
                    {
                        //按一毛计算
                        money[0.10M] = money[0.10M] + 1;

                        num = 0.0M;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        static Dictionary<decimal, int> GetInit()
        {
            Dictionary<decimal, int> money = new Dictionary<decimal, int>();

            //key表示钱，value表示钱的张数
            money.Add(100.00M, 0);
            money.Add(50.00M, 0);
            money.Add(20.00M, 0);
            money.Add(10.00M, 0);
            money.Add(5.00M, 0);
            money.Add(2.00M, 0);
            money.Add(1.00M, 0);
            money.Add(0.50M, 0);
            money.Add(0.20M, 0);
            money.Add(0.10M, 0);

            return money;
        }



    }
}