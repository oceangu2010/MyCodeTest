using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public class Recursive
    {

        /// <summary>
        /// 阶乘算法，递归思想
        /// </summary>
        /// <returns></returns>
        public static int GetFinal(int n)
        {
            if (n == 1)

                return 1;

            else

                return n * GetFinal(n - 1);
        }

        /*
       在大一时上计算机文化基础的时候我们就接触过”进制转换问题“，比如将”十进制“转化为”二进制“。
       思路：采用除2取余法，取余数为相应二进制数的最低位，然后再用商除以2得到次低位.......直到最后一次相除商为0时得到二进制的最高位，
                比如(100)10=(1100100)2，   仔细分析这个问题，会发现它是满足”递归“的三要素的，
               ① 进制转换中，数据规模会有所缩小。
               ② 当商为0时，就是我们递归的出口。
            所以这个问题我们就可以用递归拿下。
        */
        public static string ConvertToBinary(ref string str, int num)
        {
            //递的过程          
            if (num == 0)
                return string.Empty;
            ConvertToBinary(ref str, num / 2);
            //归的过程       
            return str += (num % 2);
        }


    }
}