// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary>
    ///矩形图集合
    /// </summary>
    public class RectangleLayer : Collection<IRectangleStroke>
    {
        /// <summary>
        /// 集合图形是否可见
        /// </summary>
        public bool IsVisible
        {
            set
            {
                foreach (IRectangleStroke item in this)
                {
                    item.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
            }
        }
    }
}
