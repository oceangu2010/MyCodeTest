using System;
using System.Web;

namespace MyTest.MyClassTest.CycleLink
{
    /*   双向链表的原理  
     双链表同单链表的不同在于结点类增加了一个“前驱结点”属性，此外，给双链表增加一个“尾结点”的属性。
     在获取某一结点对象时，双链表同单链表几乎没有区别，函数同单链表相似。但在添加结点、插入结点、
     删除结点、倒序排列等功能上不同。
     其中，由于双链表有尾结点属性，在末尾添加结点会更加方便，时间复杂度为O(1)，而单链表时间复杂度为O(n)。
     插入结点、删除结点的原理与单链表的相关操作相似，只是增加前驱结点的设置。删除结点时，
     由于被删除结点的前驱和后继都设置为null，引用数目为0，GC会自动回收资源。
     在这里，要求链表的头结点的前驱结点为null，这既符合常理，另外，如果不设置为null，则前驱结点仍有引用，
     GC无法自动回收，造成额外开销。
     增加了一个尾节点与前驱节点

     注：如果把双链表再做一下改造，让头尾接起来，即Head的Prev属性指向最后一个节点(就叫做Tail吧)，
     同时把Tail节点的Next属性指向Head节点，就形成了所谓的“循环双向链表”
     当然，这样的结构可以在链表中再增加一个Tail节点属性，在做元素插入或删除时，
     可以循环到底以更新尾节点Tail(当然这样会给插入/删除元素带来一些额外的开销)，
     但是却可以给GetItemAt(int i)方法带来优化的空间，比如要查找的元素在前半段时，
     可以从Head开始用next向后找；反之，如果要找的元素在后半段，则可以从Tail节点用prev属性向前找。
     注：.Net中微软已经给出了一个内置的双向链表System.Collections.Generic.LinkedList<T>，
     在了解双链表的原理后，建议大家直接系统内置的链表。


     */

    /// <summary>
    /// 双向链表节点类
    /// </summary>
    /// <typeparam name="T">节点中的存放的数据类型</typeparam>
    public class Node<T> where T : IComparable<T>
    {
        /// <summary>
        /// 当前节点的数据
        /// </summary>
        private T data;

        /// <summary>
        /// 当前节点的下一个节点
        /// </summary>
        private Node<T> next;

        /// <summary>
        /// 当前节点的上一个节点
        /// </summary>
        private Node<T> prev;

        /// <summary>
        /// 无参构造：数据为默认值，下一个节点为null，上一个节点也为null
        /// </summary>
        public Node()
        {
            data = default(T);
            next = null;
            prev = null;
        }

        /// <summary>
        /// 构造方法：数据为传过来的t，下一个节点为null，上一个节点也为null
        /// </summary>
        /// <param name="t">传入的元素值</param>
        public Node(T t)
        {
            data = t;
            next = null;
            prev = null;
        }

        /// <summary>
        /// 构造方法：数据为t，下一个节点为node
        /// </summary>
        /// <param name="t">传入的元素值</param>
        /// <param name="next">上一个节点</param>
        /// <param name="prev">下一个节点</param>
        public Node(T t, Node<T> next, Node<T> prev)
        {
            data = t;
            this.next = next;
            this.prev = prev;
        }

        /// <summary>
        /// 节点中存放的数据
        /// </summary>
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// 下一个节点
        /// </summary>
        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }

        /// <summary>
        /// 上一个节点
        /// </summary>
        public Node<T> Prev
        {
            get { return prev; }
            set { prev = value; }
        }


        /// <summary>
        /// 此方法在调试过程中使用，可以删掉
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            T p = prev == null ? default(T) : prev.data;
            T n = next == null ? default(T) : next.data;
            string s = string.Format("Data:{0},Prev:{1},Next:{2}", data, p, n);
            return s;
        }
    }


    /// <summary>
    /// 双向链表接口
    /// </summary>
    /// <typeparam name="T">链表中元素的类型</typeparam>
    public interface ILinkList<T> where T : IComparable<T>
    {
        int Count { get; }
        Node<T> Head { get; set; }
        Node<T> Tail { get; set; } //指针
        bool IsEmpty { get; }
        Node<T> this[int index] { get; }
        void AddFirst(T t);
        void AddLast(T t);
        void Clear();
        void Insert(int index, T t);
        void RemoveAt(int index);
        void RemoveFirst();
        void RemoveLast();
    }


    /// <summary>
    /// 双向链表操作类
    /// </summary>
    /// <typeparam name="T">链表中元素的类型</typeparam>
    public class LinkList<T> : ILinkList<T> where T : IComparable<T>
    {
        /// <summary>
        /// 链表头节点
        /// </summary>
        private Node<T> head;

        /// <summary>
        /// 链表大小
        /// </summary>
        private int size;

        /// <summary>
        /// 链表尾节点
        /// </summary>
        private Node<T> tail;

        #region ILinkList<T> Members

        /// <summary>
        /// 链表头节点
        /// </summary>
        public Node<T> Head
        {
            get { return head; }
            set { head = value; }
        }

        /// <summary>
        /// 链表尾节点
        /// </summary>
        public Node<T> Tail
        {
            get { return tail; }
            set { tail = value; }
        }


        /// <summary>
        /// 添加节点到链表的开头
        /// </summary>
        /// <param name="t">要添加的数据</param>
        public void AddFirst(T t)
        {
            var node = new Node<T>(t);

            //如果头为null
            if (head == null)
            {
                //把头节点设置为node
                head = node;
                //因为是空链表，所以头尾一致
                tail = node;
                //大小加一
                size++;
                return;
            }
            //原来头节点的上一个为新节点
            head.Prev = node;
            //新节点的下一个为原来的头节点
            node.Next = head;
            //新头节点为新节点
            head = node;
            //大小加一
            size++;
        }


        /// <summary>
        /// 添加节点到链表的末尾
        /// </summary>
        /// <param name="t">要添加的数据</param>
        public void AddLast(T t)
        {
            var node = new Node<T>(t);

            //如果头为null
            if (head == null)
            {
                //把头节点设置为node
                head = node;
                //因为是空链表，所以头尾一致
                tail = node;
                //大小加一
                size++;
                return;
            }

            //将原尾节点的下一个设置为新节点
            tail.Next = node;

            //将新节点的上一个设置为原尾节点
            node.Prev = tail;

            //将尾节点重新设置为新节点
            tail = node;
            //大小加一
            size++;
        }


        /// <summary>
        /// 在给定的索引处插入数据
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="t">要插入的数据</param>
        public void Insert(int index, T t)
        {
            var node = new Node<T>(t);

            //索引过小
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            //索引过大
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            //如果链表是空的，而且索引大于0
            if (IsEmpty && index > 0)
            {
                throw new IndexOutOfRangeException();
            }

            //如果索引为0，意味着向链表头部添加节点。
            if (index == 0)
            {
                AddFirst(t);
                return;
            }

            //要插入位置的节点
            Node<T> current = head;
            int i = 0;
            while (true)
            {
                if (i == index)
                {
                    break;
                }
                i++;
                current = current.Next;
            }

            //此处非常重要，特别要注意先后次序
            //当前节点的上一个的下一个设置为新节点
            current.Prev.Next = node;

            //新节点的上一个设置为当前节点的上一个
            node.Prev = current.Prev;

            //新节点的下一个设置为当前节点
            node.Next = current;

            //当前节点的上一个设置为新节点
            current.Prev = node;

            //大小加一
            size++;
        }


        /// <summary>
        /// 移除链表中的节点
        /// </summary>
        /// <param name="index">要移除的节点的索引</param>
        public void RemoveAt(int index)
        {
            //链表头节点是空的
            if (IsEmpty)
            {
                throw new Exception("链表是空的。");
            }

            //索引过小
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            //索引过大
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            //如果要移除的是头节点
            if (index == 0)
            {
                RemoveFirst();
                return;
            }

            if (index == size - 1)
            {
                RemoveLast();
                return;
            }

            //要移除的节点
            Node<T> current = head;
            int i = 0;
            while (true)
            {
                if (i == index)
                {
                    break;
                }
                i++;
                current = current.Next;
            }

            //当前节点的上一个的Next设置为当前节点的Next
            current.Prev.Next = current.Next;
            //当前节点的下一个的Prev设置为当前节点的Prev
            current.Next.Prev = current.Prev;
            //大小减一
            size--;
        }


        /// <summary>
        /// 移除头节点
        /// </summary>
        public void RemoveFirst()
        {
            //链表头节点是空的
            if (IsEmpty)
            {
                throw new Exception("链表是空的。");
            }

            //如果size为1，那就是清空链表。
            if (size == 1)
            {
                Clear();
                return;
            }

            //将头节点设为原头结点的下一个节点，就是下一个节点上移
            head = head.Next;

            //处理上一步遗留问题，原来的第二个节点的上一个是头结点，现在第二个要变成头节点，那要把它的Prev设为null才能成为头节点
            head.Prev = null;
            //大小减一
            size--;
        }


        /// <summary>
        /// 移除尾节点
        /// </summary>
        public void RemoveLast()
        {
            //链表头节点是空的
            if (IsEmpty)
            {
                throw new Exception("链表是空的。");
            }

            //如果size为1，那就是清空链表。
            if (size == 1)
            {
                Clear();
                return;
            }

            //尾节点设置为倒数第二个节点
            tail = tail.Prev;
            //将新尾节点的Next设为null，表示它是新的尾节点
            tail.Next = null;
            //大小减一
            size--;
        }


        /// <summary>
        /// 判断链表是否是空的
        /// </summary>
        public bool IsEmpty
        {
            get { return head == null; }
        }


        //显示信息
        public void Display()
        {
            Node<T> p = new Node<T>();
            p = this.head;

            while (p != null)
            {
                HttpContext.Current.Response.Write(p.Data + ",");
                p = p.Next;
            }
            HttpContext.Current.Response.Write("<br />");
        }


        /// <summary>
        /// 链表中元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                ////也可以采用遍历的方法获得长度，遍历可以从前向后，也可以从后向前
                //int count = 0;
                ////取得链表头部节点
                //Node<T> current = new Node<T>();
                //current = head;
                ////遍历整个链表，直到最后一个Next为null的节点为止
                //while (current!=null)
                //{
                //    count++;
                //    current = current.Next;
                //}
                //return count;
                return size;
            }
        }


        /// <summary>
        /// 清除链表中的数据
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            size = 0;
        }


        /// <summary>
        /// 根据索引获取链表中的节点
        /// </summary>
        /// <param name="index">整型索引</param>
        /// <returns>节点</returns>
        public Node<T> this[int index]
        {
            get
            {
                //链表头节点是空的
                if (head == null)
                {
                    throw new Exception("链表是空的。");
                }

                //索引过小
                if (index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                //索引过大
                if (index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }

                //取得头节点
                var current = new Node<T>();


                //current = head;
                //int i = 0;
                ////遍历链表
                //while (true)
                //{
                //    //找到第index个节点
                //    if (i == index)
                //    {
                //        break;
                //    }
                //    current = current.Next;
                //    i++;
                //}
                //return current;


                //如果索引在前一半，那么从前向后找
                if (index < size/2)
                {
                    current = head;
                    int i = 0;

                    //遍历链表
                    while (true)
                    {
                        //找到第index个节点
                        if (i == index)
                        {
                            break;
                        }
                        current = current.Next;
                        i++;
                    }

                    return current;
                }
                else //如果索引在后一半，那么从后向前找
                {
                    current = tail;
                    int i = size;

                    //遍历链表
                    while (true)
                    {
                        //找到第index个节点
                        if (i == index)
                        {
                            break;
                        }
                        current = current.Prev;
                        i--;
                    }

                    return current.Next;
                }
            }
        }

        #endregion

        /// <summary>
        /// 选择排序，更接近数组的选择排序
        /// 找到一个最小的节点，将其放入新
        /// 链表，然后删掉这个最小的节点，
        /// 让后重新找最小节点，直到原链表
        /// 中的节点删完为止。
        /// </summary>
        public void SelectSort1()
        {
            if (IsEmpty || size == 1)
            {
                return;
            }


            //新链表
            Node<T> newHead = null;
            //新链表尾部
            Node<T> newTail = null;
            //最小节点
            Node<T> min = null;
            //最小节点的前一个节点
            Node<T> minPrev = null;
            //循环变量
            Node<T> i = null;


            while (head != null)
            {
                //取第一个作为最小节点
                min = head;
                for (i = head; i.Next != null; i = i.Next)
                {
                    //如果下一个比最小的还小
                    if (i.Next.Data.CompareTo(min.Data) < 0)
                    {
                        //先将i放入最小节点的前节点中
                        minPrev = i;
                        //将最小节点放入min中
                        min = i.Next;
                    }
                }


                //将找到的min在原始链表中除去
                //如果最小节点就是头节点
                //那么应该让头节点下移一位
                //作用是让min独立出来
                //这一个if必须在下一个if之上
                //也就是要贯彻先删除，再添加的政策
                if (min.Data.CompareTo(head.Data) == 0)
                {
                    head = head.Next;
                }
                else
                {
                    //否则将最小节点的下一个作为作为最小节点的上一个的下一个
                    minPrev.Next = min.Next;
                }

                //如果是新链表中的第一个
                if (newHead == null)
                {
                    //将头置为最小节点
                    newHead = min;
                    //因为只有一个节点，所以头节点的Next和Prev都是null
                    newHead.Prev = null;
                    newHead.Next = null;
                    //将尾也置为最小节点
                    newTail = min;
                    //因为只有一个节点，所以尾节点的Next和Prev都是null
                    newTail.Next = null;
                    newTail.Prev = null;
                }
                else
                {
                    //如果不是第一个节点
                    //那么就在尾上添加第n小节点
                    //新尾节点的下一个是第n小的节点
                    newTail.Next = min;
                    //第n小的节点的上一个就是新尾节点
                    min.Prev = newTail;
                    //将新尾节点重新置为第n小节点
                    newTail = min;
                    //尾节点么，当然Next是null了
                    newTail.Next = null;
                }
            }

            //将最后得到的新的头节点赋给原头节点
            head = newHead;
            //将最后得到的新的尾节点赋给原尾节点
            tail = newTail;
        }
    }
}