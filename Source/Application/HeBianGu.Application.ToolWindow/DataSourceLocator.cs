using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;

namespace HeBianGu.Application.ToolWindow
{
    class DataSourceLocator
    {
        public DataSourceLocator()
        {
       
        }

        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();
    }
}
