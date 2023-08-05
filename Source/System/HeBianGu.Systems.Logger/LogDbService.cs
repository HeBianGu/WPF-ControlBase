
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Logger
{
    public interface ILogDbServiceOption
    {

    }
    [Displayer(Name = "日志记录", GroupName = SystemSetting.GroupMessage, Icon = IconAll.QuesitionFill, Description = "点击显示新手向导")]
    public class LogDbService : ServiceSettingInstance<LogDbService, ILogService>, ILogService, ILogDbServiceOption
    {
        //readonly IDebugRespository _debug;
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

        [XmlIgnore]
        [Browsable(false)]
        protected IStringRepository<hl_dm_debug> DebugRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_debug>>();
        [XmlIgnore]
        [Browsable(false)]
        protected IStringRepository<hl_dm_error> ErrorRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_error>>();
        [XmlIgnore]
        [Browsable(false)]
        protected IStringRepository<hl_dm_info> InfoRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_info>>();
        [XmlIgnore]
        [Browsable(false)]
        protected IStringRepository<hl_dm_fatal> FatalRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_fatal>>();
        [XmlIgnore]
        [Browsable(false)]
        protected IStringRepository<hl_dm_warn> WarnRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_warn>>();

        public void Debug(params string[] messages)
        {
            foreach (var message in messages)
            {
                hl_dm_debug model = new hl_dm_debug();
                model.Title = message;
                model.Message = message;
                model.Value = message;
                this.DebugRepository?.Insert(model);
                if (!this.UsePresenter) continue;
                DebugMessageViewPresenterProxy.Instance?.AddMessage(message);
            }
        }

        public void Error(params Exception[] messages)
        {
            foreach (var message in messages)
            {
                hl_dm_error model = new hl_dm_error();
                model.Message = message?.Message;
                model.Exception = message?.ToString();
                StackTrace trace = new StackTrace();
                model.Stack = trace.ToString();
                this.ErrorRepository?.Insert(model);
                if (!this.UsePresenter) continue;
                ErrorMessageViewPresenterProxy.Instance?.AddMessage(message, message.Message);
            }
        }

        public void Error(params string[] messages)
        {
            foreach (var message in messages)
            {
                hl_dm_error model = new hl_dm_error();
                model.Message = message;
                model.Exception = message;
                StackTrace trace = new StackTrace();
                model.Stack = trace.ToString();
                this.ErrorRepository?.Insert(model);
                if (!this.UsePresenter) continue;
                ErrorMessageViewPresenterProxy.Instance?.AddMessage(message);
            }
        }


        public void Fatal(params string[] messages)
        {
            foreach (var message in messages)
            {
                hl_dm_fatal model = new hl_dm_fatal();
                model.Message = message;
                model.Exception = message;
                StackTrace trace = new StackTrace();
                model.Stack = trace.ToString();
                var process = Process.GetCurrentProcess();
                var ps = process.GetType().GetProperties();
                StringBuilder sb = new StringBuilder();

                foreach (var p in ps)
                {
                    if (!p.CanRead) continue;
                    var ba = p.GetCustomAttribute<BrowsableAttribute>();
                    if (ba != null && ba.Browsable == false) continue;
                    sb.AppendLine($"{p.Name} : {p.GetValue(process)}");
                }
                model.Process = sb.ToString();
                this.FatalRepository?.Insert(model);
                if (!this.UsePresenter) continue;
                FatalMessageViewPresenterProxy.Instance?.AddMessage(message);
            }
        }

        public void Fatal(params Exception[] messages)
        {
            foreach (var message in messages)
            {
                hl_dm_fatal model = new hl_dm_fatal();
                model.Message = message?.Message;
                model.Exception = message?.ToString();
                StackTrace trace = new StackTrace();
                model.Stack = trace.ToString();
                var process = Process.GetCurrentProcess();
                var ps = process.GetType().GetProperties();
                StringBuilder sb = new StringBuilder();
                foreach (var p in ps)
                {
                    if (!p.CanRead) continue;
                    var ba = p.GetCustomAttribute<BrowsableAttribute>();
                    if (ba != null && ba.Browsable == false) continue;
                    sb.AppendLine($"{p.Name} : {p.GetValue(process)}");
                }
                model.Process = sb.ToString();
                this.FatalRepository?.Insert(model);
                if (!this.UsePresenter) continue;
                FatalMessageViewPresenterProxy.Instance?.AddMessage(message.Message);
            }
        }

        public void Info(params string[] messages)
        {
            foreach (var message in messages)
            {
                hl_dm_info model = new hl_dm_info();
                model.Title = MethodInfo.GetCurrentMethod().Name;
                model.Message = message;
                this.InfoRepository?.Insert(model);
                if (!this.UsePresenter) continue;
                InfoMessageViewPresenterProxy.Instance?.AddMessage(message);
            }
        }

        public void Trace(params string[] messages)
        {
            this.Debug(messages);
        }


        public void Warn(params string[] messages)
        {
            foreach (var message in messages)
            {
                hl_dm_warn model = new hl_dm_warn();
                model.Title = MethodInfo.GetCurrentMethod().Name;
                model.Message = message;
                this.WarnRepository?.Insert(model);
                if (!this.UsePresenter) continue;
                WarnMessageViewPresenterProxy.Instance?.AddMessage(message);
            }
        }
    }
}
