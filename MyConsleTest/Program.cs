using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Collections;

namespace MyConsleTest
{                     
    internal class Program
    {
        private static void Main(string[] args)
        {
           // string str = "dsldsdsasdsoewezssadsssASDSSDassdewcxcxca";
           //Console.WriteLine( str.Take(0));

            //B b = new B();
            //b.PrintFields();
            DictionaryTest();
            HashtableTest();

           // GetValue();
            //ThreadTask();
//            ArrTest();
//
//            ThreadTestTwo();
           // LimitAccess();

            Console.ReadKey();
        }

        static readonly SemaphoreSlim sSlim=new SemaphoreSlim(initialCount: Environment.ProcessorCount);
               
              
        #region many files download  from intenet
          //下载
         static void Downloads()
         {
             ////同时进行20个下载任务
             //for (int i = 0; i < 20; i++)
             //{
             //    var downloadTask = DownLoadFileInTask(
             //        new Uri(@"http://www.7720mm.cn/uploadfile/2010/1120/20101120073035736.jpg")
             //        , "C://1.jpg");

             //    downloadTask.ContinueWith(i => Console.WriteLine("图片:" + i.Result + "下载完毕！")); 
             //}
            
             Console.WriteLine("我是主线程，我不会被阻塞!");
             Console.Read();
         }
 
         static Task<string> DownLoadFileInTask(Uri address, string saveFile)
         {
             var wc = new WebClient();
 
             var tcs = new TaskCompletionSource<string>(address);
 
             //处理异步操作的一个委托
             AsyncCompletedEventHandler handler = null;
             handler = (sender, e) =>
             {
                 if (e.Error != null)
                 {
                     tcs.TrySetException(e.Error);
                 }
                 else
                 {
                     if (e.Cancelled)
                     {
                         tcs.TrySetCanceled();
                     }
                     else
                     {
                         tcs.TrySetResult(saveFile);
                     }
                 }
 
                 wc.DownloadFileCompleted -= handler;
             };
 
             //我们将下载事件与我们自定义的handler进行了关联
             wc.DownloadFileCompleted += handler;
 
             try
             {
                 wc.DownloadFileAsync(address, saveFile);
             }
             catch (Exception ex)
             {
                 wc.DownloadFileCompleted -= handler;
 
                 tcs.TrySetException(ex);
             }
 
             return tcs.Task;
         }


        #endregion


         #region  在趣算法题
         static void GetValue()
         {
             //string str = "dsldsdsasdsoewezssadsssASDSSDassdewcxcxca";
             //str.Take(5).SkipWhile((chr) => chr.Equals('a'));
               //商
                 int[] resultArr = { 111111, 222222, 333333, 444444, 555555, 666666, 777777, 888888, 999999 };
 
                 //除数
                 int[] numArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
 
                 int count = 0;
 
                 for (int i = 0; i < resultArr.Count(); i++)
                 {
                     for (int j = 0; j < numArr.Count(); j++)
                     {
                         count++;
 
                         var result = resultArr[i].ToString();
 
                         var num = numArr[j].ToString();
 
                         var origin = (resultArr[i] / numArr[j]).ToString();

                       

                         if (origin.LastOrDefault() == result.FirstOrDefault()
                             && origin.FirstOrDefault() == num.FirstOrDefault()
                             && result.Length - 1 == origin.Length)
                         {
                             Console.WriteLine("\n\n费了{0} 次，tmd找出来了", count);
                             Console.WriteLine("\n\n感谢一楼同学的回答。现在的时间复杂度已经降低到O(n2)，相比之前方案已经是秒杀级别\n");
 
                             Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}", origin.ElementAt(0), origin.ElementAt(1), origin.ElementAt(2), origin.ElementAt(3), origin.ElementAt(4));
                             Console.WriteLine("\n\n\tX\t\t\t\t{0}", num);
                             Console.WriteLine("—————————————————————————————");
                             Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}", result.ElementAt(0), result.ElementAt(0), result.ElementAt(0), result.ElementAt(0), result.ElementAt(0), result.ElementAt(0));
 
                             Console.Read();
                         }
                         Console.WriteLine("第{0}搜索", count);
                     }
                 }
                 Console.WriteLine("无解");
                 Console.Read();
             }

#endregion

         #region Dictionary测试

        /// <summary>
        /// Dictionary顺序测试
        /// </summary>
         static void DictionaryTest()
         {

             Dictionary<string, int> dd = new Dictionary<string, int>();
             for (var i = 0; i < 4;i++ )
             {
                 dd.Add(i.ToString(), i);
             }

             dd.Remove("2");

             dd.Add("4", 8);

             Console.WriteLine("Dictionary删除元素之后再添加新元素的顺序为:");
             foreach(var j in dd)
             {
                 Console.WriteLine(j);
             };
         
         }

         /// <summary>
         /// Hashtable存储顺序测试
         /// </summary>
         static void HashtableTest()
         {
             Hashtable ht = new Hashtable();
             for (var i = 0;  i<6; i++)
             {
                 ht.Add(i.ToString(), i);
             }

             ht.Remove("2");

             ht.Add("6",100);


             Console.WriteLine("Hashtable删除元素之后再添加新元素的顺序为:");
             foreach (var j in ht.Values)
             {
                 Console.WriteLine(j);
             };
         }

         #endregion

         //限制访问
        static  void LimitAccess()
        {
            for (int i = 0; i < 12; i++)
                Task.Factory.StartNew(Run, i);
        }

        static  void Run(Object obj)
        {
            sSlim.Wait();        
             Console.WriteLine("当前时间:{0}任务{1}",DateTime.Now,obj);
            sSlim.Release();
        }

        

        private static void ArrTest()
        {
            //查找数组元素
            int[] arr = {11, 22, 1, 2, 4, 27, 12, 111, 66};
            Array.Sort<int>(arr);
            int posintion = Array.BinarySearch(arr, 111);

            var bag = new ConcurrentBag<int>();
            var list = ParallelEnumerable.Range(0, 10000);
            list.ForAll(bag.Add); // ==list.ForAll((i)=>{bag.Add(i);});

            Console.WriteLine("bag集合中元素个数有:{0}", bag.Count);
            Console.WriteLine("list集合中元素个数总和为:{0}", list.Sum());
            Console.WriteLine("list集合中元素最大值为:{0}", list.Max());
            Console.WriteLine("list集合中元素第一个元素为:{0}", list.FirstOrDefault());
            Console.ReadKey();


        }
        


        #region 字符串比较

        private static int CompareString()
        {
            string name = "你好";
            name = String.Intern(name); //引用类型 取出指向字符串的指针地址

            const string splitStr = "中硬梆梆是厅大,你好,是0第四,季度世界大赛大,模大样你好扩大,你好,度要你好近代史" +
                                    "大模大样,大模大你好样在梦魇,剞当地时间,你好,你好,你好,你好,  厅电视,频道可你好因,歌厅固顶棒,,棒顶棚" +
                                    "近代,史J,ASKS你好SKMKS,AJS你好,你好,你好,你好,  厅大模,大样厅棒棒 ";
            string[] arr = splitStr.Split(',');
            //int count = 0;
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    if (Object.ReferenceEquals(name, arr[i]))
            //        count++;
            //}

            //return count;
            return arr.Count(t => Object.ReferenceEquals(name, String.Intern(t)));
        }

        #endregion



        #region 线程例子测试

        static  void ThreadTestTwo()
        {
            var stuList=new List<Student>()                                 
            {
                new Student(){ID = 1,Age = 22,CreateTime = DateTime.Now,Name = "张三"} ,
                new Student(){ID = 2,Age = 23,CreateTime = DateTime.Now,Name = "李四"}
            };

  var map = stuList.AsParallel().ToLookup(stu => stu.ID, count => 1);

            //化简统计       
            var reduce = from IGrouping<int, int> singleMap  in map.AsParallel()          
                         select new
                                    {
                                        Age = singleMap.Key,    
                                        Count = singleMap.Count()

                                    };       
            //最后遍历       
             reduce.ForAll(i => Console.WriteLine("当前Age={0}的人数有:{1}人", i.Age, i.Count));
             Console.ReadKey();
        }

       private static void ThreadTestOne()
        {
            var watch = Stopwatch.StartNew();

            watch.Start();

            Run1();

            Run2();

            Console.WriteLine("我是串行开发，总共耗时:{0}\n", watch.ElapsedMilliseconds);

            watch.Restart();

            Parallel.Invoke(Run1, Run2);

            watch.Stop();

            Console.WriteLine("我是并行开发，总共耗时:{0}", watch.ElapsedMilliseconds);

            Console.Read();

            Console.WriteLine(CompareString());
            Console.ReadKey(); ;
        }


       static void Run1()
       {
            Console.WriteLine("我是任务一,我跑了3s");
            Thread.Sleep(3000);
        }

        static void Run2()
        {
            Console.WriteLine("我是任务二，我跑了5s");
            Thread.Sleep(5000);
        }

           static  void ThreadTask()
             {
                 
                 for (int j = 1; j < 4; j++)
                 {
                     Console.WriteLine("\n第{0}次比较", j);

                     var bag = new ConcurrentBag<int>();

                     var watch = Stopwatch.StartNew();

                     watch.Start();
                     for (int i = 0; i < 2000000; i++)
                     {
                         bag.Add(i);
                     }

                  

                     Console.WriteLine("串行计算：集合有:{0},总共耗时：{1}", bag.Count, watch.ElapsedMilliseconds);

                     GC.Collect();

                     bag = new ConcurrentBag<int>();

                     //watch = Stopwatch.StartNew();

                     watch.Restart(); //Start();

                     //Parallel.For(0, 20000, (i,state) =>
                     //{
                     //    bag.Add(i);
                     //});
                     Parallel.ForEach(
                    Partitioner.Create(0, 2000000),
                    (i, state) =>
                    {
                        for (int m = i.Item1; m < i.Item2; m++)
                        {
                            
                            bag.Add(m);
                        }
                    });
                     Console.WriteLine("并行计算：集合有:{0},总共耗时：{1}", bag.Count, watch.ElapsedMilliseconds);

                     GC.Collect();

                     watch.Stop();
                     //Console.ReadKey();
                 }

                Console.ReadKey();
             }




        #endregion


        #region   约瑟夫环
        //约瑟夫环
        void RoleBack()
        {
            ////　有17个人围成一圈(编号0~16),从第0号的人开始从1报数，
            ////凡报到3的倍数的人离开圈子，然后再数下去，直到最后只剩下一个人为止，问此人原来的位置是多少号?


            //int[] outArr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            //int number = 0;//报数
            //int index = 0;//位置索引数

            // List<int> list=outArr.ToList<int>();
            // string msg = string.Empty;

            ////当人数大于3时，才能形成一圈
            // while (list.Count >= 2)
            // {

            //     number++;

            //     //遇到是3倍数的人则将其去除
            //     if (number % 3 == 0)
            //     {
            //         if (list.Count == 0) break;
            //         msg += string.Format("{0},", list[index]);
            //         list.RemoveAt(index);
            //         number = 0;  //从头开始数

            //     }
            //     else
            //     {
            //         //继续向下报数
            //         index++;
            //     }

            //     //当索引位置超出总人数时，开始进入下一轮计数，说明此一圈已经结束
            //     if (index >= list.Count) index = 0; 

            // }

            // //Console.WriteLine(msg);
            // Console.WriteLine("出场顺序："+msg+list[0].ToString(CultureInfo.InvariantCulture));
            // Console.ReadKey();
        }
        #endregion
      
    }  //end

    public class Student { public int ID { get; set; } public string Name { get; set; } public int Age { get; set; } public DateTime CreateTime { get; set; } }



    class A
    {
        public A()
        {
            PrintFields();
        }
        public virtual void PrintFields() { }
    }

    class B : A
    {
        int x = 1;
        int y;
        public B()
        {
            y = -1;
        }
        public override void PrintFields()
        {
            Console.WriteLine("x={0},y={1}", x, y);
        }
    }
}
