// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
using HeBianGu.Base.WpfBase;
namespace HeBianGu.Service.Mvp
{
    public interface IInfoMessageViewPresenter : IInvokePresenter
    {
        void AddMessage(string data, string title = "运行消息");
    }

    public class InfoMessageViewPresenterProxy : ServiceInstance<IInfoMessageViewPresenter>
    {

    }

    public interface IDebugMessageViewPresenterProxy : IInvokePresenter
    {
        void AddMessage(string data, string title = "操作消息");
    }

    public class DebugMessageViewPresenterProxy : ServiceInstance<IDebugMessageViewPresenterProxy>
    {

    }

    public interface IFatalMessageViewPresenter : IInvokePresenter
    {
        void AddMessage(string data, string title = "严重消息");
    }

    public class FatalMessageViewPresenterProxy : ServiceInstance<IFatalMessageViewPresenter>
    {

    }

    public interface IWarnMessageViewPresenter : IInvokePresenter
    {
        void AddMessage(string data, string title = "警告消息");
    }

    public class WarnMessageViewPresenterProxy : ServiceInstance<IWarnMessageViewPresenter>
    {

    }
}
