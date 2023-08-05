// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ScrollViewerKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.Default");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.Accent");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.Single");
        public static ComponentResourceKey MouseGesture => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.MouseGesture");
        public static ComponentResourceKey Tool => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.Tool");
    }

    [Displayer(Name = "滚动条", GroupName = SystemSetting.GroupControl)]
    public class ScrollViewerSetting : LazySettingInstance<ScrollViewerSetting>
    {
        private double _ScrollBarWidth = 10;
        [Display(Name = "滚动条宽度")]
        [DefaultValue(10.0)]
        public double ScrollBarWidth
        {
            get { return _ScrollBarWidth; }
            set
            {
                _ScrollBarWidth = value;
                RaisePropertyChanged();
            }
        }

        private double _ScrollBarHeight = 10;
        [Display(Name = "滚动条高度")]
        [DefaultValue(10.0)]
        public double ScrollBarHeight
        {
            get { return _ScrollBarHeight; }
            set
            {
                _ScrollBarHeight = value;
                RaisePropertyChanged();
            }
        }
    }


    public static partial class Extention
    {
        public static void UseScrollViewer(this IApplicationBuilder service, Action<ScrollViewerSetting> action = null)
        {
            action?.Invoke(ScrollViewerSetting.Instance);
            SystemSetting.Instance?.Add(ScrollViewerSetting.Instance);
        }
    }
}
