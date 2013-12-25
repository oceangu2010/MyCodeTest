using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;

namespace MyTest.MyClassTest
{
    public static class ArrayClass
    {
        //每次pi的值都不一样
        private static unsafe  Int32 UnsafeArray(int[,] arr)
        {
            Int32 sum = 0;
            
            int numElements = 100;

            fixed (int* pi = arr)
            {
                for (int i = 0; i < numElements; i++)
                {
                    int baseOfDim =  i* numElements;
                    for (int j = 0; j < numElements; j++)
                    {
                        sum += pi[baseOfDim + j];
                    }

                }
            }
            //Parallel.ForEach(;
            return sum;
        }

        public static int GetArrayValue()
        {
            int len = 10;
            int[,] arr = new int[len, len];
            int sum = 0;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    arr[i, j] = i + j;

                }
            }

            sum = UnsafeArray(arr);

            return sum;
        }

        /// <summary>
        /// 字符串反转
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RevertStr(string str)
        { 
            if(string.IsNullOrEmpty(str)) return null;
            //throw new ArgumentNullException("参数不能为空");
            IEnumerable<char> arr = str.Reverse<char>();
           return new string( arr.ToArray());
        }
    }

    #region 数组实例
    public sealed class DynamicArrays
    {

        public static string ShowArrayMsg()
        {
            Int32[] lowerBounds = { 2005, 1 };
            Int32[] lengths = { 5, 4 };
            Decimal[,] quarrerlyRevenue = (Decimal[,])
                Array.CreateInstance(typeof(Decimal), lengths, lowerBounds);

            StringBuilder sb = new StringBuilder();
            //show msg
            sb.Append(string.Format("{0,4},{1,9},{2,9},{3,9},{4,9} <br />", "Year", "Q1", "Q2", "Q3", "Q4"));

            int firstYear = quarrerlyRevenue.GetLowerBound(0);
            int lastYear = quarrerlyRevenue.GetUpperBound(0);
            int firstQuarter = quarrerlyRevenue.GetLowerBound(1);
            int lastQuarter = quarrerlyRevenue.GetUpperBound(1);

            for (int year = firstYear; year <= lastYear; year++)
            {
                sb.Append(string.Format("year:{0}<br />", year));
                for (int quarter = firstQuarter; quarter <= lastQuarter; quarter++)
                {
                    sb.Append(string.Format("{0,9:C}", quarrerlyRevenue[year, quarter]));
                }
            }

            return sb.ToString();
        }

    }

    #endregion
}