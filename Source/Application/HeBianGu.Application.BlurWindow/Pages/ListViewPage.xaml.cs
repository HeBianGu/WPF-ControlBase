using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfControlDemo.View
{
    /// <summary>
    /// ListViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class ListViewPage : Page
    {
        public ListViewPage()
        {
            InitializeComponent();

            this.Loaded += ListViewPage_Loaded;
        }

        private void ListViewPage_Loaded(object sender, RoutedEventArgs e)
        {
            var len = 300;
            List<Device> ds = new List<Device>();
            for (int i = 0; i < len; i++)
            {
                var d1 = new Device()
                {
                    Name = "MX33333333333333333333333331_" + i,
                    Manufacturer = "Meizu--" + i,
                    Mode = "M303",
                    OSType = EnumOSType.Android,
                    State = EnumDeviceState.Online,
                    Version = "4,2,1",
                    SerialNumber = "0123456789",
                    IsRoot = true,
                };
                ds.Add(d1);
            }
            this.gridList.ItemsSource = ds;
            //this.gridList0.ItemsSource = ds;
            //this.gridList2.ItemsSource = ds;
        }
    }

    public class Device
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public EnumOSType OSType { get; set; }
        public EnumDeviceState State { get; set; }
        public string StateDesc { get { return this.State.ToString(); } }
        public string Version { get; set; }
        public bool IsRoot { get; set; }
        public string IsRootDesc { get { return this.IsRoot ? "已Root" : "未Root"; } }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public enum EnumOSType
    {
        [Description("IOS")]
        IOS = 1,
        [Description("Android")]
        Android = 2,
    }

    public enum EnumDeviceState
    {
        [Description("在线")]
        Online = 1,
        [Description("离线")]
        Offline = 2,
        [Description("正忙")]
        Busy = 3,
    }
}
