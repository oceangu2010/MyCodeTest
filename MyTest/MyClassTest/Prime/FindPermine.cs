using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace MyTest.MyClassTest
{

    /// <summary>
    /// 求素数算法
    /// </summary>
    public class FindPermine
    {

            public static int[] PrimeList;

            public static void FindPrime(int n)
            {

                int[] IntList;

                int len = n - 1;

                IntList = new int[n];

                for (int p = 2; p <= n; p++) IntList[p - 1] = p;

                //求一个数的数术平方根
                for (int p = 2; p < Math.Sqrt(n); p++)
                {

                    if (IntList[p - 1] == 0) continue;

                    int j = p * p;

                    while (j <= n)
                    {

                        if (IntList[j - 1] != 0)
                        {

                            IntList[j - 1] = 0;

                            len = len - 1;

                        }

                        j = j + p;

                    }

                }

                PrimeList = new int[len];

                int i = 0;

                for (int p = 2; p <= n; p++)
                {

                    if (IntList[p - 1] != 0)
                    {

                        PrimeList[i] = IntList[p - 1];

                        i = i + 1;

                    }

                }

            }


        /// <summary>
        /// 求一个数的算术平方根
        /// </summary>
        /// <param name="number">一个数</param>
        /// <returns></returns>
        public  static  string GetPermine(int number)
        {
            string msg = string.Empty;
            bool isPermine = true;
            int sqrtNumber = 0;

            if (number < 2)
            {
                msg = "没有算术平方根";
                return msg;
            }

            msg = number.ToString() + "以内的素数有:<br />";

            //i从2开始
            for (int i = 2; i <= number; i++)
            {
                isPermine = true;
                sqrtNumber = Convert.ToInt32(Math.Sqrt(number));

                if (i == 2)
                {
                    msg += i.ToString() + "<br />";
                    continue;
                }

                for (int j = 2; j <= sqrtNumber ; j++)
                {
                    if( i!=j && i % j == 0 )
                    {
                        //如果是素数则中断循环，进入下一个循环求素数
                        isPermine = false;
                        break;
                    }
                }

                if (isPermine)
                {
                    msg +=i.ToString()+"<br />";
                }
            }


            return msg;

        }

    }
}