using System.Collections.ObjectModel;

namespace HeBianGu.General.WpfMvc
{
    public interface IMvcEntityViewModelBase<M> where M : new()
    {
        M AddItem { get; set; }
        ObservableCollection<M> Collection { get; set; }
        M SeletItem { get; set; }
    }
}