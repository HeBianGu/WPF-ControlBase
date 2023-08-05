// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Base.WpfBase
{
    public static class MessageProxy
    {
        public static IMessageService Messager => ServiceRegistry.Instance.GetInstance<IMessageService>();
        public static IWindowDialogService Windower => ServiceRegistry.Instance.GetInstance<IWindowDialogService>();
        public static IPresenterWindowDialogService WindowPresenter => ServiceRegistry.Instance.GetInstance<IPresenterWindowDialogService>();
        public static IPresenterMessage Presenter => ServiceRegistry.Instance.GetInstance<IPresenterMessage>();
        public static IContainerMessage Container => ServiceRegistry.Instance.GetInstance<IContainerMessage>();
        public static ISnackMessage Snacker => ServiceRegistry.Instance.GetInstance<ISnackMessage>();
        public static ITaskBarMessage TaskBar => ServiceRegistry.Instance.GetInstance<ITaskBarMessage>();
        public static ISystemNotifyMessage WindowNotify => ServiceRegistry.Instance.GetInstance<ISystemNotifyMessage>();
        public static INotifyMessage Notify => ServiceRegistry.Instance.GetInstance<INotifyMessage>();
        public static IPropertyGridMessage PropertyGrid => ServiceRegistry.Instance.GetInstance<IPropertyGridMessage>();
        public static IAutoColumnPagedDataGridMessage AutoColumnPagedDataGrid => ServiceRegistry.Instance.GetInstance<IAutoColumnPagedDataGridMessage>();
        public static IPrintBoxMessage Printer=> ServiceRegistry.Instance.GetInstance<IPrintBoxMessage>();
    }
}
