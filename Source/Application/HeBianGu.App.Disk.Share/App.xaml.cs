using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Explorer;
using HeBianGu.Control.Guide;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.About;
using HeBianGu.Systems.Identity;
using HeBianGu.Systems.Setting;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Disk
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override MainWindowBase CreateMainWindow(StartupEventArgs e)
        {
            return new ShellWindow();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddStart(x => x.ProductFontSize = 90);
            services.AddWindowDialog();
            services.AddObjectWindowDialog();
            services.AddWindowAnimation();
            services.AddMessageProxy();
            services.AddPropertyGridMessage();
            services.AddAutoColumnPagedDataGridMessage();
            //services.AddNotifyMessage();
            services.AddSetting();
            //services.AddIdentity(x => x.ProductFontSize = 50);
            services.AddMvc();
            //services.AddProjectDefault();
            services.AddXmlWebSerializerService();
            ////  Do ：注册软件更新页面
            //services.AddAutoUpgrade(x =>
            //{
            //    x.Uri = "https://gitee.com/hebiangu/wpf-auto-update/raw/master/Install/Computer/AutoUpdate.xml";
            //    x.UseIEDownload = true;
            //});
            services.AddSettingPath();
            services.AddXmlSerialize();
            services.AddDESCryptService();

            //services.AddPrintBoxMessage();

            services.AddWindowExplorer();

            #region - More -
            //services.AddUpgradeViewPresenter();
            //services.AddLicense();
            //services.AddLicenseViewPresenter();
            //services.AddVip();
            //services.AddVipViewPresenter();
            //services.AddSurveyViewPresenter();
            //services.AddFeedbackViewPresenter();
            //services.AddLogoutViewPresenter();
            services.AddAboutViewPresenter();
            services.AddMoreViewPresenter(x =>
            {
                //x.AddPersenter(UpgradeViewPresenter.Instance);
                //x.AddPersenter(LicenseViewPresenter.Instance);
                //x.AddPersenter(VipViewPresenter.Instance);
                //x.AddPersenter(SurveyViewPresenter.Instance);
                //x.AddPersenter(FeedbackViewPresenter.Instance);
                x.AddPersenter(AboutViewPresenter.Instance);
                x.AddPersenter(LogoutViewPresenter.Instance);

            });
            #endregion

            #region - WindowCaption -
            services.AddLoginViewPresenter();
            services.AddGuideViewPresenter();
            services.AddHideWindowViewPresenter();
            services.AddSettingViewPrenter();
            //services.AddThemeRightToolViewPresenter();
            services.AddThemeRightViewPresenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                //x.AddPersenter(LoginViewPresenter.Instance);
                x.AddPersenter(MoreViewPresenter.Instance);
                x.AddPersenter(GuideViewPresenter.Instance);
                //x.AddPersenter(HideWindowViewPresenter.Instance);
                x.AddPersenter(SettingViewPresenter.Instance);
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });

            #endregion

            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Module", "HeBianGu.Domain.FATFS.dll");
            //var dll = Assembly.LoadFile(path);
            //var find = dll.GetTypes().Where(x => typeof(IExplorerService).IsAssignableFrom(x))?.FirstOrDefault();
            //ServiceRegistry.Instance.Register(typeof(IExplorerService), find);
            //services.AddSingleton<IExplorerService, WindowExplorerServiceDemo>();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            app.UseStyle();

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                l.DefaultFontSize = 13D;
                l.FontSize = FontSize.Normal;
                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;
                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;
                l.ThemeType = ThemeType.DarkBlack;
                l.StyleType = StyleType.Single;
                l.Language = Language.Chinese;
                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });
        }

    }
}
