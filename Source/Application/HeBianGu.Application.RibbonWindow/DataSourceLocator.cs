using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.RibbonWindow
{
    class DataSourceLocator
    {
        public DataSourceLocator()
        {
            ServiceRegistry.Instance.Register<ShellViewModel>();
        }
        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();

    }
}
