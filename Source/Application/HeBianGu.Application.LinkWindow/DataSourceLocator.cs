using HeBianGu.Base.WpfBase;


namespace HeBianGu.Application.LinkWindow
{
    class DataSourceLocator
    {
        public DataSourceLocator()
        {
            string ss = string.Empty;
        }

        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();
        public GridViewModel GridViewModel => ServiceRegistry.Instance.GetInstance<GridViewModel>();
        public LoyoutViewModel LoyoutViewModel => ServiceRegistry.Instance.GetInstance<LoyoutViewModel>();
        public TabViewModel TabViewModel => ServiceRegistry.Instance.GetInstance<TabViewModel>();
        public TreeListViewModel TreeListViewModel => ServiceRegistry.Instance.GetInstance<TreeListViewModel>();
    }
}
