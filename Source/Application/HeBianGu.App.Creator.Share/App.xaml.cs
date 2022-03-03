using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Systems.Project;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Creator
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
            ////  Do ：注册本地化配置读写服务
            //services.AddSingleton<IThemeSerializeService, LocalizeService>();

            //  Do ：注入领域模型服务
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();

            //  Do ：注册日志服务
            services.AddSingleton<ILogService, LogService>();

            //services.UseMessageWindow();

            //services.UseWindowAnimation();

            //services.AddThemeSave();

            services.AddSingleton<IProjectService, WorkflowProjectService>();

            services.AddSetting();

            //services.AddIdentity();

            //services.AddUpgrade();

            //services.AddStart();

            //  Do ：启用窗口加载和隐藏动画 需要引用 HeBianGu.Service.Animation
            services.AddWindowAnimation();

            //  Do ：启用消息提示 需要引用 HeBianGu.Control.Message
            services.AddMessage();

            //  Do ：启用对话框 需要引用HeBianGu.Window.MessageDialog
            services.AddMessageDialog();

            services.AddTheme();

            //services.AddIdentity();

            //services.AddUpgrade();

            services.AddStart();

            //services.AddOperation();

            //services.AddLicense();

            //services.AddStudentDemo();

            //  Do ：启用右上见配置按钮 需要添加引用HeBianGu.Systems.Setting
            services.AddSetting();
            services.AddSettingViewPrenter();
            services.AddSettingPath();

            services.AddXmlSerialize();
            services.AddXmlMeta();

            services.AddMvc();

        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            //  Do：注册Mvc模式
            app.UseMvc();

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF003D99");

                l.SmallFontSize = 14D;
                l.LargeFontSize = 16D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 36;
                l.RowHeight = 40;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 2;

                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;

                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });

            app.UseProject(l =>
            {
                l.Extenstion = "wf";
                l.SaveMode = ProjectSaveMode.OnProjectChanged;
            });

            app.UseSettingDefault();

            app.UseStart(l =>
            {
                l.Title = "自动化测试系统";
                l.TitleFontSize = 50;
            });

            //app.UseStart(l => l.Type = Window.Start.WindowType.Message);
        }

    }
}
