using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public class SinglyLinkList
    {

        //static void Main(string[] args)
        //{
        //    ChainList chainList = new ChainList();

        //    Node<Student> node = null;

        //    Console.WriteLine("将三条数据添加到链表的尾部:\n");

        //    //将数据添加到链表的尾部
        //    node = chainList.ChainListAddEnd(node, new Student() { ID = 2, Name = "hxc520", Age = 23 });
        //    node = chainList.ChainListAddEnd(node, new Student() { ID = 3, Name = "博客园", Age = 33 });
        //    node = chainList.ChainListAddEnd(node, new Student() { ID = 5, Name = "一线码农", Age = 23 });

        //    Dispaly(node);

        //    Console.WriteLine("将ID=1的数据插入到链表开头:\n");

        //    //将ID=1的数据插入到链表开头
        //    node = chainList.ChainListAddFirst(node, new Student() { ID = 1, Name = "i can fly", Age = 23 });

        //    Dispaly(node);

        //    Console.WriteLine("查找Name=“一线码农”的节点\n");

        //    //查找Name=“一线码农”的节点
        //    var result = chainList.ChainListFindByKey(node, "一线码农", i => i.Name);

        //    DisplaySingle(node);

        //    Console.WriteLine("将”ID=4“的实体插入到“博客园”这个节点的之后\n");

        //    //将”ID=4“的实体插入到"博客园"这个节点的之后
        //    node = chainList.ChainListInsert(node, "博客园", i => i.Name, new Student() { ID = 4, Name = "51cto", Age = 30 });

        //    Dispaly(node);

        //    Console.WriteLine("删除Name=‘51cto‘的节点数据\n");

        //    //删除Name=‘51cto‘的节点数据
        //    node = chainList.ChainListDelete(node, "51cto", i => i.Name);

        //    Dispaly(node);

        //    Console.WriteLine("获取链表的个数:" + chainList.ChanListLength(node));
        //}



        //输出数据
        public static void Dispaly(Node<StudentSinglyLink> head)
        {
            Console.WriteLine("******************* 链表数据如下 *******************");
            var tempNode = head;
 
            while (tempNode != null)
            {
                Console.WriteLine("ID:" + tempNode.data.ID + ", Name:" + tempNode.data.Name + ",Age:" + tempNode.data.Age);
                tempNode = tempNode.next;
            }
 
            Console.WriteLine("******************* 链表数据展示完毕 *******************\n");
        }
 
        //展示当前节点数据
        public static void DisplaySingle(Node<StudentSinglyLink> head)
        {
            if (head != null)
                Console.WriteLine("ID:" + head.data.ID + ", Name:" + head.data.Name + ",Age:" + head.data.Age);
            else
                Console.WriteLine("未查找到数据！");
        }

 
     #region 学生数据实体
     /// <summary>
 /// 学生数据实体
 /// </summary>
     public class StudentSinglyLink
     {
         public int ID { get; set; }
 
         public string Name { get; set; }
 
         public int Age { get; set; }
     }
     #endregion
 
     #region 链表节点的数据结构
     /// <summary>
 /// 链表节点的数据结构
 /// </summary>
     public class Node<T>
     {
         /// <summary>
 /// 节点的数据域
 /// </summary>
         public T data;
 
         /// <summary>
 /// 节点的指针域
 /// </summary>
         public Node<T> next;
     }
     #endregion
 
     #region 链表的相关操作
     /// <summary>
 /// 链表的相关操作
 /// </summary>
     public class ChainList
     {
         #region 将节点添加到链表的末尾
         /// <summary>
 /// 将节点添加到链表的末尾
 /// </summary>
 /// <typeparam name="T"></typeparam>
 /// <param name="head"></param>
 /// <param name="data"></param>
 /// <returns></returns>
         public Node<T> ChainListAddEnd<T>(Node<T> head, T data)
         {
             Node<T> node = new Node<T>();
 
             node.data = data;
             node.next = null;
 
             ///说明是一个空链表
             if (head == null)
             {
                 head = node;
                 return head;
             }
 
             //获取当前链表的最后一个节点
             ChainListGetLast(head).next = node;
 
             return head;
         }
         #endregion
 
         #region 将节点添加到链表的开头
         /// <summary>
 /// 将节点添加到链表的开头
 /// </summary>
 /// <typeparam name="T"></typeparam>
 /// <param name="chainList"></param>
 /// <param name="data"></param>
 /// <returns></returns>
         public Node<T> ChainListAddFirst<T>(Node<T> head, T data)
         {
             Node<T> node = new Node<T>();
 
             node.data = data;
             node.next = head;
 
             head = node;
 
             return head;
 
         }
         #endregion
 
         #region 将节点插入到指定位置
         /// <summary>
 /// 将节点插入到指定位置
 /// </summary>
 /// <typeparam name="T"></typeparam>
 /// <param name="head"></param>
 /// <param name="currentNode"></param>
 /// <param name="data"></param>
 /// <returns></returns>
         public Node<T> ChainListInsert<T, W>(Node<T> head, string key, Func<T, W> where, T data) where W : IComparable
         {
             if (head == null)
                 return null;
 
             if (where(head.data).CompareTo(key) == 0)
             {
                 Node<T> node = new Node<T>();
 
                 node.data = data;
 
                 node.next = head.next;
 
                 head.next = node;
             }
 
             ChainListInsert(head.next, key, where, data);
 
             return head;
         }
         #endregion
 
         #region 将指定关键字的节点删除
         /// <summary>
 /// 将指定关键字的节点删除
 /// </summary>
 /// <typeparam name="T"></typeparam>
 /// <typeparam name="W"></typeparam>
 /// <param name="head"></param>
 /// <param name="key"></param>
 /// <param name="where"></param>
 /// <param name="data"></param>
 /// <returns></returns>
         public Node<T> ChainListDelete<T, W>(Node<T> head, string key, Func<T, W> where) where W : IComparable
         {
             if (head == null)
                 return null;
 
             //这是针对只有一个节点的解决方案
             if (where(head.data).CompareTo(key) == 0)
             {
                 if (head.next != null)
                     head = head.next;
                 else
                     return head = null;
             }
             else
             {
                 //判断一下此节点是否是要删除的节点的前一节点
                 if (head.next != null && where(head.next.data).CompareTo(key) == 0)
                 {
                     //将删除节点的next域指向前一节点
                     head.next = head.next.next;
                 }
             }
 
             ChainListDelete(head.next, key, where);
 
             return head;
         }
         #endregion
 
         #region 通过关键字查找指定的节点
         /// <summary>
 /// 通过关键字查找指定的节点
 /// </summary>
 /// <typeparam name="T"></typeparam>
 /// <typeparam name="W"></typeparam>
 /// <param name="head"></param>
 /// <param name="key"></param>
 /// <param name="where"></param>
 /// <returns></returns>
         public Node<T> ChainListFindByKey<T, W>(Node<T> head, string key, Func<T, W> where) where W : IComparable
         {
             if (head == null)
                 return null;
 
             if (where(head.data).CompareTo(key) == 0)
                 return head;
 
             return ChainListFindByKey<T, W>(head.next, key, where);
         }
         #endregion
 
         #region 获取链表的长度
         /// <summary>
 ///// 获取链表的长度
 /// </summary>
 /// <typeparam name="T"></typeparam>
 /// <param name="head"></param>
 /// <returns></returns>
         public int ChanListLength<T>(Node<T> head)
         {
             int count = 0;
 
             while (head != null)
             {
                 ++count;
                 head = head.next;
             }
 
             return count;
         }
         #endregion
 
         #region 得到当前链表的最后一个节点

         /// <summary>
         /// 得到当前链表的最后一个节点
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="head"></param>
         /// <returns></returns>
         public Node<T> ChainListGetLast<T>(Node<T> head)
         {
             if (head.next == null)
                 return head;
             return ChainListGetLast(head.next);
         }
         #endregion
 
     }
     #endregion

    
    }


}           