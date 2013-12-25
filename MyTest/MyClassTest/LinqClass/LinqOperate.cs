using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTest.MyClassTest.LinqClass;
using  System.Data.Linq;
using System.Web.UI.WebControls;

namespace MyTest.MyClassTest
{
    /*实现linq的一些基本操作，包括增、删除、改、查功能的一些事例演示*/

    public class LinqOperate
    {
        private int[] arr = {23,24,13,3,24,56,43,33,12,7};

        public  void  LinqTest1()
        {
            int[] arr2 = {101, 99, 88};

            //合并数组
            var arr3=  arr.Union(arr2);

            var bb= arr3.Last(c=>c==13);


            arr3.ToList().ForEach(c=>HttpContext.Current.Response.Write(string.Format("{0},",c)));

            //在给定的数组元素中查找33这个元素所在位置
           //var find = from p in arr  
           //           where arr[p]==24
           //           select  p;
           // var aa = find.;

           // return 0;
        }

        public  void AutoAttribute()
        {
            //嵌套对象初始化
            User user = new User
            {
                Id = 1,
                Name = "张三丰",
                Age = 22,
                Address = new Address
                {
                    AddressName = "NanJing",
                    AddrID = 21000
                }
            };



        }//方法end


        /// <summary>
        /// 查询符合条件的第一个信息
        /// </summary>
        /// <param name="table"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public  Customer QueryCustomer(Table<Customer> table ,string companyName) 
        {
            return table.FirstOrDefault(c => c.CompanyName==companyName) ;
        }

        /// <summary>
        /// 查询符合条件的集合
        /// </summary>
        /// <param name="table"></param>
        /// <param name="whereCompanyName"></param>
        /// <returns></returns>
        public List<Customer> QueryCustomerList(Table<Customer> table, string whereCompanyName)
        {
            return table.Where(c=> c.CompanyName.Contains(whereCompanyName)).ToList();
        }

        /// <summary>
        /// 取模糊匹配查询中最后一个符合条件的元素
        /// </summary>
        /// <param name="table"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public Customer QueryLastCustomer(Table<Customer> table, string companyName)
        {
            var query = from a in table
                        where a.CompanyName.Contains(companyName)
                        orderby a.CustomerID ascending  
                        select a;

            //      var aa=    from a in table where a.City.  ;
            var qq = (from b in table
                     where b.City.Contains(companyName)
            //         orderby b.CustomerID descending
                     select new
                                {
                                    b.City,
                                    b.Address,
                                    b.CompanyName
                                }).Distinct();

          //  qq.First(a => a.Address.Equals("aa"));
           // qq.SelectMany(b => b.CompanyName.Contains("bb"));
            var tt = qq.FirstOrDefault(c => c.CompanyName.Equals(companyName));
            Customer ct=new Customer()
                            {
                                
                               ContactTitle = "qqq",
                               Address = "qq",
                               City  ="qq"
                            };

            //循环键值
            foreach (var q in qq)
            {
               Console.WriteLine("city:{0},address:{1},companyname:{2}",q.City,q.Address,q.CompanyName);
                
            }
            
            return query.First();
        }



 /*       public List<T> Page<T>(string keys, int pageIndex, int pageSize, out int count) where T : IComparable<T>
        {
           
            using (T ctx = new T())
            {
                var es = ctx.CreateObjectSet();
                count = es.Count();
                var query = es.OrderBy(keys).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return query.ToList();
            }
        }
*/


        #region linq通用分页方法

        public void BindBoundControl<TSource>(IEnumerable<TSource> DataSource, GridView BoundControl, int PageSize,int currentIndex)
        {
            //获取总记录数（这里可以使用参数传入总页数　就不必每次都执行下面方法）
            int totalRecordCount = DataSource.Count();
            //计算总页数
            int totalPageCount = 0;
            if (PageSize == 0)
            {
                PageSize = totalRecordCount;
            }
            if (totalRecordCount % PageSize == 0)
            {
                totalPageCount = totalRecordCount / PageSize;
            }
            else
            {
                totalPageCount = totalRecordCount / PageSize + 1;
            }

            //从参数中获取当前页码
            if (currentIndex<0)
                currentIndex = 1;

            //如果从参数中获取页码不正确　设置页码为第一页
            if ( currentIndex <= 0 || currentIndex > totalPageCount)
            {
                currentIndex = 1;
            }
         // if(currentIndex*PageSize>totalRecordCount) 
            //绑定数据源
            BoundControl.DataSource = DataSource.Skip((currentIndex - 1) * PageSize).Take(PageSize);
            BoundControl.DataBind();
        }

        #endregion

    }//类end


    internal class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }


        public Address Address { get; set; }
    }

    internal class Address
    {
        public int  AddrID { get; set; }
        public string AddressName { get; set; }
    }
}