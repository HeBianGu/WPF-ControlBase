using System;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// 用于底层弹窗，需要外部注册
    /// </summary>
    public interface IMessageWindowService
    {
        bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts);
        int ShowDialogWith(string messge, string title = null, bool showEffect = false, params Tuple<string, Action<IMessageWindow>>[] acts);
        bool ShowSumit(string messge, string title = null, bool showEffect = false, int closeTime = -1);
    }

    public interface IMessageWindow: IWindowBase
    {
        bool Result { get; set; }
    }
}