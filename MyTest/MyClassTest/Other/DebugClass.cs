#define Debug
#define VC_V7

//定义两个类型的宏或者预编译指令
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyTest.MyClassTest.Other
{

    public class DebugClass
    {
        static void Main()
        { 
            #if(!Debug && VC_V7)
              Console.WriteLine("定义了预编译指令");
            #elif(Debug && !VC_V7)
                Console.WriteLine("11111111");
            #elif(Debug &&VC_V7)
                Console.WriteLine("222222222");
            #endif
        }
    }
}