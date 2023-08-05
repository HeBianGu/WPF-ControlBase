// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public class WindowDialogger : ServiceInstance<IWindowDialogService>
    {

    }

    public class PresenterWindowDialogger : ServiceInstance<IPresenterWindowDialogService>
    {

    }

    /// <summary>
    /// 用于底层弹窗，需要外部注册
    /// </summary>
    public interface IWindowDialogService
    {
        bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action<IDialogWindow>>[] acts);
        int ShowDialogWith(string messge, string title = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts);
        bool ShowSumit(string messge, string title = null, bool showEffect = false, int closeTime = -1);
    }

    /// <summary>
    /// 用于底层弹窗，需要外部注册
    /// </summary>
    public interface IPresenterWindowDialogService
    {
        bool ShowDialog(object content, string title = null, Action<System.Windows.Window> builder = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts);
        int ShowDialogWith(object content, string title = null, Action<System.Windows.Window> builder = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts);
        bool ShowSumit(object content, string title = null, Action<System.Windows.Window> builder = null, bool showEffect = false, int closeTime = -1);
        bool ShowClearly(object content, string title = null, Action<System.Windows.Window> builder = null, int closeTime = -1, bool showEffect = true);
    }

    public interface IWindowBase
    {
        Action<IWindowBase> CloseAnimation { get; set; }
        Action<IWindowBase> ShowAnimation { get; set; }

        void BeginClose();
        void RefreshHide();
        void Show();
        void Show(bool value);
        bool? ShowDialog();
    }

    public interface IDialogWindow : IWindowBase
    {
        bool Result { get; set; }
    }

    [TypeConverter(typeof(EnumDisplayConverter))]
    public enum DialogMode
    {
        [Display(Name = "内嵌模式")]
        Control,
        [Display(Name = "独立窗口")]
        Window

    }


    public static class MessageHost
    {
        public static async Task<bool> ShowResult(string message)
        {
            if (MessageProxy.Messager == null)
                return MessageHost.ShowDialog(message);

            return await MessageProxy.Messager.ShowResult(message);
        }

        public static bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action<IDialogWindow>>[] acts)
        {
            if (WindowDialogger.Instance == null)
                return MessageBox.Show(messge, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes;

            return WindowDialogger.Instance.ShowDialog(messge, title, closeTime, showEffect, acts);
        }
    }
}