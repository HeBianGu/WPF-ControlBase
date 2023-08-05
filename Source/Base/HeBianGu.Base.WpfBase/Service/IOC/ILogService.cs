// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public interface ILogService
    {
        void Debug(params string[] messages);
        void Error(params Exception[] messages);
        void Error(params string[] messages);
        void Fatal(params string[] messages);
        void Fatal(params Exception[] messages);
        void Info(params string[] messages);
        void Trace(params string[] messages);
        void Warn(params string[] messages);
    }

    public class Logger : ServiceInstance<ILogService>
    {

    }
}