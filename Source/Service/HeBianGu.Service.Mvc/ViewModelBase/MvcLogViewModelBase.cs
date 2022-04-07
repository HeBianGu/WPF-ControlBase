// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace HeBianGu.Service.Mvc
{
    /// <summary> 带有日志集合的MvcViewMode基类 </summary>
    public class MvcLogViewModelBase : MvcViewModelBase, IMvcLog
    {
        private ObservableCollection<Log> _logs = new ObservableCollection<Log>();
        /// <summary> 所有日志  </summary>
        public ObservableCollection<Log> Logs
        {
            get { return _logs; }
            set
            {
                _logs = value;
                RaisePropertyChanged("Logs");
            }
        }


        private ObservableCollection<Log> _runlogs = new ObservableCollection<Log>();
        /// <summary> 运行日志  </summary>
        public ObservableCollection<Log> RunLogs
        {
            get { return _runlogs; }
            set
            {
                _runlogs = value;
                RaisePropertyChanged("RunLogs");
            }
        }

        private ObservableCollection<Log> _errorlogs = new ObservableCollection<Log>();
        /// <summary> 错误日志  </summary>
        public ObservableCollection<Log> ErrorLogs
        {
            get { return _errorlogs; }
            set
            {
                _errorlogs = value;
                RaisePropertyChanged("ErrorLogs");
            }
        }

        private ObservableCollection<Log> _outputlogs = new ObservableCollection<Log>();
        /// <summary> 输出日志  </summary>
        public ObservableCollection<Log> OutPutLogs
        {
            get { return _outputlogs; }
            set
            {
                _outputlogs = value;
                RaisePropertyChanged("OutPutLogs");
            }
        }

        /// <summary> 写运行日志 </summary>
        public void RunLog(string title, string message)
        {
            Log log = new Log() { Flag = "\xe76c", Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Title = title, Message = message };

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.RunLogs.Add(log);
                this.Logs.Add(log);
            });
        }

        /// <summary> 写输出日志 </summary>
        public void OutPutLog(string title, string message)
        {
            Log log = new Log() { Flag = "\xe76c", Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Title = title, Message = message };

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.OutPutLogs.Add(log);
                this.Logs.Add(log);
            });
        }

        /// <summary> 写错误日志 </summary>
        public void ErrorLog(string title, string message)
        {
            Log log = new Log() { Flag = "\xe701", Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Title = title, Message = message };

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ErrorLogs.Add(log);
                this.Logs.Add(log);
            });

        }
    }
}
