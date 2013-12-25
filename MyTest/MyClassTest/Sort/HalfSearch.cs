using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace MyTest.MyClassTest
{
    public class HalfSearch<T> where T : IComparable<T>
    {

        #region 二分查找算法一

        //二叉查找
        public static int BinarySearch(T[] List, int Lo, int Hi, T vFind, IComparer<T> comparer)
        {
            while (Lo <= Hi)
            {
                int pMid = Lo + ((Hi - Lo) >> 1);
                int cmpResult = comparer.Compare(List[pMid], vFind);
                if (cmpResult == 0)
                {
                    return pMid;
                }
                else if (cmpResult < 0)
                {
                    Lo = pMid + 1;//在右半区间找
                }
                else
                {
                    Hi = pMid - 1;//在左半区间找
                }
            }

            return ~Lo;
        }

        public static int BinarySearch(T[] List, int Lo, int Hi, T vFind)
        {
            return BinarySearch(List, Lo, Hi, vFind, Comparer<T>.Default);
        }

        public static int BinarySearch(T[] List, T vFind)
        {
            return BinarySearch(List, List.GetLowerBound(0), List.GetUpperBound(0), vFind);
        }

        #endregion

        #region 二分查找算法二

        /// <summary>
        /// 两个值的比较委托
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns>返回值,值1大于值2返回1,值1小于值2返回-1,值1等于值2返回0</returns>
        public delegate int Compare<T>(T value1, T value2);


        /// <summary>
        /// 把一个值插入到一个有序的集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="myList">目标集合</param>
        /// <param name="insertValue">要插入的值</param>
        /// <param name="myCompareMethod">两个值的比较方法</param>
        public static void InsertToSort<T>(IList<T> myList, T insertValue, Compare<T> myCompareMethod)
        {
            int place = FindInsertPlace<T>(myList, insertValue, 0, myList.Count, myCompareMethod);
            myList.Insert(place, insertValue);
        }


        //// <summary>
        /// 是否存在此值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="myList">目标集合</param>
        /// <param name="inputKey">要检查的值</param>
        /// <param name="myCompareMethod">两个值的比较方法</param>
        /// <returns>返回值</returns>
        public static bool Contains<T>(IList<T> myList, T inputKey, Compare<T> myCompareMethod)
        {
            int place = FindPlace<T>(myList, inputKey, 0, myList.Count, myCompareMethod);
            if (place == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        ///<summary>
        /// 二分查找法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="myList">目标集合</param>
        /// <param name="inputKey">要查找的值</param>
        /// <param name="myCompareMethod">两个值的比较方法</param>
        /// <returns>值的索引,未找到返回-1</returns>
        public static int FindPlace<T>(IList<T> myList, T inputKey, Compare<T> myCompareMethod)
        {
            return FindPlace<T>(myList, inputKey, 0, myList.Count, myCompareMethod);
        }


        /// <summary>
        /// 二分查找法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="myList">目标集合</param>
        /// <param name="inputKey">要查找的值</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        /// <param name="myCompareMethod">两个值的比较方法</param>
        /// <returns>值的索引,未找到返回-1</returns>
        public static int FindPlace<T>(IList<T> myList, T inputKey, int start, int end, Compare<T> myCompareMethod)
        {
            if (myList.Count == 0) return -1;
            int nowplace = (int)((start + end) / 2);
            if (start == nowplace)
            {
                T nowvalue = myList[nowplace];
                if (myCompareMethod(nowvalue, inputKey) == 0)
                {
                    return nowplace;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                T nowvalue = myList[nowplace];
                if (myCompareMethod.Invoke(nowvalue, inputKey) == 1)
                {
                    return FindPlace(myList, inputKey, start, nowplace, myCompareMethod);
                }
                else if (myCompareMethod.Invoke(nowvalue, inputKey) == -1)
                {
                    return FindPlace(myList, inputKey, nowplace, end, myCompareMethod);
                }
                else
                {
                    return nowplace;
                }
            }
        }

        /// <summary>
        /// 查找值的插入位置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="myList">目标集合</param>
        /// <param name="inputKey">要查找的值</param>
        /// <param name="myCompareMethod">两个值的比较方法</param>
        /// <returns>插入位置</returns>
        public static int FindInsertPlace<T>(IList<T> myList, T inputKey, Compare<T> myCompareMethod)
        {
            return FindInsertPlace<T>(myList, inputKey, 0, myList.Count, myCompareMethod);
        }

        /// <summary>
        /// 查找值的插入位置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="myList">目标集合</param>
        /// <param name="inputKey">要查找的值</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        /// <param name="myCompareMethod">两个值的比较方法</param>
        /// <returns>插入位置</returns>
        public static int FindInsertPlace<T>(IList<T> myList, T inputKey, int start, int end, Compare<T> myCompareMethod)
        {
            if (myList.Count == 0) return 0;
            int nowplace = (start + end) / 2;
            if (start == nowplace)
            {
                T nowvalue = myList[nowplace];
                if (myCompareMethod.Invoke(nowvalue, inputKey) == 1)
                {
                    return start;
                }
                else
                {
                    return end;
                }
            }
            else
            {
                T nowvalue = myList[nowplace];
                if (myCompareMethod.Invoke(nowvalue, inputKey) == 1)
                {
                    return FindInsertPlace(myList, inputKey, start, nowplace, myCompareMethod);
                }
                else
                {
                    return FindInsertPlace(myList, inputKey, nowplace, end, myCompareMethod);
                }
            }
        }

        #endregion

        static void BinarySearch(ref int[] myarry, int i)
        {
            int low = 0;
            int high = myarry.Length;
            int mid;
            if (i > myarry[myarry.Length - 1] || i < myarry[0])
            {
                //Console.WriteLine("Not Found");
                throw new HttpException("没有找到");
            }
            else
                while (low <= high)
                {
                    mid = (high + low) / 2;
                    if (myarry[mid] == i)
                    {
                        Console.WriteLine(mid);
                        break;
                    }
                    else if (myarry[mid] < i)
                    {
                        low = mid + 1;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }
        }


        #region 自定义二分查找算法


        public static int FindHalfSearch(T[] array, T item)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentNullException("array is not null");
                return -1;
            }
            if (!array.Contains(item)) return 0;

            int left = 0;
            int right = array.Length - 1;

            int middle = 0;

            int index = -1;

            while (true)
            {
                middle = (left + right) / 2;

                //出口
                if (array[middle].CompareTo(item) == 0)
                {
                    index = middle;
                    break;
                }

                //当中间值大于要查找的值时，则从左边开始查找
                if (array[middle].CompareTo(item) > 0)
                {
                    right = middle - 1;
                }
                else
                {
                    left = left + 1;
                }

            }



            return index + 1;
        }


        #endregion


        /// 
        /// 二分查找索引
        /// 
        /// 查找的对象，必须实现IComparable接口
        /// 对象数组
        /// 要查找的值
        /// 找到的索引
        public static int FindIndex(T[] array, T value)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int index = -1;

            int lowIndex = 0;
            int highIndex = array.Length - 1;
            int middleIndex = -1;

            while (lowIndex <= highIndex)
            {
                middleIndex = (lowIndex + highIndex) / 2;

                if (value.CompareTo(array[middleIndex]) == 0)
                {
                    index = middleIndex;
                    break;
                }
                if (value.CompareTo(array[middleIndex]) > 0)
                    lowIndex = middleIndex + 1;
                else
                    highIndex = middleIndex - 1;
            }

            watch.Stop();
            Debug.WriteLine(string.Format("{0}ms", watch.ElapsedMilliseconds));
            return index;
        }

    } //end class
}