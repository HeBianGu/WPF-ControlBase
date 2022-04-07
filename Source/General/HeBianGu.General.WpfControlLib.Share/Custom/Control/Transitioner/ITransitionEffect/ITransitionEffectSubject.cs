// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.General.WpfControlLib
{
    public interface ITransitionEffectSubject
    {
        string MatrixTransformName { get; }
        string RotateTransformName { get; }
        string ScaleTransformName { get; }
        string SkewTransformName { get; }
        string TranslateTransformName { get; }
        TimeSpan Offset { get; }
    }
}