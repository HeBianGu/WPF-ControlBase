// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Service.MarkupExtension
{
    public class TimeSpanParseExtension : System.Windows.Markup.MarkupExtension
    {
        public string Input { get; set; }
        public string Format { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return TimeSpan.ParseExact(this.Input, this.Format, System.Globalization.CultureInfo.CurrentCulture);
        }
    }

    public class TimeSpanFromMethodExtension : System.Windows.Markup.MarkupExtension
    {
        public string Param { get; set; }
        public string MethodName { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var method = typeof(TimeSpan).GetMethod(this.MethodName);
            var param = method.GetParameters().FirstOrDefault();
            var value = Convert.ChangeType(this.Param, param.ParameterType);
            return method.Invoke(null, new object[] { param });
        }
    }
}
