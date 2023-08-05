// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Markup.Primitives;

namespace HeBianGu.Control.Adorner
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }

    [Displayer(Name = "装饰层配置", GroupName = SystemSetting.GroupStyle)]
    public class AdornerSetting : LazySettingInstance<AdornerSetting>
    {
        private double _dragAornerOpacity;
        [Display(Name = "拖拽时控件的透明度")]
        [DefaultValue(0.9)]
        [Range(0.0, 1.0, ErrorMessage = "数据超出范围范围: 0.0 - 1.0")]
        public double DragAornerOpacity
        {
            get { return _dragAornerOpacity; }
            set
            {
                _dragAornerOpacity = value;
                RaisePropertyChanged();
            }
        }
    }

    //public class GetAdorner : MarkupExtension
    //{
    //    public Type Type { get; set; }

    //    public object[] Params { get; set; }

    //    public override object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        return null;
    //    }
    //}
}
