using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;

namespace HeBianGu.App.Menu
{
    [ViewModel("Fluid")]
    internal class FluidViewModel : MvcViewModelBase
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

        protected override void Init()
        {

        }

        protected override void Loaded(string args)
        {

        }


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);

        /// <summary> 命令通用方法 </summary>
        protected async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：应用
            if (command == "Add")
            {
                Collection.Insert(0, new TestViewModel() { Value = (Collection.Count + 1).ToString() });

            }
            //  Do：取消
            else if (command == "Delete")
            {

                Collection.RemoveAt(0);

            }
            else if (command == "Order")
            {
                Collection.Random();
            }
        }

    }
}
