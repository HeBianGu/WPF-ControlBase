// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.About;
using HeBianGu.Systems.Feedback;
using HeBianGu.Systems.Identity;
using HeBianGu.Systems.Notification;
using HeBianGu.Systems.Setting;
using HeBianGu.Systems.Survey;
using HeBianGu.Systems.Upgrade;
using HeBianGu.Systems.WinTool;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Image
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override MainWindowBase CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }



        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddWindowDialog();
            services.AddWindowAnimation();
            //services.AddTheme();
            services.AddMessageProxy();
            services.AddXmlSerialize();
            services.AddXmlMeta();

            services.AddSetting();
            //services.AddStart();
            //services.AddObjectWindowDialog();
            //services.AddPropertyGridMessage();
            //services.AddIdentity();
            //////  Do ：注册软件更新页面
            ////services.AddAutoUpgrade();


            #region - More -
            //services.AddUpgradeViewPresenter();
            //services.AddLicense();
            //services.AddLicenseViewPresenter();
            //services.AddVip();
            //services.AddVipViewPresenter();
            services.AddSurveyViewPresenter();
            services.AddFeedbackViewPresenter();
            //services.AddLogoutViewPresenter();
            services.AddAboutViewPresenter();
            services.AddMoreViewPresenter(x =>
            {
                //x.AddPersenter(UpgradeViewPresenter.Instance);
                //x.AddPersenter(LicenseViewPresenter.Instance);
                //x.AddPersenter(VipViewPresenter.Instance);
                x.AddPersenter(SurveyViewPresenter.Instance);
                x.AddPersenter(FeedbackViewPresenter.Instance);
                x.AddPersenter(AboutViewPresenter.Instance);
                x.AddPersenter(LogoutViewPresenter.Instance);

            });
            #endregion

            #region - WindowCaption -
            //services.AddLoginViewPresenter();
            //services.AddGuideViewPresenter();
            services.AddHideWindowViewPresenter();
            services.AddSettingViewPrenter();
            //services.AddThemeRightToolViewPresenter();
            services.AddThemeRightViewPresenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                x.AddPersenter(LoginViewPresenter.Instance);
                x.AddPersenter(MoreViewPresenter.Instance);
                x.AddPersenter(GuideViewPresenter.Instance);
                x.AddPersenter(HideWindowViewPresenter.Instance);
                x.AddPersenter(SettingViewPresenter.Instance);
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });

            #endregion

            #region - WindowStatus -
            services.AddWinToolViewPresenter();
            services.AddWinSpecialFolderViewPresenter();
            services.AddNotificationViewPresenter();
            services.AddFastFileViewPresenter();
            services.AddExtensionViewPresenter();
            services.AddFavoriteViewPresenter();
            services.AddRecenterViewPresenter();
            //services.AddProjectViewPresenter();
            services.AddAsnycProgressViewPresenter();
            services.AddCopyRightViewPresenter();
            services.AddWindowStatusViewPresenter(x =>
            {
                x.AddPersenter(CopyRightViewPresenter.Instance);
                //x.AddPersenter(ProjectViewPresenter.Instance);
                x.AddPersenter(WinToolViewPresenter.Instance);
                x.AddPersenter(WinSpecialFolderViewPresenter.Instance);
                x.AddPersenter(NotificationViewPresenter.Instance);
                x.AddPersenter(FastFileViewPresenter.Instance);
                x.AddPersenter(ExtensionViewPresenter.Instance);
                x.AddPersenter(FavoriteViewPresenter.Instance);
                x.AddPersenter(RecenterViewPresenter.Instance);
                x.AddPersenter(AsnycProgressViewPresenter.Instance);
            });
            #endregion
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                //l.ForegroundColor = (Color)ColorConverter.ConvertFromString("#000000");
                l.DefaultFontSize = 15D;
                l.FontSize = FontSize.Small;
                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;
                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;
                l.ThemeType = ThemeType.Dark;
                l.Language = Language.Chinese;
                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });

            StyleSetting.Instance.StyleType = StyleType.Single;
        }
    }
}
