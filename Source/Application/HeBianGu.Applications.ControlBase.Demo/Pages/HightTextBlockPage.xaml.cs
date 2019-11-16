//using CsvHelper;
//using CsvHelper.Configuration;
//using HeBianGu.General.WpfControlLib;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace WpfControlDemo.View
//{
//    /// <summary>
//    /// HightTextBlockPage.xaml 的交互逻辑
//    /// </summary>
//    public partial class HightTextBlockPage : Page
//    {
//        public HightTextBlockPage()
//        {
//            InitializeComponent();

//            this.Loaded += Window_Loaded;
//        }

//        private IEnumerable<LinuxInfo> _listData;
//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {
//            TextReader tr = new StreamReader("linux.csv", Encoding.UTF8);
//            CsvReader csv = new CsvReader(tr);
//            csv.Configuration.RegisterClassMap<LinuxMap>();
//            csv.Configuration.HasHeaderRecord = false;
//            _listData = csv.GetRecords<LinuxInfo>().ToList();
//            lvContent.ItemsSource = _listData;
//        }

//        private void btnSearch_Click(object sender, RoutedEventArgs e)
//        {
//            HighlightContent.Mode = radioFullMatch.IsChecked == true
//                                        ? HighlightContentMode.FullMatch
//                                        : HighlightContentMode.AnyKey;
//            HighlightContent.ToHighlight = tbKey.Text;
//            IEnumerable<LinuxInfo> todisp;
//            if (HighlightContent.Mode == HighlightContentMode.FullMatch)
//            {
//                if (!string.IsNullOrEmpty(HighlightContent.ToHighlight))
//                {
//                    todisp = _listData.Where(r => (r.Desktop + "\0" + r.FileSystem + "\0" + r.OsName).IndexOf(HighlightContent.ToHighlight, StringComparison.OrdinalIgnoreCase) != -1);
//                }
//                else
//                {
//                    todisp = _listData;
//                }
//            }
//            else
//            {
//                string[] keys = HighlightContent.ToHighlight.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
//                if (keys.Length > 0)
//                {
//                    todisp = _listData.Where(r => keys.Any(key => (r.Desktop + "\0" + r.FileSystem + "\0" + r.OsName).IndexOf(key, StringComparison.OrdinalIgnoreCase) != -1));
//                }
//                else
//                {
//                    todisp = _listData;
//                }
//            }
//            lvContent.ItemsSource = todisp;
//        }
//    }

//    public class LinuxInfo
//    {
//        public string OsName { get; set; }
//        public string FileSystem { get; set; }
//        public string Desktop { get; set; }
//    }

//    public class LinuxMap : CsvClassMap<LinuxInfo>
//    {
//        public override void CreateMap()
//        {
//            Map(m => m.OsName).Index(0);
//            Map(m => m.FileSystem).Index(1);
//            Map(m => m.Desktop).Index(2);
//        }
//    }
//}
