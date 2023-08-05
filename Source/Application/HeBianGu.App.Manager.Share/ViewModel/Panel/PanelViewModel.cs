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
                Collection.Add(new TestViewModel() { Value = i.ToString() });
            }

        }

        protected override void Loaded(string args)
        {
        }


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);
        protected  void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.Click.Add")
            {
                Collection.Add(new TestViewModel() { Value = (Collection.Count + 1).ToString() });
            }

            else if (command == "Button.Click.Delete")
            {
                Collection.RemoveAt(0);
            }

            //  Do：等待消息
            else if (command == "Button.Click.Previous")
            {
                TestViewModel find = Collection.FirstOrDefault();

                if (find == null) return;

                Collection.Add(find);

                Collection.Remove(find);

            }

            else if (command == "Button.Click.Next")
            {
                TestViewModel find = Collection.LastOrDefault();

                if (find == null) return;


                Collection.Insert(0, find);

                Collection.Remove(find);

            }
        }


    }
}
