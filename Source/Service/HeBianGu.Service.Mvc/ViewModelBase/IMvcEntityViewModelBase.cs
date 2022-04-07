// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;

namespace HeBianGu.Service.Mvc
{
    public interface IMvcEntityViewModelBase<M>
    {
        M AddItem { get; set; }
        ObservableCollection<M> Collection { get; set; }
        M SelectedItem { get; set; }
    }
}