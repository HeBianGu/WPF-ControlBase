using System.Collections.ObjectModel;

namespace HeBianGu.Base.WpfBase
{
    public interface IMvcEntityViewModelBase<M> where M : new()
    {
        M AddItem { get; set; }
        ObservableCollection<M> Collection { get; set; }
        M SelectedItem { get; set; }
    }
}