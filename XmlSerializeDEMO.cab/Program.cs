using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMVC;


namespace XmlSerializeDEMO
{
	class Program
	{

		static void Main(string[] args)
		{
			TestMyBool();

			Console.ReadKey();
		}


		static void DEMO_1()
		{
			Class1 c1 = new Class1 { IntValue = 3, StrValue = "Fish Li" };
			string xml = XmlHelper.XmlSerialize(c1, Encoding.UTF8);
			Console.WriteLine(xml);

			Console.WriteLine("---------------------------------------");

			Class1 c2 = XmlHelper.XmlDeserialize<Class1>(xml, Encoding.UTF8);
			Console.WriteLine("IntValue: " + c2.IntValue.ToString());
			Console.WriteLine("StrValue: " + c2.StrValue);
		}


		static void DEMO_2()
		{
			Class2 c1 = new Class2 { IntValue = 3, StrValue = "Fish Li" };
			string xml = XmlHelper.XmlSerialize(c1, Encoding.UTF8);
			Console.WriteLine(xml);

			Console.WriteLine("---------------------------------------");

			Class2 c2 = XmlHelper.XmlDeserialize<Class2>(xml, Encoding.UTF8);
			Console.WriteLine("IntValue: " + c2.IntValue.ToString());
			Console.WriteLine("StrValue: " + c2.StrValue);
		}

		static void DEMO_3()
		{
			Class3 c1 = new Class3 { IntValue = 3, StrValue = "Fish Li" };
			string xml = XmlHelper.XmlSerialize(c1, Encoding.UTF8);
			Console.WriteLine(xml);

			Console.WriteLine("---------------------------------------");

			Class3 c2 = XmlHelper.XmlDeserialize<Class3>(xml, Encoding.UTF8);
			Console.WriteLine("IntValue: " + c2.IntValue.ToString());
			Console.WriteLine("StrValue: " + c2.StrValue);
		}


		static void DEMO_4()
		{
			Class4 c1 = new Class4 { IntValue = 3, StrValue = "Fish Li" };
			string xml = XmlHelper.XmlSerialize(c1, Encoding.UTF8);
			Console.WriteLine(xml);

			Console.WriteLine("---------------------------------------");

			Class4 c2 = XmlHelper.XmlDeserialize<Class4>(xml, Encoding.UTF8);
			Console.WriteLine("IntValue: " + c2.IntValue.ToString());
			Console.WriteLine("StrValue: " + c2.StrValue);
		}


		static void DEMO_5()
		{
			Class4 c1 = new Class4 { IntValue = 3, StrValue = "Fish Li" };
			Class4 c2 = new Class4 { IntValue = 4, StrValue = "http://www.cnblogs.com/fish-li/" };

			// 说明：下面二行代码的输出结果是一样的。
			//List<Class4> list = new List<Class4> { c1, c2 };
			//Class4[] list = new Class4[] { c1, c2 };

			Class4List list = new Class4List { c1, c2 };

			string xml = XmlHelper.XmlSerialize(list, Encoding.UTF8);
			Console.WriteLine(xml);

			// 序列化的结果，反序列化一定能读取，所以就不再测试反序列化了。
		}


		static void DEMO_6()
		{
			Class2 c1 = new Class2 { IntValue = 3, StrValue = "Fish Li" };
			Class2 c2 = new Class2 { IntValue = 4, StrValue = "http://www.cnblogs.com/fish-li/" };

			Class3 c3 = new Class3 { IntValue = 5, StrValue = "Test List" };

			Root root = new Root { Class3 = c3, List = new List<Class2> { c1, c2 } };

			string xml = XmlHelper.XmlSerialize(root, Encoding.UTF8);
			Console.WriteLine(xml);
		}


		static void DEMO_7()
		{
			X1 x1a = new X1 { AA = 1, BB = 2 };
			X1 x1b = new X1 { AA = 3, BB = 4 };
			X2 x2 = new X2 { CC = "ccccccccccc", DD = "dddddddddddd" };
			XRoot root = new XRoot { List = new List<XBase> { x1a, x1b, x2 } };

			string xml = XmlHelper.XmlSerialize(root, Encoding.UTF8);
			Console.WriteLine(xml);


			XRoot root2 = XmlHelper.XmlDeserialize<XRoot>(xml, Encoding.UTF8);

		}

		static void Read_DynamicHelp()
		{
			DynamicHelp help = XmlHelper.XmlDeserializeFromFile<DynamicHelp>("Links.xml", Encoding.UTF8);

			foreach( LinkGroup group in help.Groups )
				Console.WriteLine("ID: {0}, Title: {1}, Priority: {2}, Collapsed: {3}, Expanded: {4}",
					group.ID, group.Title, group.Priority, group.Glyph.Collapsed, group.Glyph.Expanded);
			
			foreach( LItem item in help.Context.Links )
				Console.WriteLine("URL: {0}, LinkGroup: {1}, Title: {2}",
					item.URL.Substring(0, 15), item.LinkGroup, item.Title);

		}

		static void TestIgnore()
		{
			TestIgnore c1 = new TestIgnore { IntValue = 3, StrValue = "Fish Li" };
			c1.Url = "http://www.cnblogs.com/fish-li/";

			string xml = XmlHelper.XmlSerialize(c1, Encoding.UTF8);
			Console.WriteLine(xml);
		}


		static void TestMyBool()
		{
			TestClass test = new TestClass { StrValue = "Fish Li", List = new List<int> { 1, 2, 3, 4, 5 } };
			ClassB1 b1 = new ClassB1 { Test = test };

			string xml = XmlHelper.XmlSerialize(b1, Encoding.UTF8);
			Console.WriteLine(xml);

			Console.WriteLine("-----------------------------------------------------");

			ClassB1 b2 = XmlHelper.XmlDeserialize<ClassB1>(xml, Encoding.UTF8);
			Console.WriteLine("StrValue: " + b2.Test.StrValue);
			foreach( int n in b2.Test.List )
				Console.WriteLine(n);
		}
	}




	

}
