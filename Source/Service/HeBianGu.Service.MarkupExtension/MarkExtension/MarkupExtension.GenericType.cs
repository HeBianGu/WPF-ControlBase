// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Service.MarkupExtension
{
    public class GenericTypeExtension : System.Windows.Markup.MarkupExtension
    {
        public Type GenericType { get; set; }

        public Type TypeArgument { get; set; }

        public Type[] TypeArguments { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (TypeArgument == null)
                return this.GenericType.MakeGenericType(this.TypeArguments);

            return this.GenericType.MakeGenericType(this.TypeArgument);
        }
    }
}
