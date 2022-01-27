using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.App.Manager
{
    [ViewModel("Panel")]
    internal class PanelViewModel : MvcViewModelBase
    {

        private ObservableCollection<TestViewModel> _collection = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private TestViewModel _selectedItem;
        /// <summary> 说明  </summary>
        public TestViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }


        protected override void Init()
        {
            for (int i = 0; i < 10; i++)
            {
                this.Collection.Add(new TestViewModel() { Value = i });
            }

        }

        protected override void Loaded(string args)
        {
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.Click.Add")
            {
                this.Collection.Add(new TestViewModel() { Value = (this.Collection.Count + 1) });
            }

            else if (command == "Button.Click.Delete")
            {
                this.Collection.RemoveAt(0);
            }

            //  Do：等待消息
            else if (command == "Button.Click.Previous")
            {
                var find = this.Collection.FirstOrDefault();

                if (find == null) return;

                this.Collection.Add(find);

                this.Collection.Remove(find);

            }

            else if (command == "Button.Click.Next")
            {
                var find = this.Collection.LastOrDefault();

                if (find == null) return;


                this.Collection.Insert(0, find);

                this.Collection.Remove(find);

            }
        }

    }
}
