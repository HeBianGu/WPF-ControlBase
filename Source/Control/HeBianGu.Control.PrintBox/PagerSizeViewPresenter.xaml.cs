
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Printing;
using System.Windows;
using System.Linq;

namespace HeBianGu.Control.PrintBox
{
    [Displayer(Name = "纸张设置", GroupName = SystemSetting.GroupControl, Icon = "\xe8d7", Description = "纸张设置")]
    public class PagerSizeViewPresenter : ServiceSettingInstance<PagerSizeViewPresenter, IPagerSizeViewPresenter>, IPagerSizeViewPresenter
    {
        public PagerSizeViewPresenter()
        {
            this.Collection = PagerSizeData.KnownSizes.ToObservable();
            this.SelectedPagerSizeData = this.Collection?.FirstOrDefault();
        }
        private PagerSizeData _selectedPagerSizeData;
        /// <summary> 说明  </summary>
        public PagerSizeData SelectedPagerSizeData
        {
            get { return _selectedPagerSizeData; }
            set
            {
                _selectedPagerSizeData = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PagerSizeData> _collection = new ObservableCollection<PagerSizeData>();
        /// <summary> 说明  </summary>
        public ObservableCollection<PagerSizeData> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private int _layoutSelectedIndex;
        /// <summary> 说明  </summary>
        public int LayoutSelectedIndex
        {
            get { return _layoutSelectedIndex; }
            set
            {
                _layoutSelectedIndex = value;
                RaisePropertyChanged();
            }
        }

        public Size GetSize()
        {
            if (this.LayoutSelectedIndex == 0)
                return new Size(this.SelectedPagerSizeData.SizeInInch.Height * 96, this.SelectedPagerSizeData.SizeInInch.Width * 96);
            return new Size(this.SelectedPagerSizeData.SizeInInch.Width * 96, this.SelectedPagerSizeData.SizeInInch.Height * 96);
        }
    }

    public class PagerSizeData
    {
        public PagerSizeData(PageMediaSizeName sizeName, Size size, bool metric, string name)
        {
            Name = name;
            SizeName = sizeName;
            if (metric)
            {
                SizeInInch = new Size(size.Width / 25.4, size.Height / 25.4);
                SizeDesc = size.Width.ToString() + " x " + size.Height.ToString() + " mm";
            }
            else
            {
                SizeInInch = size;
                SizeDesc = size.Width.ToString() + "\" x " + size.Height.ToString() + "\"";
            }

            IsMetric = metric;
        }

        public string Name { get; set; }
        public string SizeDesc { get; set; }
        public PageMediaSizeName SizeName { get; set; }
        public Size SizeInInch { get; set; }
        public bool IsMetric { get; set; }

        private static readonly PagerSizeData[] _knowSizes;
        private static readonly Dictionary<PageMediaSizeName, PagerSizeData> _sizeNameToSize
            = new Dictionary<PageMediaSizeName, PagerSizeData>();

        static PagerSizeData()
        {
            //https://en.wikipedia.org/wiki/Paper_size
            _knowSizes = new PagerSizeData[]
            {
                new PagerSizeData( PageMediaSizeName.NorthAmericaLetter, new Size(8.5, 11), false, "Letter (ANSI A)"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaLegal, new Size(8.5, 14), false, "Legal"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaTabloid, new Size(11, 17), false, "Tabloid (ANSI B)"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaCSheet, new Size(17, 22), false, "ANSI C"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaDSheet, new Size(22, 34), false, "ANSI D"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaESheet, new Size(34, 44), false, "ANSI E"),

                new PagerSizeData( PageMediaSizeName.NorthAmericaArchitectureASheet, new Size(9, 12) , false, "Arch A"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaArchitectureBSheet, new Size(12, 18) , false, "Arch B"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaArchitectureCSheet, new Size(18, 24) , false, "Arch C"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaArchitectureDSheet, new Size(24, 36) , false, "Arch D"),
                new PagerSizeData( (PageMediaSizeName)1001, new Size(30, 42) , false, "Arch E1"),
                new PagerSizeData( PageMediaSizeName.NorthAmericaArchitectureESheet, new Size(36, 48) , false, "Arch E"),

                new PagerSizeData( PageMediaSizeName.ISOA0, new Size(841, 1189), true, "A0"),
                new PagerSizeData( PageMediaSizeName.ISOA1, new Size(594, 841), true, "A1" ),
                new PagerSizeData( PageMediaSizeName.ISOA2, new Size(420, 594), true, "A2" ),
                new PagerSizeData( PageMediaSizeName.ISOA3, new Size(297, 420), true, "A3" ),
                new PagerSizeData( PageMediaSizeName.ISOA4, new Size(210, 297), true, "A4" ),

                new PagerSizeData( PageMediaSizeName.ISOB0, new Size(1000, 1414), true, "B0"),
                new PagerSizeData( PageMediaSizeName.ISOB1, new Size(707, 1000), true, "B1"),
                new PagerSizeData( PageMediaSizeName.ISOB2, new Size(500, 707), true, "B2"),
                new PagerSizeData( PageMediaSizeName.ISOB3, new Size(353, 500), true, "B3"),
                new PagerSizeData( PageMediaSizeName.ISOB4, new Size(250, 353), true, "B4"),
            };

            foreach (PagerSizeData md in _knowSizes)
                _sizeNameToSize[md.SizeName] = md;
        }

        public static PagerSizeData GetKnownSize(PageMediaSizeName sizeName)
        {
            PagerSizeData size;
            _sizeNameToSize.TryGetValue(sizeName, out size);
            return size;
        }

        public static IEnumerable<PagerSizeData> KnownSizes
        {
            get { return _knowSizes; }
        }
    }

    public interface IPagerSizeViewPresenter : IViewPresenter
    {
        ObservableCollection<PagerSizeData> Collection { get; }
        PagerSizeData SelectedPagerSizeData { get; }
    }
}
