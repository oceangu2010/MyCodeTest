using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Web;

namespace FusionChartsTest.Chart
{
    public class EnumHelper
    {
        public static string GetMemberName<FusionChartType>(FusionChartType chartType)
        {
            return Enum.GetName(typeof(FusionChartType),chartType);
        }
    }
    public enum FusionChartType
    {
        Line = 0, ScrollLine2D = 1, Column2D = 2, Column3D = 3, Bar2D = 4,
        Pie2D = 5, Pie3D = 6, Area2D = 7, Doughnut2D = 8, Doughnut3D=9,
        MSStackedColumn2D=10,MSStackedColumn2DLineDY=11
    }
    public enum FusionChartPalette
    {
        Style4 = 4
    }
    public class ValidationHelper
    {
        public static bool IsNullOrEmpty(ICollection columnNames)
        {
                return (columnNames == null || columnNames.Count == 0);
        }
        public static bool IsNullOrEmpty(string columnNames)
        {
            return string.IsNullOrEmpty(columnNames);
        }
        public static bool IsNullOrEmpty(object columnNames)
        {
            return columnNames == null ||Convert.IsDBNull(columnNames);
        }
    }
    public class StringHelper
    {
        /// <summary>
        /// �ö��ŷָ��ַ�
        /// </summary>

        /// <param name="fields">Ҫ�ö��ŷָ����ַ�������</param>
        /// <returns>һ���ö��ŷָ����ַ���</returns>
        public static string GetCommaString( params string[] fields)
        {
            //string fmt = format ?? "'{0}'";
            StringBuilder sb = new StringBuilder(50);
            if (fields.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                foreach (string field in fields)
                {
                    sb.Append( field);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1);//�Ƴ�����
                return sb.ToString();
            }
        }
        public static string RemoveLastComma(StringBuilder sb)
        {
            int i = sb.Length;
            if (sb[i - 1].Equals(','))
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public static string GenerateID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);   
        }
   }
    public class WebHelper
    {
        public static string ResolveURL(string relativeUrl)
        {
            if (relativeUrl == null) throw new ArgumentNullException("relativeUrl");

            if (relativeUrl.Length == 0 || relativeUrl[0] == '/' || relativeUrl[0] == '\\')
                return relativeUrl;

            int idxOfScheme = relativeUrl.IndexOf(@"://", StringComparison.Ordinal);
            if (idxOfScheme != -1)
            {
                int idxOfQM = relativeUrl.IndexOf('?');
                if (idxOfQM == -1 || idxOfQM > idxOfScheme) return relativeUrl;
            }

            StringBuilder sbUrl = new StringBuilder();
            sbUrl.Append(HttpRuntime.AppDomainAppVirtualPath);
            if (sbUrl.Length == 0 || sbUrl[sbUrl.Length - 1] != '/') sbUrl.Append('/');

            // found question mark already? query string, do not touch!
            bool foundQM = false;
            bool foundSlash; // the latest char was a slash?
            if (relativeUrl.Length > 1
                && relativeUrl[0] == '~'
                && (relativeUrl[1] == '/' || relativeUrl[1] == '\\'))
            {
                relativeUrl = relativeUrl.Substring(2);
                foundSlash = true;
            }
            else foundSlash = false;
            foreach (char c in relativeUrl)
            {
                if (!foundQM)
                {
                    if (c == '?') foundQM = true;
                    else
                    {
                        if (c == '/' || c == '\\')
                        {
                            if (foundSlash) continue;
                            else
                            {
                                sbUrl.Append('/');
                                foundSlash = true;
                                continue;
                            }
                        }
                        else if (foundSlash) foundSlash = false;
                    }
                }
                sbUrl.Append(c);
            }

            return sbUrl.ToString();
        }
    }
    public class BaseInfo
    {
        public static string FusionChartPath = "FusionCharts/";
    }
    public class ConvertHelper
    {
        public static int ToInt32(bool  b)
        {
            return b ? 1 : 0;
        }
    }
    public class DatabaseHelper
    {
       public static bool Contains(DataTable dt)
       {
           return dt.Rows.Count > 0;
       }
    }
    /// <summary> 
    /// FusionChartͼ��ؼ����������� 
    /// </summary> 
    ///
    public class FusionChartHelper
    {
        #region �ֶζ���
        /// <summary> 
        /// FusionChartͼ��ؼ������� 
        /// </summary> 
        private FusionChartType _chartType;
        /// <summary> 
        /// �ؼ��Ŀ�� 
        /// </summary> 
        private double _chartWidth = 500;
        /// <summary> 
        /// �ؼ��ĸ߶� 
        /// </summary> 
        private double _chartHeight = 300;
        /// <summary> 
        /// FusionChartͼ��ؼ�����ɫ��� 
        /// </summary> 
        private FusionChartPalette _chartPalette = FusionChartPalette.Style4;
        /// <summary> 
        /// �Ƿ���Բ�α�Ե��ʾ( ��Ӧchart��ǵ�useRoundEdges���� ) 
        /// </summary> 
        private bool _isUseRoundEdges = true;
        /// <summary> 
        /// ����XML�����ַ��� 
        /// </summary> 
        private StringBuilder _dataXML =new StringBuilder("");
        /// <summary> 
        /// ����,��Ӧchart��ǵ�caption���� 
        /// </summary> 
        private string _caption = string.Empty;
        /// <summary> 
        /// С����λ�� 
        /// </summary> 
        private int _decimals = 0;
        /// <summary> 
        /// �Ƿ���ʾ��ǩ��ָʾ�� 
        /// </summary> 
        private bool _isShowLine = true;
        /// <summary> 
        /// �Ƿ�������ת,Ĭ�ϲ����� 
        /// </summary> 
        private bool _isRotation = false;
        /// <summary> 
        /// ����ɫ 
        /// </summary> 
        private string _bgColor = string.Empty;
        /// <summary> 
        /// �Ƿ���ʾ�߿�,Ĭ����ʾ 
        /// </summary> 
        private bool _isShowBorder = true;
        /// <summary> 
        /// ��ʼ�Ƕ� 
        /// </summary> 
        private double _startingAngle = -1;
        /// <summary> 
        /// �����С,ȡֵ��Χ:0-72,Ĭ��20,��Ӧchart��ǵ�baseFontSize���� 
        /// </summary> 
        private int _fontSize = 12;
        /// <summary> 
        /// X����ʾ������,��Ӧchart��ǵ�xAxisName���� 
        /// </summary> 
        private string _xName;
        /// <summary> 
        /// Y����ʾ������,��Ӧchart��ǵ�yAxisName���� 
        /// </summary> 
        private string _yName;
        /// <summary> 
        /// ���ֵ�ǰ׺,��Ӧchart��ǵ�numberPrefix���� 
        /// </summary> 
        private string _numberPrefix;
        /// <summary> 
        /// �Ƿ���ͼ������ʾ��Ӧ����ֵ,��Ӧchart��ǵ�showValues���� 
        /// </summary> 
        private bool _isShowValues = true;
        /// <summary> 
        /// �Ƿ��ʽ�����̶ֿ�,Ĭ�ϸ�ʽ������Ӧchart��ǵ�formatNumberScale���� 
        /// </summary> 
        private bool _isFormatNumberScale = true;
        /// <summary> 
        /// �Ƿ�ǿ����ʾָ��λ����С���� 
        /// </summary> 
        private bool _isForceDecimals = false;
        /// <summary> 
        /// �Ƿ��ʽ������ 
        /// </summary> 
        private bool _isFormatNumber = true;
        /// <summary> 
        /// С����ָ����ַ� 
        /// </summary> 
        private string _decimalSeparator = ".";
        /// <summary> 
        /// ǧλ�ָ����ַ� 
        /// </summary> 
        private string _thousandSeparator = ",";
        /// <summary> 
        /// �Ƿ������ֺ���Ӻ�׺"%" 
        /// </summary> 
        private bool _isNumberSuffix = false;
        /// <summary> 
        /// Y����ʾ����Сֵ����Ӧchart��ǵ�yAxisMinValue���� 
        /// </summary> 
        private double _yMinValue;
        /// <summary> 
        /// Y����ʾ�����ֵ����Ӧchart��ǵ�yAxisMaxValue���� 
        /// </summary> 
        private double _yMaxValue;
        /// <summary> 
        /// Ĭ�����̶ֿ� 
        /// </summary> 
        private string _defaultNumberScale;
        /// <summary> 
        /// ���̶ֿ�ֵ�б� 
        /// </summary> 
        private string _numberScaleValue;
        /// <summary> 
        /// ���̶ֿȵ�λ�б� 
        /// </summary> 
        private string _numberScaleUnit;
        /// <summary> 
        /// �Ƿ���ʾ�ϼ�,Ĭ����ʾ 
        /// </summary> 
        private bool _isShowSum = true;
        /// <summary> 
        /// �Ƿ���ʾ����������,Ĭ����ʾ 
        /// </summary> 
        private bool _isShowNames = true;
        /// <summary> 
        /// �������������е�ӳ���ϵ 
        /// </summary> 
        private Dictionary<string, string> _seriesColumnMapping = new Dictionary<string, string>();
        /// <summary> 
        /// ����Դ 
        /// </summary> 
        private DataTable dt_DataSource = new DataTable();
        /// <summary> 
        /// ���� 
        /// </summary> 
        private List<string> _group = new List<string>();
        /// <summary> 
        /// �ӱ��� 
        /// </summary> 
        private string _subCaption;
        /// <summary> 
        /// ������ɫ 
        /// </summary> 
        private string _fontColor = "666666";
        /// <summary> 
        /// �����߿�ɫ 
        /// </summary> 
        private string _canvasBorderColor = "666666";
        #endregion

        #region ���캯��
        /// <summary> 
        /// ��ʼ������ 
        /// </summary> 
        /// <param name="chartType">ͼ��ؼ�����</param> 
        public FusionChartHelper(FusionChartType chartType)
        {
            //��ʼ��ͼ��ؼ������� 
            _chartType = chartType;
            _dataXML.Remove(0, _dataXML.Length);
            //��ʼ��dt_DataSource 
            CreateDataSource_DT();
        }

        #region ��ʼ��dt_DataSource
        /// <summary> 
        /// ��ʼ��dt_DataSource 
        /// </summary> 
        private void CreateDataSource_DT()
        {
            DataColumn column = new DataColumn();

            //��ǩ,��Ӧset��category��ǵ� lable ���� 
            column.ColumnName = "Lable";
            column.Caption = "��ǩ��";
            column.DataType = typeof(String);
            column.Unique = true;
            dt_DataSource.Columns.Add(column);

            //��1���ֵ,��Ӧset�� value ���� 
            column = new DataColumn();
            column.ColumnName = "Series1";
            column.Caption = "��1��";
            column.DataType = typeof(double);
            dt_DataSource.Columns.Add(column);

            //��2���ֵ,��Ӧset�� value ���� 
            column = new DataColumn();
            column.ColumnName = "Series2";
            column.Caption = "��2��";
            column.DataType = typeof(double);
            dt_DataSource.Columns.Add(column);

            //��3���ֵ,��Ӧset�� value ���� 
            column = new DataColumn();
            column.ColumnName = "Series3";
            column.Caption = "��3��";
            column.DataType = typeof(double);
            dt_DataSource.Columns.Add(column);

            //��4���ֵ,��Ӧset�� value ���� 
            column = new DataColumn();
            column.ColumnName = "Series4";
            column.Caption = "��4��";
            column.DataType = typeof(double);
            dt_DataSource.Columns.Add(column);

            //��5���ֵ,��Ӧset�� value ���� 
            column = new DataColumn();
            column.ColumnName = "Series5";
            column.Caption = "��5��";
            column.DataType = typeof(double);
            dt_DataSource.Columns.Add(column);

            //��6���ֵ,��Ӧset�� value ���� 
            column = new DataColumn();
            column.ColumnName = "Series6";
            column.Caption = "��6��";
            column.DataType = typeof(double);
            dt_DataSource.Columns.Add(column);
        }
        #endregion

        #endregion

        #region ����

        #region ����
        /// <summary> 
        /// ���� ( ��Ӧchart��ǵ�caption���� ) 
        /// </summary> 
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
            }
        }
        #endregion

        #region �ӱ���
        /// <summary> 
        /// �ӱ��� ( ��Ӧchart��ǵ�subcaption���� ) 
        /// </summary> 
        public string SubCaption
        {
            get
            {
                return _subCaption;
            }
            set
            {
                _subCaption = value;
            }
        }
        #endregion

        #region ������ɫ
        /// <summary> 
        /// ������ɫ ( ��Ӧchart��ǵ�baseFontColor���� ) 
        /// </summary> 
        public string FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                _fontColor = value;
            }
        }
        #endregion

        #region FusionChartͼ��ؼ�����ɫ
        /// <summary> 
        /// FusionChartͼ��ؼ�����ɫ ( ��Ӧchart��ǵ�palette���� ) 
        /// </summary> 
        public FusionChartPalette Palette
        {
            get
            {
                return _chartPalette;
            }
            set
            {
                _chartPalette = value;
            }
        }
        #endregion

        #region С����λ��
        /// <summary> 
        /// С����λ��,Ĭ�ϲ���ʾС��λ�� ( ��Ӧchart��ǵ�decimals���� ) 
        /// </summary> 
        public int Decimals
        {
            get
            {
                return _decimals;
            }
            set
            {
                _decimals = value;
            }
        }
        #endregion

        #region Ĭ�����̶ֿȵ�λ
        /// <summary> 
        /// Ĭ�����̶ֿȵ�λ,һ�����NumberScaleValue��NumberScaleUnit����ʹ�á� 
        /// ( ��Ӧchart��ǵ�defaultNumberScale���� ) 
        /// ������"bits" 
        /// </summary> 
        public string DefaultNumberScale
        {
            get
            {
                return _defaultNumberScale;
            }
            set
            {
                _defaultNumberScale = value;
            }
        }
        #endregion

        #region ���̶ֿ�ֵ�б�
        /// <summary> 
        /// ���̶ֿ�ֵ�б�,һ�����DefaultNumberScale��NumberScaleUnit����ʹ�á� 
        /// ( ��Ӧchart��ǵ�numberScaleValue���� ) 
        /// ������"8,1024,1024,1024,1024" 
        /// </summary> 
        public string NumberScaleValue
        {
            get
            {
                return _numberScaleValue;
            }
            set
            {
                _numberScaleValue = value;
            }
        }
        #endregion

        #region ���̶ֿȵ�λ�б�
        /// <summary> 
        /// ���̶ֿȵ�λ�б�,һ�����DefaultNumberScale��NumberScaleValue����ʹ�á� 
        /// ( ��Ӧchart��ǵ�numberScaleUnit���� ) 
        /// ������"bytes,KB,MB,GB,TB" 
        /// </summary> 
        public string NumberScaleUnit
        {
            get
            {
                return _numberScaleUnit;
            }
            set
            {
                _numberScaleUnit = value;
            }
        }
        #endregion

        #region �Ƿ������ֺ���Ӻ�׺"%"
        /// <summary> 
        /// �Ƿ������ֺ���Ӻ�׺"%" ,Ĭ�ϲ��Ӻ�׺�� 
        /// </summary> 
        public bool IsNumberSuffix
        {
            get
            {
                return _isNumberSuffix;
            }
            set
            {
                _isNumberSuffix = value;
            }
        }
        #endregion

        #region Y����ʾ����Сֵ
        /// <summary> 
        /// Y����ʾ����Сֵ ( ��Ӧchart��ǵ�yAxisMinValue���� ) 
        /// </summary> 
        public double YMinValue
        {
            get
            {
                return _yMinValue;
            }
            set
            {
                _yMinValue = value;
            }
        }
        #endregion

        #region Y����ʾ�����ֵ
        /// <summary> 
        /// Y����ʾ�����ֵ ( ��Ӧchart��ǵ�yAxisMaxValue���� ) 
        /// </summary> 
        public double YMaxValue
        {
            get
            {
                return _yMaxValue;
            }
            set
            {
                _yMaxValue = value;
            }
        }
        #endregion

        #region �Ƿ�ǿ����ʾС��λ��
        /// <summary> 
        /// �Ƿ�ǿ����ʾָ��λ����С��,���λ��������0�� 
        /// �����Ա������Decimals����ʹ��,Ĭ�ϲ���ʾ�� 
        /// ( ��Ӧchart��ǵ�forceDecimals���� )�� 
        /// ������ָ��Decimals����Ϊ5,��10.12��ʾΪ10.12000 
        /// </summary> 
        public bool IsForceDecimals
        {
            get
            {
                return _isForceDecimals;
            }
            set
            {
                _isForceDecimals = value;
            }
        }
        #endregion

        #region �Ƿ��ʽ�����̶ֿ�
        /// <summary> 
        /// �Ƿ��ʽ�����̶ֿ�,Ĭ�ϸ�ʽ����  
        /// ( ��Ӧchart��ǵ�formatNumberScale���� )�� 
        /// 1,000��ʽ��Ϊ1K, 1,000,000��ʽ��Ϊ1M,������10000��ʽ��Ϊ10K�� 
        /// </summary> 
        public bool IsFormatNumberScale
        {
            get
            {
                return _isFormatNumberScale;
            }
            set
            {
                _isFormatNumberScale = value;
            }
        }
        #endregion

        #region �Ƿ��ʽ������
        /// <summary> 
        /// �Ƿ��ʽ������,Ĭ�ϸ�ʽ����  
        /// ( ��Ӧchart��ǵ�formatNumber���� )�� 
        /// 1000��ʽ��Ϊ1,000 , 1000000��ʽ��Ϊ1,000,000 
        /// </summary> 
        public bool IsFormatNumber
        {
            get
            {
                return _isFormatNumber;
            }
            set
            {
                _isFormatNumber = value;
            }
        }
        #endregion

        #region С����ָ����ַ�
        /// <summary> 
        /// С����ָ����ַ�,Ĭ��Ϊ��� �� 
        /// ( ��Ӧchart��ǵ�decimalSeparator���� )�� 
        /// </summary> 
        public string DecimalSeparator
        {
            get
            {
                return _decimalSeparator;
            }
            set
            {
                _decimalSeparator = value;
            }
        }
        #endregion

        #region ǧλ�ָ����ַ�
        /// <summary> 
        /// ǧλ�ָ����ַ�,Ĭ��Ϊ���� �� 
        /// ( ��Ӧchart��ǵ�thousandSeparator���� )�� 
        /// </summary> 
        public string ThousandSeparator
        {
            get
            {
                return _thousandSeparator;
            }
            set
            {
                _thousandSeparator = value;
            }
        }
        #endregion

        #region �ؼ��Ŀ��
        /// <summary> 
        /// �ؼ��Ŀ�� 
        /// </summary> 
        public double ChartWidth
        {
            get
            {
                return _chartWidth;
            }
            set
            {
                _chartWidth = value;
            }
        }
        #endregion

        #region �ؼ��ĸ߶�
        /// <summary> 
        /// �ؼ��ĸ߶� 
        /// </summary> 
        public double ChartHeight
        {
            get
            {
                return _chartHeight;
            }
            set
            {
                _chartHeight = value;
            }
        }
        #endregion

        #region �Ƿ���ʾ��ǩ��ָʾ��
        /// <summary> 
        /// �Ƿ���ʾ��ǩ��ָʾ��,Ĭ����ʾָʾ��. ( ��Ӧchart��ǵ�enableSmartLabels���� ) 
        /// </summary> 
        public bool IsShowLableLine
        {
            get
            {
                return _isShowLine;
            }
            set
            {
                _isShowLine = value;
            }
        }
        #endregion

        #region �Ƿ�������ת
        /// <summary> 
        /// �Ƿ�������ת,Ĭ�ϲ�����,���������ת,������Ϊtrue. ( ��Ӧchart��ǵ�enableRotation���� ) 
        /// </summary> 
        public bool IsRotation
        {
            get
            {
                return _isRotation;
            }
            set
            {
                _isRotation = value;
            }
        }
        #endregion

        #region ����ɫ
        /// <summary> 
        /// ����ɫ,���÷���: "99CCFF,FFFFFF". ( ��Ӧchart��ǵ�bgColor���� ) 
        /// </summary> 
        public string BgColor
        {
            get
            {
                return _bgColor;
            }
            set
            {
                _bgColor = value;
            }
        }
        #endregion

        #region �����߿�ɫ
        /// <summary> 
        /// �����߿�ɫ. ( ��Ӧchart��ǵ�canvasBorderColor���� ) 
        /// </summary> 
        public string CanvasBorderColor
        {
            get
            {
                return _canvasBorderColor;
            }
            set
            {
                _canvasBorderColor = value;
            }
        }
        #endregion

        #region �Ƿ���ʾ�߿�
        /// <summary> 
        /// �Ƿ���ʾ�߿�,Ĭ����ʾ. ( ��Ӧchart��ǵ�showBorder���� ) 
        /// </summary> 
        public bool IsShowBorder
        {
            get
            {
                return _isShowBorder;
            }
            set
            {
                _isShowBorder = value;
            }
        }
        #endregion

        #region ��ʼ�Ƕ�
        /// <summary> 
        /// ��ʼ�Ƕ�. ( ��Ӧchart��ǵ�startingAngle���� ) 
        /// </summary> 
        public double StartingAngle
        {
            get
            {
                return _startingAngle;
            }
            set
            {
                _startingAngle = value;
            }
        }
        #endregion

        #region �����С
        /// <summary> 
        /// �����С,ȡֵ��Χ:1-72,Ĭ��12 ( ��Ӧchart��ǵ�baseFontSize���� ) 
        /// </summary> 
        public int FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
            }
        }
        #endregion

        #region X����ʾ������
        /// <summary> 
        /// X����ʾ������ ( ��Ӧchart��ǵ�xAxisName���� ) 
        /// </summary> 
        public string XName
        {
            get
            {
                return _xName;
            }
            set
            {
                _xName = value;
            }
        }
        #endregion

        #region Y����ʾ������
        /// <summary> 
        /// Y����ʾ�����ơ�ע�⣺��֧��Ӣ��. ( ��Ӧchart��ǵ�yAxisName���� ) 
        /// </summary> 
        public string YName
        {
            get
            {
                return _yName;
            }
            set
            {
                _yName = value;
            }
        }
        #endregion

        #region ���ֵ�ǰ׺
        /// <summary> 
        /// ���ֵ�ǰ׺�ַ� ( ��Ӧchart��ǵ�numberPrefix���� ) 
        /// </summary> 
        public string NumberPrefix
        {
            get
            {
                return _numberPrefix;
            }
            set
            {
                _numberPrefix = value;
            }
        }
        #endregion

        #region �Ƿ���ͼ������ʾ��Ӧ����ֵ
        /// <summary> 
        /// �Ƿ���ͼ������ʾ��Ӧ����ֵ,Ĭ�ϲ���ʾ�� ( ��Ӧchart��ǵ�showValues���� ) 
        /// </summary> 
        public bool IsShowValues
        {
            get
            {
                return _isShowValues;
            }
            set
            {
                _isShowValues = value;
            }
        }
        #endregion

        #region �Ƿ���Բ�α�Ե��ʾ
        /// <summary> 
        /// �Ƿ���Բ�α�Ե��ʾ ( ��Ӧchart��ǵ�useRoundEdges���� ) 
        /// </summary> 
        public bool IsUseRoundEdges
        {
            get
            {
                return _isUseRoundEdges;
            }
            set
            {
                _isUseRoundEdges = value;
            }
        }
        #endregion

        #region �Ƿ���ʾ�ϼ�
        /// <summary> 
        /// �Ƿ���ʾ�ϼ�,Ĭ����ʾ ( ��Ӧchart��ǵ�showSum���� ) 
        /// </summary> 
        public bool IsShowSum
        {
            get
            {
                return _isShowSum;
            }
            set
            {
                _isShowSum = value;
            }
        }
        #endregion

        #region �Ƿ���ʾ����������
        /// <summary> 
        /// �Ƿ���ʾ����������,Ĭ����ʾ ( ��Ӧchart��ǵ�shownames���� ) 
        /// </summary> 
        public bool IsShowNames
        {
            get
            {
                return _isShowNames;
            }
            set
            {
                _isShowNames = value;
            }
        }
        #endregion

        #endregion

        #region ���������

        #region ������������ط���1
        /// <summary> 
        /// ���������,���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="name">����������,��label����</param> 
        /// <param name="value">�������ֵ,��value����</param> 
        public void AddItem(string name, double value)
        {
            //�������� 
            DataRow row = dt_DataSource.NewRow();

            //�������� 
            row["Lable"] = name;
            row["Series1"] = value;
            dt_DataSource.Rows.Add(row);
        }
        #endregion

        #region ������������ط���2
        /// <summary> 
        /// ���������,���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="name">����������,��label����</param> 
        /// <param name="series1Value">��1���������ֵ</param> 
        /// <param name="series2Value">��2���������ֵ</param> 
        public void AddItem(string name, double series1Value, double series2Value)
        {
            //�������� 
            DataRow row = dt_DataSource.NewRow();

            //�������� 
            row["Lable"] = name;
            row["Series1"] = series1Value;
            row["Series2"] = series2Value;
            dt_DataSource.Rows.Add(row);
        }
        #endregion

        #region ������������ط���3
        /// <summary> 
        /// ���������,���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="name">����������,��label����</param> 
        /// <param name="series1Value">��1���������ֵ</param> 
        /// <param name="series2Value">��2���������ֵ</param> 
        /// <param name="series3Value">��3���������ֵ</param> 
        public void AddItem(string name, double series1Value, double series2Value, double series3Value)
        {
            //�������� 
            DataRow row = dt_DataSource.NewRow();

            //�������� 
            row["Lable"] = name;
            row["Series1"] = series1Value;
            row["Series2"] = series2Value;
            row["Series3"] = series3Value;
            dt_DataSource.Rows.Add(row);
        }
        #endregion

        #region ������������ط���4
        /// <summary> 
        /// ���������,���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="name">����������,��label����</param> 
        /// <param name="series1Value">��1���������ֵ</param> 
        /// <param name="series2Value">��2���������ֵ</param> 
        /// <param name="series3Value">��3���������ֵ</param> 
        /// <param name="series4Value">��4���������ֵ</param> 
        public void AddItem(string name, double series1Value, double series2Value, double series3Value,
            double series4Value)
        {
            //�������� 
            DataRow row = dt_DataSource.NewRow();

            //�������� 
            row["Lable"] = name;
            row["Series1"] = series1Value;
            row["Series2"] = series2Value;
            row["Series3"] = series3Value;
            row["Series4"] = series4Value;
            dt_DataSource.Rows.Add(row);
        }
        #endregion

        #region ������������ط���5
        /// <summary> 
        /// ���������,���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="name">����������,��label����</param> 
        /// <param name="series1Value">��1���������ֵ</param> 
        /// <param name="series2Value">��2���������ֵ</param> 
        /// <param name="series3Value">��3���������ֵ</param> 
        /// <param name="series4Value">��4���������ֵ</param> 
        /// <param name="series5Value">��5���������ֵ</param> 
        public void AddItem(string name, double series1Value, double series2Value, double series3Value,
            double series4Value, double series5Value)
        {
            //�������� 
            DataRow row = dt_DataSource.NewRow();

            //�������� 
            row["Lable"] = name;
            row["Series1"] = series1Value;
            row["Series2"] = series2Value;
            row["Series3"] = series3Value;
            row["Series4"] = series4Value;
            row["Series5"] = series5Value;
            dt_DataSource.Rows.Add(row);
        }
        #endregion

        #region ������������ط���6
        /// <summary> 
        /// ���������,���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="name">����������,��label����</param> 
        /// <param name="series1Value">��1���������ֵ</param> 
        /// <param name="series2Value">��2���������ֵ</param> 
        /// <param name="series3Value">��3���������ֵ</param> 
        /// <param name="series4Value">��4���������ֵ</param> 
        /// <param name="series5Value">��5���������ֵ</param> 
        /// <param name="series6Value">��6���������ֵ</param> 
        public void AddItem(string name, double series1Value, double series2Value, double series3Value,
            double series4Value, double series5Value, double series6Value)
        {
            //�������� 
            DataRow row = dt_DataSource.NewRow();

            //�������� 
            row["Lable"] = name;
            row["Series1"] = series1Value;
            row["Series2"] = series2Value;
            row["Series3"] = series3Value;
            row["Series4"] = series4Value;
            row["Series5"] = series5Value;
            row["Series6"] = series6Value;
            dt_DataSource.Rows.Add(row);
        }
        #endregion

        #endregion

        #region ��������Դ
        /// <summary> 
        /// ��������Դ,��һ��Ϊ���ı�ǩ(lable)������ÿһ�ж�Ӧһ���顣 
        /// ���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="dataSource">����Դ</param> 
        public void SetDataSource(DataTable dataSource)
        {
            dt_DataSource = dataSource;
        }

        /// <summary> 
        /// ��������Դ,Ĭ�ϵ�һ��Ϊ��ǩ(lable)������ÿһ�ж�Ӧһ���顣 
        /// ���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="dataSource">����Դ</param> 
        public void SetDataSource(DataSet dataSource)
        {
            dt_DataSource = dataSource.Tables[0];
        }

        /// <summary> 
        /// ��������Դ,Ĭ�ϵ�һ��Ϊ��ǩ(lable)������ÿһ�ж�Ӧһ���顣 
        /// ���ڶ���ͼ���������SetSeriesName����ÿ�е������� 
        /// </summary> 
        /// <param name="dataSource">����Դ</param> 
        public void SetDataSource(DataView dataSource)
        {
            dt_DataSource = dataSource.Table;
        }
        #endregion

        #region ��������Դ���ж�Ӧ������
        /// <summary> 
        /// ��������Դ���ж�Ӧ������ 
        /// </summary> 
        /// <param name="columnName">����Դ�е�����</param> 
        /// <param name="seriesName">��ʾ������,������</param> 
        public void SetSeriesName(string columnName, string seriesName)
        {
            //��������������ӳ���ϵ��ӵ�_seriesColumnMapping 
            _seriesColumnMapping.Add(columnName, seriesName);
        }

        /// <summary> 
        /// ��������Դ���ж�Ӧ������ 
        /// </summary> 
        /// <param name="columnIndex">����Դ�е�������</param> 
        /// <param name="seriesName">��ʾ������,������</param> 
        public void SetSeriesName(int columnIndex, string seriesName)
        {
            //��������ת��Ϊ���� 
            string columnName = dt_DataSource.Columns[columnIndex].ColumnName;

            //��������������ӳ���ϵ��ӵ�_seriesColumnMapping 
            _seriesColumnMapping.Add(columnName, seriesName);
        }
        #endregion

        #region ������Դ�е��н��з���
        /// <summary> 
        /// ������Դ�е��н��з��� 
        /// </summary> 
        /// <param name="columnNames">����Դ�з�Ϊһ����ֶ����б�</param> 
        public void SetGroup(params string[] columnNames)
        {
            //���columnNamesΪ�գ��򷵻� 
            ////if (ValidationHelper.IsNullOrEmpty(columnNames))
            ////{
            ////    return;
            ////}
            if (columnNames==null||columnNames.Length<=0)
            {
                return;
                
            }

            //���ֶ����б��ö�������,����ӵ������� 
            _group.Add(StringHelper.GetCommaString(columnNames));
        }

        /// <summary> 
        /// ������Դ���н��з��� 
        /// </summary> 
        /// <param name="columnIndexs">����Դ�з�Ϊһ����ֶ������б�</param> 
        public void SetGroup(params int[] columnIndexs)
        {
            //���columnIndexsΪ�գ��򷵻� 
            if (ValidationHelper.IsNullOrEmpty(columnIndexs))
            {
                return;
            }

            //��ʱ�ַ��� 
            StringBuilder sb = new StringBuilder();

            //���ֶ����б��ö������� 
            foreach (int index in columnIndexs)
            {
                //��������ת��Ϊ���� 
                sb.AppendFormat("{0},", dt_DataSource.Columns[index].ColumnName);
            }

            //��ӵ������� 
            _group.Add(StringHelper.RemoveLastComma(sb));
        }
        #endregion

        #region RenderChartByDataUrl����( ��ȡ���ֿؼ����ַ���,ͨ������XML�����ļ�·�� )
        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ���( ͨ������XML�����ļ�·�� ) 
        /// </summary> 
        /// <param name="chartType">ͼ��ؼ�����</param> 
        /// <param name="dataUrl">XML�����ļ���URL��ַ,��ʽӦΪ @"~/·��"</param> 
        /// <param name="chartWidth">ͼ��ؼ��Ŀ��</param> 
        /// <param name="chartHeight">ͼ��ؼ��ĸ߶�</param> 
        /// <param name="debugMode">�Ƿ����ģʽ,��Ϊtrue��Ϊ����ģʽ</param> 
        public static string RenderChartByDataUrl(FusionChartType chartType, string dataUrl, double chartWidth, double chartHeight, bool debugMode)
        {
            return RenderChart(chartType, dataUrl, "", chartWidth, chartHeight, debugMode);
        }

        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ���( ͨ������XML�����ļ�·�� ) 
        /// </summary> 
        /// <param name="chartType">ͼ��ؼ�����</param> 
        /// <param name="dataUrl">XML�����ļ���URL��ַ,��ʽӦΪ @"~/·��"</param> 
        /// <param name="chartWidth">ͼ��ؼ��Ŀ��</param> 
        /// <param name="chartHeight">ͼ��ؼ��ĸ߶�</param> 
        public static string RenderChartByDataUrl(FusionChartType chartType, string dataUrl, double chartWidth, double chartHeight)
        {
            return RenderChart(chartType, dataUrl, "", chartWidth, chartHeight, false);
        }
        #endregion

        #region RenderChartByDataXML����( ��ȡ���ֿؼ����ַ���, ͨ������XML�����ַ��� )
        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ���( ͨ������XML�����ַ��� ) 
        /// </summary> 
        /// <param name="chartType">ͼ��ؼ�����</param> 
        /// <param name="dataXML">XML��ʽ�������ַ���</param> 
        /// <param name="chartWidth">ͼ��ؼ��Ŀ��</param> 
        /// <param name="chartHeight">ͼ��ؼ��ĸ߶�</param> 
        public static string RenderChartByDataXML(FusionChartType chartType, string dataXML, double chartWidth, double chartHeight)
        {
            return RenderChart(chartType, "", dataXML, chartWidth, chartHeight, false);
        }

        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ���( ͨ������XML�����ַ��� ) 
        /// </summary> 
        /// <param name="chartType">ͼ��ؼ�����</param> 
        /// <param name="dataXML">XML��ʽ�������ַ���</param> 
        /// <param name="chartWidth">ͼ��ؼ��Ŀ��</param> 
        /// <param name="chartHeight">ͼ��ؼ��ĸ߶�</param> 
        /// <param name="debugMode">�Ƿ����ģʽ,��Ϊtrue��Ϊ����ģʽ</param> 
        public static string RenderChartByDataXML(FusionChartType chartType, string dataXML, double chartWidth, double chartHeight, bool debugMode)
        {
            return RenderChart(chartType, "", dataXML, chartWidth, chartHeight, debugMode);
        }
        #endregion

        #region RenderChart����( ��ȡ����Chart�ؼ����ַ��� )
        /// <summary> 
        /// ��ȡ����Chart�ؼ����ַ��� 
        /// </summary> 
        /// <param name="chartType">ͼ��ؼ�����</param> 
        /// <param name="dataUrl">XML�����ļ���URL��ַ,��ʽӦΪ @"~/·��"</param> 
        /// <param name="dataXML">XML��ʽ�������ַ���</param> 
        /// <param name="chartWidth">ͼ��ؼ��Ŀ��</param> 
        /// <param name="chartHeight">ͼ��ؼ��ĸ߶�</param> 
        /// <param name="debugMode">�Ƿ����ģʽ,��Ϊtrue��Ϊ����ģʽ</param> 
        private static string RenderChart(FusionChartType chartType, string dataUrl, string dataXML, double chartWidth, double chartHeight, bool debugMode)
        {
            //����һ��ID 
            string id = StringHelper.GenerateID();

            //��ȡͼ��ؼ����ļ�·�� 
            string chartPath = GetChartFilePath(chartType);

            //��dataURLӳ��Ϊ�ͻ������·�� 
            dataUrl = WebHelper.ResolveURL(dataUrl);

            //��������Chart�ؼ����ַ��� 
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<div id='Div{0}' align='center'></div>" + Environment.NewLine, id);
            builder.Append("<script type=\"text/javascript\">" + Environment.NewLine);
            builder.AppendFormat("var chart_{0} = new FusionCharts(\"{1}\", \"{0}\", \"{2}\",", id, chartPath, chartWidth);
            builder.AppendFormat("\"{0}\",\"{1}\", \"{2}\");" + Environment.NewLine, chartHeight, ConvertHelper.ToInt32(debugMode), 0);
            if (dataXML.Length == 0)
            {
                builder.AppendFormat("chart_{0}.setDataURL(\"{1}\");" + Environment.NewLine, id, dataUrl);
            }
            else
            {
                builder.AppendFormat("chart_{0}.setDataXML(\"{1}\");" + Environment.NewLine, id, dataXML);
            }
            builder.AppendFormat("chart_{0}.render(\"Div{0}\");" + Environment.NewLine, id);
            builder.Append("</script>" + Environment.NewLine);

            //���س���Chart�ؼ����ַ��� 
            return builder.ToString();
        }

        #region ��ȡChart�ؼ����ļ�·��
        /// <summary> 
        /// ��ȡChart�ؼ����ļ�·�� 
        /// </summary> 
        private static string GetChartFilePath(FusionChartType chartType)
        {
            //��ȡFusionChart���Ŀ¼�����·�� 
            string folderPath = BaseInfo.FusionChartPath;
            if (!folderPath.EndsWith(@"/"))
            {
                folderPath = folderPath + @"/";
            }

            string path = @"~/" + folderPath + EnumHelper.GetMemberName<FusionChartType>(chartType) + ".swf";
            return WebHelper.ResolveURL(path);
        }
        #endregion

        #endregion

        #region ����XML�����ַ���

        #region GetDataXML����
        /// <summary> 
        /// ����XML�����ַ��� 
        /// </summary> 
        private string GetDataXML()
        {
            //����chart��ǵ������ַ��� 
            string propertys = GetPropertys();

            //���������ַ����Ŀ�ʼ��� 
            _dataXML.AppendFormat("<chart {0} >", propertys);

            //���������� 
            _dataXML.Append(CreateItems());

            //���������ַ����Ľ������ 
            _dataXML.Append("</chart>");

            //���������ַ��� 
            return _dataXML.ToString();
        }
        #endregion

        #region ����chart��ǵ������ַ���
        /// <summary> 
        /// ����chart��ǵ������ַ��� 
        /// </summary> 
        private string GetPropertys()
        {
            //�������������ַ��� 
            StringBuilder sb = new StringBuilder();

            //================================== ������� ===================================== 

            #region ��������

            //��ӱ��� 
            sb.AppendFormat("caption='{0}' ", _caption);

            //����ӱ��� 
            sb.AppendFormat("subcaption='{0}' ", _subCaption);

            //����X����ʾ������ 
            sb.AppendFormat("xAxisName='{0}' ", _xName);

            //����Y����ʾ������ 
            sb.AppendFormat("yAxisName='{0}' ", _yName);

            //����Y����ʾ����Сֵ 
            sb.AppendFormat("yAxisMinValue='{0}' ", _yMinValue);

            //����Y����ʾ�����ֵ 
            sb.AppendFormat("yAxisMaxValue='{0}' ", _yMaxValue);

            //�Ƿ���ʾ�ϼ� 
            sb.AppendFormat("showSum='{0}' ", ConvertHelper.ToInt32(_isShowSum));

            //�Ƿ���ʾ���������� 
            sb.AppendFormat("shownames='{0}' ", ConvertHelper.ToInt32(_isShowNames));

            //�Ƿ���ͼ������ʾ��Ӧ����ֵ 
            sb.AppendFormat("showValues='{0}' ", ConvertHelper.ToInt32(_isShowValues));

            //�Ƿ���ʾ��ǩ��ָʾ�� 
            sb.AppendFormat("enableSmartLabels='{0}' ", ConvertHelper.ToInt32(_isShowLine));

            //�Ƿ�������ת 
            sb.AppendFormat("enableRotation='{0}' ", _isRotation);

            //������ʼ�Ƕ� 
            if (_startingAngle != -1)
            {
                sb.AppendFormat("startingAngle='{0}' ", _startingAngle);
            }

            //����About FusionCharts
            sb.AppendFormat("showAboutMenuItem='{0}' ", "0");

            #endregion

            #region ��ʽ����

            //���������С 
            sb.AppendFormat("baseFontSize = '{0}' ", _fontSize);

            //����������ɫ 
            sb.AppendFormat("baseFontColor='{0}' ", _fontColor);

            //���õ�ɫ�� 
            sb.AppendFormat("palette='{0}' ", (int)_chartPalette);

            //�Ƿ���Բ�α�Ե��ʾ 
            sb.AppendFormat("useRoundEdges='{0}' ", ConvertHelper.ToInt32(_isUseRoundEdges));

            //���ñ���ɫ 
            if (!ValidationHelper.IsNullOrEmpty(_bgColor))
            {
                sb.AppendFormat("bgColor='{0}' ", _bgColor);

                //���ñ���͸���� 
                sb.Append("bgAlpha='40,100' ");
                //���ñ������� 
                sb.Append("bgRatio='0,100' ");
                //���ñ����Ƕ� 
                sb.Append("bgAngle='360' ");
            }

            //���ñ߿� 
            sb.AppendFormat("showBorder='{0}' ", ConvertHelper.ToInt32(_isShowBorder));

            //����ͼר����ʽ 
            switch (_chartType)
            {
                case FusionChartType.Line:
                    SetLineStyle(sb);
                    break;
                case FusionChartType.ScrollLine2D:
                    SetLineStyle(sb);
                    break;
            }

            #endregion

            #region ��ʽ������

            //�Ƿ������ֺ���Ӻ�׺"%" 
            if (_isNumberSuffix)
            {
                sb.Append("numberSuffix='%25' ");
            }

            //����Ĭ�����̶ֿ� 
            sb.AppendFormat("defaultNumberScale='{0}' ", _defaultNumberScale);

            //�������̶ֿ�ֵ�б� 
            sb.AppendFormat("numberScaleValue='{0}' ", _numberScaleValue);

            //�������̶ֿȵ�λ�б� 
            sb.AppendFormat("numberScaleUnit='{0}' ", _numberScaleUnit);

            //�������ֵ�ǰ׺�ַ� 
            sb.AppendFormat("numberPrefix='{0}' ", _numberPrefix);

            //�Ƿ�ǿ����ʾָ��λ����С�� 
            sb.AppendFormat("forceDecimals='{0}' ", ConvertHelper.ToInt32(_isForceDecimals));

            //�������ָ�ʽ���ָ��� 
            sb.AppendFormat("decimalSeparator='{0}' ", _decimalSeparator);

            //����ǧλ�ָ����ַ� 
            sb.AppendFormat("thousandSeparator='{0}' ", _thousandSeparator);

            //�Ƿ��ʽ�����̶ֿ� 
            sb.AppendFormat("formatNumberScale='{0}' ", ConvertHelper.ToInt32(_isFormatNumberScale));

            //�Ƿ��ʽ������ 
            sb.AppendFormat("formatNumber='{0}' ", ConvertHelper.ToInt32(_isFormatNumber));

            //����С��λ�� 
            sb.AppendFormat("decimals='{0}' ", _decimals);

            #endregion

            //���ع������� 
            return sb.ToString();
        }

        #region ����ͼר����ʽ
        /// <summary> 
        /// ����ͼר����ʽ 
        /// </summary> 
        /// <param name="sb">��ʽ�ַ���</param> 
        private void SetLineStyle(StringBuilder sb)
        {
            //���ý���ɫ 
            sb.AppendFormat("alternateHGridColor='#FCB541' ");
            //���ý���ɫ��͸���� 
            sb.AppendFormat("alternateHGridAlpha='20' ");
            //����DIV�߿�ɫ 
            sb.AppendFormat("divLineColor='FCB541' ");
            //����DIV�߿�ɫ��͸���� 
            sb.AppendFormat("divLineAlpha='50' ");
            //���û����߿�ɫ 
            sb.AppendFormat("canvasBorderColor='{0}' ", _canvasBorderColor);
            //�������ߵ���ɫ 
            sb.AppendFormat(" lineColor='FCB541' ");
        }
        #endregion

        #endregion

        #region ����������

        #region CreateItems����
        /// <summary> 
        /// ���������� 
        /// </summary> 
        private string CreateItems()
        {
            //���dt_DataSourceû�����ݣ��򷵻� 
            if (!DatabaseHelper.Contains(dt_DataSource))
            {
                return string.Empty;
            }

            //���ݲ�ͬ�ؼ�,������Ӧ���������ַ��� 
            switch (_chartType)
            {
                //����ؼ� 
                case FusionChartType.Column2D:
                    return CreateItems_SingleSeries();
                case FusionChartType.Column3D:
                    return CreateItems_SingleSeries();
                case FusionChartType.Bar2D:
                    return CreateItems_SingleSeries();
                case FusionChartType.Pie2D:
                    return CreateItems_SingleSeries();
                case FusionChartType.Pie3D:
                    return CreateItems_SingleSeries();
                case FusionChartType.Line:
                    return CreateItems_SingleSeries();
                case FusionChartType.Area2D:
                    return CreateItems_SingleSeries();
                case FusionChartType.Doughnut2D:
                    return CreateItems_SingleSeries();
                case FusionChartType.Doughnut3D:
                    return CreateItems_SingleSeries();
                //����ؼ� 
                case FusionChartType.MSStackedColumn2D:
                    return CreateItems_Group();
                case FusionChartType.MSStackedColumn2DLineDY:
                    return CreateItems_Group();
                //����ؼ� 
                default:
                    return CreateItems_MultiSeries();
            }
        }
        #endregion

        #region ��������ͼ���������
        /// <summary> 
        /// ��������ͼ��������� 
        /// </summary> 
        private string CreateItems_SingleSeries()
        {
            //�������ַ��� 
            StringBuilder dataItems = new StringBuilder();

            //����dt_DataSource,ƴ��set��� 
            foreach (DataRow row in dt_DataSource.Rows)
            {
                dataItems.AppendFormat("<set label='{0}' value='{1}' />", row[0].ToString(), row[1].ToString());
            }

            //�����������ַ��� 
            return dataItems.ToString();
        }
        #endregion

        #region ��������ͼ���������
        /// <summary> 
        /// ��������ͼ��������� 
        /// </summary> 
        private string CreateItems_MultiSeries()
        {
            //�������ַ��� 
            StringBuilder dataItems = new StringBuilder();

            //�������� 
            dataItems.Append(CreateCategoryTag());

            //====================================== �������� ======================================= 
            //����_seriesColumnMapping,ƴ��ÿ���dataset��� 
            foreach (KeyValuePair<string, string> keyValue in _seriesColumnMapping)
            {
                //�����鿪ʼ��� 
                dataItems.AppendFormat("<dataset seriesName='{0}' >", keyValue.Value);
                //��������� 
                foreach (DataRow row in dt_DataSource.Rows)
                {
                    dataItems.AppendFormat("<set value='{0}' />", row[keyValue.Key].ToString());
                }
                //������������ 
                dataItems.Append("</dataset>");
            }

            //�����������ַ��� 
            return dataItems.ToString();
        }
        #endregion

        #region ��������ͼ���������
        /// <summary> 
        /// ��������ͼ��������� 
        /// </summary> 
        private string CreateItems_Group()
        {
            //�Է���ʣ�µ��н��д��� 
            ProcessGroup();

            //�������ַ��� 
            StringBuilder dataItems = new StringBuilder();

            //�������� 
            dataItems.Append(CreateCategoryTag());

            //====================================== ���������� ======================================= 
            //�������� 
            foreach (string item in _group)
            {
                //��ӷ��鿪ʼ��� 
                dataItems.Append("<dataset>");

                //����������������� 
                string[] columns = item.Split(',');
                foreach (string columnName in columns)
                {
                    //�����鿪ʼ��� 
                    dataItems.AppendFormat("<dataset seriesName='{0}' >", _seriesColumnMapping[columnName]);
                    //��������� 
                    foreach (DataRow row in dt_DataSource.Rows)
                    {
                        dataItems.AppendFormat("<set value='{0}' />", row[columnName].ToString());
                    }
                    //������������ 
                    dataItems.Append("</dataset>");
                }

                //��ӷ��������� 
                dataItems.Append("</dataset>");
            }

            //�����������ַ��� 
            return dataItems.ToString();
        }
        #endregion

        #region ���������
        /// <summary> 
        /// ��������� 
        /// </summary> 
        private string CreateCategoryTag()
        {
            //===================================== ��������� ====================================== 
            StringBuilder categoryTag = new StringBuilder();

            //�������ʼ��� 
            categoryTag.Append("<categories>");
            //���������� 
            foreach (DataRow row in dt_DataSource.Rows)
            {
                categoryTag.AppendFormat("<category label='{0}' />", row[0].ToString());
            }
            //������������� 
            categoryTag.Append("</categories>");

            //��������� 
            return categoryTag.ToString();
        }
        #endregion

        #region �Է���ʣ�µ��н��д���
        /// <summary> 
        /// �Է���ʣ�µ��н��д��� 
        /// </summary> 
        private void ProcessGroup()
        {
            //=============== �����÷���ʣ�µ�����ӵ�_group =========================== 
            //��ȡ�ѷ������������ 
            List<string> groupColumns = new List<string>();
            foreach (string groupStr in _group)
            {
                string[] columns = groupStr.Split(',');
                foreach (string column in columns)
                {
                    groupColumns.Add(column);
                }
            }

            //����ʣ�µ����� 
            List<string> remainColumns = new List<string>();

            //��ȡ�ö��ŷָ���������Ч�е��ַ��� 
            foreach (DataColumn column in dt_DataSource.Columns)
            {
                //�Ա�ǩ������ 
                if (column.ColumnName == dt_DataSource.Columns[0].ColumnName)
                {
                    continue;
                }

                //�����һ����ֵ,��˵��������ж���Ч 
                if (ValidationHelper.IsNullOrEmpty(dt_DataSource.Rows[0][column]))
                {
                    break;
                }

                //�����δ���飬����ӵ�remainColumns 
                if (!groupColumns.Contains(column.ColumnName))
                {
                    remainColumns.Add(column.ColumnName);
                }
            }

            //��ӵ�_group 
            if (remainColumns.Count != 0)
            {
                _group.Add(StringHelper.GetCommaString(remainColumns.ToArray()));
            }
        }
        #endregion

        #endregion

        #endregion

        #region ��дToString
        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ��� 
        /// </summary> 
        public  string getChartString()
        {
            return getChartString(false);
        }

        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ��� 
        /// </summary> 
        /// <param name="debugMode">�Ƿ����ģʽ,��Ϊtrue��Ϊ����ģʽ</param> 
        public string getChartString(bool debugMode)
        {
            //��ȡXML���� 
            string dataXML = GetDataXML();

            return getChartString(dataXML, debugMode);
        }

        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ��� 
        /// </summary> 
        /// <param name="dataXML">XML��ʽ�������ַ���</param> 
        public string getChartString(string dataXML)
        {
            return getChartString(dataXML, false);
        }

        /// <summary> 
        /// ��ȡ����FusionChart�ؼ����ַ��� 
        /// </summary> 
        /// <param name="dataXML">XML��ʽ�������ַ���</param> 
        /// <param name="debugMode">�Ƿ����ģʽ,��Ϊtrue��Ϊ����ģʽ</param> 
        public string getChartString(string dataXML, bool debugMode)
        {
            return RenderChartByDataXML(_chartType, dataXML, _chartWidth, _chartHeight, debugMode);
        }
        #endregion
    }
}
