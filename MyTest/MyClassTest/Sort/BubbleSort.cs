using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public class Tree<T> where T : IComparable<T>
    {
        private T data;
        private Tree<T> left;
        private Tree<T> right;
    }



    public class BubbleSort<T> where T: IComparable<T>
    {


        #region Hash表检索数据

        public BubbleSort()
        {
        }

        ///<summary>
        ///Hash表检索数据
        ///</summary>
        ///<param name="dic"></param>
        ///<param name="hashLength"></param>
        ///<param name="key"></param>
        ///<returns></returns>
        static int SearchHash(int[] hash, int hashLength, int key)
        {
            //哈希函数
            int hashAddress = key % hashLength;
            //指定hashAdrress对应值存在但不是关键值，则用开放寻址法解决
            while (hash != null && (hash[hashAddress] != 0 && hash[hashAddress] != key))
            {
                hashAddress = (++hashAddress) % hashLength;
            }
            //查找到了开放单元，表示查找失败
            if (hash[hashAddress] == 0)
                return -1;


            return hashAddress;
        }

        ///<summary>
        ///数据插入Hash表
        ///</summary>
        ///<param name="dic">哈希表</para>
        ///<param name="hashLength"></param>
        ///<param name="data"></param>
        static void InsertHash(int[] hash, int hashLength, int data)
        {
            //哈希函数
            int hashAddress = data % 13;
            //如果key存在，则说明已经被别人占用，此时必须解决冲突
            while (hash[hashAddress] != 0)
            {
                //用开放寻址法找到
                hashAddress = (++hashAddress) % hashLength;
            }
            //将data存入字典中
            hash[hashAddress] = data;
        }


        #endregion

        #region 冒泡排序

        public static void Bubble(ref T[] tArr )
        {

            T temp;//临时交换数据用的
            if (null == tArr)
                return ;

            //T[] newArr = new T[tArr.Length];
//            tArr.CopyTo(newArr, 0);


            for (int i = 0; i < tArr.Length;i++ )
            {
                for (int j = i + 1; j < tArr.Length; j++)
                {
                   
                    if (tArr[i].CompareTo(tArr[j]) > 0)
                    {
                        temp = tArr[i];
                        tArr[i] = tArr[j];
                        tArr[j] = temp;
                    }
                }
                
            }
        }

        #endregion


        #region 二叉树排序
         /*
        //此方法在二叉树中插入一个值为T的结点。
        //注意，因为用户可以使用构造器在树中插入初始的根结点，所以在此方法中
        //我们树是非空的。
        public void Insert<T>(T newItem)
        {
            T currentNodeValue = this.NodeData;
            // CompareTo含义上面已作解释，用于判断当前节点值是否大于新项
            if (currentNodeValue.CompareTo(newItem) > 0)
            {
                if (this.LeftTree == null)
                {
                    this.LeftTree = new Tree<T>(newItem);
                }
                else
                {
                    this.LeftTree.Insert(newItem);
                }
            }
            else
            {
                if (this.RightTree == null)
                {
                    this.RightTree = new Tree<T>(newItem);
                }
                else
                {
                    this.RightTree.Insert(newItem);
                }
            }
        }
        //此方法负责遍历二叉树－把结点值转换为字符串，并输出到控制台
        public void WalkTree()
        {
            if (this.LeftTree != null) //判断左结点是否为空
            {
                this.LeftTree.WalkTree();//非空，则递归遍历左子树
            }
            Console.WriteLine(this.NodeData.ToString());//输出到控制台
            if (this.RightTree != null) //判断右结点是否为空
            {
                this.RightTree.WalkTree();//非空，则递归遍历右子树
            }
        }


           */
        #endregion

    } // end class


}