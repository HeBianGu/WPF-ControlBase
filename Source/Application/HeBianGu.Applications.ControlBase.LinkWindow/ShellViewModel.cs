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


        private UpLoadViewModel _upLoadViewModel = new UpLoadViewModel();
        /// <summary> 说明  </summary>
        public UpLoadViewModel UpLoadViewModel
        {
            get { return _upLoadViewModel; }
            set
            {
                _upLoadViewModel = value;
                RaisePropertyChanged("UpLoadViewModel");
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

        Link setlink;
        protected async override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {
                this.IsBuzy = true;

                await Task.Run(() =>
                {
                    Thread.Sleep(2000);

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
                //this.CheckSetting();

                await Task.Delay(1000).ContinueWith(l =>
                 {
                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         this.RelayMethod("SearchHealthData");
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

                //  Do：初始化开始和结束日期
                this.UpLoadViewModel.StartSelectedDate = DateTime.Now.AddYears(-1);
                this.UpLoadViewModel.EndSelectedDate = DateTime.Now;

            }
            //  Do：取消
            else if (command == "SearchHealthData")
            {

                this.UpLoadViewModel.CurrentIndex = "1";

                await this.RefreshPage();

                MessageService.ShowSnackMessageWithNotice($"查询完成，共计{this.UpLoadViewModel.Total}条");

            }
            //  Do：取消
            else if (command == "UploadFirstPage")
            {

                if (this.UpLoadViewModel.CurrentIndex == "1")
                {
                    MessageService.ShowSnackMessageWithNotice("已经是第一页");
                    return;
                }

                this.UpLoadViewModel.CurrentIndex = "1";

                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "UploadPreviousPage")
            {

                if (this.UpLoadViewModel.CurrentIndex == "1")
                {
                    MessageService.ShowSnackMessageWithNotice("已经是第一页");
                    return;
                }

                this.UpLoadViewModel.CurrentIndex = (int.Parse(this.UpLoadViewModel.CurrentIndex) - 1).ToString();


                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "UploadNextPage")
            {

                if (this.UpLoadViewModel.CurrentIndex == this.UpLoadViewModel.PageCount)
                {
                    MessageService.ShowSnackMessageWithNotice("已经是最后一页");
                    return;
                }

                this.UpLoadViewModel.CurrentIndex = (int.Parse(this.UpLoadViewModel.CurrentIndex) + 1).ToString();

                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "UploadLastPage")
            {

                if (this.UpLoadViewModel.CurrentIndex == this.UpLoadViewModel.PageCount)
                {
                    MessageService.ShowSnackMessageWithNotice("已经是最后一页");
                    return;
                }

                this.UpLoadViewModel.CurrentIndex = this.UpLoadViewModel.PageCount;

                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "MatchLisData")
            {
                await MessageService.ShowWaittingMessge(() =>
                {
                    Thread.Sleep(2000);
                });

                if (random.Next(2) == 1)
                {
                    await MessageService.ShowSumitMessge("导入Lis数据错误，请检查！");
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("导入Lis数据完成，请选择要提交的数据");
                }
            }

            //  Do：取消
            else if (command == "UpLoadData")
            {
                await MessageService.ShowWaittingMessge(() =>
                {
                    Thread.Sleep(2000);
                });


                if (random.Next(2) == 1)
                {
                    await MessageService.ShowSumitMessge("上传错误，请检查！");
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("上传成功，合计20条");
                }
            }

            //  Do：取消
            else if (command == "SearchUpLoadData")
            {
                await MessageService.ShowWaittingMessge(() =>
                {
                    Thread.Sleep(2000);
                });


                if (random.Next(2) == 1)
                {
                    await MessageService.ShowSumitMessge("查询错误，请检查！");
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("查询完成");
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
            //  Do：取消
            else if (command == "TreeList.Load")
            {
                await MessageService.ShowWaittingMessge(() =>
                {
                    //  Do：加载TreeList数据源
                    string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");

                    string txt = System.IO.File.ReadAllText(url, Encoding.Default);

                    var collecion = JsonConvert.DeserializeObject<List<TreeNodeEntity>>(txt);

                    //  Message：初始化树形控件（只需初始化一遍）
                    this.TreeListViewModel.InitTyeEncodeDevice(collecion);

                    this.TreeListViewModel.LoadTyeEncodeCheckDevice(collecion.Where(l => l.Code.Length == 2).ToList());
                });

                MessageService.ShowSnackMessageWithNotice("加载完成！");
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
        async Task RefreshPage()
        {
            List<UpLoadItem> result = null;

            string err = string.Empty;

            uint total = 0;

            await MessageService.ShowWaittingMessge(() =>
            {
                System.Diagnostics.Debug.WriteLine(this.UpLoadViewModel.StartSelectedDate);

                Thread.Sleep(2000);

                result = AssemblyDomain.Instance.GetGuideList(out err, out total,
                    this.UpLoadViewModel.StartSelectedDate?.ToString("yyyy-MM-dd"),
                    this.UpLoadViewModel.EndSelectedDate?.ToString("yyyy-MM-dd"),
                    this.UpLoadViewModel.SearchName, this.UpLoadViewModel.SelectType == 0 ? 1 : 0,
                    (uint)(int.Parse(this.UpLoadViewModel.CurrentIndex)),
                    17);
            });

            if (result == null)
            {
                await MessageService.ShowSumitMessge("查询错误，请检查！" + err);
            }
            else
            {

                //this.UpLoadViewModel.Collection.Clear();

                //foreach (var item in result)
                //{
                //    UpLoadItem upLoadItem = new UpLoadItem();

                //    upLoadItem.CopyFromObjExtend(item, true);

                //    this.UpLoadViewModel.Collection.Add(upLoadItem);
                //}

                this.UpLoadViewModel.Collection.Clear();

                foreach (var item in result)
                {
                    this.UpLoadViewModel.Collection.Add(item);
                }


                this.UpLoadViewModel.Total = total.ToString();

                this.UpLoadViewModel.PageCount = ((total % 15) == 0 ? total / 15 : total / 15 + 1).ToString();

                this.UpLoadViewModel.CanMathcLisData = true;

            }
        }
    }

    public class UpLoadViewModel : Base.WpfBase.NotifyPropertyChanged
    {

        private string _total = "0";
        /// <summary> 说明  </summary>
        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged("Total");
            }
        }


        private string _pageCount = "0";
        /// <summary> 说明  </summary>
        public string PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                RaisePropertyChanged("PageCount");
            }
        }


        private string _currentIndex = "0";
        /// <summary> 说明  </summary>
        public string CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                RaisePropertyChanged("CurrentIndex");
            }
        }



        private bool _canMatchLisData;
        /// <summary> 说明  </summary>
        public bool CanMathcLisData
        {
            get { return _canMatchLisData; }
            set
            {
                _canMatchLisData = value;
                RaisePropertyChanged("CanMathcLisData");
            }
        }


        private DateTime? _startSelectedDate;
        /// <summary> 说明  </summary>
        public DateTime? StartSelectedDate
        {
            get { return _startSelectedDate; }
            set
            {
                _startSelectedDate = value;
                RaisePropertyChanged("StartSelectedDate");
            }
        }

        private int _selectType = 1;
        /// <summary> 说明  </summary>
        public int SelectType
        {
            get { return _selectType; }
            set
            {
                _selectType = value;
                RaisePropertyChanged("SelectType");
            }
        }


        private DateTime? _endSelectedDate;
        /// <summary> 说明  </summary>
        public DateTime? EndSelectedDate
        {
            get { return _endSelectedDate; }
            set
            {
                _endSelectedDate = value;
                RaisePropertyChanged("EndSelectedDate");
            }
        }


        private string _searchName;
        /// <summary> 说明  </summary>
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                RaisePropertyChanged("SearchName");
            }
        }


        private ObservableCollection<UpLoadItem> _collection = new ObservableCollection<UpLoadItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<UpLoadItem> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

    }


    public class UpLoadItem : Base.WpfBase.NotifyPropertyChanged
    {

        private string _index;
        /// <summary> 说明  </summary>
        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged("Index");
            }
        }


        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }


        private string _path;
        /// <summary> 说明  </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged("Path");
            }
        }



        private string _type;
        /// <summary> 说明  </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged("Type");
            }
        }


        private string _size;
        /// <summary> 说明  </summary>
        public string Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged("Size");
            }
        }


        private string _span;
        /// <summary> 说明  </summary>
        public string Span
        {
            get { return _span; }
            set
            {
                _span = value;
                RaisePropertyChanged("Span");
            }
        }


        private string _time;
        /// <summary> 说明  </summary>
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged("Time");
            }
        }


        private string _tag;
        /// <summary> 说明  </summary>
        public string Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                RaisePropertyChanged("Tag");
            }
        }

    }

    class SettingViewModel : Base.WpfBase.NotifyPropertyChanged
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
