// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;

namespace HeBianGu.Service.Mvc
{
    public interface IMvcViewModelBase
    {
        RelayCommand<string> DoActionCommand { get; }
        RelayCommand<string> GoToActionCommand { get; }
        RelayCommand<ILinkAction> GoToLinkCommand { get; }
        RelayCommand LoadedCommand { get; }
        ObservableCollection<ILinkAction> Navigation { get; set; }
        ILinkAction SelectLink { get; set; }
    }
}