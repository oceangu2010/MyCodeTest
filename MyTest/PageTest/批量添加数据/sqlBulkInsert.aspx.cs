using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;

namespace MyTest.PageTest.批量添加数据
{
    public partial class sqlBulkInsert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region 第一种方法 //sql builk方式
        private static string GetConnectionString()
        // To avoid storing the sourceConnection string in your code, 
        // you can retrieve it from a configuration file. 
        {
            return "Data Source=(local); " +
                " Integrated Security=true;" +
                "Initial Catalog=AdventureWorks;";
        }

        //sql builk方式
        void SqlBuilkInsertData()
        {
            string connectionString = GetConnectionString();
            // Open a sourceConnection to the AdventureWorks database.
            using (SqlConnection sourceConnection =
                       new SqlConnection(connectionString))
            {
                sourceConnection.Open();

                // Perform an initial count on the destination table.
                SqlCommand commandRowCount = new SqlCommand(
                    "SELECT COUNT(*) FROM " +
                    "dbo.BulkCopyDemoMatchingColumns;",
                    sourceConnection);
                long countStart = System.Convert.ToInt32(
                    commandRowCount.ExecuteScalar());
                Console.WriteLine("Starting row count = {0}", countStart);

                // Get data from the source table as a SqlDataReader.
                SqlCommand commandSourceData = new SqlCommand(
                    "SELECT ProductID, Name, " +
                    "ProductNumber " +
                    "FROM Production.Product;", sourceConnection);
                SqlDataReader reader =
                    commandSourceData.ExecuteReader();

                // Open the destination connection. In the real world you would 
                // not use SqlBulkCopy to move data from one table to the other 
                // in the same database. This is for demonstration purposes only.
                using (SqlConnection destinationConnection =
                           new SqlConnection(connectionString))
                {
                    destinationConnection.Open();

                    // Set up the bulk copy object. 
                    // Note that the column positions in the source
                    // data reader match the column positions in 
                    // the destination table so there is no need to
                    // map columns.
                    using (SqlBulkCopy bulkCopy =
                               new SqlBulkCopy(destinationConnection))
                    {
                        bulkCopy.DestinationTableName =
                            "dbo.BulkCopyDemoMatchingColumns";

                        try
                        {
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(reader);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            // Close the SqlDataReader. The SqlBulkCopy
                            // object is automatically closed at the end
                            // of the using block.
                            reader.Close();
                        }
                    }

                    // Perform a final count on the destination 
                    // table to see how many rows were added.
                    long countEnd = System.Convert.ToInt32(
                        commandRowCount.ExecuteScalar());
                    Console.WriteLine("Ending row count = {0}", countEnd);
                    Console.WriteLine("{0} rows were added.", countEnd - countStart);
                    Console.WriteLine("Press Enter to finish.");
                    Console.ReadLine();
                }
            }
        }
        #endregion

        #region 第二种方法 表参数插入方式
        //表值参数是SQL Server 2008新特性，简称TVPs
        public static void TableValuedToDB(DataTable dt)
        {
            SqlConnection sqlConn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
            const string TSqlStatement =
             "insert into BulkTestTable (Id,UserName,Pwd)" +
             " SELECT nc.Id, nc.UserName,nc.Pwd" +
             " FROM @NewBulkTestTvp AS nc";
            SqlCommand cmd = new SqlCommand(TSqlStatement, sqlConn);
            SqlParameter catParam = cmd.Parameters.AddWithValue("@NewBulkTestTvp", dt);
            catParam.SqlDbType = SqlDbType.Structured;
            //表值参数的名字叫BulkUdt，在上面的建立测试环境的SQL中有。
            catParam.TypeName = "dbo.BulkUdt";
            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
            new DataColumn("Id",typeof(int)),
            new DataColumn("UserName",typeof(string)),
            new DataColumn("Pwd",typeof(string))});

            return dt;
        }

        static void loadSqlBulk()
        {
            Stopwatch sw = new Stopwatch();
            for (int multiply = 0; multiply < 10; multiply++)
            {
                DataTable dt = GetTableSchema();
                for (int count = multiply * 100000; count < (multiply + 1) * 100000; count++)
                {
                    DataRow r = dt.NewRow();
                    r[0] = count;
                    r[1] = string.Format("User-{0}", count * multiply);
                    r[2] = string.Format("Pwd-{0}", count * multiply);
                    dt.Rows.Add(r);
                }
                sw.Start();
                TableValuedToDB(dt);
                sw.Stop();
                Console.WriteLine(string.Format("Elapsed Time is {0} Milliseconds", sw.ElapsedMilliseconds));
            }

            Console.ReadLine();
        }
        #endregion

        #region 第三种方法 /*使用CTE递归循环插入 运用CTE递归插入，速度较快*/  
        /*
        TRUNCATE table Customers  
        GO 
        DBCC DROPCLEANBUFFERS  
        DBCC FREEPROCCACHE  
  
        SET STATISTICS IO ON;  
        SET STATISTICS TIME ON;  
        GO  
  
        DECLARE @d Datetime  
        SET @d=getdate();  
  
        WITH Seq (num,CustomerNumber, CustomerName, CustomerCity) AS  
        (SELECT 1,'0000','Customer 0',cast('X-City' as NVARCHAR(20))  
        UNION ALL  
        SELECT num + 1,'0000','Customer 0',  
        cast(CHAR(65 + (num % 26)) + '-City' AS NVARCHAR(20))  
        FROM Seq  
        WHERE num <= 5000000  
        )  
        INSERT INTO Customers (CustomerNumber, CustomerName, CustomerCity)  
        SELECT CustomerNumber, CustomerName, CustomerCity  
        FROM Seq  
        OPTION (MAXRECURSION 0)  
  
        select [500万数据量插入完毕，共花费时间(毫秒)]=datediff(ms,@d,getdate())  
  
        SET STATISTICS IO OFF ;  
        SET STATISTICS TIME OFF;  
        GO
        * //*CTE批量递归500万数据量 用时一般大概3分钟 因个人电脑而已*/
        #endregion
    }
}