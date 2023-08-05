using HeBianGu.App.Touch.View.Share;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.App.Touch
{
    [ViewModel("Loyout")]
    internal class LoyoutViewModel : MvcViewModelBase
    {
        private LinkActionEntity _selectLink;
        /// <summary> 说明  </summary>
        public new LinkActionEntity SelectLink
        {
            get { return _selectLink; }
            set
            {
                //  Message：停止上一次任务
                Stop();
                _selectLink = value;
                RaisePropertyChanged("SelectLink");
            }
        }

        private IAssemblyDomain _domain = null;

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
            return LinkActions.LastOrDefault() == SelectLink;
        }

        protected override void Init()
        {
            LinkActions.Add(new BmiLinkActionEntity());
            LinkActions.Add(new FatLinkActionEntity());
            LinkActions.Add(new OxygenLinkActionEntity());
            LinkActions.Add(new PressureLinkActionEntity());
            LinkActions.Add(new TemperatureLinkActionEntity() { VisbleNext = Visibility.Collapsed });
        }

        protected override void Loaded(object args)
        {
            Clear();
        }


        public void Clear()
        {
            SelectLink = null;
            SelectLink = LinkActions.FirstOrDefault();
            LinkActions.Foreach(l => l.Clear());
        }

        public void Stop()
        {
            RunNextEngine = false;
            RunSumitEngine = false;
            RunReMeasureEngine = false;
            SelectLink?.Stop();
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
            SelectLink.Stop();

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                LinkActionEntity find = LinkActions.FirstOrDefault(l => l.DisplayName == SelectLink.DisplayName);

                int index = LinkActions.IndexOf(find);

                if (index < LinkActions.Count - 1)
                {
                    find.Success = true;

                    SelectLink = LinkActions[index + 1];
                }
                else
                {
                    MessageProxy.Snacker.Show("已是最后一项");
                }

                //this.RunNextEngine = false;

                RunReMeasureEngine = false;
            });
        }


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);
        /// <summary> 命令通用方法 </summary>
        protected async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.Click.Sumit")
            {
                Stop();

                //  Do ：输入身份证号
                bool result = await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
                {
                    CardIDControl card = new CardIDControl();

                    return await Messager.Instance.ShowDialog<bool>(card);
                });

                _domain.GoToLinkAction("Report", "Report", new object[] { LinkActions, true });
            }

            //  Do：等待消息
            else if (command == "Button.Click.Next")
            {
                GoNext();
            }
            //  Do：等待消息
            else if (command == "Button.Click.ReMeasure")
            {
                Stop();
                SelectLink.Clear();

                await CheckAndRunMeasure(SelectLink);
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
                object value = await current.Start();

                if (current.IsCancel) return false;

                //  Do ：没有取消操作
                if (value != null)
                {
                    if (IsLast())
                    {
                        RunSumitEngine = true;
                    }
                    else
                    {
                        RunNextEngine = true;
                    }

                    return true;
                }
                else
                {
                    RunReMeasureEngine = true;
                }
            }
            else
            {
                if (current.IsCancel) return false;

                RunReMeasureEngine = true;
            }

            return result;
        }
    }

    internal interface IMeasure
    {
        Task<bool> BeginInit();

        Task<object> Start();

    }







}
