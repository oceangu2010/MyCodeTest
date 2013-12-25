using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeqQueue
{
    public class SeqQueue
    {
        //static void Main(string[] args)
        //{
        //    SeqQueue<Student> seqQueue = new SeqQueue<Student>();

        //    SeqQueueClass queueManage = new SeqQueueClass();

        //    Console.WriteLine("目前队列是否为空：" + queueManage.SeqQueueIsEmpty(seqQueue) + "\n");

        //    Console.WriteLine("将ID=1和ID=2的实体加入队列");
        //    queueManage.SeqQueueIn(seqQueue, new Student() { ID = 1, Name = "hxc520", Age = 23 });
        //    queueManage.SeqQueueIn(seqQueue, new Student() { ID = 2, Name = "一线码农", Age = 23 });

        //    Display(seqQueue);

        //    Console.WriteLine("将队头出队");
        //    //将队头出队
        //    var student = queueManage.SeqQueueOut(seqQueue);

        //    Display(seqQueue);

        //    //获取队顶元素
        //    student = queueManage.SeqQueuePeek(seqQueue);

        //    Console.Read();
        //}
        //展示队列元素
        static void Display(SeqQueue<Student> seqQueue)
        {
            Console.WriteLine("******************* 链表数据如下 *******************");

            for (int i = seqQueue.head; i < seqQueue.tail; i++)
                Console.WriteLine("ID:" + seqQueue.data[i].ID +
                                  ",Name:" + seqQueue.data[i].Name +
                                  ",Age:" + seqQueue.data[i].Age);

            Console.WriteLine("******************* 链表数据展示完毕 *******************\n");
        }
    }

    #region 学生数据实体
    /// <summary>
    /// 学生数据实体
    /// </summary>
    public class Student
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
    #endregion

    #region 队列的数据结构
    /// <summary>
    /// 队列的数据结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeqQueue<T>
    {
        private const int maxSize = 100;

        public int MaxSize
        {
            get { return maxSize; }
        }

        /// <summary>
        /// 顺序队列的存储长度
        /// </summary>
        public T[] data = new T[maxSize];

        //头指针
        public int head;

        //尾指针
        public int tail;

    }
    #endregion

    #region 队列的基本操作
    /// <summary>
    /// 队列的基本操作
    /// </summary>
    public class SeqQueueClass
    {
        #region 队列的初始化操作
        /// <summary>
        /// 队列的初始化操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seqQueue"></param>
        public SeqQueue<T> SeqQueueInit<T>(SeqQueue<T> seqQueue)
        {
            seqQueue.head = 0;
            seqQueue.tail = 0;

            return seqQueue;
        }
        #endregion

        #region 队列是否为空
        /// <summary>
        /// 队列是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seqQueue"></param>
        /// <returns></returns>
        public bool SeqQueueIsEmpty<T>(SeqQueue<T> seqQueue)
        {
            //如果两指针重合，说明队列已经清空
            if (seqQueue.head == seqQueue.tail)
                return true;
            return false;
        }
        #endregion

        #region 队列是否已满
        /// <summary>
        /// 队列是否已满
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seqQueue"></param>
        /// <returns></returns>
        public bool SeqQueueIsFull<T>(SeqQueue<T> seqQueue)
        {
            //如果尾指针到达数组末尾，说明队列已经满
            if (seqQueue.tail == seqQueue.MaxSize)
                return true;
            return false;
        }
        #endregion

        #region 队列元素入队
        /// <summary>
        /// 队列元素入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seqQueue"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public SeqQueue<T> SeqQueueIn<T>(SeqQueue<T> seqQueue, T data)
        {
            //如果队列已满，则不能进行入队操作
            if (SeqQueueIsFull(seqQueue))
                throw new Exception("队列已满,不能入队操作");

            //入队操作
            seqQueue.data[seqQueue.tail++] = data;

            return seqQueue;
        }
        #endregion

        #region 队列元素出队
        /// <summary>
        /// 队列元素出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seqQueue"></param>
        /// <returns></returns>
        public T SeqQueueOut<T>(SeqQueue<T> seqQueue)
        {
            if (SeqQueueIsEmpty(seqQueue))
                throw new Exception("队列已空，不能进行出队操作");

            var single = seqQueue.data[seqQueue.head];

            //head指针自增
            seqQueue.data[seqQueue.head++] = default(T);

            return single;

        }
        #endregion

        #region 获取队头元素
        /// <summary>
        /// 获取队头元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seqQueue"></param>
        /// <returns></returns>
        public T SeqQueuePeek<T>(SeqQueue<T> seqQueue)
        {
            if (SeqQueueIsEmpty(seqQueue))
                throw new Exception("队列已空，不能进行出队操作");

            return seqQueue.data[seqQueue.head];
        }
        #endregion

        /// <summary>
        /// 获取队列长度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seqQueue"></param>
        /// <returns></returns>
        public int SeqQueueLen<T>(SeqQueue<T> seqQueue)
        {
            return seqQueue.tail - seqQueue.head;
        }
    }
    #endregion
}