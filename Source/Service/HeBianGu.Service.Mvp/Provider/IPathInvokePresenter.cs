// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;

namespace HeBianGu.Service.Mvp
{
    public interface IPathInvokePresenter : IItemsViewPresenter
    {
        string FullPath { get; set; }
    }

    public class PathInvokePresenter : ItemsViewPresenterBase<PathInvokePresenter, IPathInvokePresenter>, IPathInvokePresenter
    {
        public string FullPath { get; set; }
    }
}
