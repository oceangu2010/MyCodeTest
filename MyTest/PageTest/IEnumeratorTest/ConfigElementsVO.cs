using System;
using System.Collections.Generic;
using System.Text;

namespace MyTest.PageTest
{
    [Serializable]
    public enum LevelType
    {
       Transaction = 1,
       Claim       = 2,
       Service     = 3
    };

    [Serializable]
    public class ConfigElementsVO
    {
        private int _id;
		private string _segmentname;
		private string _elementname;
		private string _elementvalue;
		private string _editype= "835";
		private LevelType _levelid;
		private string _leveldescription;

        public ConfigElementsVO()
        {
            _levelid=LevelType.Transaction;
        }

		/// <summary>
		/// pkid
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}

		/// <summary>
		/// SegmentName,example:ISA,GE.....
		/// </summary>
		public string SegmentName
		{
			set{ _segmentname=value;}
			get{return _segmentname;}
		}

		/// <summary>
		/// ElementName,example:ISA01,GE01.....
		/// </summary>
		public string ElementName
		{
			set{ _elementname=value;}
			get{return _elementname;}
		}

		/// <summary>
		/// ElementValue
		/// </summary>
		public string ElementValue
		{
			set{ _elementvalue=value;}
			get{return _elementvalue;}
		}

		/// <summary>
		/// EDIType,example:835,997.....
		/// </summary>
		public string EDIType
		{
			set{ _editype=value;}
			get{return _editype;}
		}
        
		/// <summary>
		/// 1,Transaction;  
        /// 2,Claim;  
        /// 3,Service;
		/// </summary>
		public LevelType LevelId
		{
			set{ _levelid=value;}
			get{return _levelid;}
		}

		/// <summary>
		/// levelDescription
		/// </summary>
		public string LevelDescription
		{
			set{ _leveldescription=value;}
			get{return _leveldescription;}
		}

    }
}
