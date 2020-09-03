using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
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
    [ViewModel("Custom")]
    class CustomViewModel : MvcViewModelBase
    {
        private StudentViewModel _student=new StudentViewModel();
        /// <summary> 说明  </summary>
        public StudentViewModel Student
        {
            get { return _student; }
            set
            {
                _student = value;
                RaisePropertyChanged("Student");
            }
        }


        //Shuttles


        private ObservableCollection<ShuttleItem> _shuttles = new ObservableCollection<ShuttleItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ShuttleItem> Shuttles
        {
            get { return _shuttles; }
            set
            {
                _shuttles = value;
                RaisePropertyChanged("Shuttles");
            }
        }


        protected override void Init()
        {

            {
                ShuttleItem shuttle = new ShuttleItem();

                shuttle.Header = "第一组";

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 0; i < 10; i++)
                {
                    source.Add(new TestViewModel() { Value = (i + 1).ToString()});
                }

                shuttle.ItemSource = source;

                this.Shuttles.Add(shuttle);
            }

            {
                ShuttleItem shuttle = new ShuttleItem();

                shuttle.Header = "第二组";

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 0; i < 10; i++)
                {
                    source.Add(new TestViewModel() { Value = (i + 1).ToString() });
                }

                shuttle.ItemSource = source;

                this.Shuttles.Add(shuttle);
            }


        }

        protected override void Loaded(string args)
        {
            this.Student.LoadDefault();

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
}
