using System.Collections.ObjectModel;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Base.WpfBase
{
    public interface IMvcViewModelBase
    {
        RelayCommand<string> DoActionCommand { get; }
        RelayCommand<string> GoToActionCommand { get; }
        RelayCommand<ILinkActionBase> GoToLinkCommand { get; }
        RelayCommand<string> LoadedCommand { get; }
        ObservableCollection<ILinkActionBase> Navigation { get; set; }
        ILinkActionBase SelectLink { get; set; }
    }
}