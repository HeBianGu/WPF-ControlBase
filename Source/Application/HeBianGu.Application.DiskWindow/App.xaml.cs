using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Application.DiskWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override System.Windows.Window CreateMainWindow(StartupEventArgs e)
        {
            return new ShellWindow(); 
        }


        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            //  Do ：启用窗口加载和隐藏动画 需要引用 HeBianGu.Service.Animation
            services.AddWindowAnimation();

            //  Do ：启用消息提示 需要引用 HeBianGu.Control.Message
            services.AddMessage();

            //  Do ：启用对话框 需要引用HeBianGu.Window.MessageDialog
            services.AddMessageDialog();

            //  Do ：启用和显示右上角主题设置
            services.AddTheme();

            ////  Do ：启用右上见配置按钮 需要添加引用HeBianGu.Systems.Setting
            //services.AddSetting();
            //services.AddSettingViewPrenter();

            //  Do ：启用启动窗口 需要添加引用HeBianGu.Window.Start
            services.AddStart();

            //  Do ：启用启动窗口 需要添加引用HeBianGu.Systems.Identity
            services.AddIdentity();

            //  Do ：注入领域模型服务
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            //  Do ：配置启动窗口
            app.UseStart(l =>
            {
                l.Title = "HeBianGu Disk";
                l.TitleFontSize = 50;
            });

            //  Do：应用Mvc 需要引用HeBianGu.Service.Mvc
            app.UseMvc();

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF003D99");
                //l.ForegroundColor = (Color)ColorConverter.ConvertFromString("#727272");

                l.SmallFontSize = 13D;
                l.LargeFontSize = 15D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;

                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;

                l.AccentBrushType = AccentBrushType.LinearGradientBrush;

            });
        }

    }
}
