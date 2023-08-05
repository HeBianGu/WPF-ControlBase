// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls.Primitives;

namespace HeBianGu.General.WpfControlLib
{
    public static class CustomPopupPlacementCallbackHelper
    {
        public static readonly CustomPopupPlacementCallback LargePopupCallback;

        static CustomPopupPlacementCallbackHelper()
        {
            LargePopupCallback =
                (size, targetSize, offset) => new[] { new CustomPopupPlacement(new Point(), PopupPrimaryAxis.Horizontal) };
        }
    }
}
