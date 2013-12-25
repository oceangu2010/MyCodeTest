using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSerializeDEMO
{
	public class TestClass : IXmlSerializable
	{
		public string StrValue { get; set; }

		public List<int> List { get; set; }

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			StrValue = reader.GetAttribute("s");

			string numbers = reader.ReadString();
			if( string.IsNullOrEmpty(numbers) == false )
				List = (from s in numbers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
						let n = int.Parse(s)
						select n).ToList();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("s", StrValue);
			writer.WriteString(string.Join(",", List.ConvertAll<string>(x => x.ToString()).ToArray()));
		}
	}


	//public class TestClass
	//{
	//    public string StrValue { get; set; }

	//    public List<int> List { get; set; }
	//}

	public class ClassB1
	{
		public TestClass Test { get; set; }
	}


}
