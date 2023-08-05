// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.TypeConverter;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Control.Vlc
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum FullScreenType
    {
        [Display(Name = "鼠标悬停")]
        MouseOver,
        [Display(Name = "默认")]
        Default,
        [Display(Name = "无")]
        None,
        [Display(Name = "鹰眼")]
        ScrollViewerTransfor
    }
}
