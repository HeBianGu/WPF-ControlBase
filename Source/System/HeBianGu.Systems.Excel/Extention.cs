using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Excel;
using System.Reflection;
using System.Linq;

namespace System
{
    public static class Extention
    {


        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddNPOI(this IServiceCollection service, Action<INpoiServiceOption> action = null)
        {
            service.AddSingleton<IExcelService, NpoiService>();
            action?.Invoke(NpoiService.Instance);
            SystemSetting.Instance.Add(NpoiService.Instance);
        }
    }
}
