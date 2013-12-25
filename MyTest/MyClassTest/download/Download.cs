 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Net;
 using System.Threading;
 using System.Threading.Tasks;
 using System.IO;
 using System.Collections.Concurrent;
 using System.Diagnostics;
 using System.Drawing;
 
 
 namespace MyTest.MyClassTest
 {
     public class Download
     {
         public static CountdownEvent cde = new CountdownEvent(0);
 
         //每个线程下载的字节数，方便最后合并
         public static ConcurrentDictionary<long, byte[]> dic = new ConcurrentDictionary<long, byte[]>();
 
         //请求文件
         public static string url = "http://www.pncity.net/bbs/data/attachment/forum/201107/30/1901108yyd8gnrs2isadrr.jpg";
 
         static void Main(string[] args)
         {
             for (int i = 0; i < 1; i++)
             {
                 Console.WriteLine("\n****************************\n第{0}次比较\n****************************", (i + 1));
 
                 //不用线程
                 //RunSingle();
 
                 //使用多线程
                 RunMultiTask();
             }
 
             Console.Read();
         }
 


         /// <summary>
         /// 使用多线程
         /// </summary>
         static void RunMultiTask()
         {
             Stopwatch watch = Stopwatch.StartNew();
 
             //开5个线程
             int threadCount = 5;
 
             long start = 0;
 
             long end = 0;
 
             var total = GetSourceHead();
 
             if (total == 0)
                 return;
 
             var pageSize = (int)Math.Ceiling((Double)total / threadCount);
 
             cde.Reset(threadCount);
 
             Task[] tasks = new Task[threadCount];
 
             for (int i = 0; i < threadCount; i++)
             {
                 start = i * pageSize;
 
                 end = (i + 1) * pageSize - 1;
 
                 if (end > total)
                     end = total;
 
                 var obj = start + "|" + end;
 
                 tasks[i] = Task.Factory.StartNew(j => new DownFile().DownTaskMulti(obj), obj);
             }

             //并行线程
             Task.WaitAll(tasks);
 
             var targetFile = "C://" + url.Substring(url.LastIndexOf('/') + 1);
 
             FileStream fs = new FileStream(targetFile, FileMode.Create);
 
             var result = dic.Keys.OrderBy(i => i).ToList();
 
             foreach (var item in result)
             {
                 fs.Write(dic[item], 0, dic[item].Length);
             }
 
             fs.Close();
 
             watch.Stop();
 
             Console.WriteLine("多线程：下载耗费时间:{0}", watch.Elapsed);
         }
 
         static void RunSingle()
         {
             Stopwatch watch = Stopwatch.StartNew();
 
             if (GetSourceHead() == 0)
                 return;
 
             var request = (HttpWebRequest)HttpWebRequest.Create(url);
 
             var response = (HttpWebResponse)request.GetResponse();
 
             var stream = response.GetResponseStream();
 
             var outStream = new MemoryStream();
 
             var bytes = new byte[10240];
 
             int count = 0;
 
             while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
             {
                 outStream.Write(bytes, 0, count);
             }
 
             var targetFile = "C://" + url.Substring(url.LastIndexOf('/') + 1);
 
             FileStream fs = new FileStream(targetFile, FileMode.Create);
 
             fs.Write(outStream.ToArray(), 0, (int)outStream.Length);
 
             outStream.Close();
 
             response.Close();
 
             fs.Close();
 
             watch.Stop();
 
             Console.WriteLine("不用线程：下载耗费时间:{0}", watch.Elapsed);
         }


 
         //获取头信息
         public static long GetSourceHead()
         {
             var request = (HttpWebRequest)HttpWebRequest.Create(url);
 
             request.Method = "Head";
             request.Timeout = 3000;

             //long len= (HttpWebRequest)request.GetResponse().ContentLength as long;

             var response = (HttpWebResponse)request.GetResponse();
 
             var code = response.StatusCode;
 
             if (code != HttpStatusCode.OK)
             {
                 Console.WriteLine("下载的资源无效！");
                 return 0;
             }
 
             var total = response.ContentLength;
 
             Console.WriteLine("当前资源大小为:" + total);
 
             response.Close();
 
             return total;
         }




     }



 
     public class DownFile
     {
         // 多线程下载
         public void DownTaskMulti(object obj)
         {
             var single = obj.ToString().Split('|');
 
             long start = Convert.ToInt64(single.FirstOrDefault());
 
             long end = Convert.ToInt64(single.LastOrDefault());
 
             var request = (HttpWebRequest)HttpWebRequest.Create(Download.url);
 
             request.AddRange(start, end);
 
             var response = (HttpWebResponse)request.GetResponse();
 
             var stream = response.GetResponseStream();
 
             var outStream = new MemoryStream();
 
             var bytes = new byte[10240];
 
             int count = 0;
 
             while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
             {
                 outStream.Write(bytes, 0, count);
             }
 
             outStream.Close();
 
             response.Close();

             Download.dic.TryAdd(start, outStream.ToArray());

             Download.cde.Signal();
         }
     }
 }
