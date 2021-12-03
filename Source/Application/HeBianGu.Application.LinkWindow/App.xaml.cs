using HeBianGu.Applications.ControlBase.LinkWindow;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Application.LinkWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void OnMainWindow(StartupEventArgs e)
        {
            MainWindow shellWindow = new MainWindow();

            LoginWindow login = new LoginWindow();

            login.Title = this.GetVersonInfo();
        
            login.InitAccount(()=>
            {
                Thread.Sleep(1000);

                return Tuple.Create("HeBianGu","89757",true);
            });


            login.IsMatch = () =>
            {
                string name = login.UseName;
                string password = login.PassWord;
                bool remenber = login.Remenber;

                return Task.Run(() =>
                {
                    Thread.Sleep(1000);

                    var result = AssemblyDomain.Instance.Login(name, password, remenber, out string error);

                    if (!result)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            login.LoginMessage = error;
                        });
                    }

                    return result;
                });

            };

            var r = login.ShowDialog();

            if (r != true) return;

            shellWindow.Show(); 

        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            //  Do：注册Mvc模式
            services.UseMvc();

            //  Do ：注册本地化配置读写服务
            services.AddSingleton<IThemeSerializeService, AssemblyDomain>();

            ////  Do ：注册日志服务
            //services.AddSingleton<ILogService, AssemblyDomain>();

            services.UseMessageWindow();

            services.UseWindowAnimation();

            services.UseThemeSave();


        }

        protected override void Configure(IApplicationBuilder app)
        {
            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF003D99");
                //l.ForegroundColor = (Color)ColorConverter.ConvertFromString("#727272");

                l.SmallFontSize = 15D;
                l.LargeFontSize = 18D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;

                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;

                l.DialogCoverBrush = new SolidColorBrush(Colors.Black) { Opacity = 0.6 };

                l.AccentBrushType = AccentBrushType.LinearGradientBrush;

            });

            //app.UseTheme(l => l.DialogCoverBrush = new SolidColorBrush(Colors.Black) { Opacity = 0.6 });
        }
    }
}
