// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Control.ImagePlayer
{
    public class LoactionArgs : EventArgs
    {
        public readonly double Left;

        public readonly double Top;

        public LoactionArgs(double left, double top)
        {
            Left = left;
            Top = top;
        }

    }
}
