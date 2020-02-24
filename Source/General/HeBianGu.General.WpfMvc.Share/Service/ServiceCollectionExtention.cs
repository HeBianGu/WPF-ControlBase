using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfMvc
{
    public static class ServiceCollectionExtention
    {
        /// <summary> 注入单例模式 </summary>
        public static IServiceCollection UseMvc(this IServiceCollection service)
        {
            ServiceRegistry.Instance.UseMvc();

            return service;
        }
    }
}
