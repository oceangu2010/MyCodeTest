using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//二叉树
namespace MyTest.MyClassTest
{
    class BinarySearchTree<T> where T : IComparable<T>
    {
        //代表节点的内部类
        public class BinaryNode
        {
            public T element;
            public BinaryNode LChild;
            public BinaryNode RChild;

            public BinaryNode(T element)
            {
                this.element = element;
                LChild = null;
                RChild = null;
            }
        }
        //根节点
        private BinaryNode root;
        //构造函数
        public BinarySearchTree() { }
        public BinarySearchTree(T data)
        {
            root = new BinaryNode(data);
        }


        #region 插入操作

        private void Insert(T element, ref BinaryNode node)
        {
            if (node == null)
                node = new BinaryNode(element);
            else if (element.CompareTo(node.element) < 0)
                Insert(element, ref node.LChild);
            else if (element.CompareTo(node.element) > 0)
                Insert(element, ref node.RChild);
        }

        public void Insert(T element)
        {
            Insert(element, ref this.root);
        }

        #endregion


        #region 判断是否存在某个元素

        #endregion



        #region 判断是否存在某个元素

        private bool Contains(T element, BinaryNode node)
        {
            if (node == null)
                return false;
            else if (element.CompareTo(node.element) < 0)
                return Contains(element, node.LChild);
            else if (element.CompareTo(node.element) > 0)
                return Contains(element, node.RChild);
            else
                return true;
        }

        public bool Contains(T element)
        {
            return Contains(element, this.root);
        }

        #endregion


        #region 查找最大和最小的元素


        public BinaryNode FindMin(BinaryNode node)
        {
            if (node == null)
                return null;
            if (node.LChild == null)
                return node;
            return FindMin(node.LChild);
        }
        public BinaryNode FindMin()
        {
            return FindMin(this.root);
        }

        public BinaryNode FindMax(BinaryNode node)
        {
            if (node == null)
                return null;
            if (node.RChild == null)
                return node;
            return FindMax(node.RChild);
        }
        public BinaryNode FindMax()
        {
            return FindMax(this.root);
        }

        #endregion


        #region 删除操作

        public void remove(T element, BinaryNode node)
        {
            if (node == null)
                return;
            if (element.CompareTo(node.element) < 0)
                remove(element, node.LChild);
            else if (element.CompareTo(node.element) > 0)
                remove(element, node.RChild);
            else if (node.LChild != null && node.RChild != null)  //Two Children
            {
                node.element = FindMin(node.RChild).element;
                remove(node.element, node.RChild);
            }
            else
            {
                node = (node.LChild != null) ? node.LChild : node.RChild;
            }

        }
        public void remove(T element)
        {
            remove(element, this.root);
        }

        #endregion



        #region 前序 中序 后序 的递归遍历: 遍历操作接受一个Action的委托，在这个委托里面可以定义读相应的元素的操作
          /// <summary>
          /// 前序 中序 后序 的递归遍历: 遍历操作接受一个Action的委托，在这个委托里面可以定义读相应的元素的操作
          /// </summary>
          /// <param name="doDelegate"></param>
          /// <param name="node"></param>
        public void PreOrderTraverse(Action<T> doDelegate, BinaryNode node)
        {
            if (node == null)
                return;
            doDelegate(node.element);
            PreOrderTraverse(doDelegate, node.LChild);
            PreOrderTraverse(doDelegate, node.RChild);
        }


        public void PreOrderTraverse(Action<T> doDelegate)
        {
            PreOrderTraverse(doDelegate, this.root);
        }

        public void PostOrderTraverse(Action<T> doDelegate, BinaryNode node)
        {
            if (node == null)
                return;
            PostOrderTraverse(doDelegate, node.LChild);
            PostOrderTraverse(doDelegate, node.RChild);
            doDelegate(node.element);
        }


        public void PostOrderTraverse(Action<T> doDelegate)
        {
            PostOrderTraverse(doDelegate, this.root);
        }

        public void InOrderTraverse(Action<T> doDelegate, BinaryNode node)
        {
            if (node == null)
                return;
            InOrderTraverse(doDelegate, node.LChild);
            doDelegate(node.element);
            InOrderTraverse(doDelegate, node.RChild);
        }

        public void InOrderTraverse(Action<T> doDelegate)
        {
            InOrderTraverse(doDelegate, this.root);
        }


        #endregion




    }//end class




}