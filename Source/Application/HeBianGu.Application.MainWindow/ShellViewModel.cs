using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.MainWindow
{
    /// <summary> 说明</summary>
    internal class ShellViewModel : NotifyPropertyChanged
    {
        #region - 属性 -

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



        private CustomNameViewModel _customNameViewModel=new CustomNameViewModel();
        /// <summary> 说明  </summary>
        public CustomNameViewModel CustomNameViewModel
        {
            get { return _customNameViewModel; }
            set
            {
                _customNameViewModel = value;
                RaisePropertyChanged("CustomNameViewModel");
            }
        }




        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void Init()
        {
            base.Init();

        }

        Random random = new Random();

        protected override void RelayMethod(object obj)
        {
            string command = obj?.ToString();

            //  Do：应用
            if (command == "Add")
            {
                this.Collection.Add(new TestViewModel() { Value = (this.Collection.Count + 1).ToString() });

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion
    }

     /// <summary> 说明</summary>
    internal class CustomNameViewModel : NotifyPropertyChanged
    {
        #region - 属性 -
        private MyEnum _selectedMyEnum;
        /// <summary> 说明  </summary>
        public MyEnum SelectedMyEnum
        {
            get { return _selectedMyEnum; }
            set
            {
                _selectedMyEnum = value;
                RaisePropertyChanged("SelectedMyEnum");
            }
        }
        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

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

        #endregion
    }

}
