using System;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;

namespace ConsoleAppInsertTest
{
    class Program
    {
        static int count = 100000;           //插入的条数
        static void Main(string[] args)
        {
            long runTime = 0;

            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnection, CommandType.Text, "TRUNCATE TABLE dbo.Passport");
            runTime = SqlBulkCopyInsert();
            Console.WriteLine(string.Format("使用SqlBulkCopy插入{1}条数据所用的时间是{0}毫秒", runTime, count));

            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnection, CommandType.Text, "TRUNCATE TABLE dbo.Passport");
            runTime = TransactionInsert2();
            Console.WriteLine(string.Format("       使用事务插入{1}条数据所用的时间是{0}毫秒  --使用事务插入数据，分组执行", runTime, count));

            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnection, CommandType.Text, "TRUNCATE TABLE dbo.Passport");
            runTime = TransactionInsert1();
            Console.WriteLine(string.Format("       使用事务插入{1}条数据所用的时间是{0}毫秒  --先存到string再一次性执行", runTime, count));

            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnection, CommandType.Text, "TRUNCATE TABLE dbo.Passport");
            runTime = TransactionInsert();
            Console.WriteLine(string.Format("       使用事务插入{1}条数据所用的时间是{0}毫秒  --在事务里一条一条执行", runTime, count));


            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnection, CommandType.Text, "TRUNCATE TABLE dbo.Passport");
            runTime = CommonInsert1();
            Console.WriteLine(string.Format("       普通方式插入{1}条数据所用的时间是{0}毫秒 --保持SqlConnection", runTime, count));

            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnection, CommandType.Text, "TRUNCATE TABLE dbo.Passport");
            runTime = CommonInsert();
            Console.WriteLine(string.Format("       普通方式插入{1}条数据所用的时间是{0}毫秒  --一条一条插入", runTime, count));

            //使用SqlBulkCopy插入100000条数据所用的时间是1301毫秒
            //       使用事务插入100000条数据所用的时间是5811毫秒  --使用事务插入数据，分组执行
            //       使用事务插入100000条数据所用的时间是23315毫秒  --先存到string再一次性执行
            //       使用事务插入100000条数据所用的时间是15756毫秒  --在事务里一条一条执行
            //       普通方式插入100000条数据所用的时间是50287毫秒 --保持SqlConnection
            //       普通方式插入100000条数据所用的时间是49693毫秒  --一条一条插入

            //使用SqlBulkCopy插入100000条数据所用的时间是1257毫秒
            //       使用事务插入100000条数据所用的时间是5870毫秒  --使用事务插入数据，分组执行
            //       使用事务插入100000条数据所用的时间是25062毫秒  --先存到string再一次性执行
            //       使用事务插入100000条数据所用的时间是16943毫秒  --在事务里一条一条执行
            //       普通方式插入100000条数据所用的时间是55764毫秒 --保持SqlConnection
            //       普通方式插入100000条数据所用的时间是58620毫秒  --一条一条插入

            //使用SqlBulkCopy插入100000条数据所用的时间是1409毫秒
            //       使用事务插入100000条数据所用的时间是6041毫秒  --使用事务插入数据，分组执行
            //       使用事务插入100000条数据所用的时间是23330毫秒  --先存到string再一次性执行
            //       使用事务插入100000条数据所用的时间是15313毫秒  --在事务里一条一条执行
            //       普通方式插入100000条数据所用的时间是50665毫秒 --保持SqlConnection
            //       普通方式插入100000条数据所用的时间是50775毫秒  --一条一条插入

            //通过三次测试分析发现，SqlBulkCopy速度是最快的，其次是SQL语句组合分组后执行，然后是SQL事务一条一条执行，然后是其它的
            //速度比是1:5:15:50,推荐前两种方法

            Console.ReadLine();

        }

        /// <summary>
        /// 使用普通插入数据
        /// </summary>
        /// <returns></returns>
        private static long CommonInsert()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnection, CommandType.Text, "insert into passport(PassportKey) values('" + Guid.NewGuid() + "')");
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        /// <summary>
        /// 使用普通插入数据,保持SqlConnection
        /// </summary>
        /// <returns></returns>
        private static long CommonInsert1()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SqlConnection sqlconn = new SqlConnection(SqlHelper.SqlConnection);
            for (int i = 0; i < count; i++)
            {
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.Text, "insert into passport(PassportKey) values('" + Guid.NewGuid() + "')");
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        /// <summary>
        /// 使用事务插入数据
        /// </summary>
        /// <returns></returns>
        private static long TransactionInsert()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (SqlConnection ConnNow = new SqlConnection(SqlHelper.SqlConnection))
            {
                ConnNow.Open();
                using (SqlTransaction trans = ConnNow.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "insert into passport(PassportKey) values('" + Guid.NewGuid() + "')");
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 使用事务插入数据,先存到string再一次性执行
        /// </summary>
        /// <returns></returns>
        private static long TransactionInsert1()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.AppendFormat("insert into passport(PassportKey) values('{0}');", Guid.NewGuid());
            }
            using (SqlConnection ConnNow = new SqlConnection(SqlHelper.SqlConnection))
            {
                ConnNow.Open();
                using (SqlTransaction trans = ConnNow.BeginTransaction())
                {
                    try
                    {
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sb.ToString());
                        for (int i = 0; i < count; i++)
                        {
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "insert into passport(PassportKey) values('" + Guid.NewGuid() + "')");
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 使用事务插入数据，分组执行-- 感谢 闫道民 提供
        /// </summary>
        /// <returns></returns>
        private static long TransactionInsert2()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (SqlConnection ConnNow = new SqlConnection(SqlHelper.SqlConnection))
            {
                ConnNow.Open();
                string tmp = string.Empty; ;
                int itmp = 0;
                using (SqlTransaction trans = ConnNow.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            itmp++;
                            tmp = tmp + "insert into passport(PassportKey) values('" + Guid.NewGuid() + "');";
                            if (itmp == 180)
                            {
                                itmp = 0;
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, tmp);
                                tmp = string.Empty;
                            }
                        }
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, tmp);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 使用SqlBulkCopy方式插入数据
        /// </summary>
        /// <returns></returns>
        private static long SqlBulkCopyInsert()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            DataTable dataTable = GetTableSchema();
            for (int i = 0; i < count; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[2] = Guid.NewGuid();
                dataTable.Rows.Add(dataRow);
            }

            //Console.WriteLine(stopwatch.ElapsedMilliseconds);//初始化数据时间

            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlHelper.SqlConnection);
            sqlBulkCopy.DestinationTableName = "Passport";

            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                sqlBulkCopy.WriteToServer(dataTable);
            }
            sqlBulkCopy.Close();


            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        private static DataTable GetTableSchema()
        {
            return SqlHelper.ExecuteDataset(SqlHelper.SqlConnection, CommandType.Text, "select * from Passport where 1=2").Tables[0];
        }

    }
}
