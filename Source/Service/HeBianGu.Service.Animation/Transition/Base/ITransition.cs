// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Service.Animation
{
    public interface ITransition
    {
        /// <summary> 用于切换页面 隐藏 </summary>
        void BeginPrevious(UIElement element, Action complate = null);

        /// <summary> 用于切换页面 显示 </summary>

        void BeginCurrent(UIElement element, Action complate = null);

        /// <summary> 用于加载页面 隐藏 </summary>
        void BeginHidden(UIElement element, Action complate = null);

        /// <summary> 用于加载页面 显示 </summary>
        void BeginVisible(UIElement element, Action complate = null);
    }
}
