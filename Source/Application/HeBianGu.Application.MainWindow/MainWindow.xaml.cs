using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Application.MainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }


        public void Method()
        {
            Ping ping = new Ping();

            Task.Run(async () =>
            {
                while (true)
                {
                    var reply = await ping.SendPingAsync("www.baidu.com");

                    if (reply.Status == IPStatus.Success)
                    {
                        Debug.WriteLine(reply.RoundtripTime);
                    }
                    else
                    {
                        Debug.WriteLine("无法连接");
                    }

                    Thread.Sleep(1000);
                }
            });
        }

    }


    public class MyEnumConverter : EnumConverter
    {
        public MyEnumConverter(Type type) : base(type)
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

    [TypeConverter(typeof(MyEnumConverter))]
    enum MyEnum
    {
        [Description("dsfsdfs")]
        Defatul,
        [Description("34353")]
        First,
        [Description("ghgjhj")]
        Second
    }

    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return this._enumType; }
            set
            {
                if (value != this._enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }
                    this._enumType = value;
                }
            }
        }

        public EnumBindingSourceExtension()
        {

        }

        public EnumBindingSourceExtension(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException("This EnumType must be specified.");
            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            Array enumVlues = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
                return enumVlues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumVlues.Length + 1);

            enumVlues.CopyTo(tempArray, 1);

            return tempArray;

            //if (null == this._enumType)
            //    throw new InvalidOperationException("This EnumType must be specified.");
            //Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;

            //Array enumValues = Enum.GetValues(actualEnumType);
            //Array retValues = Array.CreateInstance(typeof(string), enumValues.Length);

            //for (int index=0;index<enumValues.Length;index++)
            //{
            //    string inputValue = enumValues.GetValue(index).ToString();
            //    Enum enumValue = (Enum)(Enum.Parse(actualEnumType, inputValue));

            //    FieldInfo field = enumValue.GetType().GetField(inputValue);
            //    object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            //    if (objs == null || objs.Length == 0)
            //    {
            //        retValues.SetValue(inputValue, index);
            //    }
            //    else
            //    {
            //        System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            //        retValues.SetValue(da.Description, index);

            //    }
            //}
            //if (actualEnumType == this._enumType)
            //    return retValues;

            //Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            //enumValues.CopyTo(tempArray, 1);
            //return tempArray;

        }
    }
}
