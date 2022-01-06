using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> Application基类 封装一些加载初始化和注入方法 </summary>
    public abstract class ApplicationBase : Application
    {
        public ApplicationBase()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException; 

            ServiceRegistry.Instance.Register<IServiceCollection, ServiceCollection>();
            ServiceRegistry.Instance.Register<IApplicationBuilder, ApplicationBuilder>();

            this.ConfigureServices(this.IServiceCollection);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.ILogger?.Info("系统启动");

            this.Configure(this.IApplicationBuilder);
            this.CheckInstance();
            base.OnStartup(e);
            this.OnStart(e);
        }

        protected virtual void OnStart(StartupEventArgs e)
        {
            this.InitResources();
            this.CreateMainWindow(e);  
            this.Initializeing();

            this.MainWindow.Show();
        }

        protected virtual void InitResources()
        {
            ////  Do ：引用样式
            //this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            //{
            //    Source = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Light.xaml", UriKind.Relative)
            //});
            //this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            //{
            //    Source = new Uri("/HeBianGu.Base.WpfBase;component/Theme/HeBianGu.Theme.Default.xaml", UriKind.Relative)
            //});

            //this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            //{
            //    Source = new Uri("/HeBianGu.General.WpfControlLib;component/Themes/Resource.xaml", UriKind.Relative)
            //});
        }

        /// <summary>
        /// 启动主窗口
        /// </summary>
        protected abstract Window CreateMainWindow(StartupEventArgs e);


        /// <summary>
        /// 初始化数据
        /// </summary>
        protected virtual void Initializeing()
        {
            VersionData versionData = null;

            Func<IStartWindow, bool> func = l =>
             { 
                 //  Do ：版本数据
                 IUpgradeService upgradeService = ServiceRegistry.Instance.GetInstance<IUpgradeService>();

                 if (upgradeService != null)
                 {
                     l?.SetMessage("正在检查版本");

#if DEBUG
                     Thread.Sleep(1000);
#endif

                     versionData = upgradeService.GetVersion(out string message);

                     if (versionData != null)
                     {
                         //  Do ：存在版本升级
                         return true;
                     }
                 }

                 //  Do ：加载工程
                 IProjectService project = ServiceRegistry.Instance.GetInstance<IProjectService>();

                 if (project != null)
                 {
                     l?.SetMessage("正在加载工程");
#if DEBUG
                     Thread.Sleep(1000);
#endif
                     project?.Load();
                 }

                 //  Do ：加载配置
                 ISystemSettingService setting = ServiceRegistry.Instance.GetInstance<ISystemSettingService>();

                 if (setting != null)
                 {
                     l?.SetMessage("正在加载配置");
#if DEBUG
                     Thread.Sleep(1000);
#endif

                     setting?.Load();
                 } 

                 //  Do ：加载资源
                 l?.SetMessage("正在资源");
                 StyleLoader.Instance?.LoadWpfControlLib();

                 l?.SetMessage("正在启动");
                 Thread.Sleep(1000);

                 return true;
             };

            //  Do ：启动窗口服务
            IStartInitService start = ServiceRegistry.Instance.GetInstance<IStartInitService>();

            if (start != null)
            {
                start.Start(func);
            }
            else
            {
                func(null);
            }

            //  Do ：调用版本升级
            IUpgradeInitService upgrade = ServiceRegistry.Instance.GetInstance<IUpgradeInitService>();

            string error = null;

            if (upgrade?.Init(versionData, out error) == false)
            {
                this.ILogger?.Info(error);
            }

            //  Do ：身份认证
            IIdentityInitService identity = ServiceRegistry.Instance.GetInstance<IIdentityInitService>();

            if (identity?.Init(out error) == false)
            {
                this.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                this.ILogger?.Info("系统退出");
            }
            catch (Exception ex)
            {
                this.ILogger?.Error(ex);
            }
            finally
            {
                base.OnExit(e);
            }
        }

        #region - Excetpion -


        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var message = ServiceRegistry.Instance.GetInstance<IMessageDialogService>();

            Current.Dispatcher.Invoke(() => message.ShowSumit(e.Exception.Message, "系统异常", false, 5));

            e.Handled = true;

            this.ILogger?.Error("系统异常");
            this.ILogger?.Error(e.Exception);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;

            Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));


            this.ILogger?.Fatal("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作");
            this.ILogger?.Fatal(error);
        }


        /// <summary> 异步线程抛出没有补货的异常 </summary>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Exception item in e.Exception.InnerExceptions)
            {
                sb.AppendLine($@"异常类型：{item.GetType()}
异常内容：{item.Message}
来自：{item.Source}
{item.StackTrace}");
            }

            e.SetObserved();

            this.ILogger?.Error("Task Exception");
            this.ILogger?.Error(sb.ToString());

            var message = ServiceRegistry.Instance.GetInstance<IMessageDialogService>();

            Current.Dispatcher.Invoke(() => message.ShowSumit(sb.ToString(), "系统任务异常", false, 5));
        }

        protected string GetVersonInfo()
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;

            return $"V{version.Major}.{version.Minor}.{version.Build}";
        }

        #endregion

        //protected string GetVersonInfo()
        //{
        //    var version = Assembly.GetExecutingAssembly().GetName().Version;

        //    return $"V{version.Major}.{version.Minor}.{version.Build}";
        //}

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureServices(IServiceCollection services)
        { 
            services.AddStyle();
        }

        /// <summary>
        /// 配置数据
        /// </summary>
        /// <param name="app"></param>
        protected virtual void Configure(IApplicationBuilder app)
        {
            app.UseStyle();
        }

        public ILogService ILogger
        {
            get
            {
                return ServiceRegistry.Instance.GetInstance<ILogService>();
            }
        }

        public IApplicationBuilder IApplicationBuilder
        {
            get
            {
                return ServiceRegistry.Instance.GetInstance<IApplicationBuilder>();
            }
        }

        public IServiceCollection IServiceCollection
        {
            get
            {
                return ServiceRegistry.Instance.GetInstance<IServiceCollection>();
            }
        }

        Mutex mutex;
        /// <summary> 只创建一个实例 </summary>
        public virtual void CheckInstance()
        {
            //Process thisProc = Process.GetCurrentProcess();

            //var p = Process.GetProcessesByName(thisProc.ProcessName);

            //if (p.Length > 1)
            //{
            //    MessageWindow.ShowSumit("当前程序已经运行！");

            //    this.Shutdown();
            //}

            Process thisProc = Process.GetCurrentProcess();

            //互斥量创建成功标志
            bool createdNew;

            //创建互斥量
            mutex = new Mutex(true, thisProc.ProcessName, out createdNew);

            if (!createdNew)
            {
                var message = ServiceRegistry.Instance.GetInstance<IMessageDialogService>();


                if (message == null)
                {
                    MessageBox.Show("当前程序已经运行！");
                }
                else
                {
                    message.ShowSumit("当前程序已经运行！");
                }

                Application.Current.Shutdown();
            }
        }

    }

}
