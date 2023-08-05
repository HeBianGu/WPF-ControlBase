
using HeBianGu.Base.WpfBase;
using System.Windows;

namespace HeBianGu.Systems.Design
{
    //public class Service : IService
    //{

    //}

    //public interface IService
    //{

    //}

    [Displayer(Name = "设计器", GroupName = SystemSetting.GroupSystem)]
    public class DesignSetting : LazySettingInstance<DesignSetting>
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.RowHeight = (double)Application.Current.FindResource(SystemKeys.RowHeight);
        }

        private double _rowHeight;
        /// <summary> 说明  </summary>
        public double RowHeight
        {
            get { return _rowHeight; }
            set
            {
                _rowHeight = value;
                RaisePropertyChanged();
            }
        }

    }
}
