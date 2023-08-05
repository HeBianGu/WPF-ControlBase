// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Service.MarkupExtension
{
    /// <summary> 显示枚举中所有选项 </summary>
    public class EnumSourceExtension : System.Windows.Markup.MarkupExtension
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

        public EnumSourceExtension()
        {

        }

        public EnumSourceExtension(Type enumType)
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


        }
    }

    /// <summary> 根据DisplayAttribute特性中组名显示选项 </summary>
    public class EnumGroupSourceExtension : System.Windows.Markup.MarkupExtension
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

        public string GroupName { get; set; }

        public EnumGroupSourceExtension()
        {

        }

        public EnumGroupSourceExtension(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException("This EnumType must be specified.");
            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;

            string[] names = Enum.GetNames(actualEnumType);

            List<string> matchs = new List<string>();

            foreach (string item in names)
            {
                FieldInfo field = actualEnumType.GetField(item);

                if (field == null) continue;

                DisplayAttribute display = field.GetCustomAttribute<DisplayAttribute>();

                if (display == null) 
                    continue;
                if (display.GroupName == null)
                    continue;
                if (display.GroupName.Split(',').Contains(this.GroupName)==true)
                {
                    matchs.Add(item);
                }
            }

            return matchs.Select(l => Enum.Parse(actualEnumType, l));
        }
    }

    public class GetEnumExtension : GetValueToTypeExtensionBase
    { 
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.Parse(this.Type, this.Value);
        }
    }
}
