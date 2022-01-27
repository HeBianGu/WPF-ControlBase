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

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：应用
            if (command == "Add")
            {
                this.Collection.Insert(0, new TestViewModel() { Value = (this.Collection.Count + 1)});

            }
            //  Do：取消
            else if (command == "Delete")
            {

                this.Collection.RemoveAt(0);

            }
            else if (command == "Order")
            {
                this.Collection.Random();
            }
        }

    }
}
