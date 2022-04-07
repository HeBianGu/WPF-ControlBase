using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Touch
{
    internal class DataSourceLocator
    {
        public DataSourceLocator()
        {

        }


        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();



        public static DataSourceLocator Instance
        {
            get
            {
                return System.Windows.Application.Current.FindResource("S.DataSource.Locator") as DataSourceLocator;
            }
        }
    }
}
