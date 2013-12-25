using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public class JoseCycle
    {


        /// <summary>
        ///  有17个人围成一圈(编号0~16),从第0号的人开始从1报数，
        ///  凡报到3的倍数的人离开圈子，然后再数下去，直到最后只剩下一个人为止，问此人原来的位置是多少号?
        /// </summary>
        /// <param name="inArr"></param>
        /// <returns></returns>
        public static List<int> GetJose(int[] inArr, out string outMsg)
        {
            outMsg = "最后出场的成员是：";

            int[] outArr = inArr; //{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int number = 0;//报数
            int index = 0;//位置索引数

            List<int> newList = new List<int>();
            List<int> list = outArr.ToList<int>();
            string msg = string.Empty;

            //当人数大于3时，才能形成一圈
            while (list.Count >= 2)
            {
                number++;

                //遇到是3倍数的人则将其去除
                if (number % 3 == 0)
                {
                    if (list.Count == 0) break;
                    newList.Add(list[index]); //将出局的人依次加入新的集合
                    list.RemoveAt(index);
                    number = 0;  //从头开始数

                }
                else
                {
                    //继续向下报数
                    index++;
                }

                //当索引位置超出总人数时，开始进入下一轮计数，说明此一圈已经结束
                if (index >= list.Count) index = 0;

            }

            foreach (var val in list)
            {
                newList.Add(val);
                outMsg += string.Format("{0},", val);
            }


            return newList;

        }

        //[c#算法和数据结构]约瑟夫环问题
        // 问题描述: 
        //设有n个人围坐一圈,现以某个人开始报数,数到m的人出列,接着从出列的下一个人开始重新报数,
        //数到m的人又出列,如此下去,直到所有人都出列为止.按出列顺序输出.

        //从第start人开始计数，以alter为单位循环记数出列，总人数为total 
        public static int[] Jose(int total, int alter, int start)
        {
            int i, j, k = 0;

            //count数组存储按出列顺序的数据，以当结果返回 
            int[] count = new int[total];

            //s数组存储初始数据 
            int[] s = new int[total];

            //对数组s赋初值,第一个人序号为0,第二人为1，依此下去
            for (i = 0; i < total; i++)
            {
                s[i] = i;
            }

            //按出列次序依次存于数组count中 
            for (i = total; i >= 2; i--)
            {
                start = (start + alter - 1) % i;
                if (start == 0)
                    start = i;
                count[k] = s[start];
                k++;
                for (j = start + 1; j <= i; j++)
                    s[j - 1] = s[j];
            }

            count[k] = s[1];

            //结果返回 
            return count;

        }


        public static int[] OutQueue(int total, int start, int num)
        {
            int temp = 0;

            int[] inArr = new int[total]; //入队列
            int[] outArr = new int[total];//出队列

            //给所在队列中人员进行编号
            for (int i = 0; i < total; i++)
            {
                inArr[i] = i;
            }

            for (int i = 2; i < total; i++)
            {
                //开始移值
                start = (start + num - 1) % i;
                if (start == 0) start = i;
                outArr[temp] = inArr[start];
                temp++;

                //开始重新移队列
                for (int j = start + 1; j <= i; j++)
                    inArr[j - 1] = inArr[j];

            }
            outArr[temp] = inArr[1];

            return outArr;

        }


        private ArrayList CateEatMouse(ArrayList list, int step)
        {
            list.RemoveAt(0);
            int j = 1;
            while (list.Count > step)
            {
                if (j % (step + 1) == 0)
                {
                    list.RemoveAt(0);
                }
                else
                {
                    list.Add(int.Parse(list[0].ToString()));
                    list.RemoveAt(0);
                }
                j++;
            }
            return list;
        }

    }




    class LinkNode<T>
    {
        T data;//存放数据
        LinkNode<T> next;//指向下一个节点的引用
        //无参数构造
        public LinkNode()
        {
            this.data = default(T);
            this.next = null;
        }
        //  给出数据的额构造器
        public LinkNode(T e)
        {
            this.data = e;
            this.next = null;
        }
        //只给出节点的构造
        public LinkNode(LinkNode<T> p)
        {
            this.next = p;
        }
        //给出节点和数据的构造
        public LinkNode(T e, LinkNode<T> p)
        {
            this.data = e;
            this.next = p;
        }
        // 数据属性
        public T Data
        {
            get { return data; }
            set { data = value; }
        }
        //引用属性
        public LinkNode<T> Next
        {
            get { return next; }
            set { next = value; }
        }
    }



    class LinkList<T>
    {
        public LinkNode<T> head;//表头
        public LinkNode<T> Head
        {
            get { return head; }
            set { head = value; }
        }//表头属性
        public LinkList()//构造空表
        {
        }


        // 表尾添加e
        public void append(T e)
        {
            LinkNode<T> s = new LinkNode<T>(e);
            LinkNode<T> p = new LinkNode<T>();
            if (head == null)
            {
                head = s;
                s.Next = head;
                return;
            }
            p = head;
            while (p.Next != head)//p后移到表尾
            {
                p = p.Next;
            }
            p.Next = s;
            s.Next = head;//首位相连构成链表
        }


        public int GetLength()   //获得表长
        {
            LinkNode<T> p = head;
            int len = 0;
            if (p == null)
            {
                return len;
            }
            else
            {
                len = len + 1;
            }
            while (p.Next != head)
            {
                len++;
                p = p.Next;
            }
            return len;
        }


        public bool IsEmpty()//是否为空
        {
            if (head == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertNode(int i, T e)//插入某一元素
        {
            LinkNode<T> p = Locate(i - 1);
            if (p != null)
            {
                LinkNode<T> s = new LinkNode<T>(e);
                s.Next = p.Next;
                p.Next = s;
                return true;
            }
            else
                return false;
        }

        public LinkNode<T> Locate(int i)//已知该节点的节点数，寻找该节点
        {
            if (IsEmpty())
            {
                return null;
            }

            if (i > 0 && i > GetLength())
            {
                return null;
            }

            LinkNode<T> p = head;
            int j = 1;
            while (p != null && j < i)
            {
                j++;
                p = p.Next;
            }

            if (j == i)
            {
                return p;
            }
            else
                return null;
        }

        public LinkNode<T> Locate(T x)//已知节点数值，查找该节点
        {
            if (IsEmpty())
            {
                return null;
            }
            LinkNode<T> p = head;
            while (p != null && !p.Data.Equals(x))
            {
                p = p.Next;
            }
            if (p.Data.Equals(x))
                return p;
            else
                return null;
        }

        public T GetElement(int i)
        {
            LinkNode<T> p = Locate(i);
            if (p == null)
            {
                return default(T);
            }
            else
            {
                return p.Data;
            }
        }

        public LinkNode<T> Delete(int i, int len)//已知该节点的位置和总链长，删除该节点
        {
            if (i > 1)
            {
                LinkNode<T> p = Locate((i - 1));
                LinkNode<T> q = new LinkNode<T>();
                q = p.Next;
                p.Next = q.Next;
                return q;
            }
            else
            {
                LinkNode<T> p = Locate(len);
                LinkNode<T> q = new LinkNode<T>();
                q = p.Next;
                p.Next = q.Next;
                head = p.Next;
                return q;
            }
        }
        public int Loca(T x)//查询节点数值为x的节点的位置
        {
            int t = 1;
            if (IsEmpty())
            {
                return 0;
            }
            LinkNode<T> p = head;
            while (p != null && !p.Data.Equals(x))
            {
                p = p.Next;
                t++;
            }
            if (p.Data.Equals(x))
                return t;
            else
                return 0;
        }

        public LinkNode<T> Delete(LinkNode<T> p)//删除节点p
        {
            if (p != null)
            {
                LinkNode<T> q = p.Next;
                p.Next = q.Next;
                return q;
            }
            else { return null; }
        }
    }


}//end class