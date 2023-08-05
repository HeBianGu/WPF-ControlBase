using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Control.MessageListBox;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.About;
using HeBianGu.Systems.Design;
using HeBianGu.Systems.Feedback;
using HeBianGu.Systems.Identity;
using HeBianGu.Systems.Logger;
using HeBianGu.Systems.Notification;
using HeBianGu.Systems.Operation;
using HeBianGu.Systems.Project;
using HeBianGu.Systems.Repository;
using HeBianGu.Systems.Setting;
using HeBianGu.Systems.Survey;
using HeBianGu.Systems.Upgrade;
using HeBianGu.Systems.WinTool;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace HeBianGu.App.Report
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override MainWindowBase CreateMainWindow(StartupEventArgs e)
        {
            //RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
            return new MainWindow();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddStart(x => x.ProductFontSize = 100);
            services.AddWindowDialog();
            services.AddObjectWindowDialog();
            services.AddWindowAnimation();
            services.AddMessageProxy();
            services.AddPropertyGridMessage();
            services.AddAutoColumnPagedDataGridMessage();
            services.AddSetting();
            services.AddIdentity(x => x.ProductFontSize = 50);

            services.AddProjectDefault();

            services.AddXmlWebSerializerService();
            //  Do ：注册软件更新页面
            services.AddAutoUpgrade(x =>
            {
                x.Uri = "https://gitee.com/hebiangu/wpf-auto-update/raw/master/Install/Report/AutoUpdate.xml";
                x.UseIEDownload = true;
            });
            services.AddSettingPath();
            services.AddXmlSerialize();
            services.AddDESCryptService();
            services.AddTheme();
            services.AddPrintBoxMessage();

            #region - WindowStatus -
            services.AddWinToolViewPresenter();
            services.AddWinSpecialFolderViewPresenter();
            services.AddNotificationViewPresenter();
            services.AddFastFileViewPresenter();
            services.AddExtensionViewPresenter();
            services.AddFavoriteViewPresenter();
            services.AddRecenterViewPresenter();
            services.AddProjectViewPresenter();
            services.AddAsnycProgressViewPresenter();
            services.AddCopyRightViewPresenter();
            services.AddWindowStatusViewPresenter(x =>
            {
                x.AddPersenter(CopyRightViewPresenter.Instance);
                x.AddPersenter(ProjectViewPresenter.Instance);
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

            #region - WindowMessage -
            services.AddInfoMessageViewPresenter();
            services.AddErrorMessageViewPresenter();
            services.AddWindowMessageViewPresenter(x =>
            {
                x.AddPersenter(InfoMessageViewPresenter.Instance);
                x.AddPersenter(ErrorMessageViewPresenter.Instance);

            });
            #endregion

            #region - WindowMenu -
            services.AddNewProjectTreeViewPresenter(x =>
            {
                x.PredefinePath = System.IO.Path.Combine("文件", "工程管理", "再管理");
            });
            services.AddWindowMenuViewPresenter(x =>
            {
                x.AddPresenter(NewProjectTreeViewPresenter.Instance);
            });
            #endregion

            #region - More -
            services.AddUpgradeViewPresenter();
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
                x.AddPersenter(UpgradeViewPresenter.Instance);
                //x.AddPersenter(LicenseViewPresenter.Instance);
                //x.AddPersenter(VipViewPresenter.Instance);
                x.AddPersenter(SurveyViewPresenter.Instance);
                x.AddPersenter(FeedbackViewPresenter.Instance);
                x.AddPersenter(AboutViewPresenter.Instance);
                x.AddPersenter(LogoutViewPresenter.Instance);

            });
            #endregion

            //#region - Window -
            //services.AddWindowContentViewPresenter();
            //#endregion

            #region - WindowCaption -
            services.AddLoginViewPresenter();
            services.AddGuideViewPresenter();
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

            #region - LoginManager -
            services.AddUserViewPresenter();
            services.AddRoleViewPresenter();
            services.AddOparationViewPresenter();
            services.AddPasswordEditViewPresenter();
            services.AddLogoutViewPresenter();
            services.AddLoginManagerViewPresenter(x =>
            {
                x.AddPersenter(UserViewPresenter.Instance);
                x.AddPersenter(RoleViewPresenter.Instance);
                x.AddPersenter(PasswordEditViewPresenter.Instance);
                x.AddPersenter(OparationViewPresenter.Instance);
                x.AddPersenter(LogoutViewPresenter.Instance);

            });
            #endregion

            //#region - WindowTree -
            //services.AddLoginOparationViewPresenter();
            //services.AddLoggerViewPresenter();
            //services.AddHomeViewPresenter(x =>
            //{
            //    x.AddPersenter(LoginOparationViewPresenter.Instance);
            //    x.AddPersenter(LoggerViewPresenter.Instance);
            //});

            //services.AddUserViewPresenter();
            //services.AddRoleViewPresenter();
            //services.AddOparationViewPresenter();
            //services.AddDebugRepositoryViewPresenter();
            //services.AddWindowTreeViewPresenter(x =>
            //{
            //    //x.AddPreDefinePath(System.IO.Path.Combine("文件", "新建", "工程管理"));
            //    //x.AddPreDefinePath(System.IO.Path.Combine("文件", "打开", "工程管理"));
            //    //x.AddPreDefinePath(System.IO.Path.Combine("编辑", "复制", "工程管理"));
            //    //x.AddPreDefinePath(System.IO.Path.Combine("视图", "打开", "工程管理", "工程管理"));
            //    x.AddHomePresenter(HomeViewPresenter.Instance);
            //    x.AddPresenter(UserViewPresenter.Instance);
            //    x.AddPresenter(RoleViewPresenter.Instance);
            //    x.AddPresenter(OparationViewPresenter.Instance);
            //    x.AddPresenter(new RepositoryViewPresenter()
            //    {
            //        Name = "错误日志",
            //        ViewModel = new RepositoryViewModel<hl_dm_error>(),
            //        PredefinePath = "日志管理"
            //    });


            //    x.AddPresenter(new RepositoryViewPresenter()
            //    {
            //        Name = "运行日志",
            //        ViewModel = new RepositoryViewModel<hl_dm_info>(),
            //        PredefinePath = "日志管理"
            //    });
            //    x.AddPresenter(DebugRepositoryViewPresenter.Instance);
            //    x.AddPresenter(new RepositoryViewPresenter()
            //    {
            //        Name = "警告日志",
            //        ViewModel = new RepositoryViewModel<hl_dm_warn>(),
            //        PredefinePath = "日志管理"
            //    });
            //    x.AddPresenter(new RepositoryViewPresenter()
            //    {
            //        Name = "严重日志",
            //        ViewModel = new RepositoryViewModel<hl_dm_fatal>(),
            //        PredefinePath = "日志管理"
            //    });
            //});
            //#endregion 

            //#region - WindowToolbar -
            //services.AddWindowToolbarViewPresenter(x =>
            //{
            //    x.AddPersenter(WinSpecialFolderViewPresenter.Instance);
            //    x.AddPersenter(MoreViewPresenter.Instance);
            //});
            //#endregion


            #region - WindowSideEdit -   
            services.AddWindowSideEditViewPresenter(x =>
            {
                //service.AddSingleton<IInfoMessageViewPresenter, InfoMessageViewPresenter>();
                //action?.Invoke(InfoMessageViewPresenter.Instance);
                //WindowMessageViewPresenter.Instance.AddPersenter(InfoMessageViewPresenter.Instance);

                x.AddPersenter(InfoMessageViewPresenter.Instance);
                x.AddPersenter(ErrorMessageViewPresenter.Instance);
            });
            #endregion


            //services.AddAuthority(x =>
            //{

            //});
            //services.AddLoginManagerDefaultViewPresenter();


            services.AddDbContext();
            services.AddSystemRepository();
            services.AddDataBaseInitService();
            services.AddDbLogger();

            services.AddDesginViewPresenter(x =>
            {
                x.AddSource(new TestDesignDataSource());
            });
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            app.UseStyle();

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                l.DefaultFontSize = 15D;
                l.FontSize = FontSize.Small;
                l.ItemHeight = 30;
                l.RowHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 2;
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
