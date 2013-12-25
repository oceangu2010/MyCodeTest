using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTest.MyClassTest
{
    public class QuickSort<T>  where T: IComparable<T>
    {
        /*
         * 快速排序思维想
         * 快速排序，正如它的名字所示，它是在实践中最快的已知排序算法，
         它的算法思想是从待排序记录序列中选取一个记录为枢纽元，
         其关键字设为K，然后将其余记录中关键字小于K的记录移到前面，
         而将关键字大于K的记录移到后面，结果将待排序记录分成两个部分(S1、S2)，
         最后将关键子K的记录插入到其分界线位置，这个过程是一趟快速排序。
         通过一次划分后，就以关键字为K的记录为界，将待排序的序列分成两个子序列，
         前面记录S1中的关键字均不大于K，而后面记录S2中的所有记录的关键字均不小于K。
         对分割后的记录继续按上面的原则进行分割，一直到所有子记录的长度是1 为止，此时待排序得表就变成了一个有序表。
       
         分界点的选择

         通常没有经过充分考虑的选择是将第一个元素用作枢纽元，如果输入的数是随机的，
         那么这种选择方法是可以接收的，但是如果输入是与排序或者反序的，那么这就很糟糕了，
         所有元素不是都被划入前一部分S1，就是都被划入后半部分S2了，这样的话花费的时间也是很多的。
         有种安全的办法就是随机选取枢纽元，但是随机数的生成也会浪费些时间，所以这也不是很好的选择。
         我们在这里采用的做法是使用左端、右端和中心位置上的三个元素的中值作为枢纽元。
         例如，输入为8,1,4,9,6，它的左边元素为8，右边元素为6，中心位置上的元素为4，
         于是枢纽元则是4.这样就可以消除与排序输入的怀情形了。

         算法步骤：
         假设待排序的序列为 r[low]、r[low + 1]、r[center]…….r[high],low为最小下标，center为中间下标，high为最大下标。
         在进行排序之前，先将r[low],r[center],r[high]这三个值按从小到大进行排序，然后按照low，center，high的顺序，
         依次将最小，中间大，最大的值存放在相应位置，最后将low和center位置的值进行交换。这样r[low]就存放的是(枢纽元)中间大的值了。
         首先将枢纽元r[low]移至变量base中，使r[low]这个单元空出来，也就是说让r[low]相当于空单元，
         紧接着反复执行如下两个过程，直到low和high相遇
         
         (1)high从右向左扫描，当r[high]<base时，将r[high]移到空单元r[low],执行这一操作后，r[high]也就变成空单元了。
          
         (2)low从左向右扫描, 当r[low]>base时,将r[low]移至(上一步腾出来的)空单元r[high],这样r[low]又为下一轮循环腾出了空单元。
            当low和high相遇时，r[low](或者r[high])相当于空单元了，并且r [low]左边的所有记录的关键字都不大于枢纽元，
             r[high]右边所有记录的关键字均不小于枢纽元的关键字。最后将枢纽元移至r[low]中，这样一次划分就完成了。

         */


        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[] SortData(T[] arr)
        {

            return null;
        }

        #region 插入排序算法

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="List">需要排序的数据对象</param>
        /// <param name="Lo">低位</param>
        /// <param name="Hi">高位</param>
        /// <param name="comparer">比较</param>
        public static void InsertSort(ref T[] List, int Lo, int Hi, IComparer<T> comparer)
        {
            while (Hi < Lo)
            {
                int pMax = Lo;
                //进行比较
                for (int pNow = Lo; pNow <= Hi; pNow++)
                {
                    if (comparer.Compare(List[pNow], List[pMax]) > 0)
                        pMax = pNow;
                }

                //    交换高位记录和最大数记录
                T dwTmp = List[pMax];

                List[pMax] = List[Hi];

                List[Hi] = dwTmp;

                --Hi;//高位指针往Lo指针移动
            }
        }

        public static void InsertSort(ref T[] List, int Lo, int Hi)
        {
            InsertSort(ref List, Lo, Hi, Comparer<T>.Default);
        }

        public static void InsertSort(ref T[] List)
        {
            InsertSort(ref List, List.GetLowerBound(0), List.GetUpperBound(0));
        }

        #endregion


        #region 泛型快速排序 方法四

        //快速排序
        public static void Quick(ref T[] list, int lo, int hi, IComparer<T> comparer)
        {
            if (hi <= lo) return;

            if ((hi - lo) <= 8)
            {
                InsertSort(ref list, lo, hi, comparer);
            }
            else
            {
                int pLo = lo;
                int pHi = hi;

                T vPivot = list[lo + (hi - lo) >> 1];//分割点

                while (pLo <= pHi)
                {
                    //扫描左半区间的元素是否小于分割点
                    while (pLo < hi && (comparer.Compare(list[pLo], vPivot) < 0))
                        ++pLo;

                    //扫描右半区间的元素是否大于分割点
                    while (pHi > lo && (comparer.Compare(vPivot, list[pHi]) < 0))
                        --pHi;


                    // 交换pLo和pHi之间的元素
                    //(这时 List(pLo) > vPivot 而 List(Hi) <vPivot ，所以要交换他们)
                    if (pLo <= pHi)
                    {
                        T Temp = list[pLo];
                        list[pLo] = list[pHi];
                        list[pHi] = Temp;

                        ++pLo;
                        --pHi;
                    }
                }

                if (lo < pHi)
                {
                    Quick(ref list, lo, pHi, comparer);//左半区间递归((pHi -- Hi 已经ok))
                }

                if (pLo < hi)
                {
                    Quick(ref list, pLo, hi, comparer);//右半区间递归((Lo --pLo 已经ok))
                }
            }
        }

        public static void Quick(ref T[] list, int lo, int hi)
        {
            Quick(ref list, lo, hi, Comparer<T>.Default);
        }

        public static void Quick(ref T[] list)
        {
            Quick(ref list, list.GetLowerBound(0), list.GetUpperBound(0));
        }

        #endregion

        #region 精简linq实现泛型排序 方法三

        /// <summary>
        /// Linq实现的快速排序的泛型版本
        /// </summary>
        /// <typeparam name="T">限定参数类型为实现ICompareble接口的类型</typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> QuickSortingFunc<T>(IEnumerable<T> list) where T : IComparable<T>
        {
            if (list.Count() <= 1) return list;

            var baseNumber = list.First();//取第一个元素为比较基数。

            return QuickSortingFunc(list.Where(x => x.CompareTo(baseNumber) < 0))  //把比basenumber小的元素都找出来，放在前面
                .Concat(list.Where(x => x.CompareTo(baseNumber) == 0))
                .Concat(QuickSortingFunc(list.Where(x => x.CompareTo(baseNumber) > 0))); //把比basenumber大的元素都找出来，放在后面，然后连接起来，递归调用该方法。
        }

        #endregion


        #region 快速排序方法一

        ///<summary> 
         /// 分割函数 
        ///</summary> 
        ///<param name="list">待排序的数组</param> 
        ///<param name="left">数组的左下标</param> 
        ///<param name="right"></param> 
        ///<returns></returns> 
        public static int Division(List<T> list, int left, int right)
        { 
            //首先挑选一个基准元素 
            T baseNum = list[left]; 

            //每次找到最小的数作为基准数据

            //开始移动比较数据
            while (left < right)
            { 
                //先从数组的右端开始向左端查找
                //从数组的右端开始向前找，一直找到比base小的数字为止(包括base同等数) 
                while (left < right && list[right].CompareTo(baseNum) >=0)
                {
                    //找到了向前称一位
                    right = right - 1;
                }
                //此时的list[right]就是找到的最小数
                //最终找到了比baseNum小的元素，要做的事情就是此元素放到base的位置 
                list[left] = list[right]; //将找到的最小数的设置为基准比较数据

                //再从数组右端开始的查找比较
                //从数组的左端开始向后找，一直找到比base大的数字为止（包括base同等数）
                while (left < right && list[left].CompareTo(baseNum)<=0)
                {
                    left = left + 1;
                }

                //最终找到了比baseNum大的元素，要做的事情就是将此元素放到最后的位置
                list[right] = list[left]; 
            } 

            //最后就是把baseNum放到该left的位置 
            list[left] = baseNum; 

            //最终，我们发现left位置的左侧数值部分比left小，left位置右侧数值比left大 
            //至此，我们完成了第一篇排序
            return left; 
        }


        public static void Quick(ref List<T> list, int left, int right)
        {
            List<T> nList = list;

            //左下标一定小于右下标，否则就超越了         
            if (left < right)
            { 
                //对数组进行分割，取出下次分割的基准标号 
                int i = Division(list, left, right); 

                //对“基准标号“左侧的一组数值进行递归的切割，以至于将这些数值完整的排序 
                Quick(ref list, left, i - 1); 

                //对“基准标号“右侧的一组数值进行递归的切割，以至于将这些数值完整的排序
                Quick(ref list, i + 1, right);
            }
        }

        #endregion

        #region 快速排序方法二

        ///<summary>
        /// 最简洁的快速排序
        ///</summary>
        ///<typeparam name="T">可比较的类型</typeparam>
        ///<param name="array">待排序数组</param>
        ///<param name="start">数组起始索引</param>
        ///<param name="end">数组末尾索引</param>
        public static void FastSort(ref T[] array, int start, int end) //where T : IComparable<T>
        {
            var left = start;
            var right = end;

            //基准数据
            var middle = array[start];

            //循环处理
            while (true)
            {
                //从右边开始与基准数据进行比较，如果比基准数据大，则继续比较下一个数，依次类推
                while (left<right && array[right].CompareTo(middle) >= 0) right--;

                //如果将左指针移动与右指针相等啦，则结束比较
                if (right == left) break;

                //进行数据位置交换
                Swap(array, left++, right);

                //从左边开始进行数据进行比较，如果比基准数据小，则继续比较下一个数
                while (left < right && array[left].CompareTo(middle) <= 0) left++;
                //如果将左指针移动与右指针相等啦，则结束比较
                if (right == left) break;

                //进行数据位置交换
                Swap(array, left, right--);
            }

            //如子区间块长大于1，则继续对区间排序

            //左区间进行数据比较
            if (left - start > 1) FastSort(ref array, start, left - 1); 

            //右区间进行数据比较
            if (end - left > 1) FastSort(ref array, left + 1, end);

        }

        ///<summary>
        /// 交换数组中两个索引对应的元素
        ///</summary>
       public static void Swap<T>(T[] array, int left, int right)
        {
           //交换两个数的位置
            var temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
        
        #endregion

        #region 快速排序 自定义方法

        /// <summary>
        /// 快速排序 自定义排序模仿写法
        /// </summary>
        /// <param name="arr">排序数据源</param>
        /// <param name="left">起始位置索引</param>
        /// <param name="right">结束位置索引</param>
        /// <returns></returns>
       public static void QuickSortDefine(ref T[] arr, int start, int end)
       {
           //保留原始的初始索引
           int left  = start;
           int right = end ;

           if(arr==null ||arr.Length==0)return;

           T middle=arr[start]; //参照基准数值

           //条件要满足
           if (left < right)
           {

               while (true)
               { 
                    // 先从右边开始比较   满足条件 则继续向左移动比较    
                   while(left < right && arr[right].CompareTo(middle) >= 0) right--;
                   if (left == right) break;//当从右边开始移动到最左边时，则跳出循环，说明比较结束

                   //将左边的数据与右边的数据进行交换
                   ChangePost(ref arr, left++, right);  //将右边的数据交换到基准数据的下一位数据

                   //从左边开始向右比较 满足条 则继续向右移动
                   while (left < right && arr[left].CompareTo(middle) <= 0) left++;
                   if (right == left) break; //当从右边开始移动到最左边时，则跳出循环，说明比较结束
                   ChangePost(ref arr, left, right--);   //将参照的基准数据交换到右边 倒数第二位
               }

               //左半区域 开始比较    区域为[start,left-1] 其中left位置是参照基准数据，所以处以中间位置不动
               if (left - start > 1) QuickSortDefine(ref arr, start, left - 1);

               //右半区域开始比较   区域为[left+1,end] 其中left位置是参照基准数据，所以处以中间位置不动
               if (end - left > 1) QuickSortDefine(ref arr, left + 1, end);
           
           }
       
       }


        /// <summary>
        /// 进行数据交换
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
       private static void ChangePost(ref T[] arr,int left,int right)
       { 
            T temp=arr[left];
            arr[left]=arr[right];
            arr[right] = temp;
       
       }
        #endregion
    }
}