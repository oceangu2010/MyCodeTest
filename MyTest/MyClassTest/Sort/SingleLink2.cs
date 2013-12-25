using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest.SingleLink
{
    #region 单链表

    /*
     要注意的是，单链表的Add（）方法最好不要频繁调用，尤其是链表长度较长的时候，因为每次Add，
     都会从头节点到尾节点进行遍历，这个缺点的优化方法是将节点添加到头部，但顺序是颠倒的。
     所以，在下面的例子中，执行Purge（清洗重复元素）的时候，没有使用Add（）方法去添加元素，
     而是定义一个节点，让它始终指向目标单链表的最后一个节点，这样就不用每次都从头到尾遍历
     此外，链表还可以做成循环链表，即最后一个结点的next属性等于head，主要操作与单链表相似，
     判断最后一个结点，不是等于null，而是等于head
     */

    //IListDs接口如下：
    public interface IListDS<T>
    {

        int GetLength(); //求长度
        void Clear(); //清空操作
        bool IsEmpty(); //判断线性表是否为空
        void Append(T item); //附加操作
        void InsertPrev(T item, int i); //插入操作
        T Delete(int i); //删除操作
        T GetElem(int i); //取表元
        int Locate(T value); //按值查找
        //void Rervese();

    }

    public class Node<T>
    {
        private T data;          //数据域 临时保存新添加的据
        private Node<T> next;   //引用域 定义当前节点的下一个节点

        //构造器
        public Node(T val, Node<T> p)
        {
            data = val;
            next = p;
        }

        //构造器
        public Node(Node<T> p)
        {
            next = p;
        }


        //构造器
        public Node(T val)
        {
            data = val;
            next = null;
        }

        //构造器
        public Node()
        {
            data = default(T);
            next = null;
        }

        //数据域属性
        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        //引用域属性
        public Node<T> Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }


    }

    // 可以将实例化对象定义成一个属性


    //链表操作实现类
    public class LinkList<T> : IListDS<T>
    {
        private Node<T> head; //单链表的头引用

        //头引用属性
        public Node<T> Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }

        //构造器
        public LinkList()
        {
            head = null;
        }

        //求单链表的长度
        public int GetLength()
        {
            //定义一个指针，移动指针来计算链表的长度
            Node<T> p = head;

            int len = 0;
            while (p != null)
            {
                ++len;
                p = p.Next;
            }

            return len;
        }

        //清空单链表
        public void Clear()
        {
            head = null;
        }

        //判断单链表是否为空
        public bool IsEmpty()
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

        //在单链表的末尾添加新元素
        //单链表从尾部开始插入，与队列插入方法类似
        public void Append(T item)
        {
            //构造一个新的节点，用于保存当前元素的值
            Node<T> current = new Node<T>(item);

            //声明一个新的链表节点
            Node<T> newNode = new Node<T>();

            //如果没有链表头，说明这个链表为空，那么此时添加的节点就是链表的头
            if (head == null)
            {
                head = current;
                return;
            }

            //将头节点赋予当前的空节点
            newNode = head;

            //循环链表，如果链表此节点有下一个节点
            //则将此节点进行前移，一直到新增加的节点加入到链表中为止
            while (newNode.Next != null)
            {
                newNode = newNode.Next;
            }

            //将链表最后一个节点的位置留给当前新增加的节点
            newNode.Next = current;

            //此时节点添加成功
        }


         /**/
        //在单链表的第i个结点的位置前插入一个值为item的结点
        public void InsertPrev(T item, int i)
        {
            //如果链表为空，则直接返回
            if (IsEmpty() || i < 1)
            {
                throw new Exception("List is empty or Position is error!");
                return;
            }

            //从链表头开始插入
            if (i == 1)
            {
                //将头元素开始后移
                Node<T> q = new Node<T>(item);
                q.Next = head;
                head = q;
                return;
            }

            //定义两个空节点
            Node<T> p = head;
            Node<T> r = new Node<T>();
            int j = 1;

            //
            while (p.Next != null && j < i)
            {
                r = p;
                p = p.Next;
                ++j;
            }

            if (j == i)
            {
                Node<T> q = new Node<T>(item);
                q.Next = p;
                r.Next = q;
            }

        }

        //在单链表的第i个结点的位置后插入一个值为item的结点
        public void InsertNext(T item, int i)
        {
            if (IsEmpty() || i < 1)
            {
                //Console.WriteLine("List is empty or Position is error!");
                throw new Exception("List is empty or Position is error!");
                return;
            }

            if (i == 1)
            {
                Node<T> q = new Node<T>(item);
               q.Next = head.Next;
                head.Next = q;
               //q.Next = head;
               // head = q;
                return;
            }

            Node<T> p = head;
            int j = 1;

            while (p != null && j < i)
            {
                p = p.Next;
                ++j;
            }

            if (j == i)
            {
                Node<T> q = new Node<T>(item);
                q.Next = p.Next;
                p.Next = q;
            }

        }

        //删除单链表的第i个结点
        public T Delete(int i)
        {
            if (IsEmpty() || i < 0)
            {
                Console.WriteLine("Link is empty or Position is error!");
                return default(T);
            }

            Node<T> q = new Node<T>();

            if (i == 1)
            {
                q = head;
                head = head.Next;
                return q.Data;
            }

            Node<T> p = head;
            int j = 1;

            //从头指针位置开始移动位置，一直移动到所需要位置的前一个位置
            while (p.Next != null && j < i)
            {
                ++j;
                q = p;
                p = p.Next;
            }

            //开始赋值处理
            if (j == i)
            {
                //把中间那个节点给移除啦
                //
                q.Next = p.Next;
                return p.Data;
            }
            else
            {
                throw  new Exception("The ith node is not exist!");
                return default(T);
            }
        }

        //获得单链表的第i个数据元素
        public T GetElem(int i)
        {
            if (IsEmpty())
            {
                throw new NullReferenceException("List is empty!");
                return default(T);
            }

            Node<T> p = new Node<T>();
            p = head;
            int j = 1;
            while (p.Next != null && j < i)
            {
                ++j;
                p = p.Next;
            }

            if (j == i)
            {
                return p.Data;
            }
            else
            {
                //没有找到节点
                throw  new KeyNotFoundException("The ith node is not exist!");
                return default(T);
            }
        }

        /// <summary>
        /// 在单链表中查找值为value的结点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Locate(T value)
        {
            if (IsEmpty())
            {
                throw  new Exception("List is Empty!");
                return -1;
            }

            Node<T> p = new Node<T>();
            p = head;
            int i = 1;

            while (!p.Data.Equals(value) && p.Next != null)
            {
                p = p.Next;
                ++i;
            }

            return i;
        }

        //显示信息
        public void Display()
        {
            Node<T> p = new Node<T>();
            p = this.head;

            while (p != null)
            {
                HttpContext.Current.Response.Write(p.Data+",");
                p = p.Next;
            }
            HttpContext.Current.Response.Write("<br />");
        }


        public  T Add(T data)
        {
            //新生成一个链表节点
            Node<T> current=new Node<T>(data);
            Node<T> newNode = head;

            if(head==null)
            {
                head = current;
                return head.Data;
            }

            //int j = 1;

            while (newNode.Next!=null)
            {
                newNode = newNode.Next;
            }

            newNode.Next = current;

            return newNode.Next.Data;

        }

    }



    //链表的应用
    public class LinkListApplication
    {

        //在头部插入结点建立单链表的算法如下：
        public static LinkList<int> CreateLListHead()
        {
            int d;
            LinkList<int> L = new LinkList<int>();
            d = Int32.Parse(Console.ReadLine());

            while (d != -1)
            {
                Node<int> p = new Node<int>(d);
                p.Next = L.Head;
                L.Head = p;
                d = Int32.Parse(Console.ReadLine());
            }

            return L;
        }


        //在尾部插入结点建立单链表的算法如下：
        public static LinkList<int> CreateListTail()
        {
            Node<int> R = new Node<int>();
            int d;

            LinkList<int> L = new LinkList<int>();
            R = L.Head;
            d = Int32.Parse(Console.ReadLine());

            while (d != -1)
            {
                Node<int> p = new Node<int>(d);
                if (L.Head == null)
                {
                    L.Head = p;
                }
                else
                {
                    R.Next = p;
                }
                R = p;
                d = Int32.Parse(Console.ReadLine());
            }

            if (R != null)
            {
                R.Next = null;
            }

            return L;
        }


        //将两表合并成一表的算法实现如下：
        public LinkList<int> Merge(LinkList<int> Ha, LinkList<int> Hb)
        {
            LinkList<int> Hc = new LinkList<int>();
            Node<int> s = new Node<int>();
            Node<int> p = Ha.Head.Next;
            Node<int> q = Hb.Head.Next;

            Hc = Ha;
            Hc.Head.Next = null;

            while (p != null && q != null)
            {
                if (p.Data < q.Data)
                {
                    s = p;
                    p = p.Next;
                }
                else
                {
                    s = q;
                    q = q.Next;
                }

                Hc.Append(s.Data);
            }

            if (p == null)
            {
                p = q;
            }

            while (p != null)
            {
                s = p;
                p = p.Next;
                Hc.Append(s.Data);
            }

            return Hc;
        }


        //删除单链表中相同值的结点的算法实现如下：
        public LinkList<int> Purge(LinkList<int> Ha)
        {
            LinkList<int> Hb = new LinkList<int>();
            Node<int> p = Ha.Head.Next;
            Node<int> q = new Node<int>();
            Node<int> s = new Node<int>();

            s = p;
            p = p.Next;
            s.Next = null;
            Hb.Head.Next = s;

            while (p != null)
            {
                s = p;
                p = p.Next;
                q = Hb.Head.Next;

                while (q != null && q.Data != s.Data)
                {
                    q = q.Next;
                }

                if (q == null)
                {
                    s.Next = Hb.Head.Next;
                    Hb.Head.Next = s;
                }

            }

            return Hb;
        }
    }



    #endregion


}