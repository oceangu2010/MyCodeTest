using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MyTest.MyClassTest
{
    /// <summary>
    /// 汉诺塔背景：约19世纪末，在欧州的商店中出售一种智力玩具，在一块铜板上有三根杆，
    /// 最左边的杆上自上而下、由小到大顺序串着由64个圆盘构成的塔。
    /// 目的是将最左边杆上的盘全部移到右边的杆上，条件是一次只能移动一个盘，且不允许大盘放在小盘的上面。
    /// 汉诺塔问题：
    /// 共有3根杆子，在初始的杆子上，有n个圆盘，这些圆盘从下至上按照从大到小的顺序排列。
    /// 现在要把这些原盘移动到第三根杆子上，
    /// 要求，每次移动只能移动一个圆盘，而且大的圆盘不能放到小的圆盘上面。
    /// 标准解法是这样的：
    /// 我们假设杆子为a,b,c，其中a是初始杆，c是目的杆，b是辅助杆。盘子的编号为1...n，其中编号越大的圆盘越大。
    /// 如果我们要移动n个圆盘，我们可以先把n-1个圆盘移动到辅助杆上，然后再把第n个圆盘移动到目的杆上，
    /// 最后把n-1个圆盘从辅助杆移动到目的杆上。
    /// 最开始 a n..1 b             c
    /// 第一步 a n     b n-1..1   c 
    /// 第二步 a        b n-1..1   c n
    /// 第三部 a        b            c n..1
    /// 假设移动其余n-1个圆盘需要L(n-1)步，那么按照上面的步骤去做的话
    /// L(n) = L(n-1) + 1 + L(n-1)
    /// </summary>
    /// 
   
/*
    
标准解法是这样的：
我们假设杆子为a,b,c，其中a是初始杆，c是目的杆，b是辅助杆。盘子的编号为1...n，其中编号越大的圆盘越大。
如果我们要移动n个圆盘，我们可以先把n-1个圆盘移动到辅助杆上，然后再把第n个圆盘移动到目的杆上，
最后把n-1个圆盘从辅助杆移动到目的杆上。
最开始 a n..1 b             c
第一步 a n     b n-1..1   c 
第二步 a        b n-1..1   c n
第三部 a        b            c n..1

假设移动其余n-1个圆盘需要L(n-1)步，那么按照上面的步骤去做的话
L(n) = L(n-1) + 1 + L(n-1)
实际上这也是最为简洁的方法，因为要完成汉诺塔，必须要移动第n块圆盘至少一次。
按照这种思路，要解决移动n个圆盘的问题，只需要解决移动n-1个圆盘的问题，而解决移动n-1个圆盘的问题，
只需解决移动n-2个圆盘的问题......用递归算法，当n=1的时候，问题是最简单的，
我们就把一个圆盘从初始杆移动到目的杆就可以了。
 
所以我们可以把解决问题步骤总结为一个这样的函数：
 
function(初始杆a，目的杆b， 辅助杆c,  问题规模n)
if (n==1)//问题规模为1，则直接把圆盘从初始杆移动到目的杆
else
1  //将n-1个圆盘移动到辅助杆
    function(初始杆a，目的杆c, 辅助杆b, 问题规模n-1)
    //(以初始杆作为初始杆，以目的杆作为辅助杆，以辅助杆作为目的杆，把n-1块圆盘进行移动)
2  将第n个圆盘从初始杆移动到目的杆
3  //将n-1个圆盘移动到目的杆
    function(初始杆b，目的杆b, 辅助杆a, 问题规模n-1)
    //(以初始杆作为辅助杆，以目的杆作为目的杆，以辅助杆作为初始杆，把n-1块圆盘进行移动)
显然到这里，我们已经可以编程解决汉诺塔问题了。
 
那么解决问题规模为n的汉诺塔问题需要多少步呢？
我们观察一下这个过程：
 当n=1时，显然 a -- 1 --> b 
当n=2时，
    1）先把n-1个圆盘移动到辅助杆，因此 a -- 1 --> c 
    2）然后把第n个圆盘移动到目的杆 a -- 2 --> b
    3）再把n-1个圆盘移动到目的杆 c -- 1 --> b
当n=3时，1）先把n-1个圆盘移动到辅助杆, 因此 a --1,2 --> c
为此要转换杆的角色（辅助杆和目的杆互换, b变为辅助杆）：
    1）先把n-1-1个圆盘移动到辅助杆, 因此 a --1-->b
    2）然后把第n-1个圆盘移动到目的杆 a -- 2 --> c
    3）再把n-1-1个圆盘移动到目的杆 b -- 1 --> c
    2）然后把第n个圆盘移动到目的杆 a -- 3 --> b
    3）再把n-1个圆盘移动到目的杆c --1,2 --> b
为此要转换杆的角色 （辅助杆和初始杆互换，a变为辅助杆）
    1）先把n-1-1个圆盘移动到辅助杆, 因此 c --1-->a
    2）然后把第n-1个圆盘移动到目的杆 c -- 2 --> b
    3）再把n-1-1个圆盘移动到目的杆 a -- 1 --> b
 
我们可以观察到完成n个规模汉诺塔的移动，我们需要移动第n个圆盘1次，第n-1个圆盘2次，......第1个圆盘2^(n-1)次
因此共需要步骤 2^(n-1) + 2^(n-2) + ...... 2^0 = 2^n - 1次。
这一点，我们也可以利用公式 L(n) = 2*L(n-1)+1结合数学归纳法来证明。
 
但是，并不是说这个就是最优解法了，这个算法有这样的问题：
    1 这个流程用程序来跑很简单，但是人来做的话没有很直观的参考价值
    2 频繁的递归会造成调用栈的膨胀
其实观察这个程序的运行结果可以看到，当n为偶数的时候，第一个圆盘会一直按照
a-->b-->c-->a这样的顺序移动，而当n为奇数时，顺序是相反的
a-->c-->b-->a
第一个圆盘移动之后，第二步一定是剩下的2个杆上，较小的圆盘移动到较大的圆盘（或者空杆）上面，
我们只要遵循这个原则和上面的移动顺序，就算是人也可以轻易地解决汉诺塔问题哦。
但是这里面的数学原理我还没有想清楚，留待以后讨论。
*/

    public class Hanoi
    {
          Tower _tower1, _tower2, _tower3;

            public Hanoi(int diskCount)
            {
                _tower1 = new Tower("first", diskCount);
                _tower2 = new Tower("second", 0);
                _tower3 = new Tower("third", 0);
            }

            /// <summary>
            /// 开始计算
            /// </summary>
            public void Play()
            {
                PrintTowerMessage();

                //以第一个塔为源，第二个塔为辅助，第三个塔为目标，将第一个塔的盘子全部移动到第三个塔
                Play(_tower1, _tower3, _tower2, _tower1.DiskCount);

                PrintTowerMessage();
            }

            /// <summary>
            /// 内部计算函数
            /// </summary>
            /// <param name="towerFrom">源塔</param>
            /// <param name="towerTo">目标塔</param>
            /// <param name="towerHelp">中间的辅助塔</param>
            /// <param name="count">所需要移动的盘子数目</param>
            private void Play(Tower towerFrom, Tower towerTo, Tower towerHelp, int count)
            {
                if (count > 1)
                {   //当盘子数大于1
                    //首先从源塔把n-1个盘子移动到辅助塔
                    Play(towerFrom, towerHelp, towerTo, count - 1);
                    //从源塔把第n个盘子移动到目标塔
                    towerTo.PushDisk(towerFrom.PopDisk());
                    PrintTowerMessage();
                    //再从辅助塔把n-1个盘子移动到目标塔
                    Play(towerHelp, towerTo, towerFrom, count - 1);
                }
                else//count == 1
                {//如果盘子数为1
                    //直接把盘子从源塔移动到目标塔
                    towerTo.PushDisk(towerFrom.PopDisk());
                    PrintTowerMessage();
                }
            }

            private void PrintTowerMessage()
            {
                Console.WriteLine(_tower1.ToString());
                Console.WriteLine(_tower2.ToString());
                Console.WriteLine(_tower3.ToString());
                Console.ReadLine();
            }


            #region 汉诺塔另一种解法

           public static int moveNum = 0;
            /// <summary>
            /// 汉诺塔经典递归算法
            /// </summary>
            /// <param name="n">圆盘的个数</param>
            /// <param name="a">a柱</param>
            /// <param name="b">b柱</param>
            /// <param name="c">c柱</param>
            public static void HanoiFunction2(int n, string a, string b, string c)
            {
                moveNum++;
                if (n == 1)
                {
                    Move(a, c,moveNum);
                    //出口
                }
                else
                {
                    HanoiFunction2(n - 1, a, c, b);
                    Move(a, c,moveNum);
                    HanoiFunction2(n - 1, b, a, c);
                }
            }

            public static void Move(string sour, string dest,int move)
            {
                HttpContext.Current.Response.Write(string.Format("第{0}步:把{1}石柱最上面的盘子移动到{2}石柱上。<br />",move, sour, dest));
            }


            #endregion
 

        }

        /// <summary>
        /// 塔
        /// </summary>
        internal class Tower
        {
            /// <summary>
            /// 存储结构
            /// </summary>
            Stack<Disk> _tower;
            /// <summary>
            /// 用于显示的塔名
            /// </summary>
            string _towerName;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="towerName">塔名</param>
            /// <param name="count">塔上面的盘子数量</param>
            public Tower(string towerName, int count)
            {
                _towerName = towerName;

                _tower = new Stack<Disk>();

                for (int i = count; i > 0; i--)
                {
                    Disk disk = new Disk() { Number = i };
                    _tower.Push(disk);
                }
            }

            /// <summary>
            /// 取出最上面的盘子
            /// </summary>
            /// <returns></returns>
            public Disk PopDisk()
            {
                return _tower.Pop();
            }

            /// <summary>
            /// 放入一个盘子
            /// </summary>
            /// <param name="disk"></param>
            public void PushDisk(Disk disk)
            {
                if (_tower.Count == 0)
                {
                    _tower.Push(disk);
                }
                else
                {
                    if (_tower.Peek().Number <= disk.Number)
                    {
                        throw new Exception(
                            string.Format("error new disk {0}, top disk {1}, tower:{2}", disk.Number, _tower.Peek().Number, _towerName));
                    }
                    else
                    {
                        _tower.Push(disk);
                    }
                }
            }

            /// <summary>
            /// 盘子数量
            /// </summary>
            public int DiskCount
            {
                get { return _tower.Count; }
            }

            /// <summary>
            /// 显示塔的信息
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                StringBuilder resultBuilder = new StringBuilder();
                resultBuilder.AppendLine(_towerName);
                foreach (Disk disk in _tower)
                {
                    resultBuilder.AppendLine(disk.Number.ToString());
                }
                return resultBuilder.ToString();
            }
        }

        /// <summary>
        /// 盘子
        /// </summary>
        struct Disk
        {
            public int Number;
        }
  
}