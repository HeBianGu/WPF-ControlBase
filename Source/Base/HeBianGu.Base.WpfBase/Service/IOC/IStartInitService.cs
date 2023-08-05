// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public interface IStartInitService
    {
        bool Start(Func<IStartWindow, bool> action);
    }

    public interface IStartWindow
    {
        void SetMessage(string message);
    }

    public interface IAppLoadService : ILoad
    {

    }

    public interface IAppSaveService : ISave
    {

    }


    public interface IDataBaseSaveService : ISave
    {

    }

    public interface ISave
    {
        string Name { get; }
        bool Save(out string message);
    }

    public interface ILoad
    {
        string Name { get; }
        bool Load(out string message);
    }

    public interface IStartWindowLoad
    {
        bool Load(IStartWindow window, out string message);
    }
}