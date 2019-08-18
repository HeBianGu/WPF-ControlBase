using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    class DataSourceLocator
    {
        public DataSourceLocator()
        {
           
        }


        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();
        public GridViewModel GridViewModel => ServiceRegistry.Instance.GetInstance<GridViewModel>();
        public LoyoutViewModel LoyoutViewModel => ServiceRegistry.Instance.GetInstance<LoyoutViewModel>();
        public TabViewModel TabViewModel => ServiceRegistry.Instance.GetInstance<TabViewModel>();
        public TreeListViewModel TreeListViewModel => ServiceRegistry.Instance.GetInstance<TreeListViewModel>();
    }
}
