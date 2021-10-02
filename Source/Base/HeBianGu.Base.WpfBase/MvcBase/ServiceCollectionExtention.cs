using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public static class MvcServiceCollectionExtention
    {
        /// <summary> 注入单例模式 </summary>
        public static IServiceCollection UseMvc(this IServiceCollection service)
        {
            ServiceRegistry.Instance.UseMvc();

            return service;
        }
    }
}
