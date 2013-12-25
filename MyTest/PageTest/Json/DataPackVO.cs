using System;
using System.Collections.Generic;
using System.Text;


    public enum LevelDef
    {
        Root = 0,
        Transaction,
        Claim,
        ServiceLine
    };

    public enum TemplateDef
    {
        Default = 0,
        NAOX_Correspondence = 1,
        NAOX_ZeroPay = 2
    }

    [Serializable]
    public class DataPackVO
    {
        private string _version;
        private int _currentPI;
        private List<PageVO> _pages;

        public DataPackVO()
        {
            _version = "1.0";
            _currentPI = -1;
            _pages = null;
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public int CurrentPI
        {
            get { return _currentPI; }
            set { _currentPI = value; }
        }

        public List<PageVO> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }
    }

    [Serializable]
    public class PageVO
    {
        private int _index;
        private string _imageName;
        private LevelVO _level;

        public PageVO()
        {
            _index = 0;
            _imageName = string.Empty;
            _level = null;
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public string ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }

        public LevelVO Level
        {
            get { return _level; }
            set { _level = value; }
        }
    }

    [Serializable]
    public class LevelVO
    {
        private int _levelId;
        private int _defId;
        private List<FieldVO> _fields;
        private List<LevelVO> _childLevels;

        private int _pageIndex;

        public LevelVO()
        {
            _levelId = -1;
            _defId = -1;
            _fields = null;
            _childLevels = null;

            _pageIndex = -1;
        }

        public int LevelId
        {
            get { return _levelId; }
            set { _levelId = value; }
        }

        public int DefId
        {
            get { return _defId; }
            set { _defId = value; }
        }

        public List<FieldVO> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public List<LevelVO> ChildLevels
        {
            get { return _childLevels; }
            set { _childLevels = value; }
        }

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }
    }

    [Serializable]
    public class FieldVO
    {
        private int _defId;
        private string _value;

        public FieldVO()
        {
        }

        public int DefId
        {
            get { return _defId; }
            set { _defId = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    [Serializable]
    public class SortedIndexRowVO
    {
        private List<string> _cols;

        public SortedIndexRowVO()
        {
        }

        public List<string> Cols
        {
            get { return _cols; }
            set { _cols = value; }
        }
    }

    [Serializable]
    public class StatusVO
    {
        private bool _isOk;
        private string _errorMsg;

        public StatusVO()
        {
            _isOk = true;
            _errorMsg = string.Empty;
        }

        public bool IsOk
        {
            get { return _isOk; }
            set { _isOk = value; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; }
        }
    }    

