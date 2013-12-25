using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyTest.MyClassTest;

namespace MyTest
{
    public partial class MyGenericTest : System.Web.UI.Page
    {
            protected void Page_Load(object sender, EventArgs e)
            {
                //测试泛型堆栈
                ShowGenericDemo();

                //测试队列
                ShowQueueDemo();

                //链表
                ShowLinkNode();

                //hash表
                ShowHashTable();

                //sortlist
                ShowSortList();

                //namevaluecollect
                ShowNameValueCollection();

                //student sort
                ShowStudentSort();

                //泛型函数输出
                Response.Write(GenericFunction.GetValue(12)+"<br >");  ;
                Response.Write(GenericFunction.GetValue("你NNDD") + "<br >"); 

            }

            #region 泛型集合应用实例

            /// <summary>
            /// 显示学生排序
            /// </summary>
            private  void ShowStudentSort()
            {
                Response.Write("===============ShowStudentSort start==================<br /><br />");
                List<Student> arr = new List<Student>();
                arr.Add(new Student("张三", 7, "一年级"));
                arr.Add(new Student("李四", 11, "二年级"));
                arr.Add(new Student("王五", 21, "一年级"));
                arr.Add(new Student("陈六", 8, "三年级"));
                arr.Add(new Student("刘七", 15, "二年级"));

                Response.Write("按年级排序:<br />");
                // 调用Sort方法，实现按年级排序
                arr.Sort(new StudentComparer(StudentComparer.CompareType.Grade));

                // 循环显示集合里的元素
                foreach (Student item in arr)
                    Response.Write(item.ToString()+"<br />");

                Response.Write("<br /><br />按姓名排序:<br />");
                // 调用Sort方法，实现按姓名排序
                arr.Sort(new StudentComparer(StudentComparer.CompareType.Name));

                // 循环显示集合里的元素
                foreach (Student item in arr)
                    Response.Write(item.ToString() + "<br />");

                Response.Write("<br /><br />按年龄排序:<br />");
                // 调用Sort方法，实现按姓名排序
                arr.Sort(new StudentComparer(StudentComparer.CompareType.Age));

                // 循环显示集合里的元素
                foreach (Student item in arr)
                    Response.Write(item.ToString() + "<br />");


                Response.Write("===============ShowStudentSort end==================<br /><br />");
            }

             /// <summary>
            /// NameValueCollection集合类型
            /// 键值对
            /// </summary>
            private void ShowNameValueCollection()
            {
                Response.Write("===============NameValueCollection start==================<br /><br />");
                NameValueCollection nvc = new NameValueCollection();

                //添加值
                nvc.Add("a", "aaaaa");
                nvc.Add("b", "bbbbb");
                nvc.Add("c", "ccccc");

               // IEnumerator ide = sl.();

                string msg = "字符串类型集合(NameValueCollection):";

                //数据字典
                foreach (var key in nvc.AllKeys)
                {
                    msg += string.Format("Key值为:{0}|Value值:{1};   ", key, nvc[key].ToString()); 
                }

                msg = msg.Substring(0, msg.Length - 1) + "<br /><br />";
                Response.Write(msg);
                Response.Write("===============NameValueCollection end==================<br /><br />");
            }

             /// <summary>
            /// sortList集合类型
            /// 键值对
            /// </summary>
            private  void ShowSortList()
            {
                Response.Write("===============SortedList start==================<br /><br />");    
                SortedList sl=new SortedList();
                //添加值
                sl.Add("a", "你好");
                sl.Add("b", "大家");
                sl.Add("c", "商业");

                IDictionaryEnumerator ide = sl.GetEnumerator();

                string msg = "字符串类型集合(SortedList):";

                //数据字典
                while (ide.MoveNext())
                {
                    msg += string.Format("Key值为:{0}|Value值:{1};   ", ide.Key, ide.Value);
                }

                msg = msg.Substring(0, msg.Length - 1) + "<br /><br />";
                Response.Write(msg);
                Response.Write("===============SortedList end==================<br /><br />");
            }

             /// <summary>
            /// 显示hash表
            /// 键值对
            /// </summary>
            private void ShowHashTable()
            {
                Response.Write("===============Hashtable start==================<br /><br />");
                Hashtable ht = new Hashtable();
                ht.Add("a",12);
                ht.Add("b",15);
                ht.Add("c",28);

                ICollection keys = ht.Values;

                string msg = "int类型hash表(HashTable):";

                //数据字典
                IDictionaryEnumerator ide = ht.GetEnumerator();
                while (ide.MoveNext())
                {
                    msg += string.Format("Key值为:{0}|Value值:{1};  ", ide.Key, ide.Value);
                }

                msg = msg.Substring(0, msg.Length - 1) + "<br /><br />";
                Response.Write(msg);
                Response.Write("===============Hashtable end==================<br /><br />");

            }

             /// <summary>
            /// 显示链表
            /// </summary>
            private void ShowLinkNode()
            {
                Response.Write("===============链表 start==================<br /><br />");
                LinkedList<int, string> link = new LinkedList<int, string>();
                link.AddHead(0, "AA");
                link.AddHead(1, "BB");
                link.AddHead(2, "CC");

                List<string> list = link.OutputLinkNodes();
                string msg = "int类型链表(linkNode):";

                for (int i = 0; i < list.Count; i++)
                {
                    if (i < 0) break;
                    msg += list[i] + ",";
                }
                msg = msg.Substring(0, msg.Length - 1) + "<br /><br />";
                Response.Write(msg);
                Response.Write("===============链表 end==================<br /><br />");
            }

            /// <summary>
            /// 显示队列值
            /// 队列特点：先进先出
            /// </summary>
            private void ShowQueueDemo()
            {
                Response.Write("===============Queue start==================<br /><br />");
                Queue<int> queue = new Queue<int>(3);

                queue.Enqueue(99);
                queue.Enqueue(88);
                queue.Enqueue(77);
                string msg = "int类型队列(Queue):";

                int de;

                while (true)
                {
                    de = queue.Count == 0 ? 0 : queue.Dequeue();
                    if (de != 0)
                    {
                        msg += de.ToString() + ",";
                    }
                    else
                    {
                        break;
                    }
                }
                msg = msg.Substring(0, msg.Length - 1) + "<br /><br />";
                Response.Write(msg);
                Response.Write("===============Queue end==================<br /><br />");
            }


            /// <summary>
            /// 测试泛型 实例
            /// 栈 特点先进后出
            /// </summary>
            private void ShowGenericDemo()
            {
                Response.Write("===============Stack start==================<br /><br />");
                StackClass<int> stack = new StackClass<int>(3);

                stack.Push(14);
                stack.Push(22);
                stack.Push(-12);

                string msg = "int类型堆栈(Stack):";
                int[] iArr = stack.PopArray();
                for (int i = iArr.Length - 1; i < iArr.Length; i--)
                {
                    if (i < 0) break;
                    msg += iArr[i].ToString() + ",";
                }
                msg = msg.Substring(0, msg.Length - 1) + "<br /><br />";
                Response.Write(msg);

                string msgStr = "字符串类型堆栈(stack):";
                StackClass<string> stackStr = new StackClass<string>(3);
                stackStr.Push("第一项");
                stackStr.Push("第二项");
                stackStr.Push("第三项");

                string[] strArr = stackStr.PopArray();
                for (int i = strArr.Length - 1; i < strArr.Length; i--)
                {
                    if (i < 0) break;
                    msgStr += strArr[i] + ",";
                }
                msgStr = msgStr.Substring(0, msgStr.Length - 1) + "<br /><br />";
                //输出     
                Response.Write(msgStr);
                Response.Write("===============Stack end==================<br /><br />");
            }

            #endregion

       }
    }
