using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using System.Text;
using MyTest.MyClassTest.SingleLink;

namespace MyTest.MyClassTest
{
    //循环链表VS有自带的类与方法
    //    var tt =new  System.Collections.Generic.LinkedList<int>();
    public partial class MySortTest : System.Web.UI.Page
    {
        private Stopwatch sw = new Stopwatch();
        string timeMsg = "程序耗时时间为:{0}<br>";
        private string showMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //将所有textbox控件赋值为空值
            //foreach (System.Windows.Forms.Control control in this.Controls)
            //{
            //    if (control is System.Windows.Forms.TextBox)
            //    {
            //        System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)control;
            //        tb.Text = String.Empty;
            //    }
            //} 
            int Num = 5;
            int Sum = 0;
            for (int i = 1; i < Num + 1; i++)
            {
                if ((i % 2) == 1)
                {
                    Sum += i;
                }
                else
                {
                    Sum = Sum - i;
                }
            }
            int aa = Sum;

            ////汉诺塔经典算法
            //ShowHanior();

            ////单链表
            //ShowSingleLink();

            ////双向链表
            //ShowCycleLink();

            ////二分查找
            //ShowHalfSearch();

            ////每隔三位数添加逗号
            //ShowDouhao();

            //冒泡排序
            ShowBubbleSort();

            ////堆排序
            //ShowHeapSort();

            ////linq类型的快速排序
            //ShowLinqQuickSort();

            ////一般类型快速排序
            //ShowQuickSort();

            ////约瑟夫环算法
            //ShowJoseCycle();



        }

        #region 汉诺塔经典递归算法
        private void ShowHanior()
        {
            showMsg = "";
            showMsg += "汉诺塔经典递归算法：<br />";
            // sw.Start();

            Response.Write(showMsg);
            Hanoi.moveNum = 0;
            Hanoi.HanoiFunction2(3, "A", "B", "C");

            //  sw.Stop();

            // showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);


        }

        #endregion

        #region 双向链表
        private void ShowCycleLink()
        {
            showMsg = "";
            showMsg += "双向链表简单操作：<br />";
            sw.Start();
            //    var tt =new  System.Collections.Generic.LinkedList<int>();

            var link = new MyTest.MyClassTest.CycleLink.LinkList<int>();

            int i = 1;
            while (i < 11)
            {
                link.AddLast(i);
                i++;
            }

            sw.Stop();

            //list.ForEach(Action);
            Response.Write("<br />");

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);

            Response.Write(showMsg);
            //  list.ToList().ForEach(a => Response.Write(a + ", ")); //打印已排序的数组
            link.Display();
            Response.Write("<br />");

        }

        #endregion

        #region 二分查找
        private void ShowHalfSearch()
        {
            showMsg = "";
            showMsg += "二分查找：<br />";

            int[] list = 
                {
                    5, 13, 16, 23, 24, 42, 88, 89, 100, 103
                };

            sw.Start();

            int index = HalfSearch<int>.FindHalfSearch(list, 23);

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);

            Response.Write(showMsg);
            //list.Reverse();
            //list.ForEach(a => Response.Write(a + ", ")); //打印已排序的数组
            if (index > 0)
            {
                Response.Write("查找元素所处的位置是: " + index.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                Response.Write("未找到需要的元素");
            }
            Response.Write("<br />");

        }

        #endregion


        #region linq类型的快速排序

        /// <summary>
        /// linq类型的快速排序
        /// </summary>
        private void ShowLinqQuickSort()
        {
            showMsg = "";
            showMsg += "快速排序(linq)：<br />";

            List<int> list = new List<int> { 91, 88, 42, 24, 23, 16, 13, 5 };
            sw.Start();
            List<int> resultList = QuickSort<int>.QuickSortingFunc<int>(list).ToList<int>();
            // QuickSort<double>.Quick(ref list,0,7);
            sw.Stop();

            //list.ForEach(Action);
            Response.Write("<br />");

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);

            Response.Write(showMsg);
            list.Reverse();
            list.ForEach(a => Response.Write(a + ", ")); //打印已排序的数组

            Response.Write("<br />");
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        private void ShowQuickSort()
        {

            showMsg = "";
            showMsg += "快速排序(非linq)：<br />";

            int[] list = { 32, 88, 42, 24, 91, 16, 13, 5, 6, 24, 91 };
            // List<int> list =new List<int>{ 32, 88, 42, 24, 91, 16, 13, 5 };
            sw.Start();
            // List<double> resultList = QuickSort<double>.QuickSortingFunc<double>(list).ToList<double>();
            //QuickSort<double>.Quick(ref list, 0, 7);
            // QuickSort<int>.Quick(ref list);
            // QuickSort<int>.Quick(ref list,0,list.Count-1);
            // QuickSort<int>.FastSort(ref list,0,list.Length-1);
            QuickSort<int>.QuickSortDefine(ref list, 0, list.Length - 1);
            sw.Stop();

            //list.ForEach(Action);
            Response.Write("<br />");

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);

            Response.Write(showMsg);
            list.ToList().ForEach(a => Response.Write(a + ", ")); //打印已排序的数组

            Response.Write("<br />");
        }

        #endregion


        #region 二叉树

        private void ShowBinarySearchTree()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>(5);
            //采用从文件逐个读取单词，并利用单词的某些信息来近似模拟随机生成的二叉排序树
            StreamReader reader = new StreamReader(@"C:/Download/test.txt");
            int ch = 0;
            int word = 0;
            while ((ch = reader.Read()) != -1)
            {
                if (IsCharacter(ch))
                {
                    //用单词的每个字母的长度加上长度的尾数的平方之和作为插入
                    //的二叉树的Key,这样生成的二叉树比较平衡 
                    word += ch + (ch % 10) ^ 2;
                }
                else
                {
                    if (word != 0)
                    {
                        bst.Insert(word);
                        word = 0;
                    }
                }
            }

            Action<int> dFunc = (int ele) =>
            {
                Response.Write(ele + " ");
            };
            Response.Write("PreOrderTraverse:");
            bst.PreOrderTraverse(dFunc);
            Response.Write("<br >");
            Response.Write("PostOrderTraverse:");
            bst.PostOrderTraverse(dFunc);
            Response.Write("<br >");
            Response.Write("InOrderTraverse:");
            bst.InOrderTraverse(dFunc);
            Response.Write("<br >");
            Response.End();

        }

        public static bool IsCharacter(int a)
        {
            return (a > 'a' && a < 'z') || (a > 'A' && a < 'Z');
        }


        #endregion


        #region 冒泡排序

        private void ShowBubbleSort()
        {
            int[] dArr = { 88, 24, 5, 91, 16, 42, 13, 23};//{ 11, 2, -5, 2.53, -78, 2232, 0, 121 };//
            showMsg = "";
            showMsg += "冒泡排序：<br />";

            sw.Start();

            //BubbleSort<double>.Bubble(ref dArr);
            IList<int> dlist = SelectSort<int>.SelectDataSort(dArr.ToList<int>());
            sw.Stop();

           //dArr.ToList<int>();
            string val = string.Empty;

            foreach (var d in dlist)
            {
                val += string.Format("{0},", d);
            }
            val = val.Substring(0, val.Length - 1) + "<br />";

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);
            showMsg += val;

            Response.Write(showMsg);
            Response.Write("<br />");
        }

        #endregion


        #region 堆排序

        private void ShowHeapSort()
        {
            double[] dArr = { 42, 13, 24, 91, 23, 16, 05, 88 };
            showMsg = "";
            showMsg += "堆排序：<br />";

            sw.Start();

            IList<double> dlist = SelectSort<double>.HeapSort(dArr, 8);

            sw.Stop();

            // IList<double> dlist = dArr.ToList<double>();
            string val = string.Empty;

            foreach (var d in dlist)
            {
                val += string.Format("{0},", d);
            }
            val = val.Substring(0, val.Length - 1) + "<br />";

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);
            showMsg += val;

            Response.Write(showMsg);
            Response.Write("<br />");
        }

        #endregion

        #region 约瑟夫环

        private void ShowJoseCycle()
        {
            showMsg = "";
            showMsg += "约瑟夫环问题：<br />";
            Response.Write("<br />");

            sw.Start();
            // int[] arr=   JoseCycle.OutQueue(15, 4, 3);
            int[] arr = { 42, 13, 24, 91, 23, 16, 05, 99, 11, 22, 33, 44, 55, 66, 77, 88 };
            string outMsg = string.Empty; ;
            List<int> list = JoseCycle.GetJose(arr, out outMsg); //arr.ToList();

            sw.Stop();

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);

            Response.Write(showMsg);
            list.ForEach(a => Response.Write(a + ","));

            Response.Write("<br />");
            Response.Write(outMsg + "<br />");

            Response.Write("<br />");
        }

        #endregion

        #region 单链表节点显示

        private void ShowSingleLink()
        {
            int[] arr = { 42, 13, 24, 91, 23, 16, 05, 99, 11, 22, 33, 44, 55, 66, 77, 88 };

            showMsg = "";
            showMsg += "单链表：<br />";
            Response.Write("<br />");

            sw.Start();
            MyTest.MyClassTest.SingleLink.LinkList<int> link = new MyTest.MyClassTest.SingleLink.LinkList<int>();

            for (int i = 1; i < 11; i++)
                link.Add(i);

            //插入向后的节点
            //  link.InsertNext(88, 1);

            //插入向前的节点
            // link.InsertPrev(77, 1);

            //  link.Delete(1);

            sw.Stop();

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);

            Response.Write(showMsg);

            link.Display();

            Response.Write("<br />");
        }

        #endregion

        #region 将一段数字从右向左，每隔三位添加一个逗号
        private void ShowDouhao()
        {

            string strNum = "1212333123434554455445656";
            //string msg = string.Empty;
            StringBuilder sb = new StringBuilder();

            showMsg = "";
            showMsg += "数据反转：<br />";
            Response.Write("<br />");
            sw.Start();

            char[] list = strNum.ToCharArray();

            int i = 0;
            for (int j = list.Length - 1; j >= 0; j--)
            {

                if (i == 3)
                {
                    i = 0;
                    sb.Append("," + list[j]);
                }
                else
                {
                    sb.Append(list[j]);
                }
                i++;
            }


            char[] newList = sb.ToString().ToCharArray();
            Array.Reverse(newList);

            string str = new string(newList);


            sw.Stop();

            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks);

            Response.Write(showMsg);


            Response.Write(str);
            Response.Write("<br /><br />");

        }

        #endregion
    }


 
}