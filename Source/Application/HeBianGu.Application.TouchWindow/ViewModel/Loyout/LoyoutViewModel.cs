using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using HeBianGu.Application.TouchWindow.View.Share;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;


namespace HeBianGu.Application.TouchWindow
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    {
        private LinkActionEntity _selectLink;
        /// <summary> 说明  </summary>
        public new LinkActionEntity SelectLink
        {
            get { return _selectLink; }
            set
            {
                //  Message：停止上一次任务
                this.Stop();

                _selectLink = value;
                RaisePropertyChanged("SelectLink");
            }
        }

        IAssemblyDomain _domain = null;

        public LoyoutViewModel(IAssemblyDomain domain)
        {
            _domain = domain;
        }

        private ObservableCollection<LinkActionEntity> _linkActions = new ObservableCollection<LinkActionEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<LinkActionEntity> LinkActions
        {
            get { return _linkActions; }
            set
            {
                _linkActions = value;
                RaisePropertyChanged("LinkActions");
            }
        }

        /// <summary> 是否是最后一项 </summary>
        public bool IsLast()
        {
            return this.LinkActions.LastOrDefault() == this.SelectLink;
        }

        protected override void Init()
        {
            LinkActions.Add(new BmiLinkActionEntity());
            LinkActions.Add(new FatLinkActionEntity());
            LinkActions.Add(new OxygenLinkActionEntity());
            LinkActions.Add(new PressureLinkActionEntity());
            LinkActions.Add(new TemperatureLinkActionEntity() { VisbleNext = Visibility.Collapsed });
        }

        protected override void Loaded(string args)
        {
            this.Clear();
        }


        public void Clear()
        {
            this.SelectLink = null;

            this.SelectLink = LinkActions.FirstOrDefault();

            this.LinkActions.Foreach(l => l.Clear());
        }

        public void Stop()
        {
            this.RunNextEngine = false;
            this.RunSumitEngine = false;
            this.RunReMeasureEngine = false;

            this.SelectLink?.Stop();
        }

        private bool _runNextEngine;
        /// <summary> 启动下一项倒计时  </summary>
        public bool RunNextEngine
        {
            get { return _runNextEngine; }
            set
            {
                _runNextEngine = value;
                RaisePropertyChanged("RunNextEngine");
            }
        }


        private bool _runSumitEngine;
        /// <summary> 启动提交倒计时  </summary>
        public bool RunSumitEngine
        {
            get { return _runSumitEngine; }
            set
            {
                _runSumitEngine = value;
                RaisePropertyChanged("RunSumitEngine");
            }
        }


        private bool _runReMeasureEngine;
        /// <summary> 启动重新测量倒计时  </summary>
        public bool RunReMeasureEngine
        {
            get { return _runReMeasureEngine; }
            set
            {
                _runReMeasureEngine = value;
                RaisePropertyChanged("RunReMeasureEngine");
            }
        }

        /// <summary> 下一项 </summary>
        public void GoNext()
        {
            this.SelectLink.Stop();

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var find = LinkActions.FirstOrDefault(l => l.DisplayName == this.SelectLink.DisplayName);

                var index = LinkActions.IndexOf(find);

                if (index < LinkActions.Count - 1)
                {
                    find.Success = true;

                    this.SelectLink = LinkActions[index + 1];
                }
                else
                {
                    MessageService.ShowSnackMessage("已是最后一项");
                }

                //this.RunNextEngine = false;

                this.RunReMeasureEngine = false;
            });
        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.Click.Sumit")
            {
                this.Stop();

                //  Do ：输入身份证号
                var result = await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
                {
                    CardIDControl card = new CardIDControl();

                    return await MessageService.ShowCustomDialog<bool>(card);
                });

                _domain.GoToLinkAction("Report", "Report", new object[] { this.LinkActions, true });
            }

            //  Do：等待消息
            else if (command == "Button.Click.Next")
            {
                GoNext();
            }
            //  Do：等待消息
            else if (command == "Button.Click.ReMeasure")
            { 
                this.Stop();

                this.SelectLink.Clear();

                await CheckAndRunMeasure(this.SelectLink);
            }

            //if (obj is SelectionChangedEventArgs args)
            //{
            //    if (this.SelectLink == null) return;

            //    if (args.RemovedItems.Count > 0 && args.RemovedItems[0] is LinkActionEntity old)
            //    {
            //        old.Stop();
            //    }

            //    //await CheckAndRunMeasure(this.SelectLink);
            //}
        }

        /// <summary> 检测当前体检项目初始化 获取数据 取消等操作 </summary>
        public async Task<bool> CheckAndRunMeasure(LinkActionEntity current)
        {
            bool result = false;

            //  Message：初始化设备
            if (await current.BeginInit())
            {
                if (current.IsCancel) return false;

                //  Message：开始测量
                var value = await current.Start();

                if (current.IsCancel) return false;

                //  Do ：没有取消操作
                if (value != null)
                {
                    if (this.IsLast())
                    {
                        this.RunSumitEngine = true;
                    }
                    else
                    {
                        this.RunNextEngine = true;
                    }

                    return true;
                }
                else
                {
                    this.RunReMeasureEngine = true;
                }
            }
            else
            {
                if (current.IsCancel) return false;

                this.RunReMeasureEngine = true;
            }

            return result;
        }
    }

    interface IMeasure
    {
        Task<bool> BeginInit();

        Task<object> Start();

    }







}
