// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;

namespace HeBianGu.App.Music
{
    internal class ShellViewModel : MvcViewModelBase
    { 
		public RelayCommand PlayListCommand { get; set; } = new RelayCommand(l =>
		{
			MessageProxy.Presenter.ShowRightClose(new PlayList(),null,"播放列表",x=>
			{
				x.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
                x.Padding = new System.Windows.Thickness(0);
            });
		});
    }
}
