using System;

namespace HeBianGu.General.WpfControlLib
{
    internal interface ICancelable : IDisposable
    {
        bool IsDisposed { get; }
    }
}
