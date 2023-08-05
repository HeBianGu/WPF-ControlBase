using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Repository;
using HeBianGu.Systems.Setting;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Repository
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
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();
            services.AddWindowDialog();
            services.AddWindowAnimation();
            services.AddStudentDemo();
            services.AddXmlSerialize();
            services.AddXmlMeta();
            services.AddSetting();
            services.AddSettingPath();
            services.AddMessageProxy();
            services.AddPropertyGridMessage();
            //services.AddAutomation();
            services.AddMvp();
            services.AddAutoColumnPagedDataGridMessage();

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

            services.AddSingleton<Systems.Repository.IRepository<hi_dd_user>, Repository<hi_dd_user>>();
            services.AddSingleton<Systems.Repository.IRepository<hi_dd_role>, Repository<hi_dd_role>>();
            services.AddSingleton<Systems.Repository.IRepository<hi_dd_log>, Repository<hi_dd_log>>();
            services.AddSingleton<Systems.Repository.IRepository<hi_dd_author>, Repository<hi_dd_author>>();

            services.AddSingleton<Systems.Repository.IRepository<mbc_db_areatype>, Repository<mbc_db_areatype>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_db_articulationtype>, Repository<mbc_db_articulationtype>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_db_extendtype>, Repository<mbc_db_extendtype>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_db_fromtype>, Repository<mbc_db_fromtype>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_db_mediatype>, Repository<mbc_db_mediatype>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_db_tagtype>, Repository<mbc_db_tagtype>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_db_viptype>, Repository<mbc_db_viptype>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_dc_case>, Repository<mbc_dc_case>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_dv_image>, Repository<mbc_dv_image>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_dv_movie>, Repository<mbc_dv_movie>>();
            services.AddSingleton<Systems.Repository.IRepository<mbc_dv_movieimage>, Repository<mbc_dv_movieimage>>();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            app.UseMvp(l =>
            {
                l.UseVisiblity = false;
                l.UseIsEnabled = true;
            });

            app.UseRepository(l =>
            {
                l.PageCount = 5;
                l.UsePagination = false;
                l.UseLoyout = false;
                l.UseTempate = false;
                l.UseDetial = true;
                l.UseCheckBox = false;
                l.ItemsControlType = ItemsControlType.PagedDataGrid;
            });

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

                l.ThemeType = ThemeType.DarkGray;
                l.StyleType = StyleType.Single;

                l.Language = Language.Chinese;

                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });


            //app.UseSetting(l =>
            //{
            //    //l.Settings.Add(RepositorySetting.Instance);
            //    //l.Settings.Add(ThemeSetting.Instance);
            //    //l.Settings.Add(StartSetting.Instance);
            //    //l.Settings.Add(MessageSetting.Instance);
            //    l.Settings.Add(StyleSetting.Instance);
            //    //l.Settings.Add(SystemPathSetting.Instance);
            //    //l.Settings.Add(MvpSetting.Instance); 
            //});

            app.UseMeta();

            app.UsePagedDataGrid();

            app.UseLinkGroupsManagerWindowSetting();

            app.UseMessage();

            app.UseMainWindowSetting();

        }

    }
}
