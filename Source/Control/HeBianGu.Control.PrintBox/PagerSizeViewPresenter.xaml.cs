
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
            this.Collection = PageSize.GetSizes().ToObservable();
            this.SelectedPagerSizeData = this.Collection?.FirstOrDefault();
        }
        private PageSize _selectedPagerSizeData;
        /// <summary> 说明  </summary>
        public PageSize SelectedPagerSizeData
        {
            get { return _selectedPagerSizeData; }
            set
            {
                _selectedPagerSizeData = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PageSize> _collection = new ObservableCollection<PageSize>();
        /// <summary> 说明  </summary>
        public ObservableCollection<PageSize> Collection
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
                return new Size(this.SelectedPagerSizeData.Size.Height * 96, this.SelectedPagerSizeData.Size.Width * 96);
            return new Size(this.SelectedPagerSizeData.Size.Width * 96, this.SelectedPagerSizeData.Size.Height * 96);
        }
    }

    public class PageSize
    {
        public PageSize(PageMediaSizeName sizeName, Size size, string name)
        {
            Name = name;
            SizeName = sizeName;
            Size = size;

        }
        public string Name { get; set; }
        public PageMediaSizeName SizeName { get; set; }
        public Size Size{ get; set; }

        public static IEnumerable<PageSize> GetSizes()
        {
            yield return new PageSize(PageMediaSizeName.ISOA0, new Size(841, 1189), "A0");
            yield return new PageSize(PageMediaSizeName.ISOA1, new Size(594, 841), "A1");
            yield return new PageSize(PageMediaSizeName.ISOA2, new Size(420, 594), "A2");
            yield return new PageSize(PageMediaSizeName.ISOA3, new Size(297, 420), "A3");
            yield return new PageSize(PageMediaSizeName.ISOA4, new Size(210, 297), "A4");
            yield return new PageSize(PageMediaSizeName.ISOB0, new Size(1000, 1414), "B0");
            yield return new PageSize(PageMediaSizeName.ISOB1, new Size(707, 1000), "B1");
            yield return new PageSize(PageMediaSizeName.ISOB2, new Size(500, 707), "B2");
            yield return new PageSize(PageMediaSizeName.ISOB3, new Size(353, 500), "B3");
            yield return new PageSize(PageMediaSizeName.ISOB4, new Size(250, 353), "B4");
        }
    }

    public interface IPagerSizeViewPresenter : IViewPresenter
    {
        ObservableCollection<PageSize> Collection { get; }
        PageSize SelectedPagerSizeData { get; }
    }
}
