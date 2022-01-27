using System;

namespace HeBianGu.Base.WpfBase
{
    public interface IStartInitService
    {
        bool Start(params Func<IStartWindow, bool>[] action);
    }

    public interface IStartWindow
    {
        void SetMessage(string message); 
    }
}