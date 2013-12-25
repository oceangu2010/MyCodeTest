using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTest.MyClassTest.SingleLink;


namespace MyTest.MyClassTest
{

        /// <summary>
        /// 循环链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class LoopLink<T> : IListDS<T>
        {
            /// <summary>
            /// 链表头属性
            /// </summary>
            private LoopLinkNode<T> head;
            public LoopLinkNode<T> Head
            {
                set
                {
                    head = value;
                    head.Next = head;
                }
                get { return head; }
            }
            /// <summary>
            /// 构造函数，构造空链表
            /// </summary>
            public LoopLink()
            {
                this.head = null;
            }
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="head"></param>
            public LoopLink(LoopLinkNode<T> head)
            {
                this.head = head;
                this.head.Next = this.head;
            }
            /// <summary>
            /// 实现索引器
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public LoopLinkNode<T> this[int index]
            {
                set
                {
                    if (IsEmpty())
                        throw new Exception("链表为空");
                    if (index < 0 || index > this.GetLength() - 1)
                        throw new Exception("索引超出链表长度");
                    LoopLinkNode<T> node = head;
                    for (int i = 0; i < index; i++)
                    {
                        node = node.Next;
                    }
                    node.Data = value.Data;
                    node.Next = value.Next;
                }
                get
                {
                    if (IsEmpty())
                        throw new Exception("链表为空");
                    if (index < 0 || index > this.GetLength() - 1)
                        throw new Exception("索引超出链表长度");
                    LoopLinkNode<T> node = head;
                    for (int i = 0; i < index; i++)
                    {
                        node = node.Next;
                    }
                    return node;
                }
            }
            /// <summary>
            /// 获取链表长度
            /// </summary>
            /// <returns></returns>
            public int GetLength()
            {
                if (IsEmpty())
                    return 0;
                int length = 1;
                LoopLinkNode<T> temp = head;
                while (temp.Next != head)
                {
                    temp = temp.Next;
                    length++;
                }
                return length;
            }
            /// <summary>
            /// 清空链表所有元素
            /// </summary>
            public void Clear()
            {
                this.head = null;
            }
            /// <summary>
            /// 检查链表是否为空
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                if (head == null)
                    return true;
                return false;
            }
            /// <summary>
            /// 检查链表是否已满
            /// </summary>
            /// <returns></returns>
            public bool IsFull()
            {
                return false;
            }
            /// <summary>
            /// 在链表的最末端添加一个新项
            /// </summary>
            /// <param name="item"></param>
            public void Append(T item)
            {
                if (IsEmpty())
                {
                    this.Head = new LoopLinkNode<T>(item);
                    return;
                }
                LoopLinkNode<T> node = new LoopLinkNode<T>(item);
                LoopLinkNode<T> temp = head;
                while (temp.Next != head)
                {
                    temp = temp.Next;
                }
                temp.Next = node;
                node.Next = head;
            }
            /// <summary>
            /// 在链表的指定位置添加一个新项
            /// </summary>
            /// <param name="item"></param>
            /// <param name="index"></param>
            public void Insert(T item, int index)
            {
                if (IsEmpty())
                    throw new Exception("数据链表为空");
                if (index < 0 || index > this.GetLength())
                    throw new Exception("给定索引超出链表长度");
                LoopLinkNode<T> temp = new LoopLinkNode<T>(item);
                LoopLinkNode<T> node = head;
                if (index == 0)
                {
                    while (node.Next != head)
                    {
                        node = node.Next;
                    }
                    node.Next = temp;
                    temp.Next = this.head;
                    return;
                }
                for (int i = 0; i < index - 1; i++)
                {
                    node = node.Next;
                }
                LoopLinkNode<T> temp2 = node.Next;
                node.Next = temp;
                temp.Next = temp2;
            }
            /// <summary>
            /// 删除链表指定位置的项目
            /// </summary>
            /// <param name="index"></param>
            public void Delete(int index)
            {
                if (IsEmpty())
                    throw new Exception("链表为空，没有可清除的项");
                if (index < 0 || index > this.GetLength() - 1)
                    throw new Exception("给定索引超出链表长度");
                LoopLinkNode<T> node = head;
                if (index == 0)
                {
                    while (node.Next != head)
                    {
                        node = node.Next;
                    }
                    this.head = head.Next;
                    node.Next = this.head;
                    return;
                }
                for (int i = 0; i < index - 1; i++)
                {
                    node = node.Next;
                }
                node.Next = node.Next.Next;
            }
            /// <summary>
            /// 获取链表指定位置的元素的值
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T GetItem(int index)
            {
                if (index < 0 || index > this.GetLength() - 1)
                    throw new Exception("索引超出链表长度");
                LoopLinkNode<T> node = head;
                for (int i = 0; i < index; i++)
                {
                    node = node.Next;
                }
                return node.Data;
            }

            /// <summary>
            /// 根据给定的值查找链表中哪个元素为这个值，如果链表中存在两个元素值相同，则取排在链表前面的元素
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public int Locate(T value)
            {
                if (IsEmpty())
                    throw new Exception("链表为空");
                LoopLinkNode<T> node = head;
                int index = 0;
                while (node.Next != head)
                {
                    if (node.Data.Equals(value))
                        return index;
                    else
                        index++;
                    node = node.Next;
                }
                if (node.Data.Equals(value))
                    return index;
                else
                    return -1;
            }
        }

        public class LoopLinkNode<T>
        {
            private T data;
            private LoopLinkNode<T> next;
            public T Data
            {
                set { data = value; }
                get { return data; }
            }
            public LoopLinkNode<T> Next
            {
                set { next = value; }
                get { return next; }
            }
            public LoopLinkNode()
            {
                data = default(T);
                next = null;
            }
            public LoopLinkNode(T data)
            {
                this.data = data;
                next = null;
            }
            public LoopLinkNode(T data, LoopLinkNode<T> item)
            {
                this.data = data;
                this.next = item;
            }

    }
}