using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public sealed class Singleton
    {
        /// <summary>
        /// 经di
        /// </summary>
        private static  Singleton singleton = null;
        private static readonly object objLock = new object();

        public static Singleton Instace
        {
            get
            {
                if (singleton == null)
                {
                    lock (objLock)
                    {
                        if (singleton == null)
                        {
                            singleton = new Singleton();
                        }
                    }
                }
                return singleton;
            }
        }


    }
}