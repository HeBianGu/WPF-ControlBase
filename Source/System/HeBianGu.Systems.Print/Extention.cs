using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PrintBox;
using HeBianGu.Systems.Print;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddPrintBoxMessage(this IServiceCollection service, Action<IPrintBoxMessageOption> action = null)
        {
            service.AddSingleton<IPrintBoxMessage, PrintBoxMessage>();
            action?.Invoke(PrintBoxMessage.Instance);
            SystemSetting.Instance.Add(PrintBoxMessage.Instance);
        }
    }
}
