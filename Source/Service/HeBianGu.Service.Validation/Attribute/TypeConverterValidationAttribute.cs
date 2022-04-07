// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Service.Validation
{
    /// <summary>
    /// 应用TypeConverter去验证数据是否合法
    /// </summary>
    public class TypeConverterValidationAttribute : ValidationAttribute
    {
        public Type Type { get; set; }

        public TypeConverterValidationAttribute(Type type)
        {
            this.Type = type;
        }
    }

}
