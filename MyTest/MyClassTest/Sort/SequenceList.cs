using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTest.MyClassTest
{

    #region 定义一个顺序表的存储结构
    ///<summary>
    /// 定义一个顺序表的存储结构
    ///</summary>
    public class SeqListType<T>
    {
        private const int maxSize = 100;
        public int MaxSize { get { return maxSize; } }
        //数据为100个存储空间
        public T[] ListData = new T[maxSize];
        public int ListLen { get; set; }
    }
    #endregion


    public class SequenceList
    {
        //static void Main(string[] args)
        //{
        //    SeqList seq = new SeqList();
        //    SeqListType<Student2> list = new SeqListType<Student2>();
        //    Console.WriteLine("\n********************** 添加二条数据 ************************\n");
        //    seq.SeqListAdd<Student2>(list, new Student2() { ID = "1", Name = "一线码农", Age = 23 });
        //    seq.SeqListAdd<Student2>(list, new Student2() { ID = "3", Name = "huangxincheng520", Age = 23 });
        //    Console.WriteLine("添加成功");
        //    //展示数据
        //    Display(list);
        //    Console.WriteLine("\n********************** 正在搜索Name=“一线码农”的实体 ************************\n");
        //    var Student2 = seq.SeqListFindByKey<Student2, string>(list, "一线码农", s => s.Name);
        //    Console.WriteLine("\n********************** 展示一下数据 ************************\n");
        //    if (Student2 != null)
        //        Console.WriteLine("ID:" + Student2.ID + ",Name:" + Student2.Name + ",Age:" + Student2.Age);
        //    else
        //        Console.WriteLine("对不起，数据未能检索到。");
        //    Console.WriteLine("\n********************** 插入一条数据 ************************\n");
        //    seq.SeqListInsert(list, 1, new Student2() { ID = "2", Name = "博客园", Age = 40 });
        //    Console.WriteLine("插入成功");
        //    //展示一下
        //    Display(list);
        //    Console.WriteLine("\n********************** 删除一条数据 ************************\n");
        //    seq.SeqListDelete(list, 0);
        //    Console.WriteLine("删除成功");
        //    //展示一下数据
        //    Display(list);
        //    Console.Read();
        //}

        ///<summary>
        /// 展示输出结果
        ///</summary>
        static void Display(SeqListType<Student2> list)
        {
            Console.WriteLine("\n********************** 展示一下数据 ************************\n");
            if (list == null || list.ListLen == 0)
            {
                Console.WriteLine("呜呜，没有数据");
                return;
            }
            for (int i = 0; i < list.ListLen; i++)
            {
                Console.WriteLine("ID:" + list.ListData[i].ID + ",Name:" + list.ListData[i].Name + ",Age:" + list.ListData[i].Age);
            }
        }
    }

    #region 学生的数据结构
    ///<summary>
    /// 学生的数据结构
    ///</summary>
    public class Student2
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    #endregion



    #region 顺序表的相关操作
    ///<summary>
    ///顺序表的相关操作
    ///</summary>
    public class SeqList
    {
        #region 顺序表初始化
        ///<summary>
        /// 顺序表初始化
        ///</summary>
        ///<param name="t"></param>
        public void SeqListInit<T>(SeqListType<T> t)
        {
            t.ListLen = 0;
        }
        #endregion

        #region 顺序表的长度
        ///<summary>
        /// 顺序表的长度
        ///</summary>
        ///<param name="t"></param>
        ///<returns></returns>
        public int SeqListLen<T>(SeqListType<T> t)
        {
            return t.ListLen;
        }
        #endregion

        #region 顺序表的添加
        ///<summary>
        ///顺序表的添加
        ///</summary>
        ///<param name="t"></param>
        ///<returns></returns>
        public bool SeqListAdd<T>(SeqListType<T> t, T data)
        {
            //防止数组溢出
            if (t.ListLen == t.MaxSize)
                return false;
            t.ListData[t.ListLen++] = data;
            return true;
        }
        #endregion

        #region 顺序表的插入操作
        ///<summary>
        /// 顺序表的插入操作
        ///</summary>
        ///<param name="t"></param>
        ///<param name="n"></param>
        ///<param name="data"></param>
        ///<returns></returns>
        public bool SeqListInsert<T>(SeqListType<T> t, int n, T data)
        {
            //首先判断n是否合法
            if (n < 0 || n > t.MaxSize - 1)
                return false;
            //说明数组已满，不能进行插入操作
            if (t.ListLen == t.MaxSize)
                return false;
            //需要将插入点的数组数字依次向后移动
            for (int i = t.ListLen - 1; i >= n; i--)
            {
                t.ListData[i + 1] = t.ListData[i];
            }

            //最后将data插入到腾出来的位置
            t.ListData[n] = data;
            t.ListLen++;
            return true;
        }
        #endregion

        #region 顺序表的删除操作
        ///<summary>
        /// 顺序表的删除操作
        ///</summary>
        ///<param name="t"></param>
        ///<param name="n"></param>
        ///<returns></returns>
        public bool SeqListDelete<T>(SeqListType<T> t, int n)
        {
            //判断删除位置是否非法
            if (n < 0 || n > t.ListLen - 1)
                return false;
            //判断数组是否已满
            if (t.ListLen == t.MaxSize)
                return false;
            //将n处后的元素向前移位
            for (int i = n; i < t.ListLen; i++)
                t.ListData[i] = t.ListData[i + 1];
            //去掉数组最后一个元素
            --t.ListLen;
            return true;
        }
        #endregion

        #region 顺序表的按序号查找
        ///<summary>
        /// 顺序表的按序号查找
        ///</summary>
        ///<param name="t"></param>
        ///<param name="n"></param>
        ///<returns></returns>
        public T SeqListFindByNum<T>(SeqListType<T> t, int n)
        {
            if (n < 0 || n > t.ListLen - 1)
                return default(T);
            return t.ListData[n];
        }
        #endregion

        #region  顺序表的关键字查找
        ///<summary>
        /// 顺序表的关键字查找
        ///</summary>
        ///<typeparam name="T"></typeparam>
        ///<typeparam name="W"></typeparam>
        ///<param name="t"></param>
        ///<param name="key"></param>
        ///<param name="where"></param>
        ///<returns></returns>
        public T SeqListFindByKey<T, W>(SeqListType<T> t, string key, Func<T, W> where) where W : IComparable
        {

            for (int i = 0; i < t.ListLen; i++)
            {
                if (where(t.ListData[i]).CompareTo(key) == 0)
                {
                    return t.ListData[i];
                }
            }
            return default(T);
        }
        #endregion
    }
    #endregion
}