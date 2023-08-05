// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.About;
using HeBianGu.Systems.Feedback;
using HeBianGu.Systems.Identity;
using HeBianGu.Systems.Setting;
using HeBianGu.Systems.Survey;
using HeBianGu.Systems.Upgrade;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Application.TestWindow
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

            //  Do ：注入领域模型服务
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();

            services.AddWindowDialog();

            services.AddObjectWindowDialog();

            services.AddWindowAnimation();

            services.AddStudentDemo();

            services.AddMessageProxy();

            services.AddSetting();

            services.AddPropertyGridMessage();


            //services.AddIdentity();
            //services.AddLoginViewPresenter();

            //services.AddVip();
            //services.AddVipViewPresenter();
            //services.AddLicense();
            //services.AddLicenseViewPresenter();
            services.AddGuideViewPresenter();
            services.AddUpgradeViewPresenter();
            ////  Do ：注册软件更新页面
            //services.AddAutoUpgrade();
            services.AddSurveyViewPresenter();
            services.AddFeedbackViewPresenter();
            services.AddLogoutViewPresenter();
            services.AddAboutViewPresenter();
            services.AddSettingViewPrenter();

            //services.AddStart(x =>
            //{
            //    x.SleepMilliseconds = 5000;
            //});

            services.AddMoreViewPresenter(x =>
            {
                x.AddPersenter(UpgradeViewPresenter.Instance);
                //x.AddPersenter(LicenseViewPresenter.Instance);
                //x.AddPersenter(VipViewPresenter.Instance);
                x.AddPersenter(SurveyViewPresenter.Instance);
                x.AddPersenter(FeedbackViewPresenter.Instance);
                x.AddPersenter(AboutViewPresenter.Instance);
                x.AddPersenter(LogoutViewPresenter.Instance);

            });
            services.AddHideWindowViewPresenter();

            services.AddWindowStatusViewPresenter();
            services.AddProjectViewPresenter();

            services.AddWindowMessageViewPresenter();
            services.AddInfoMessageViewPresenter();
            services.AddErrorMessageViewPresenter();

            services.AddWindowMenuViewPresenter(x =>
            {

            });

            services.AddNewProjectTreeViewPresenter(x =>
            {
                x.PredefinePath = System.IO.Path.Combine("文件", "工程管理", "再管理");
            });

            services.AddWinToolViewPresenter();
            services.AddNotificationViewPresenter();
            services.AddNPOI();
            services.AddLogger();
            services.AddWindowExplorer();

            #region - WindowCaption -
            services.AddGuideViewPresenter();
            services.AddSettingViewPrenter();
            services.AddThemeRightViewPresenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                x.AddPersenter(MoreViewPresenter.Instance);
                x.AddPersenter(GuideViewPresenter.Instance);
                x.AddPersenter(SettingViewPresenter.Instance);
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });
            #endregion

        }

        protected override void Configure(IApplicationBuilder app)
        {
            app.UseMainWindowSetting();

            base.Configure(app);

            //app.UseFeedback(x =>
            //{
            //    x.DialogMode = DialogMode.Control;
            //});

            //app.UseSurvey(x =>
            //{
            //    x.DialogMode = DialogMode.Control;
            //});

            //app.UseIdentity(x =>
            //{
            //    x.UseVisitor = false;
            //});

            //app.UseLicense();

            //app.UseAbout();

            ////  Do ：添加软件更新配置
            //app.UseUpgrade(l =>
            //{
            //    //@"http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4"
            //    l.Uri = "https://gitee.com/hebiangu/wpf-auto-update/raw/master/Install/Movie/Movie.xml";
            //    l.UseIEDownload = false;
            //});

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                l.DefaultFontSize = 14D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 36;
                l.RowHeight = 40;
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
