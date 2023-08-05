// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;

namespace HeBianGu.Service.Mvp
{

    public interface ITreeViewItemPresenter : IInvokePresenter
    {
        string PredefinePath { get; set; }
    }

    public interface ITreeViewItemPresenterOption : IInvokePresenter
    {
        string PredefinePath { get; set; }
    }

    public class TreeViewItemPresenter : InvokePresenterBase, ITreeViewItemPresenter
    {
        public string PredefinePath { get; set; }
    }

    public abstract class TreeViewItemPresenter<Setting, Interface> : ServiceMvpSettingBase<Setting, Interface>, ITreeViewItemPresenter, ITreeViewItemPresenterOption
        where Setting : class, Interface, new()
        where Interface : IViewPresenter
    {
        public TreeViewItemPresenter()
        {
            this.PredefinePath = this.GroupName;
        }
        [Browsable(false)]
        public string PredefinePath { get; set; }

    }
}
