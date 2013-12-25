using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading;
using System.IO;

namespace MyTest.MyClassTest
{
    public class AsycFileStream
    {


              public class FileData
          {
              public FileStream Stream;
              public int Length;
              public byte[] ByteData;
          }
  
          static void Main(string[] args)
          {       
              //把线程池的最大值设置为1000
              ThreadPool.SetMaxThreads(1000, 1000);
              ThreadPoolMessage("Start");
              ReadFile();
  
              Console.ReadKey();
          }
  
          static void ReadFile()
          {
              byte[] byteData=new byte[80961024];
              FileStream stream = new FileStream("File1.sour", FileMode.OpenOrCreate, 
                                      FileAccess.ReadWrite, FileShare.ReadWrite, 1024, true);
              
              //把FileStream对象,byte[]对象，长度等有关数据绑定到FileData对象中，以附带属性方式送到回调函数
              FileData fileData = new FileData();
              fileData.Stream = stream;
              fileData.Length = (int)stream.Length;
              fileData.ByteData = byteData;
              
              //启动异步读取
              stream.BeginRead(byteData, 0, fileData.Length, new AsyncCallback(Completed), fileData);
          }
   
          static void Completed(IAsyncResult result)
          {
              ThreadPoolMessage("Completed");
  
              //把AsyncResult.AsyncState转换为FileData对象，以FileStream.EndRead完成异步读取
              FileData fileData = (FileData)result.AsyncState;
              int length=fileData.Stream.EndRead(result);
              fileData.Stream.Close();
  
              //如果读取到的长度与输入长度不一致，则抛出异常
              if (length != fileData.Length)
                  throw new Exception("Stream is not complete!");
  
              string data=Encoding.ASCII.GetString(fileData.ByteData, 0, fileData.Length);
              Console.WriteLine(data.Substring(2,22));
          }
  
          //显示线程池现状
          static void ThreadPoolMessage(string data)
          {
              int a, b;
              ThreadPool.GetAvailableThreads(out a, out b);
              string message = string.Format("{0}\n  CurrentThreadId is {1}\n  "+
                           "WorkerThreads is:{2}  CompletionPortThreads is :{3}",
                           data, Thread.CurrentThread.ManagedThreadId, a.ToString(), b.ToString());
              Console.WriteLine(message);      
          }
    }
}