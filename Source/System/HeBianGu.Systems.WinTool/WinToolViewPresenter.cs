using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Controls;

namespace HeBianGu.Systems.WinTool
{

    public interface IWinToolViewPresenter : IInvokePresenter
    {

    }

    public interface IWinToolViewPresenterOption : IItemsSettingOption
    {
        Dock Dock { get; set; }

        int Columns { get; set; }
    }

    [Displayer(Name = "系统工具", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以快速访问Window系统自带的工具")]
    public class WinToolViewPresenter : ItemsViewPresenterBase<WinToolViewPresenter, IWinToolViewPresenter>, IWinToolViewPresenter, IWinToolViewPresenterOption
    {
        private Dock _dock;
        [DefaultValue(Dock.Right)]
        [Display(Name = "停靠方向")]
        public Dock Dock
        {
            get { return _dock; }
            set
            {
                _dock = value;
                RaisePropertyChanged();
            }
        }

        private int _columns;
        [DefaultValue(5)]
        [Display(Name = "布局列数")]
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }

        public override bool Invoke(out string message)
        {
            message = null;
            return true;
        }
    }
}
