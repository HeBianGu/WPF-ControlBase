
using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HeBianGu.Control.ColorBox
{
    public class ColorBoxService : IColorBoxService
    {

    }

    public interface IColorBoxService
    {

    }

    [Displayer(Name = "颜色列表设置", GroupName = SystemSetting.GroupControl)]
    public class ColorBoxSetting : LazySettingInstance<ColorBoxSetting>
    {
        public ColorBoxSetting()
        {
            this.IsVisibleInSetting = false;
        }
        private int _from = 0;
        [Display(Name = "起始位置")]
        [DefaultValue(0)]
        [Browsable(false)]
        public int From
        {
            get { return _from; }
            set
            {
                _from = value;
                RaisePropertyChanged();
            }
        }

        private int _to = 360;
        [Display(Name = "结束位置")]
        [DefaultValue(360)]
        [Browsable(false)]
        public int To
        {
            get { return _to; }
            set
            {
                _to = value;
                RaisePropertyChanged();
            }
        }

        private int _step = 48;
        [Display(Name = "步进")]
        [DefaultValue(48)]
        [Browsable(false)]
        public int Step
        {
            get { return _step; }
            set
            {
                _step = value;
                RaisePropertyChanged();
            }
        }

        private DoubleCollection _depthes = new DoubleCollection(new double[] { 1.0, 0.8, 0.6, 0.4, 0.2 });
        [Browsable(false)]
        [Display(Name = "颜色深度")]
        public DoubleCollection Depthes
        {
            get { return _depthes; }
            set
            {
                _depthes = value;
                RaisePropertyChanged();
            }
        }
    }
}
