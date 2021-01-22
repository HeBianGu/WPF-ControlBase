using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
