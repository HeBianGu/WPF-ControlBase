// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public abstract class ApplicationBase : Application
    {
        public ApplicationBase()
        {
            this.InitCatchExcetion();
            ServiceRegistry.Instance.Register<IServiceCollection, ServiceCollection>();
            ServiceRegistry.Instance.Register<IApplicationBuilder, ApplicationBuilder>();
            this.ConfigureServices(this.IServiceCollection);
        }

        protected virtual void InitCatchExcetion()
        {
            //#if DEBUG
            //            return;
            //#endif
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //this.ILogger?.Info("系统启动");
            //RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
            this.Configure(this.IApplicationBuilder);
            this.CheckInstance();
            base.OnStartup(e);
            this.OnStart(e);
            this.ILogger?.Info("系统启动");
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
        protected abstract MainWindowBase CreateMainWindow(StartupEventArgs e);


        /// <summary>
        /// 初始化数据
        /// </summary>
        protected virtual void Initializeing()
        {
            Func<IStartWindow, bool> func = l =>
             {
                 //  Do ：加载工程
                 ILoad database = ServiceRegistry.Instance.GetInstance<IDataBaseInitService>();
                 if (database != null)
                 {
                     l?.SetMessage($"正在初始化{database.Name}");
                     //#if DEBUG
                     //                     Thread.Sleep(1000);
                     //#endif
                     if (database.Load(out string message) == false)
                     {
                         l?.SetMessage(message);
                         Logger.Instance?.Error(message);
                         Thread.Sleep(1000);
                         return false;
                     }
                 }
                 //  Do ：加载配置
                 ILoad load = ServiceRegistry.Instance.GetInstance<ISystemSettingService>();
                 if (load != null)
                 {
                     l?.SetMessage($"正在初始化{load.Name}");
                     //Thread.Sleep(500);
                     if (load.Load(out string message) == false)
                     {
                         l?.SetMessage(message);
                         Logger.Instance?.Error(message);
                         Thread.Sleep(1000);
                     }
                 }

                 //  ToDo：这块需要放到前面，如果配置有问题没调用升级
                 //  Do ：验证更新  
                 ILoad upgrade = ServiceRegistry.Instance.GetInstance<IUpgradeInitService>();
                 if (upgrade != null)
                 {
                     l?.SetMessage($"正在初始化{upgrade.Name}");
                     if (upgrade.Load(out string message) == false)
                     {
                         l?.SetMessage(message);
                         Logger.Instance?.Error(message);
                         Thread.Sleep(1000);
                     }
                 }

                 //  Do ：验证许可
                 ILoad license = ServiceRegistry.Instance.GetInstance<ILicenseInitService>();
                 if (license != null)
                 {
                     l?.SetMessage($"正在检查{license.Name}");
                     if (license.Load(out string message) == false)
                     {
                         l?.SetMessage(message);
                         Logger.Instance?.Error(message);
                         Thread.Sleep(1000);
                         return false;
                     }
                 }

                 //  Do ：加载工程
                 ILoad project = ServiceRegistry.Instance.GetInstance<IProjectService>();
                 if (project != null)
                 {
                     l?.SetMessage($"正在加载{project.Name}");

                     if (project?.Load(out string message) == false)
                     {
                         l?.SetMessage(message);
                         Logger.Instance?.Error(message);
                         Thread.Sleep(1000);
                     }
                 }

                 IAppLoadService custom = ServiceRegistry.Instance.GetInstance<IAppLoadService>();
                 if (custom != null)
                 {
                     l?.SetMessage($"正在加载{custom.Name}");
                     if (custom.Load(out string message) == false)
                     {
                         l?.SetMessage(message);
                         Logger.Instance?.Error(message);
                         Thread.Sleep(1000);
                         return false;
                     }
                 }

                 var loads = ServiceRegistry.Instance.GetAllAssignableFrom<IStartWindowLoad>().ToList().Distinct();
                 foreach (var item in loads)
                 {
                     if (item.Load(l, out string message) == false)
                     {
                         l?.SetMessage(message);
                         Logger.Instance?.Error(message);
                         Thread.Sleep(1000);
                         return false;
                     }
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
                bool r = start.Start(func);
                if (!r) this.Shutdown();
            }
            else
            {
                bool r = func(null);
                if (!r) this.Shutdown();
            }
            //  Do ：身份认证
            IIdentityInitService identity = ServiceRegistry.Instance.GetInstance<IIdentityInitService>();
            if (identity?.Init(out string error) == false)
            {
                this.ILogger?.Info(error);
                this.Shutdown();
            }
            this.OnLoginSuccess();
        }

        protected virtual void OnLoginSuccess()
        {

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


        protected void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //#if DEBUG
            //            Trace.Assert(false);
            //#endif
            Current.Dispatcher.Invoke(() =>
            {
                if (MessageProxy.Windower == null)
                {
                    MessageBox.Show(e.Exception?.ToString(), "系统异常");
                }
                else
                {
                    MessageProxy.Windower?.ShowSumit(e.Exception?.ToString(), "系统异常");
                }
            });

            e.Handled = true;
            this.ILogger?.Error("系统异常");
            this.ILogger?.Error(e.Exception);
        }

        protected void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //#if DEBUG
            //            Trace.Assert(false);
            //#endif
            Exception error = (Exception)e.ExceptionObject;
            Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));
            this.ILogger?.Fatal("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作");
            this.ILogger?.Fatal(error);
        }


        /// <summary> 异步线程抛出没有补货的异常 </summary>
        protected void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //#if DEBUG
            //            Trace.Assert(false);
            //#endif
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
            Current.Dispatcher.Invoke(() => MessageProxy.Windower.ShowSumit(sb.ToString(), "系统任务异常", false, -1));
        }

        protected virtual string GetVersonInfo()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;

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
            //app.UseStyle();

            //app.UseTextBox();
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

        private Mutex mutex;
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
                if (MessageProxy.Windower == null)
                {
                    MessageBox.Show("当前程序已经运行！");
                }
                else
                {
                    MessageProxy.Windower.ShowSumit("当前程序已经运行！");
                }

                Application.Current.Shutdown();
            }
        }
    }
}
