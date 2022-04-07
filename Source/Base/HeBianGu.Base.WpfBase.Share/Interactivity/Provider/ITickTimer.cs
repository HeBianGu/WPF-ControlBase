// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification = "This isn't an exception.")]
    internal interface ITickTimer
    {
        event EventHandler Tick;
        void Start();
        void Stop();
        TimeSpan Interval { get; set; }
    }
}
