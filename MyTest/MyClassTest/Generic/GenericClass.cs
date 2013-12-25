using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

/*
 * 
 * 
 泛型 类使用测试实例
 * 
 */
namespace MyTest.MyClassTest
{
    public class GenericClass
    {
        public GenericClass()
        {

        }


    }

    #region 在堆栈中实现泛型

    public class StackClass<T>
    {
        T[] m_Items;//声明一个泛型数组
        int m_StackPointer = 0; //指针
        readonly int m_size;

        //默认数组最大默认项数为100
        public StackClass()
            : this(100)
        { }

        //构造函数
        public StackClass(int size)
        {
            this.m_size = size;
            m_Items = new T[m_size];
        }

        /// <summary>
        /// 压栈操作
        /// </summary>
        /// <param name="item">数组类型</param>
        public void Push(T item)
        {
            if (m_StackPointer >= m_size)
                throw new StackOverflowException();//堆栈溢出
            m_Items[m_StackPointer] = item; //开如压栈赋值
            m_StackPointer++;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            m_StackPointer--;//指针递减

            if (m_StackPointer >= 0)
            {
                return m_Items[m_StackPointer];
            }
            else
            {
                m_StackPointer = 0;
                return default(T);
            }
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public T[] PopArray()
        {
            if (m_StackPointer > 0)
            {
                return m_Items;
            }

            return null;

        }

    }

    #endregion

    #region 一般链表

    //链表节点 定义
    public class Node<K, T>
    {
        public K Key; //位置
        public T Item; //值
        public Node<K, T> NextNode;

        public Node()
        {
            Key = default(K);
            Item = default(T);
            NextNode = null;
        }

        public Node(K key, T item, Node<K, T> nextNode)
        {
            Key = key;
            Item = item;
            NextNode = nextNode;
        }
    }

    //链表节点
    public class LinkedList<K, T>
    {
        Node<K, T> m_Head;
        List<T> list;

        public LinkedList()
        {
            m_Head = new Node<K, T>();
            list = new List<T>();
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        public void AddHead(K key, T item)
        {
            Node<K, T> newNode = new Node<K, T>(key, item, m_Head.NextNode);
            m_Head.NextNode = newNode;
            list.Add(item);

        }

        //输出节点
        public List<T> OutputLinkNodes()
        {

            if (list.Count != 0)
                return list;
            else
                return null;
        }

    }

    #endregion

    #region 任意泛型序列化

    public class SerializableHelper
    {
        /// <summary>
        /// 序列化成一个字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte[] SerializeToBytes<T>(T t)
        {
            MemoryStream mStream = new MemoryStream();
            BinaryFormatter ser = new BinaryFormatter();
            ser.Serialize(mStream, t);
            return mStream.ToArray();
        }

        /// <summary>
        /// 序列化成一个字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>序列化代码</returns>
        public static string SerializeToXml<T>(T t)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                Stream stream = new MemoryStream();
                s.Serialize(stream, t);

                stream.Seek(0, SeekOrigin.Begin); //这一点非常重要 否则无法读取
                string strSource = "";
                using (StreamReader reader = new StreamReader(stream))
                {
                    strSource = reader.ReadToEnd();
                }
                return strSource;
            }
            catch { return null; }

        }
        /// <summary>
        /// xml 文件反序列化为泛型数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(FileInfo fi)
        {
            if (fi.Exists == false) return default(T);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            FileStream fs = fi.OpenRead();
            T t;
            try
            {
                t = (T)xmlSerializer.Deserialize(fs);
            }
            finally
            {
                fs.Close();
            }
            return t;
        }
        /// <summary>
        /// 字符串反序列化成一个类
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(string xmlSource)
        {
            if (string.IsNullOrEmpty(xmlSource)) return default(T);

            try
            {
                XmlSerializer x = new XmlSerializer(typeof(T));

                Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlSource));
                stream.Seek(0, SeekOrigin.Begin);
                object obj = x.Deserialize(stream);
                stream.Close();

                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }
        public static Dictionary<TKey, TValue> DeSerialize<TKey, TValue>(FileInfo fi)
        {
            if (fi.Exists == false) return default(Dictionary<TKey, TValue>);

            FileStream fs = fi.OpenRead();
            if (fs.Length == 0) return default(Dictionary<TKey, TValue>);

            XmlReader reader = XmlReader.Create(fs);
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;

            if (wasEmpty)
                return default(Dictionary<TKey, TValue>);

            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element) continue;
                if (reader.Name == "Root") continue;

                reader.ReadStartElement("Row");
                reader.ReadStartElement("Key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("Value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                dic.Add(key, value);
                reader.ReadEndElement();
                //reader.MoveToContent();

            }
            return dic;
        }

        /// <summary>
        /// 序列化泛型数组为xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="FullName"></param>
        /// <returns>是否序列化成功</returns>
        public static bool Serialize<T>(T t, string FullName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(FullName);
            try
            {
                xmlSerializer.Serialize(writer, t);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                writer.Close();
            }
        }
        /// <summary>
        /// 序列化 Dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="FullName"></param>
        /// <returns></returns>
        public static bool Serialize<TKey, TValue>(Dictionary<TKey, TValue> dic, string FullName)
        {
            try
            {
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                settings.Indent = true;
                settings.ConformanceLevel = ConformanceLevel.Fragment;

                XmlWriter writer = XmlWriter.Create(FullName, settings);


                XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
                XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));


                writer.WriteStartElement("Root");
                foreach (KeyValuePair<TKey, TValue> kv in dic)
                {
                    writer.WriteStartElement("Row");
                    writer.WriteStartElement("Key");
                    KeySerializer.Serialize(writer, kv.Key);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Value");
                    ValueSerializer.Serialize(writer, kv.Value);
                    writer.WriteEndElement();
                    writer.WriteEndElement();

                } writer.WriteEndElement();
                writer.Close();

                return true;
            }
            catch
            {
                try
                {
                    File.Delete(FullName);
                }
                catch { }
                return false;
            }
        }


        /// <summary>
        /// 序列化DataTable
        /// </summary>
        /// <param name="pDt">包含数据的DataTable</param>
        /// <returns>序列化的DataTable</returns>
        public static string SerializeDataTableXml(DataTable pDt)
        {
            // 序列化DataTable
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }

        /// <summary>
        /// 反序列化DataTable
        /// </summary>
        /// <param name="pXml">序列化的DataTable</param>
        /// <returns>DataTable</returns>
        public static DataTable DeserializeDataTable(string pXml)
        {

            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));

            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;

            return dt;
        }


    }


    #endregion

    ///// <summary>
    ///// 泛型继承
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class Man<T> where T : Man<T>
    //{
    //   public Man(T type,string name)
    //   {

    //   }
    //}

    ///// <summary>
    ///// 继承父类
    ///// </summary>
    //public class boy : Man<boy>
    //{
    //    public string name;
    //    public boy()
    //    {
    //    }
    //    //起名字
    //    public void SetName()
    //    {
    //        HttpContext.Current.Response.Write("男孩子的名字就叫:" + this.name + "<br />");
    //    }
    //}

    //public class girl : Man<girl>
    //{
    //    public string name;
    //    public  girl()
    //    {
    //    }

    //    //起名字
    //    public  void SetName()
    //    {
    //        HttpContext.Current.Response.Write("女孩子的名字就叫:"+this.name+"<br />");
    //    }
    //}
}

//namespace Gfl.Comm
//{
//    /// <summary>
//    /// 实体框架助手
//    /// </summary>
//    public class EFHelper
//    {
//        /// <summary>
//        /// 对象上下文
//        /// </summary>
//        public ObjectContext Context { get; set; }
//        /// <summary>
//        /// 修改值
//        /// </summary>
//        /// <typeparam name="T">泛型类</typeparam>
//        /// <param name="aChanged">修改后的值</param>
//        /// <param name="aOrgValue">修改前的值</param>
//        public void UpdateObject<T>(T aChanged, T aOrgValue)
//            where T : IEntityWithKey
//        {
//            string entityname = typeof(T).Name;
//            Context.Attach(aChanged);
//            Context.ApplyOriginalValues(entityname, aOrgValue as IEntityWithKey);
//        }

//        /// <summary>
//        /// 增加一个对象
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="aValue"></param>
//        public void AddObject<T>(T aValue)
//            where T : IEntityWithKey
//        {
//            Context.AddObject(typeof(T).Name, aValue);
//        }
//    }
//}


/*
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Dare.Utilities.Data;
using Dare.DN.Components;
using System.Data;
using log4net.Core;

namespace Dare.DN.Data
{
    public abstract class DataProviderTemplate<Entity， Key> : DataProviderBase
        where Entity : class， IEntity， IEntity<Key>，new（）
        where Key : struct
    {
        
        protected List<EntityRelationDataProvider<Entity， Key>> relationDataProviders;

        public DataProviderTemplate（）
        {
            relationDataProviders = new List<EntityRelationDataProvider<Entity， Key>>（）;
        }

        ＃region 关系操纵办法
        public void AddRelationDataProviders（params EntityRelationDataProvider<Entity， Key>[] providers）
        {
            relationDataProviders.AddRange（providers）;
        }

        protected virtual void InsertRelations（TransactionManager manager， DbCommand cmd， IEnumerable<Entity> entities）
        {
            if （entities == null） return;
            foreach （EntityRelationDataProvider<Entity， Key> provider in relationDataProviders）
            {
                provider.Insert（manager， cmd， entities）;
            }
        }

        protected virtual void InsertRelations（TransactionManager manager， DbCommand cmd， params Entity[] entities）
        {
            InsertRelations（manager， cmd， （IEnumerable<Entity>）entities）;
        }

        protected virtual void UpdateRelations（TransactionManager manager， DbCommand cmd， IEnumerable<Entity> entities）
        {
            if （entities == null） return;
            foreach （EntityRelationDataProvider<Entity， Key> provider in relationDataProviders）
            {
                provider.Update（manager， cmd， entities）;
            }
        }

        protected virtual void UpdateRelations（TransactionManager manager， DbCommand cmd， params Entity[] entities）
        {
            UpdateRelations（manager， cmd， （IEnumerable<Entity>）entities）;
        }

        protected virtual void DeleteRelations（TransactionManager manager， DbCommand cmd， IEnumerable<Key> keys）
        {
            if （keys == null） return;
            foreach （EntityRelationDataProvider<Entity， Key> provider in relationDataProviders）
            {
                provider.Delete（manager， cmd， keys）;
            }
        }

        protected virtual void DeleteRelations（TransactionManager manager， DbCommand cmd， params Key[] keys）
        {
            DeleteRelations（manager， cmd， （IEnumerable<Key>）keys）;
        }

        protected virtual void DeleteRelations（TransactionManager manager， DbCommand cmd， string whereClause）
        {
            string whereCluase = ProcessWhereClause（whereClause）;
            foreach （EntityRelationDataProvider<Entity， Key> provider in relationDataProviders）
            {
                provider.Delete（manager， cmd， whereClause）;
            }
        }

        protected virtual void CleanRelations（DbCommand cmd）
        {
            foreach （EntityRelationDataProvider<Entity， Key> provider in relationDataProviders）
            {
                provider.Clean（cmd）;
            }
        }

        protected virtual void RetrieveRelations（TransactionManager manager， DbCommand cmd， IEnumerable<Entity> entities）
        {
            if （entities == null） return;
            foreach （EntityRelationDataProvider<Entity， Key> provider in relationDataProviders）
            {
                provider.Retrieve（manager， cmd， entities）;
            }
        }

        protected virtual void RetrieveRelations（TransactionManager manager， DbCommand cmd， params Entity[] entities）
        {
            RetrieveRelations（manager， cmd， （IEnumerable<Entity>）entities）;
        }
        ＃endregion

        ＃region 根蒂根基操纵办法
        public abstract void Insert（TransactionManager manager， Entity entity）;


        public abstract void Import（TransactionManager manager， IEnumerable<Entity> entities）;

        public abstract void Update（TransactionManager manager， Entity entity）;

        public abstract void Delete（TransactionManager manager， Key key）;

        public abstract void DeleteAll（TransactionManager manager， string whereClause）;

        public abstract Entity Get（TransactionManager manager， Key key）;

        public virtual List<Entity> GetAll（TransactionManager manager， string whereClause， string orderBy）
        {
            return GetPaged（manager， whereClause， orderBy， -1， -1）;
        }

        public abstract List<Entity> GetPaged（TransactionManager manager， string whereClause， string orderBy， int pageIndex， int pageSize）;

        public abstract int GetCount（TransactionManager manager， string whereClause）;

        public abstract long GetVersion（TransactionManager manager， string whereClause）;

        public abstract Entity Fill（IDataReader reader， Entity entity）;
        ＃endregion

        ＃region 内部操纵模板办法
        protected virtual int InternaluteNonQuery（TransactionManager manager， string sql， params DbParameter[] parameters）
        {
            int result = 0;
            ute（manager， delegate（TransactionManager mgr， DbCommand cmd）
            {
               
                //获取SQL语句
                cmd.CommandText = sql;
                cmd.Parameters.AddRange（parameters）;

                //履行
                result = cmd.uteNonQuery（）;
                
               
            }）;

            return result;
        }

        protected virtual List<Entity> InternalGetList（TransactionManager manager， string sql， params DbParameter[] parameters）
        {
            List<Entity> list = new List<Entity>（）;
            ute（manager， delegate（TransactionManager mgr， DbCommand cmd）
            {
               
                //获取SQL语句
                cmd.CommandText = sql;
                cmd.Parameters.AddRange（parameters）;

                //履行获取实体集
                using （IDataReader reader = cmd.uteReader（））
                {
                    while （reader.Read（））
                    {
                        list.Add（Fill（reader， null））;
                    }
                }

                //获取关系对象
                RetrieveRelations（mgr， cmd， list）;

               
            }）;

            return list;
        }

        protected virtual List<Key> InternalGetKeyList（TransactionManager manager， string sql， params DbParameter[] parameters）
        {
            List<Key> list = new List<Key>（）;
            ute（manager， delegate（TransactionManager mgr， DbCommand cmd）
            {
               
                //获取SQL语句
                cmd.CommandText = sql;
                cmd.Parameters.AddRange（parameters）;

                //履行获取实体集
                using （IDataReader reader = cmd.uteReader（））
                {
                    while （reader.Read（））
                    {
                        list.Add（（Key）Convert.ChangeType（reader.GetValue（0）， typeof（Key）））;
                    }
                }

                
            }）;

            return list;
        }

        protected virtual T InternalGetValue<T>（TransactionManager manager， string sql， T defaultValue， params DbParameter[] parameters）
        {
            T val = default（T）;
            ute（manager， delegate（TransactionManager mgr， DbCommand cmd）
            {
               
                //获取SQL语句
                cmd.CommandText = sql;
                cmd.Parameters.AddRange（parameters）;

                //履行获取实体集
                object objValue = cmd.uteScalar（）;
                val = DbConvert.ChangeType<T>（objValue， defaultValue）;

                
            }）;

            return val;
        }

        protected virtual List<T> InternalGetValueList<T>（TransactionManager manager， string sql， T defaultValue， params DbParameter[] parameters）
        {
            List<T> list = new List<T>（）;
            ute（manager， delegate（TransactionManager mgr， DbCommand cmd）
            {
               
                //获取SQL语句
                cmd.CommandText = sql;
                cmd.Parameters.AddRange（parameters）;

                //履行获取实体集
                using （IDataReader reader = cmd.uteReader（））
                {
                    while （reader.Read（））
                    {
                        list.Add（DbConvert.ChangeType<T>（reader[0]， defaultValue））;
                    }
                }

                
            }）;

            return list;
        }

        public delegate T FillEntityHandler<T>（IDataReader reader， T entity）;
        public delegate void AfterRetrievedEntityListHandler<T>（TransactionManager mgr， DbCommand cmd， IEnumerable<T> entities）;

        protected T InternalGet<T>（TransactionManager manager， string sqlGet， FillEntityHandler<T> fillEntityHandler， AfterRetrievedEntityListHandler<T> afterRetrieveHandler， params DbParameter[] parameters）
            where T : class
        {
            if （fillEntityHandler == null） throw new ArgumentNullException（"fillEntityHandler"， "实体数据填充调用办法不克不及为空!"）;
            T entity = null;
            ute（manager， delegate（TransactionManager mgr， DbCommand cmd）
            {
               
                cmd.CommandText = sqlGet;
                cmd.Parameters.AddRange（parameters）;

                //履行获取实体
                using （IDataReader reader = cmd.uteReader（））
                {
                    if （reader.Read（））
                    {
                        entity = fillEntityHandler（reader， null）;
                    }
                }

                if （afterRetrieveHandler != null && entity != null）
                {
                    afterRetrieveHandler（mgr， cmd， new T[] { entity }）;
                }
                
            }）;

            return entity;
        }

        protected Entity InternalGet（TransactionManager manager， string sqlGet， params DbParameter[] parameters）
        {
            return InternalGet<Entity>（manager， sqlGet， Fill， RetrieveRelations， parameters）;
        }

        protected List<Entity> InternalGetPaged（TransactionManager manager， string sqlGetPaged， string sqlGetCount， int pageIndex， int pageSize， out int totalCount， params DbParameter[] parameters）
        {
            return InternalGetPaged<Entity>（manager， sqlGetPaged， sqlGetCount， Fill， RetrieveRelations， pageIndex， pageSize， out totalCount， parameters）;
        }

        protected List<T> InternalGetPaged<T>（TransactionManager manager， string sqlGetPaged， string sqlGetCount， FillEntityHandler<T> fillEntityHandler， AfterRetrievedEntityListHandler<T> afterRetrieveHandler， int pageIndex， int pageSize， out int totalCount， params DbParameter[] parameters）
            where T:class
        {
            if （fillEntityHandler == null） throw new ArgumentNullException（"fillEntityHandler"， "实体数据填充调用办法不克不及为空!"）;
            List<T> list = null;
            int count = 0;
            ute（manager， delegate（TransactionManager mgr， DbCommand cmd）
            {
               
                //获取总记录数
                cmd.CommandText = sqlGetCount;
                cmd.Parameters.AddRange（parameters）;
                count = Convert.ToInt32（cmd.uteScalar（））;

                if （count > 0）
                {
                    //断定分页和获取记录的数量
                    if （pageIndex < 0 || pageSize <= 0）
                    {
                        list = new List<T>（count）; //不消分页
                    }
                    else if （count > （pageIndex-1） * pageSize）
                    {
                        list = new List<T>（pageSize）;  //应用分页
                    }
                    else
                    {
                        list = new List<T>（0）; //分页跨越记录数量，输出空记录集
                        return;
                    }

                    //获取记录集
                    cmd.CommandText = sqlGetPaged;
                    using （IDataReader reader = cmd.uteReader（））
                    {
                        while （reader.Read（））
                        {
                            list.Add（fillEntityHandler（reader， null））;
                        }
                    }

                    if （afterRetrieveHandler != null）
                    {
                        afterRetrieveHandler（mgr， cmd， list）;
                    }
                }
                else
                {
                    list = new List<T>（0）; //无记录返回空记录集
                    return;
                }
                
            }）;

            totalCount = count;
            return list;
        }
        ＃endregion
    }
}
*/



