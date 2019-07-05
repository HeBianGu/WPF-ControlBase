using HeBianGu.Applications.ControlBase.LinkWindow.ViewModel;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    class ShellViewModel : WindowLinkViewModel
    {

        public static ShellViewModel Instance = new ShellViewModel();


        private GridViewModel _gridViewModel = new GridViewModel();
        /// <summary> 说明  </summary>
        public GridViewModel GridViewModel
        {
            get { return _gridViewModel; }
            set
            {
                _gridViewModel = value;
                RaisePropertyChanged("GridViewModel");
            }
        }


        private Link _currentLink;
        /// <summary> 说明  </summary>
        public Link CurrentLink
        {
            get { return _currentLink; }
            set
            {
                _currentLink = value;
                RaisePropertyChanged("CurrentLink");
            }
        } 

        private SettingViewModel _settingViewModel = new SettingViewModel();
        /// <summary> 说明  </summary>
        public SettingViewModel SettingViewModel
        {
            get { return _settingViewModel; }
            set
            {
                _settingViewModel = value;
                RaisePropertyChanged("SettingViewModel");
            }
        }

        private TreeListViewModel _treeListViewModel = new TreeListViewModel();
        /// <summary> 说明  </summary>
        public TreeListViewModel TreeListViewModel
        {
            get { return _treeListViewModel; }
            set
            {
                _treeListViewModel = value;
                RaisePropertyChanged("TreeListViewModel");
            }
        }


        Random random = new Random();

        protected async override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {
                this.IsBuzy = true;

                await Task.Run(() =>
                {
                    Thread.Sleep(500);

                    string err;

                    var result = AssemblyDomain.Instance.Login(this.LoginUseName, this.LoginPassWord, this.Remenber, out err);

                    if (result)
                    {
                        this.LoginMessage = "登录成功";
                        this.IsBuzy = false;

                        Task.Delay(500).ContinueWith(l =>
                        {
                            this.IsLogined = true;

                        });
                    }
                    else
                    {
                        this.IsBuzy = false;
                        this.IsEnbled = false;

                        //this.LoginMessage = "网络错误，登录失败";
                        this.LoginMessage = err;

                        Task.Delay(1000).ContinueWith(l =>
                        {
                            this.LoginMessage = "登录";
                            this.IsEnbled = true;
                        });
                    }
                    //});

                });

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
            //  Do：取消
            else if (command == "Loaded")
            {
                ////  Do：加载数据库配置
                await Task.Delay(100).ContinueWith(l =>
                 {
                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         this.GridViewModel.RelayCommand.Execute("SearchHealthData");
                     });
                 });

            }
            //  Do：取消
            else if (command == "init")
            {
                //  Do：加载记住账号和密码
                var result = AssemblyDomain.Instance.GetAccountConfig();

                if (result != null)
                {
                    if (result.Remenber)
                    {
                        this.LoginPassWord = result.LoginPassWord;
                        this.Remenber = true;
                    }
                    this.LoginUseName = result.LoginUseName;

                }
            }
            //  Do：取消
            else if (command == "SetCancel")
            {
                bool result = false;

                await MessageService.ShowWaittingMessge(() =>
                {
                    result = this.CheckSetting();
                });

                if (!result) return;

                MessageService.ShowSnackMessageWithNotice("已取消");

                MessageService.CloseWithLayer();


            }
            //  Do：取消
            else if (command == "SetSumit")
            {
                string err = string.Empty;

                bool result = false;

                if (!this.SettingViewModel.IsaVailable())
                {
                    MessageService.ShowSnackMessageWithNotice("请输入所有必填项目，请检查！");
                    return;
                }

                await MessageService.ShowWaittingMessge(() =>
                {
                    result = AssemblyDomain.Instance.SaveSetting(this.SettingViewModel, out err);
                });

                if (!result)
                {
                    await MessageService.ShowSumitMessge("保存错误！" + err);
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("保存成功！");

                    MessageService.CloseWithLayer();
                }
            }

        }


        bool CheckSetting()
        {
            Uri uri = new Uri(@"/HeBianGu.Applications.ControlBase.LinkWindow;component/SettingControl.xaml", UriKind.RelativeOrAbsolute);

            //  Do：加载数据库配置
            string err;

            var setting = AssemblyDomain.Instance.GetSetting(out err);

            if (setting == null)
            {
                //  Do：显示配置页面
                MessageService.ShowWithLayer(uri);

                MessageService.ShowSnackMessageWithNotice("未检测到配置文件，请检查！");

                return false;
            }

            if (!setting.IsaVailable())
            {
                //  Do：显示配置页面
                MessageService.ShowWithLayer(uri);

                MessageService.ShowSnackMessageWithNotice("请输入所有必填项目，请检查！");

                return false;
            }

            this.SettingViewModel = setting;

            return true;
        }
    }

    class SettingViewModel : NotifyPropertyChanged
    {
        string _ip;
        /// <summary> 数据库ip地址 </summary>
        public string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
                RaisePropertyChanged();
            }
        }

        string _port;
        /// <summary> 数据库端口号 </summary>
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                RaisePropertyChanged();
            }
        }

        string _name;
        /// <summary> 数据库名称 </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        string _user;
        /// <summary> 数据库登录名称 </summary>
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        string _psd;
        /// <summary> 数据库登录密码 </summary>
        public string PSD
        {
            get
            {
                return _psd;
            }
            set
            {
                _psd = value;
                RaisePropertyChanged();
            }
        }

        internal bool IsaVailable()
        {
            return !this.GetType().GetProperties().ToList().Any(l => l.GetValue(this) == null || l.GetValue(this).ToString() == string.Empty);
        }
    }

}
