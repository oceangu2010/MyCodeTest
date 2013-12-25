using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Stack
{

    /*  
     * 顺序栈和链栈分别类似于顺序表和单链表，只是由于栈的First In Last Out性质，其操作相对简单，是顺序表和单链表的子集。
       链栈中的链不使用Head属性，这一属性是多余的，使用链栈类的TopNode属性即可。另外，为了避免每次返回链栈的长度都要遍历所有结点，
       在链栈类中增加Num属性，Push操作时，Num自加，Pop操作时，Num自减，始终等于链栈中的结点数。*/

    //顺序栈与栈链表实例

    //栈的接口
    public interface IStackDS<T>
    {
        int GetLength();
        bool IsEmpty();
        void Clear();
        void Push(T t);
        T Pop();
        T GetTop();
    }


    //顺序栈类
    class SequenceStack<T> : IStackDS<T>
    {
        private int intMaxSize;
        private int intTopPointer;
        private T[] tData;
        public int MaxSize
        {
            get { return this.intMaxSize; }
            set { this.intMaxSize = value; }
        }
        public int TopPointer//没有set，不应该在类外修改top的Pointer
        {
            get { return this.intTopPointer; }
        }
        public T this[int i]//没有set，不支持类外修改
        {
            get { return this.tData[i]; }
        }

        public SequenceStack()
            : this(100)//默认MaxSize为100
        {
        }
        public SequenceStack(int size)
        {
            this.tData = new T[size];
            this.intMaxSize = size;
            this.intTopPointer = -1;//栈为空时，top的pointer为-1
        }
        #region IStackDS<T> 成员
        public int GetLength()
        {
            return this.intTopPointer + 1;
        }
        public bool IsEmpty()
        {
            return this.intTopPointer == -1;
        }
        public void Clear()
        {
            this.intTopPointer = -1;
        }
        public void Push(T t)
        {
            this.intTopPointer++;
            this.tData[this.intTopPointer] = t;
        }
        public T Pop()
        {
            if (this.IsEmpty())
            {
                Console.WriteLine("The stack has no elements!");
                return default(T);
            }
            else
            {
                T t = this.tData[this.intTopPointer];
                this.intTopPointer--;
                return t;
            }
        }
        public T GetTop()
        {
            if (this.IsEmpty())
            {
                Console.WriteLine("The stack has no elements!");
                return default(T);
            }
            else
            {
                return this.tData[this.intTopPointer];
            }
        }
        #endregion
    }
    //链栈的结点类
    class Node<T>
    {
        private T tVal;
        private Node<T> tNext;
        public T Val
        {
            get { return this.tVal; }
            set { this.tVal = value; }
        }
        public Node<T> Next
        {
            get { return this.tNext; }
            set { this.tNext = value; }
        }
        public Node()
        {
            this.tVal = default(T);
            this.tNext = null;
        }
        public Node(T t)
        {
            this.tVal = t;
            this.tNext = null;
        }
    }
    //链栈类。不设置里面链表的head属性，因为这是多余的。用TopNode表示栈顶即可。
    //另，为了避免返回长度时对链表遍历，使用num计数，使num与栈内的结点个数同时变化。
    class LinkedStack<T> : IStackDS<T>
    {
        private Node<T> nTopNode;
        private int intNum;
        public Node<T> TopNode
        {
            get { return this.nTopNode; }
            set { this.nTopNode = value; }
        }
        public int Num
        {
            get { return this.intNum; }
            set { this.intNum = value; }
        }
        public LinkedStack()
        {
            this.nTopNode = null;
            this.intNum = 0;
        }
        public LinkedStack(Node<T> node)
        {
            this.nTopNode = node;
            this.intNum = 1;
        }
        #region IStackDS<T> 成员
        public int GetLength()
        {
            return this.intNum;
        }
        public bool IsEmpty()
        {
            return this.intNum == 0;
        }
        public void Clear()
        {
            this.nTopNode = null;
            this.intNum = 0;
        }
        public void Push(T t)
        {
            Node<T> node = new Node<T>(t);
            node.Next = this.nTopNode;//无论TopNode是否为null，都可以直接赋，无需判断是否为null
            this.nTopNode = node;
            this.intNum++;
        }
        public T Pop()
        {
            if (this.IsEmpty())
            {
                Console.WriteLine("The stack has no elements!");
                return default(T);
            }
            else
            {
                Node<T> node = this.nTopNode;
                this.nTopNode = this.nTopNode.Next;
                this.intNum--;
                return node.Val;
            }
        }
        public T GetTop()
        {
            if (this.IsEmpty())
            {
                Console.WriteLine("The stack has no elements!");
                return default(T);
            }
            else
            {
                Node<T> node = this.nTopNode;
                return node.Val;
            }
        }
        #endregion
    }


    /*顺序栈与栈调用方法*/
    class Program
    {
        static void Main(string[] args)
        {
            SequenceStack<int> stack = new SequenceStack<int>(10);
            stack.Push(3);
            stack.Push(9);
            while (!stack.IsEmpty())
            {
                int i = stack.Pop();
                Console.WriteLine(i);
            }
            Console.ReadLine();

            LinkedStack<int> stack2 = new LinkedStack<int>();
            stack2.Push(3);
            stack2.Push(9);
            while (!stack2.IsEmpty())
            {
                int i = stack2.Pop();
                Console.WriteLine(i);
            }
            Console.ReadLine();
        }
    }
}