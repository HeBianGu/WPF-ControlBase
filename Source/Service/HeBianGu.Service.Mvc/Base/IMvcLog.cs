// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Service.Mvc
{
    /// <summary> 用于注入日志的接口 </summary>
    public interface IMvcLog
    {
        void ErrorLog(string title, string message);

        void RunLog(string title, string message);

        void OutPutLog(string title, string message);

    }
}