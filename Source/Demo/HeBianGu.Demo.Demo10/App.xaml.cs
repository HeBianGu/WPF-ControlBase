using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Setting;
using HeBianGu.Systems.Upgrade;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Demo.Demo10
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

            //  Do ：注册动画
            services.AddWindowAnimation();

            //  Do ：注册消息
            services.AddMessageProxy();

            //  Do ：注册序列化保存接口，注册后主题的配置会保存到本地，再次启动会读取
            services.AddXmlSerialize();

            //  Do ：注册后可以使用框架自带的对话框
            services.AddWindowDialog();

            //  Do ：注册配置加载方式
            services.AddSetting();

            //  Do ：注册窗口配置，注册后窗口右侧有可设置主题的按钮
            services.AddThemeRightViewPresenter();

            services.AddUpgradeViewPresenter();
            services.AddMoreViewPresenter(x =>
            {
                x.AddPersenter(UpgradeViewPresenter.Instance);
            });
            //  Do ：注册右上角配置页面
            services.AddSettingViewPrenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                x.AddPersenter(MoreViewPresenter.Instance);
                x.AddPersenter(SettingViewPresenter.Instance);
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });

            //  Do ：注册启动页面
            services.AddStart(l =>
            {
                l.Title = "HeBianGu";
                l.ProductFontSize = 80;
            });

            //  Do ：注册登录页面和使用测试接口
            services.AddIdentity();

            ////  Do ：注册登录页面和使用自定义接口
            //services.AddSingleton<IIdentityService, IdentityService>();

            services.AddXmlWebSerializerService();
            //  Do ：注册软件更新页面
            services.AddAutoUpgrade(l =>
            {
                //@"http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4"
                l.Uri = "https://gitee.com/hebiangu/wpf-auto-update/raw/master/Install/Movie/Movie.xml";
                l.UseIEDownload = true;
              
            });


        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            //  Do ：添加自定义配置信息
            app.UseSetting(l =>
            {
                l.Settings.Add(TestSetting.Instance);
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

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;

                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });
        }

    }

    [Displayer(Name = "自定义", GroupName = "我是分组")]
    public class TestSetting : LazySettingInstance<TestSetting>
    {
        private bool _useIsEnabled;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "配置项一")]
        public bool UseIsEnabled
        {
            get { return _useIsEnabled; }
            set
            {
                _useIsEnabled = value;
                RaisePropertyChanged();
            }
        }


        private string _value;
        /// <summary> 说明  </summary> 
        [DefaultValue("HeBianGu")]
        [Display(Name = "配置项二")]
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }

}
