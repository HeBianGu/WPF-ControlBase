using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Systems.Repository;
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
        protected override System.Windows.Window CreateMainWindow(StartupEventArgs e)
        {
            return new ShellWindow();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            //  Do ：注入领域模型服务
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();

            ////  Do ：注册日志服务
            //services.AddSingleton<ILogService, AssemblyDomain>();

            services.AddMessageDialog();

            services.AddWindowAnimation();

            services.AddTheme();

            services.AddStudentDemo();

            services.AddXmlSerialize();
            services.AddXmlMeta();

            services.AddSetting();
            services.AddSettingPath();
            services.AddSettingViewPrenter();

            services.AddMessage();

            //services.AddAutomation();

            services.AddMvp();

            //services.AddStart();

            services.AddSingleton<IRepository<hi_dd_user>, Repository<hi_dd_user>>();
            services.AddSingleton<IRepository<hi_dd_role>, Repository<hi_dd_role>>();
            services.AddSingleton<IRepository<hi_dd_log>, Repository<hi_dd_log>>();
            services.AddSingleton<IRepository<hi_dd_author>, Repository<hi_dd_author>>();

            services.AddSingleton<IRepository<mbc_db_areatype>, Repository<mbc_db_areatype>>();
            services.AddSingleton<IRepository<mbc_db_articulationtype>, Repository<mbc_db_articulationtype>>();
            services.AddSingleton<IRepository<mbc_db_extendtype>, Repository<mbc_db_extendtype>>();
            services.AddSingleton<IRepository<mbc_db_fromtype>, Repository<mbc_db_fromtype>>();
            services.AddSingleton<IRepository<mbc_db_mediatype>, Repository<mbc_db_mediatype>>();
            services.AddSingleton<IRepository<mbc_db_tagtype>, Repository<mbc_db_tagtype>>();
            services.AddSingleton<IRepository<mbc_db_viptype>, Repository<mbc_db_viptype>>();
            services.AddSingleton<IRepository<mbc_dc_case>, Repository<mbc_dc_case>>();
            services.AddSingleton<IRepository<mbc_dv_image>, Repository<mbc_dv_image>>();
            services.AddSingleton<IRepository<mbc_dv_movie>, Repository<mbc_dv_movie>>();
            services.AddSingleton<IRepository<mbc_dv_movieimage>, Repository<mbc_dv_movieimage>>();
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

            app.UseStart(l =>
            {
                l.Title = "综合管理系统";
                l.TitleFontSize = 60;
            });

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");

                l.SmallFontSize = 14D;
                l.LargeFontSize = 16D;
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
