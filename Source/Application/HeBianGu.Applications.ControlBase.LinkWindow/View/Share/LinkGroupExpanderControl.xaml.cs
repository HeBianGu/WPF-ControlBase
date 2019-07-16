using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// LinkGroupLeftControl.xaml 的交互逻辑
    /// </summary>
    public partial class LinkGroupExpanderControl : UserControl
    {
        public LinkGroupExpanderControl()
        {
            InitializeComponent();
        }
    }

    class LinkGroupViewModel : NotifyPropertyChanged
    {


        private ObservableCollection<LinkGroup> _collection = new ObservableCollection<LinkGroup>();
        /// <summary> 说明  </summary>
        public ObservableCollection<LinkGroup> Collection
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
            if (command == "init")
            {
                Collection.Add(new LinkGroup() { DisplayName="我的分组一"});
                Collection.Add(new LinkGroup() { DisplayName = "我的分组二" });
            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

    }
}
