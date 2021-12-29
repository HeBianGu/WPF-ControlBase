using System;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// 用于底层弹窗，需要外部注册
    /// </summary>
    public interface IMessageDialogService
    {
        bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts);
        int ShowDialogWith(string messge, string title = null, bool showEffect = false, params Tuple<string, Action<IMessageDialogWindow>>[] acts);
        bool ShowSumit(string messge, string title = null, bool showEffect = false, int closeTime = -1);
    }

    public interface IMessageDialogWindow: IWindowBase
    {
        bool Result { get; set; }
    }

    public class MessageDialog : ServiceInstance<IMessageDialogService>
    {

    }
}