using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public class SelectSort<T> where T:IComparable<T>
    {

        #region 选择排序

            //选择排序48         
           public static List<int> SelectDataSort(List<int> list)
            {
                int tempIndex;
                var tempData=list[0];
                //要遍历的次数
                for (int i = 0; i < list.Count - 1; i++)
                {
                    //假设tempIndex的下标的值最小
                    tempIndex = i;
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        //如果tempIndex下标的值大于j下标的值,则记录较小值下标j
                        if (list[tempIndex] > list[j])
                        {
                            tempIndex = j;
                        }
                    }

                    //最后将假想最小值跟真的最小值进行交换
                    tempData = list[tempIndex];
                    list[tempIndex] = list[i];
                    list[i] = tempData;

                }

                //if (list[tempIndex] > list[j])
                //{
                //    tempIndex = j;
                //}
               // tempData = list[tempIndex];
               //list[tempIndex]=list[i];
               //list[tempIndex] = tempData;

               
                return list;
            }


       
        #endregion

        #region 堆排序

       ///<summary> 
        /// 构建堆 
        /// </summary> 
        /// <param name="list">待排序的集合</param> 
        ///<param name="parent">父节点</param> 
        ///<param name="length">输出根堆时剔除最大值使用</param> 
          static void HeapAdjust(T[] list, int parent, int length)
          {
              //temp保存当前父节点 
              T temp = list[parent]; 

              //得到左孩子(这可是二叉树的定义哇) 
              int child = 2 * parent + 1; 
              while (child < length) 
              { 
                  //如果parent有右孩子，则要判断左孩子是否小于右孩子
                  if (child + 1 < length && list[child].CompareTo(list[child + 1])<0) 
                      child++; 
                  //父节点大于子节点，不用做交换 
                  if (temp.CompareTo(list[child]) >= 0) 
                      break; 
                  //将较大子节点的值赋给父亲节点 
                  list[parent] = list[child];
                  //然后将子节点做为父亲节点，已防止是否破坏根堆时重新构造 
                  parent = child; 
                  //找到该父节点左孩子节点 
                  child = 2 * parent + 1; 
              } 
              //最后将temp值赋给较大的子节点，以形成两值交换 
              list[parent] = temp; 
          } 


        ///<summary> 
        /// 堆排序
        /// </summary>
        ///<param name="list">待排序的集合</param>
         ///<param name="top">前K大</param> 
         ///<returns></returns>
         public static List<T> HeapSort(T[] list, int top) 
         { 
             List<T> topNode = new List<T>(); 
             //list.Count/2-1:就是堆中非叶子节点的个数 
             for (int i = list.Length / 2 - 1; i >= 0; i--) 
             {               
                 HeapAdjust(list, i, list.Length); 

             }           
             
             //最后输出堆元素（求前K大） 
             for (int i = list.Length - 1; i >= list.Length - top; i--) 
             {
                 //堆顶与当前堆的第i个元素进行值对调
                 T temp = list[0];
                 list[0] = list[i];
                 list[i] = temp;
                 //最大值加入集合
                 topNode.Add(temp);
                 
                 //因为顺序被打乱，必须重新构造堆
                 HeapAdjust(list, 0, i);
             }
             return topNode;
         }

       #endregion

        #region 希尔排序

       ///<summary>
       ///数组的划分 
       ///</summary>
       ///<param name="array">待排序数组</param>
       ///<param name="temparray">临时存放数组</param>
       ///<param name="left">序列段的开始位置，</param>
       ///<param name="right">序列段的结束位置</param>
       static void MergeSort(int[] array, int[] temparray, int left, int right)
       {           
           if (left < right)           
           {                 //取分割位置
               int middle = (left + right) / 2;
               //递归划分数组左序列
               MergeSort(array, temparray, left, middle);
               //递归划分数组右序列
               MergeSort(array, temparray, middle + 1, right);
               //数组合并操作
               Merge(array, temparray, left, middle + 1, right);
           }
       }        
        
        ///<summary>
        ///数组的两两合并操作
        ///</summary>
        ///<param name="array">待排序数组</param>
        ///<param name="temparray">临时数组</param>
        ///<param name="left">第一个区间段开始位置</param>
        ///<param name="middle">第二个区间的开始位置</param>
        ///<param name="right">第二个区间段结束位置</param>
        static void Merge(int[] array, int[] temparray, int left, int middle, int right)
        {
            //左指针尾
            int leftEnd = middle - 1;
            //右指针头
            int rightStart = middle;
            //临时数组的下标
            int tempIndex = left;
            //数组合并后的length长度
            int tempLength = right - left + 1;
            //先循环两个区间段都没有结束的情况
            while ((left <= leftEnd) && (rightStart <= right))
            {
                //如果发现有序列大，则将此数放入临时数组
                if (array[left] < array[rightStart])
                    temparray[tempIndex++] = array[left++];
                else
                    temparray[tempIndex++] = array[rightStart++];
            }

            //判断左序列是否结束
            while (left <= leftEnd)
                temparray[tempIndex++] = array[left++];
            //判断右序列是否结束         
            
            while (rightStart <= right)
                temparray[tempIndex++] = array[rightStart++];
            //交换数据
            for (int i = 0; i < tempLength; i++)
            {
                array[right] = temparray[right];
                right--;
            }
        }



        #endregion


    } //end class


}