// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 字符串属性类型 </summary>
    public class TextPropertyItem : ObjectPropertyItem<string>
    {
        public TextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override string GetValue()
        {
            return this.PropertyInfo.GetValue(this.Obj)?.ToString();
        }


        public static bool IsIConvertible(PropertyInfo info)
        {
            return typeof(IConvertible).IsAssignableFrom(info.PropertyType);
        }

        public static bool IsTypeConverter(PropertyInfo info)
        {
            TypeConverterAttribute propertyConvert = info.GetCustomAttribute<TypeConverterAttribute>();

            if (propertyConvert != null) return true;

            TypeConverterAttribute typeConvert = info.PropertyType.GetCustomAttribute<TypeConverterAttribute>();

            if (typeConvert != null) return true;

            return false;
        }
    }

    public class OpenPathTextPropertyItem : OpenClearPathTextPropertyItem
    {
        public OpenPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

    }

    public class ClearPathTextPropertyItem : OpenClearPathTextPropertyItem
    {
        public ClearPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

    }

    public class OpenClearPathTextPropertyItem : TextPropertyItem
    {
        public OpenClearPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        public RelayCommand OpenCommand => new RelayCommand(l =>
        {
            try
            {
                Process.Start(this.Value);

                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("删除成功");
            }
            catch (Exception ex)
            {

                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("删除失败,详情请查看日志");
                Logger?.Error(ex);
            }
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });


        public RelayCommand ClearCommand => new RelayCommand(l =>
        {
            try
            {
                if (File.Exists(this.Value))
                {
                    File.Delete(this.Value);
                }

                if (Directory.Exists(this.Value))
                {
                    Directory.Delete(this.Value, true);
                }

                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("删除成功");
            }
            catch (Exception ex)
            {

                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("删除失败,详情请查看日志");
                Logger?.Error(ex);
            }
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });

    }
}
