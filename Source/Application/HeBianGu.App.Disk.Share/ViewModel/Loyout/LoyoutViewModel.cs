using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.App.Disk
{
    [ViewModel("Loyout")]
    internal class LoyoutViewModel : MvcViewModelBase
    {
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


        private string _nearPath;
        /// <summary> 说明  </summary>
        public string NearPath
        {
            get { return _nearPath; }
            set
            {
                _nearPath = value;
                RaisePropertyChanged("NearPath");
            }
        }


        private string _sharePath;
        /// <summary> 说明  </summary>
        public string SharePath
        {
            get { return _sharePath; }
            set
            {
                _sharePath = value;
                RaisePropertyChanged("SharePath");
            }
        }



        protected override void Init()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            NearPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            SharePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            LinkActions.Add(new LinkAction() { Action = "Near", Controller = "Loyout", DisplayName = "最近使用", Logo = "\xe6f3" });
            LinkActions.Add(new LinkAction() { Action = "Explorer", Controller = "Loyout", DisplayName = "全部文件", Logo = "" });
            LinkActions.Add(new LinkAction() { Action = "Image", Controller = "Loyout", DisplayName = "    图片", Logo = "" });
            LinkActions.Add(new LinkAction() { Action = "Video", Controller = "Loyout", DisplayName = "    视频", Logo = "" });
            LinkActions.Add(new LinkAction() { Action = "Document", Controller = "Loyout", DisplayName = "    文档", Logo = "" });
            LinkActions.Add(new LinkAction() { Action = "Music", Controller = "Loyout", DisplayName = "    音乐", Logo = "" });
            LinkActions.Add(new LinkAction() { Action = "Explorer", Controller = "Loyout", DisplayName = "    种子", Logo = "" });
            LinkActions.Add(new LinkAction() { Action = "Recent", Controller = "Loyout", DisplayName = "    其他", Logo = "" });
            LinkActions.Add(new LinkAction() { Action = "Space", Controller = "Loyout", DisplayName = "隐藏空间", Logo = "\xe613" });
            LinkActions.Add(new LinkAction() { Action = "Share", Controller = "Loyout", DisplayName = "我的分享", Logo = "\xe764" });
            LinkActions.Add(new LinkAction() { Action = "Near", Controller = "Loyout", DisplayName = "回收站", Logo = "\xe618" });

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                      {
                          SelectLink = LinkActions[1];
                      }));

        }


        protected override void Loaded(string args)
        {

        }

    }

    internal class DataFileViewModel : ObservableSourceViewModel<TestViewModel>
    {
        protected override void Init()
        {
            base.Init();



        }
    }
}
