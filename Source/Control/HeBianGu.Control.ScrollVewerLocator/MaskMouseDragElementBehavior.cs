// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows.Controls;

namespace HeBianGu.Control.ScrollVewerLocator
{
    /// <summary> 应用到Canvas的拖拽 </summary>
    public class MaskMouseDragElementBehavior : MouseDragElementBehavior
    {
        protected override void ApplyTranslationTransform(double x, double y)
        {
            Canvas.SetLeft(this.AssociatedObject, Canvas.GetLeft(this.AssociatedObject) + x);
            Canvas.SetTop(this.AssociatedObject, Canvas.GetTop(this.AssociatedObject) + y);

            if (this.AssociatedObject is MaskGrid mask)
            {
                //  Do ：触发位置改变事件
                mask.OnLocationChanged();
            }
        }
    }
}
