using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XmlSerializeDEMO
{
	public class Class1
	{
		public int IntValue { get; set; }

		public string StrValue { get; set; }
	}


	public class Class2
	{
		[XmlAttribute]
		public int IntValue { get; set; }

		[XmlElement]
		public string StrValue { get; set; }
	}


	public class Class3
	{
		[XmlAttribute]
		public int IntValue { get; set; }

		[XmlText]
		public string StrValue { get; set; }
	}


	[XmlType("c4")]
	public class Class4
	{
		[XmlAttribute("id")]
		public int IntValue { get; set; }

		[XmlElement("name")]
		public string StrValue { get; set; }
	}


	// 二种Attribute都可以完成同样的功能。
	//[XmlType("c4List")]
	[XmlRoot("c4List")]
	public class Class4List : List<Class4> { }


	//public class Root
	//{
	//    public Class3 Class3 { get; set; }

	//    public List<Class2> List { get; set; }
	//}

	//public class Root
	//{
	//    public Class3 Class3 { get; set; }

	//    [XmlArrayItem("c2")]
	//    [XmlArray("cccccccccccc")]
	//    public List<Class2> List { get; set; }
	//}

	public class Root
	{
		public Class3 Class3 { get; set; }

		[XmlElement("c2")]
		public List<Class2> List { get; set; }
	}


	public class XBase { }

	[XmlType("x1")]
	public class X1 : XBase
	{
		[XmlAttribute("aa")]
		public int AA { get; set; }

		[XmlAttribute("bb")]
		public int BB { get; set; }
	}

	[XmlType("x2")]
	public class X2 : XBase
	{
		[XmlElement("cc")]
		public string CC { get; set; }

		[XmlElement("dd")]
		public string DD { get; set; }
	}
	
	public class XRoot
	{
		[XmlArrayItem(typeof(X1)),
		XmlArrayItem(typeof(X2))]
		public List<XBase> List { get; set; }
	}


	//public class TestIgnore
	//{
	//    [XmlIgnore]	// 这个属性将不会参与序列化
	//    public int IntValue { get; set; }

	//    public string StrValue { get; set; }

	//    public string Url;
	//}

	public class TestIgnore
	{
		[XmlIgnore]	// 这个属性将不会参与序列化
		public int IntValue { get; set; }

		[XmlElement(Order = 1)]
		public string StrValue { get; set; }

		[XmlElement(Order = 2)]
		public string Url;
	}
}
