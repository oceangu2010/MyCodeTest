using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XmlSerializeDEMO
{
	[XmlRoot(Namespace = "http://msdn.microsoft.com/vsdata/xsd/vsdh.xsd")]
	public class DynamicHelp
	{
		[XmlElement("LinkGroup")]
		public List<LinkGroup> Groups { get; set; }

		public Context Context { get; set; }
	}

	public class LinkGroup 
	{
		[XmlAttribute]
		public string ID { get; set; }
		[XmlAttribute]
		public string Title { get; set; }
		[XmlAttribute]
		public int Priority { get; set; }

		public Glyph Glyph { get; set; }
	}

	public class Glyph
	{
		[XmlAttribute]
		public int Collapsed { get; set; }
		[XmlAttribute]
		public int Expanded { get; set; }
	}

	public class Context 
	{
		public List<LItem> Links { get; set; }
	}

	public class LItem
	{
		[XmlAttribute]
		public string URL { get; set; }
		[XmlAttribute]
		public string LinkGroup { get; set; }

		[XmlText]
		public string Title { get; set; }
	}

}
