// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.App.Music.ViewModel.Home;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Music
{
    internal class DataSourceLocator
    {
        public DataSourceLocator()
        {
            ServiceRegistry.Instance.Register<ShellViewModel>();
        }
        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();
        public HomeViewModel HomeViewModel => ServiceRegistry.Instance.GetInstance<HomeViewModel>();
    }
}
