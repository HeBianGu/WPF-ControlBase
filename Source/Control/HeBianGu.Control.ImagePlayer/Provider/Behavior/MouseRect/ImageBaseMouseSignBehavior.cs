// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class ImageBaseMouseSignBehavior : ImageBaseMouseDrawRectBehaviorBase
    {
        //public ImageBaseMouseSignBehavior(ImageCore imageBase) : base(imageBase)
        //{

        //}

        protected override void OnOverDrawRect(Rect rect)
        {
            AssociatedObject.AddShape(rect);
        }

        protected override bool IsMatch()
        {
            return AssociatedObject.OperateType == OperateType.Sign;
        }
    }
}
