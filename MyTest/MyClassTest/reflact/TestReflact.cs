using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Reflection;

namespace MyTest.MyClassTest
{
    public class DemoReflact
    {
        public string Id { get; set; }
        public string Name;

        public string GetValue(string first, string sencode)
        {
            return first + sencode;
        }
    }

    public class TestReflact
    {

        public static object OutputReflact()
        { 
        
           Type t= typeof( DemoReflact);
           MethodInfo m = t.GetMethod("GetValue");
           object obj = Activator.CreateInstance(t);
           object[] objArr= { "aaa","bbb"};
           object result=  m.Invoke(obj,objArr);

           Assembly assem = Assembly.Load("");
           //Assembly myAssembly = Assembly.LoadFrom("DynamicAssembly.dll");
           //Assembly myArr = Assembly.LoadFile("dll path");
           //Type[] tArr = myArr.GetTypes();
           //foreach (Type item in tArr)
           //{
           //   MethodInfo mi= item.GetMethod("function name");
              
           //}
           return result;

        }
    }
}