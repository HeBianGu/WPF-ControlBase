// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 曲线视图 </summary>
    public class LayerListBox : ListBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LayerListBox), "S.LayerListBox.Default");
        public static ComponentResourceKey PointKey => new ComponentResourceKey(typeof(LayerListBox), "S.LayerListBox.Point.Default");

        static LayerListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayerListBox), new FrameworkPropertyMetadata(typeof(LayerListBox)));
        }
    }
}
