using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Controls;
using System;

namespace HeBianGu.Systems.WinTool
{
    public interface IWinSpecialFolderViewPresenter : IInvokePresenter
    {

    }

    public interface IWinSpecialFolderViewPresenterOption: IItemsSettingOption
    {
        Dock Dock { get; set; }

        int Columns { get; set; }
    }

    [Displayer(Name = "特殊文件夹", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以快速访问系统定义的特殊文件夹")]
    public class WinSpecialFolderViewPresenter : ItemsViewPresenterBase<WinSpecialFolderViewPresenter, IWinSpecialFolderViewPresenter>, IWinSpecialFolderViewPresenter, IWinSpecialFolderViewPresenterOption
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
