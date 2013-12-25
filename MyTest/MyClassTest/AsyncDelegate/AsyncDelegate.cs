using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyTest.MyClassTest
{
    /// <summary>
    /// 委托异步调用
    /// </summary>
    public class AsyncDelegate
    {
        #region 有参委托
        //带参数
        //定义一个委托
        private delegate void DoSomething(string name, int age);

        public  void RealizeAsync(string name, int age)
        {
            //1.实例化一个委托，调用者发送一个请求，请求执行该方法体（还未执行）
            DoSomething doSomething = new DoSomething(PlayMessage);

            //3 。调用者（主线程）去触发异步调用，采用异步的方式请求上面的方法体
            IAsyncResult result = doSomething.BeginInvoke(name,age,
                //2.自定义上面方法体执行后的回调函数
                 new AsyncCallback
                     (
                //5.以下是回调函数方法体
                //asyncResult.AsyncState其实就是AsyncCallback委托中的第二个参数
                       AsyncEndCallback
                     )
                 , doSomething);
            //DoSomething......调用者（主线程）会去做自己的事情
            Console.ReadKey();
        }

       //委托调用的函数
       private  void PlayMessage(string name, int age)
       {
           Console.WriteLine("如果委托使用beginInvoke的话，这里便是异步方法体,姓名:{0},年龄:{1}", name, age);
       }

        //结束回调方法
       private void AsyncEndCallback(IAsyncResult iasync)
       {
           DoSomething ds = (DoSomething)iasync.AsyncState;
           ds.EndInvoke(iasync);
       }

      #endregion

        #region 无参委托

        ////定义一个委托
       //public delegate void DoSomething();
       //static void Main(string[] args)
       //{
       //    //1.实例化一个委托，调用者发送一个请求，请求执行该方法体（还未执行）
       //    DoSomething doSomething = new DoSomething(
       //        () =>
       //        {
       //            Console.WriteLine("如果委托使用beginInvoke的话，这里便是异步方法体");
       //            //4，实现完这个方法体后自动触发下面的回调函数方法体
       //        });
       //    //3 。调用者（主线程）去触发异步调用，采用异步的方式请求上面的方法体
       //    IAsyncResult result = doSomething.BeginInvoke(
       //        //2.自定义上面方法体执行后的回调函数
       //         new AsyncCallback
       //             (
       //        //5.以下是回调函数方法体
       //        //asyncResult.AsyncState其实就是AsyncCallback委托中的第二个参数
       //                asyncResult =>
       //                {
       //                    doSomething.EndInvoke(asyncResult);
       //                    Console.WriteLine(asyncResult.AsyncState.ToString());
       //                }
       //             )
       //         , "BeginInvoke方法的第二个参数就是传入AsyncCallback中的AsyncResult.AsyncState,我们使用时可以强转成相关类型加以使用");
       //    //DoSomething......调用者（主线程）会去做自己的事情
       //    Console.ReadKey();
       //}

        #endregion

    }

    //测试访问类型
    public class A {
        internal string name = string.Empty;
        protected string name1 = string.Empty;
        private string name2 = string.Empty;
        public string name3=null;
        public A() { }
    
    }

   public class B {
        protected string name1 = string.Empty;
        private string name2 = string.Empty;
        public string name3=null;
        public B() { }
    }

   public class E {
        A aa = new A();
        //aa.name3="name";
        bag bb = new bag();
      

    }
}





namespace MyTest.MyClassTest
{
    using MyTest.MyClassTest;
    public class C : A
    {
        A aa = new A();
       
    }

    class D {

        C cClass = new C();

    }
}