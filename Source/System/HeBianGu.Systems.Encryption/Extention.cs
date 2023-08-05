using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Encryption;
using System.Reflection;
using System.Linq;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDESCryptService(this IServiceCollection service)
        {
            service.AddSingleton<ICryptService, DESCryptService>();
        }
    }
}
