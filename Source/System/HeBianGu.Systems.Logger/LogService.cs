
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.AppConfig;
using HeBianGu.Service.Mvp;
using log4net;
using log4net.Appender;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HeBianGu.Systems.Logger
{
    public interface ILogServiceOption
    {
        string LogPath { get; set; }
        string TempPath { get; set; }
    }

    [Displayer(Name = "日志记录", GroupName = SystemSetting.GroupSystem, Icon = IconAll.QuesitionFill, Description = "点击显示新手向导")]
    public class LogService : ServiceSettingInstance<LogService, ILogService>, ILogService, ILogServiceOption
    {
        private bool _useDebug;
        [Display(Name = "启用操作日志记录")]
        public bool UseDebug
        {
            get { return _useDebug; }
            set
            {
                _useDebug = value;
                RaisePropertyChanged();
            }
        }

        private bool _useInfo;
        [Display(Name = "启用运行日志记录")]
        public bool UseInfo
        {
            get { return _useInfo; }
            set
            {
                _useInfo = value;
                RaisePropertyChanged();
            }
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            //this.TempPath = System.Environment.GetEnvironmentVariable("TEMP") + @"\";
            this.LogPath = SystemPathSetting.Instance.Log;
            this.TempPath = SystemPathSetting.Instance.Cache;
        }

        private string _tempPath;
        [Display(Name = "缓存路径")]
        public string TempPath
        {
            get { return _tempPath; }
            set
            {
                _tempPath = value;
                RaisePropertyChanged();
            }
        }

        private string _logPath;
        [Display(Name = "日志路径")]
        public string LogPath
        {
            get { return _logPath; }
            set
            {
                _logPath = value;
                RaisePropertyChanged();
            }
        }

        private bool _usePresenter;
        [DefaultValue(true)]
        [Display(Name = "应用消息列表")]
        public bool UsePresenter
        {
            get { return _usePresenter; }
            set
            {
                _usePresenter = value;
                RaisePropertyChanged();
            }
        }

        public LogService()
        {
            var process = Process.GetCurrentProcess().ProcessName;
            this.InitLogger(process);
        }

        protected virtual void InitLogger(string name)
        {
            string logconfig = @"log4net.config";
            ReplaceFileTag(logconfig);
            Stopwatch st = new Stopwatch();
            //  开始计时
            st.Start();
            log4net.GlobalContext.Properties["dynamicName"] = name;
            Logger = LogManager.GetLogger(name);
            //  终止计时
            st.Stop();
            if (st.ElapsedMilliseconds > 2000)
            {
                Logger.Info("log4net.config file ERROR!!!");
                System.IO.FileStream fs = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                string str = sr.ReadToEnd();
                str = str.Replace(@"ref=""SQLAppender""", @"ref=""SQLAppenderError""");
                sr.Close();
                fs.Close();
                System.IO.FileStream fs1 = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.Write);
                StreamWriter swWriter = new StreamWriter(fs1, System.Text.Encoding.UTF8);
                swWriter.Flush();
                swWriter.Write(str);
                swWriter.Close();
                fs1.Close();
            }

            //string exeFileFullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            //string exeName = Path.GetFileNameWithoutExtension(exeFileFullPath);
            //string binPath = Path.GetDirectoryName(exeFileFullPath);

            //binPath = Path.GetDirectoryName(binPath);

            //string logFilePath = Path.GetDirectoryName(binPath);

            //var exe = Process.GetCurrentProcess();

            //if (exe == null) return;

            //string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //string logPath = Path.Combine(documentPath, "HeBianGu", exeName, "Log");

            if (!Directory.Exists(this.LogPath))
            {
                Directory.CreateDirectory(this.LogPath);
            }

            InitLogPath(this.LogPath);
        }


        ILog Logger = null;
        void InitLogPath(string repository)
        {
            RollingFileAppender appender = new RollingFileAppender();
            appender.File = repository + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            appender.AppendToFile = true;
            appender.MaxSizeRollBackups = -1;
            //appender.MaximumFileSize = "1MB";  
            appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
            appender.DatePattern = "yyyy-MM-dd_HH\".log\"";
            appender.StaticLogFileName = false;
            appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            appender.Layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level - %message%newline");
            appender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(appender);
        }

        ///// <summary>
        ///// 事件传送信息打印
        ///// </summary>
        //void EventsMsg(object sender, object e, System.Diagnostics.StackFrame SourceFile)
        //{
        //    string msg = string.Format("[FILE:{0} ],LINE:{1},{2}] sender:{3},e:{4}", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod(), sender.GetType(), e.GetType());
        //    //Trace.WriteLine(msg);
        //    if (Logger != null)
        //    {
        //        Logger.Info(msg);
        //    }
        //}

        //string GetFileMsg(System.Diagnostics.StackFrame SourceFile)
        //{
        //    return string.Format("FILE: [{0}] LINE:[{1}] Method:[{2}]", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod());
        //}

        //void Error(System.Diagnostics.StackFrame SourceFile, Exception ex)
        //{
        //    if (ex == null)
        //        return;
        //    if (Logger != null)
        //    {
        //        Logger.Info(GetFileMsg(SourceFile));
        //        Logger.Error(ex.Message);
        //        if (ex.InnerException != null)
        //            Logger.Fatal(ex.InnerException);
        //    }
        //}

        //void Info(System.Diagnostics.StackFrame SourceFile, string infomsg)
        //{
        //    if (infomsg == null) return;

        //    if (Logger == null) return;

        //    Logger.Info(GetFileMsg(SourceFile));
        //    Logger.Info(infomsg);
        //}

        void ReplaceFileTag(string logconfig)
        {
            try
            {
                FileStream fs = new FileStream(logconfig, FileMode.Open, FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                fs.Close();

                if (str.IndexOf("#LOG_PATH#") > -1)
                {
                    str = str.Replace(@"#LOG_PATH#", this.TempPath);
                    FileStream fs1 = new FileStream(logconfig, FileMode.Open, FileAccess.Write);
                    StreamWriter swWriter = new StreamWriter(fs1, Encoding.UTF8);
                    swWriter.Flush();
                    swWriter.Write(str);
                    swWriter.Close();
                    fs1.Close();
                }
            }
            catch { }
        }

        public void Debug(params string[] messages)
        {
            if (!this.UseDebug) return;
            foreach (var item in messages)
            {
                this.Logger.Debug(item);
            }
        }

        public void Error(params string[] messages)
        {
            foreach (var item in messages)
            {
                this.Logger.Error(item);
                if (!this.UsePresenter) continue;
                ErrorMessageViewPresenterProxy.Instance?.AddMessage(item);
            }
        }

        public void Fatal(params string[] messages)
        {
            foreach (var item in messages)
            {
                this.Logger.Fatal(item);
                if (!this.UsePresenter) continue;
                FatalMessageViewPresenterProxy.Instance?.AddMessage(item);
            }
        }

        public void Fatal(params Exception[] messages)
        {
            foreach (var item in messages)
            {
                this.Logger.Fatal(item.Message, item);
                if (!this.UsePresenter) continue;
                FatalMessageViewPresenterProxy.Instance?.AddMessage(item.Message);
            }
        }

        public void Trace(params string[] messages)
        {
            foreach (var item in messages)
            {
                this.Logger.Debug(item);
                if (!this.UsePresenter) continue;
                DebugMessageViewPresenterProxy.Instance?.AddMessage(item);
            }
        }

        public void Warn(params string[] messages)
        {
            foreach (var item in messages)
            {
                this.Logger.Warn(item);
                if (!this.UsePresenter) continue;
                WarnMessageViewPresenterProxy.Instance?.AddMessage(item);
            }
        }

        public void Error(params Exception[] messages)
        {
            foreach (var item in messages)
            {
                this.Logger.Error(item);
                if (!this.UsePresenter) continue;
                ErrorMessageViewPresenterProxy.Instance?.AddMessage(item);

            }
        }

        public void Info(params string[] messages)
        {
            foreach (var item in messages)
            {
                this.Logger.Info(item);
                if (!this.UsePresenter) continue;
                InfoMessageViewPresenterProxy.Instance?.AddMessage(item);

            }
        }
    }
}
