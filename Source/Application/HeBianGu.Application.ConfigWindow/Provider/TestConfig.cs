using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HeBianGu.Application.ConfigWindow
{
	[Serializable]
	public class TestConfig
	{ 
		public TestConfig()
		{
			TestCategories = new List<TestCategory>(); 
		} 

		public List<TestCategory> TestCategories
		{
			get;
			set;
		}

		public void SaveToFile(string path)
		{
			//string path = "TestItemConfig.xml";
			XmlSerializer serializer = new XmlSerializer(typeof(TestConfig));
			string content = string.Empty;
			using (StringWriter stringWriter = new StringWriter())
			{
				serializer.Serialize(stringWriter, this);
				content = stringWriter.ToString();
			}
			using (StreamWriter sw= new StreamWriter(path))
			{
				sw.Write(content);
			}
		}

		public TestConfig LoadFromFile(string path)
		{
			//string path = "TestItemConfig.xml";
			XmlSerializer serializer = new XmlSerializer(typeof(TestConfig));
			
			using (StreamReader sr = new StreamReader(path))
			{
				 return (TestConfig)serializer.Deserialize(sr) ;
			}
		}
	}

	[Serializable]
	public class TestCategory
	{
		public TestCategory()
		{
			Items = new List<Item>();
		}

		public string Name
		{
			get;
			set;
		}

		public List<Item> Items
		{
			get;
			set;
		}
	}

	[Serializable]
	public class Item
	{
		public Item()
		{
			Instruments = new List<InstrumentCfg>();
			Parameters = new List<Parameter>();
			Limits = new List<Limit>();
		}

		public string Name
		{
			get;
			set;
		}

		public string ShowName
		{
			get;
			set;
		}


		public string DLLName
		{
			get;
			set;
		}

		public string ClassName
		{
			get;
			set;
		}

		public List<InstrumentCfg> Instruments
		{
			get;
			set;
		}

		public List<Parameter> Parameters
		{
			get;
			set;
		}

		public List<Limit> Limits
		{
			get;
			set;
		}
	}

	[Serializable]
	public class InstrumentCfg
	{
		public InstrumentCfg()
		{
			Options = new List<Option>();
		}

		public string InstrumentType
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string InstrumentModel
		{
			get;
			set;
		}

		public List<Option> Options
		{
			get;
			set;
		}
	}

	[Serializable]
	public class Option
	{
		public string Name
		{
			get;
			set;
		}

		public int Value
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}
	}

	[Serializable]
	public class Parameter
	{
		public Parameter()
		{
			Options = new List<Option>();
			Conditions = new List<CompareCondition>(); 
		}

		public string Name
		{
			get;
			set;
		}

		public string ShowName
		{
			get;
			set;
		}

		 


		public string DefaultValue
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}

		public string Unit
		{
			get;
			set;
		}

		public EDataType ParaType
		{
			get;
			set;
		}

		public List<Option> Options
		{
			get;
			set;
		}

		public List<CompareCondition> Conditions
		{
			get;
			set;
		}
	}

	[Serializable]
	public class Limit
	{
		public Limit()
		{
			CompareConditions = new List<CompareCondition>();
		}

		public string Name
		{
			get;
			set;
		}

		public string ShowName
		{
			get;
			set;
		}

		public EDataType LimitType
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}

		public string Unit
		{
			get;
			set;
		}

		

		public List<CompareCondition> CompareConditions
		{
			get;
			set;
		}
	}

	[Serializable]
	public class CompareCondition
	{
		public ECompareType Compare
		{
			get;
			set;
		}

		public string StringValue
		{
			get;
			set;
		}

		public double Value
		{
			get;
			set;
		}
	}


	[Serializable]
	public enum EDataType
	{
		/// <summary>
		/// 数值框
		/// </summary>
		[Description("数值型")]
		Number,
		/// <summary>
		/// 文本框
		/// </summary>
		[Description("字符型")]
		String,
		/// <summary>
		/// 下拉框
		/// </summary>
		[Description("枚举型")]
		Enum,
		/// <summary>
		/// 复选框
		/// </summary>
		[Description("布尔型")]
		Bool,
	}

	[Serializable]
	[TypeConverter(typeof(ECompareTypeConverter))]
	public enum ECompareType
	{
		[Description("等于")]
		Equal,
		[Description("大于")]
		GraetThan,
		[Description("小于")]
		LessThan,
		[Description("小于等于")]
		LessAndEqual,
		[Description("大于等于")]
		GreatAndEqual,
		[Description("不等于")]
		NotEqual,
	}

	public class ECompareTypeConverter : EnumConverter
	{
		public ECompareTypeConverter(Type type) : base(type)
		{

		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value != null)
				{
					FieldInfo fi = value.GetType().GetField(value.ToString());
					if (fi != null)
					{
						var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

						return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
					}
				}
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
