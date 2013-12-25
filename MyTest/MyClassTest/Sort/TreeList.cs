using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyTest.MyClassTest
{
    class TreeList
    {
        //static void Main(string[] args)
        //{
        //    List<int> list = new List<int>() { 50, 30, 70, 10, 40, 90, 80 };

        //    //创建二叉遍历树
        //    BSTree bsTree = CreateBST(list);

        //    Console.Write("中序遍历的原始数据：");

        //    //中序遍历
        //    LDR_BST(bsTree);

        //    Console.WriteLine("\n---------------------------------------------------------------------------n");

        //    //查找一个节点
        //    Console.WriteLine("\n10在二叉树中是否包含：" + SearchBST(bsTree, 10));

        //    Console.WriteLine("\n---------------------------------------------------------------------------n");

        //    bool isExcute = false;

        //    //插入一个节点
        //    InsertBST(bsTree, 20, ref isExcute);

        //    Console.WriteLine("\n20插入到二叉树，中序遍历后：");

        //    //中序遍历
        //    LDR_BST(bsTree);

        //    Console.WriteLine("\n---------------------------------------------------------------------------n");

        //    Console.Write("删除叶子节点 20， \n中序遍历后：");

        //    //删除一个节点(叶子节点)
        //    DeleteBST(ref bsTree, 20);

        //    //再次中序遍历
        //    LDR_BST(bsTree);

        //    Console.WriteLine("\n****************************************************************************\n");

        //    Console.WriteLine("删除单孩子节点 90， \n中序遍历后：");

        //    //删除单孩子节点
        //    DeleteBST(ref bsTree, 90);

        //    //再次中序遍历
        //    LDR_BST(bsTree);

        //    Console.WriteLine("\n****************************************************************************\n");

        //    Console.WriteLine("删除根节点 50， \n中序遍历后：");
        //    //删除根节点
        //    DeleteBST(ref bsTree, 50);

        //    LDR_BST(bsTree);

        //}

        ///<summary>
        /// 定义一个二叉排序树结构
        ///</summary>
        public class BSTree
        {
            public int data;
            public BSTree left;
            public BSTree right;
        }

        ///<summary>
        /// 二叉排序树的插入操作
        ///</summary>
        ///<param name="bsTree">排序树</param>
        ///<param name="key">插入数</param>
        ///<param name="isExcute">是否执行了if语句</param>
        static void InsertBST(BSTree bsTree, int key, ref bool isExcute)
        {
            if (bsTree == null)
                return;

            //如果父节点大于key，则遍历左子树
            if (bsTree.data > key)
                InsertBST(bsTree.left, key, ref isExcute);
            else
                InsertBST(bsTree.right, key, ref isExcute);

            if (!isExcute)
            {
                //构建当前节点
                BSTree current = new BSTree()
                {
                    data = key,
                    left = null,
                    right = null
                };

                //插入到父节点的当前元素
                if (bsTree.data > key)
                    bsTree.left = current;
                else
                    bsTree.right = current;

                isExcute = true;
            }

        }

        ///<summary>
        /// 创建二叉排序树
        ///</summary>
        ///<param name="list"></param>
        static BSTree CreateBST(List<int> list)
        {
            //构建BST中的根节点
            BSTree bsTree = new BSTree()
            {
                data = list[0],
                left = null,
                right = null
            };

            for (int i = 1; i < list.Count; i++)
            {
                bool isExcute = false;
                InsertBST(bsTree, list[i], ref isExcute);
            }
            return bsTree;
        }

        ///<summary>
        /// 在排序二叉树中搜索指定节点
        ///</summary>
        ///<param name="bsTree"></param>
        ///<param name="key"></param>
        ///<returns></returns>
        static bool SearchBST(BSTree bsTree, int key)
        {
            //如果bsTree为空，说明已经遍历到头了
            if (bsTree == null)
                return false;

            if (bsTree.data == key)
                return true;

            if (bsTree.data > key)
                return SearchBST(bsTree.left, key);
            else
                return SearchBST(bsTree.right, key);
        }

        ///<summary>
        /// 中序遍历二叉排序树
        ///</summary>
        ///<param name="bsTree"></param>
        ///<returns></returns>
        static void LDR_BST(BSTree bsTree)
        {
            if (bsTree != null)
            {
                //遍历左子树
                LDR_BST(bsTree.left);

                //输入节点数据
                Console.Write(bsTree.data + "");

                //遍历右子树
                LDR_BST(bsTree.right);
            }
        }

        ///<summary>
        /// 删除二叉排序树中指定key节点
        ///</summary>
        ///<param name="bsTree"></param>
        ///<param name="key"></param>
        static void DeleteBST(ref BSTree bsTree, int key)
        {
            if (bsTree == null)
                return;

            if (bsTree.data == key)
            {
                //第一种情况：叶子节点
                if (bsTree.left == null && bsTree.right == null)
                {
                    bsTree = null;
                    return;
                }
                //第二种情况：左子树不为空
                if (bsTree.left != null && bsTree.right == null)
                {
                    bsTree = bsTree.left;
                    return;
                }
                //第三种情况，右子树不为空
                if (bsTree.left == null && bsTree.right != null)
                {
                    bsTree = bsTree.right;
                    return;
                }
                //第四种情况，左右子树都不为空
                if (bsTree.left != null && bsTree.right != null)
                {
                    var node = bsTree.right;

                    //找到右子树中的最左节点
                    while (node.left != null)
                    {
                        //遍历它的左子树
                        node = node.left;
                    }

                    //交换左右孩子
                    node.left = bsTree.left;

                    //判断是真正的叶子节点还是空左孩子的父节点
                    if (node.right == null)
                    {
                        //删除掉右子树最左节点
                        DeleteBST(ref bsTree, node.data);

                        node.right = bsTree.right;
                    }
                    //重新赋值一下
                    bsTree = node;

                }
            }

            if (bsTree.data > key)
            {
                DeleteBST(ref bsTree.left, key);
            }
            else
            {
                DeleteBST(ref bsTree.right, key);
            }
        }
    }
}