using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Application.MainWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);





            //int c = 2;

            //Func<double, double> RoundDown = l =>
            //       {
            //           if (l > 0)
            //           {
            //               var dfdf = (int)l.Log10() - c;

            //               return l.RoundDown(Math.Pow(10, dfdf));
            //           }
            //           else
            //           {
            //               l = -l;

            //               var dfdf = (int)l.Log10() - c;

            //               return -(l.RoundUp(Math.Pow(10, dfdf)));
            //           }
            //       };


            //Func<double, double> RoundUp = l =>
            //{
            //    if (l > 0)
            //    {
            //        var dfdf = (int)l.Log10() - c;

            //        return l.RoundUp(Math.Pow(10, dfdf));
            //    }
            //    else
            //    {
            //        l = -l;

            //        var dfdf = (int)l.Log10() - c;

            //        return -l.RoundDown(Math.Pow(10, dfdf));
            //    }
            //};


            //for (double i = 0.0000000001; i < 100000000; i = i + 0.1)
            //{
            //    double d = 0.00336;

            //    //d = 44320.00336;

            //    var sss = RoundDown(d);
            //    var sss1 = RoundUp(d);

            //    if (d < sss)
            //    {
            //        Debug.WriteLine("当前值 " + d);
            //        Debug.WriteLine("最小值 " + sss);
            //    }

            //    if (d > sss1)
            //    {
            //        Debug.WriteLine("当前值 " + d);
            //        Debug.WriteLine("最大值 " + sss1);
            //    }
            //}

            //{
            //    double d = 0.00336;

            //    //d = 44320.00336;

            //    var sss = RoundDown(d);
            //    var sss1 = RoundUp(d);

            //    if (d < sss)
            //    {
            //        Debug.WriteLine("当前值 " + d);
            //        Debug.WriteLine("最小值 " + sss);
            //    }

            //    if (d > sss1)
            //    {
            //        Debug.WriteLine("当前值 " + d);
            //        Debug.WriteLine("最大值 " + sss1);
            //    }

            //}

            //{
            //    double d = -0.00336;

            //    //d = 44320.00336;

            //    var sss = RoundDown(d);
            //    var sss1 = RoundUp(d);

            //    Debug.WriteLine("当前值 " + d);
            //    Debug.WriteLine("最小值 " + sss);
            //    Debug.WriteLine("最大值 " + sss1);
            //}

            //{
            //    double d = 0.00336;

            //    d = 44320.00336;

            //    var sss = RoundDown(d);
            //    var sss1 = RoundUp(d);

            //    Debug.WriteLine("当前值 " + d);
            //    Debug.WriteLine("最小值 " + sss);
            //    Debug.WriteLine("最大值 " + sss1);
            //}

            //{
            //    double d = 0.00336;

            //    d = -44320.00336;

            //    var sss = RoundDown(d);
            //    var sss1 = RoundUp(d);

            //    Debug.WriteLine("当前值 " + d);
            //    Debug.WriteLine("最小值 " + sss);
            //    Debug.WriteLine("最大值 " + sss1);
            //}

            //var fdf = d.RoundDown(Math.Pow(10, dfdf));

            //var fdfdf = d.RoundUp(Math.Pow(10, dfdf));

            MainWindow shellWindow = new MainWindow();

            shellWindow.Show();

        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            //  Do ：注册本地化配置读写服务
            services.AddSingleton<IThemeLocalizeService, AssemblyDomain>();

            ////  Do ：注册日志服务
            //services.AddSingleton<ILogService, AssemblyDomain>();

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
            });
        }
    }
}
