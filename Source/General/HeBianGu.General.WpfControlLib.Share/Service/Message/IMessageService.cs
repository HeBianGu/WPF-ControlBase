using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{

    public class MessageCloseLayerCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Message.Instance.CloseLayer();
        }

        public event EventHandler CanExecuteChanged;
    } 

    public interface IMessageService
    {
        MessageCloseLayerCommand CloseLayerCommand { get; }

        void BeginDefaultBlurEffect(bool isuse);
        void CloseLayer();
        bool IsOpened();
        Task<T> ShowCustomDialog<T>(UIElement element, Action<object, RoutedEventArgs> action = null);
        void ShowLayer(FrameworkElement element);
        void ShowNotifyMessage(string message, string title = null, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000);
        Task<bool> ShowObjectWithContent<T>(T value, Predicate<T> match = null, string title = null);
        Task ShowPercentProgress(Action<IPercentProgress> action, Action closeAction = null);
        Task<T> ShowPercentProgress<T>(Func<IPercentProgress, T> action, Action closeAction = null);
        Task<R> ShowProgressMessge<T, R>(Func<T, R> action, Action closeAction = null) where T : new();
        Task<bool> ShowResultMessge(string message);
        Task ShowResultMessge(string message, Action<object, RoutedEventArgs> action);
        void ShowSnackMessage(object message);
        void ShowSnackMessage(object message, object actionContent, Action actionHandler);
        void ShowSnackMessage<TArgument>(string message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);
        void ShowSnackMessageWithNotice(object message);
        Task ShowStringProgress(Action<IStringProgress> action, Action closeAction = null);
        Task<T> ShowStringProgress<T>(Func<IStringProgress, T> action, Action closeAction = null);
        Task ShowSumitMessge(string message, bool isLog = true);
        void ShowTaskbar(Action<TaskbarItemInfo> action);
        void ShowTaskbarImage(ImageSource image);
        void ShowTaskbarNormal(Action<TaskbarItemInfo> action);
        Task ShowTaskbarPercent(Action<TaskbarItemInfo> action);
        Task<bool> ShowTaskbarWaitting(Func<bool> action);
        Task ShowWaittingMessge(Action action, Action closeAction = null);
        Task<T> ShowWaittingResultMessge<T>(Func<T> action);
    }

    public interface IPercentProgress
    {

        int Value { set; }
    }

    public interface IStringProgress
    {
        string MessageStr { set; }
    }

}
