using HeBianGu.Base.WpfBase;


namespace HeBianGu.App.Tool
{
    internal class DataSourceLocator
    {
        public DataSourceLocator()
        {

        }

        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();

    }
}
