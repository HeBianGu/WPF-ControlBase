// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.MessageDialog;

namespace System
{
    public static class Extention
    {
        public static void AddWindowDialog(this IServiceCollection service)
        {
            service.AddSingleton<IWindowDialogService, WindowDialogService>();
        }

        public static void AddObjectWindowDialog(this IServiceCollection service)
        {
            service.AddSingleton<IPresenterWindowDialogService, ObjectWindowDialogService>();
        }

        public static void UseWindowDialog(this IApplicationBuilder service, Action<WindowDialogSetting> action)
        {
            action?.Invoke(WindowDialogSetting.Instance);
            SystemSetting.Instance?.Add(WindowDialogSetting.Instance);
        }
    }


    [Displayer(Name = "窗口对话框", GroupName = SystemSetting.GroupMessage)]
    public class WindowDialogSetting : LazySettingInstance<WindowDialogSetting>
    {
       
    }
}
