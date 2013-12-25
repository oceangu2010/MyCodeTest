using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MyTest.PageTest
{
    public class DelegateClass
    {
        #region 委托实例
        public delegate string Speak(string languangeName);

        private event Speak Option;


        private string EnglishSpeak(string languangeName)
        {
            return string.Format("你说的语言是:{0}", languangeName);
        }

        private string ChineseSpeak(string languangeName)
        {
            return string.Format("你说的语言是:{0}", languangeName);
        }

        private string FreachSpeak(string languangeName)
        {
            return string.Format("你说的语言是:{0}", languangeName);
        }

        public string SpeakLanguage(string languageName, Speak sp)
        {
            languageName = sp(languageName);
            return languageName;
        }

        public String MethodTest(string languageName)
        {
            //直接调用方法   
            string str = null;
            Option = EnglishSpeak;

            if (Option != null)
            {
                str += Option(languageName);
            }
            str += SpeakLanguage(languageName, EnglishSpeak);
            return str;
        }
        #endregion

        #region 事件回调函数

        Func<int, int, string> fb = (n1, n2) => { int sum = n1 + n2; return sum.ToString(); };

        private static string GetStr(int n1,int n2)
        {
            return ( n1+n2).ToString();
        }

        private static string FuncDefine(int n1, int n2, Func<int, int, string> fb)
        {
           return  fb(n1, n2);
        }

        public static string FuncTest()
        {
           return FuncDefine(22, 33,GetStr);
        }


        private void TestLambda()
        {
            string[] namesArr = { "Jeff", "Kristin", "Aidan", "Grant", "Tomes"};
            char findChar='a';
            namesArr = Array.FindAll(namesArr, name => name.IndexOf(findChar)>-1);

            Array.ConvertAll(namesArr, name => name.ToUpper());

            //显示每个元素
            Array.ForEach(namesArr, name => Console.WriteLine(name));

        }
        #endregion
    }


}