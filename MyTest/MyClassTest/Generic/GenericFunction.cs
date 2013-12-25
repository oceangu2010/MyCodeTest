using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public class GenericFunction
    {

        public  GenericFunction()
        {

        }

        public static T GetValue<T>(T para)
        {
            return para;
        }
    }

    public class MyGenericClass<T> where T : IComparable, new()
    {
        // The following line is not possible without new() constraint:
        T item = new T();
    }

    public class Teacher<T> where T : class, new()
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

         T t=new T();
    }



    /*
        *   注:
        *   1.where关键字，若上类中的K和T都要约束，那么两个where之间用空格隔开
            2.K:IComparable表示K只接受实现了IComparable接口的类型
            3.尽管如此，还是无法避免传入值类型的K所带来的装箱问题，原因是IComparable下的CompareTo方法的参数仍是object类型。所以最终的正确代码如下：
     * 
            1.在C#2.0中，所有的派生约束必须放在类的实际派生列表之后
             public class LinkedList<K,T> :IEnumerable<T> where K:IComparable<K>
             {......}
             2.通常只需要在需要的级别定义类的约束，即：哪里编译异常哪里约束。
             3.一个泛型参数上约束多个接口(彼此用,分隔)
             public class LinkedList<K,T> where K:IComparable<K>,IConvertible
             {......}
             4.在一个约束中最多只能有一个基类，同时约束的基类不能是密封类或静态类，因为密封类不能被继承，静态类不能被实例化。
             5.不能将System.Delegate或System.Array作为泛型基类
             6.可以同时约束一个基类和一个或多个接口，但是基类必须首先出现在约束列表中
             public class LinkedList<K,T> where K:MyBaseClass,IComparable<K>,IConvertible
             {......}
             7.C#允许你将另一个一般类型指定为约束
             public class LinkeList<K,T> where K:T
             {......}
             8.自定义基类或接口进行泛型约束
             自定义接口
             public interface IMyInterface
             {......}
             public class MyClass<T> where T:IMyInterface
             {......}
             MyClass<IMyInterface> obj=new MyClass<IMyInterface>();
             自定义基类
             public class MyOtherClass
             {......}
             public class MyClass<T> where T:MyOtherClass
             {......}
             MyClass<MyOtherClass> obj=new MyClass<MyOtherClass>();
             9.自定义的基类或接口必须与泛型参数具有一致的可见性


        */

    //public class LinkedList2<M, T> 
    //    where M : IComparable<T>
    //    //where T: IEnumerator<T>
    //{
    //    Node2<M,T> m_Head;
    //    public M key;
    //    public T item;

    //    T Find(M key)
    //    {

    //        Node2<M,T> current = m_Head;

    //        while (current.NextNode != null)
    //        {

    //            //因为编译器不知道K类型的key是否支持"=="运算符，例如在默认情况，结构体就不提供这种实现

    //            if (current.key.CompareTo(key) == 0) //Will not Compile
    //            {
    //                break;
    //            }

    //            else
    //            {
    //                current = current.NextNode;
    //            }

    //        }

    //        return current.item;

    //    }

    //}




    public class Node2<M, T> where T : new()
    {

        public M key;

        public T item;

        public Node2<M, T> NextNode;

        public Node2()
        {

            key = default(M);

            item = new T();

            NextNode = null;

        }

    }



}