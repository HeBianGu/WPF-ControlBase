using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Application.DiskWindow
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    {
        //private DataFileViewModel _dataFile=new DataFileViewModel();
        ///// <summary> 说明  </summary>
        //public DataFileViewModel DataFile
        //{
        //    get { return _dataFile; }
        //    set
        //    {
        //        _dataFile = value;
        //        RaisePropertyChanged("DataFile");
        //    }
        //}


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
            this.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            this.NearPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            this.SharePath= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }


        protected override void Loaded(string args)
        {

        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Sumit")
            {

            }

            //  Do：等待消息
            else if (command == "Cancel")
            {

            }
        }

    }

    class DataFileViewModel: ObservableSourceViewModel<TestViewModel>
    {
        protected override void Init()
        {
            base.Init();


           
        }
    }
}
