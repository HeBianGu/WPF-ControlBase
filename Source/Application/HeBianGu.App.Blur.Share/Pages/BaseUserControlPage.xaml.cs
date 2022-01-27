using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.Windows.Controls;

namespace WpfControlDemo.View
{
    /// <summary>
    /// BaseUserControlPage.xaml 的交互逻辑
    /// </summary>
    public partial class BaseUserControlPage : Page
    {
        public BaseUserControlPage()
        {
            InitializeComponent();

            this.DataContext = this;

            RelayCommand = new RelayCommand(RelayMethod);
        }

    }




    partial class BaseUserControlPage
    {
        private bool _isShow = true;
        /// <summary> 说明  </summary>
        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                _isShow = value;
                RaisePropertyChanged("IsShow");
            }
        }


        public void RelayMethod(object obj)
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

    partial class BaseUserControlPage : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

}
