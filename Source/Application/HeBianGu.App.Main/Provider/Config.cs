// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Application.MainWindow
{
    [Serializable]
    public class Config
    {
        public Config()
        {
            Categories = new ObservableCollection<Category>();
        }

        [Display(Name = "国家列表")]
        public ObservableCollection<Category> Categories
        {
            get;
            set;
        }

        public void SaveToFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            string content = string.Empty;
            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, this);
                content = stringWriter.ToString();
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(content);
            }
        }

        public Config LoadFromFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            using (StreamReader sr = new StreamReader(path))
            {
                return (Config)serializer.Deserialize(sr);
            }
        }
    }

    [Serializable]
    [Description("国家")]
    public class Category
    {
        public Category()
        {
            Items = new ObservableCollection<Item>();
        }

        [Display(Name = "国家名称")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "省份列表")]
        public ObservableCollection<Item> Items
        {
            get;
            set;
        }
    }

    [Serializable]
    [Description("省份")]
    public class Item
    {
        public Item()
        {
            Instruments = new ObservableCollection<InstrumentCfg>();
            Parameters = new ObservableCollection<Parameter>();
            Limits = new ObservableCollection<Limit>();
        }

        [Display(Name = "省份名称")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "简称")]
        public string ShowName
        {
            get;
            set;
        }

        [Display(Name = "地址")]
        public string DLLName
        {
            get;
            set;
        }

        [Display(Name = "等级")]
        public string ClassName
        {
            get;
            set;
        }

        [Display(Name = "城市列表")]
        public ObservableCollection<InstrumentCfg> Instruments
        {
            get;
            set;
        }

        [Display(Name = "民族列表")]
        public ObservableCollection<Parameter> Parameters
        {
            get;
            set;
        }

        [Display(Name = "会员列表")]
        public ObservableCollection<Limit> Limits
        {
            get;
            set;
        }
    }

    [Serializable]
    [Description("城市")]
    public class InstrumentCfg
    {
        public InstrumentCfg()
        {
            Options = new ObservableCollection<Option>();
        }

        [Display(Name = "城市等级")]
        public string InstrumentType
        {
            get;
            set;
        }

        [Display(Name = "城市名称")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "城市人口")]
        public string InstrumentModel
        {
            get;
            set;
        }

        [Display(Name = "区域列表")]
        public ObservableCollection<Option> Options
        {
            get;
            set;
        }
    }

    [Serializable]
    [Description("区域")]
    public class Option
    {
        [Display(Name = "名称")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "人口")]
        public int Value
        {
            get;
            set;
        }

        [Display(Name = "是否显示")]
        public bool Visible
        {
            get;
            set;
        }
    }

    [Serializable]
    [Description("民族")]
    public class Parameter
    {
        public Parameter()
        {
            Options = new ObservableCollection<Option>();
            Conditions = new ObservableCollection<CompareCondition>();
        }

        [Display(Name = "名称")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "别名")]
        public string ShowName
        {
            get;
            set;
        }

        [Display(Name = "数量")]
        public string DefaultValue
        {
            get;
            set;
        }

        [Display(Name = "是否可见")]
        public bool Visible
        {
            get;
            set;
        }

        [Display(Name = "单位")]
        public string Unit
        {
            get;
            set;
        }

        [Display(Name = "类型")]
        public EDataType ParaType
        {
            get;
            set;
        }

        [Display(Name = "区域列表")]
        public ObservableCollection<Option> Options
        {
            get;
            set;
        }

        [Display(Name = "条件列表")]
        public ObservableCollection<CompareCondition> Conditions
        {
            get;
            set;
        }
    }

    [Serializable]
    [Description("会员")]
    public class Limit
    {
        public Limit()
        {
            CompareConditions = new ObservableCollection<CompareCondition>();
        }

        [Display(Name = "名称")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "别名")]
        public string ShowName
        {
            get;
            set;
        }

        [Display(Name = "类型")]
        public EDataType LimitType
        {
            get;
            set;
        }

        [Display(Name = "是否可见")]
        public bool Visible
        {
            get;
            set;
        }

        [Display(Name = "单位")]
        public string Unit
        {
            get;
            set;
        }


        [Display(Name = "操作列表")]
        public ObservableCollection<CompareCondition> CompareConditions
        {
            get;
            set;
        }
    }

    [Serializable]
    [Description("条件")]
    public class CompareCondition
    {
        [Display(Name = "操作")]
        public ECompareType Compare
        {
            get;
            set;
        }

        [Display(Name = "附加值")]
        public string StringValue
        {
            get;
            set;
        }

        [Display(Name = "值")]
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
                        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                        return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
                    }
                }
                return string.Empty;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
