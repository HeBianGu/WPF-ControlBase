// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{

    [Displayer(Name = "饼状图", Icon = "\xe7e1")]
    public class PiePresenter : Chart2DPresenterBase
    {
        public PiePresenter()
        {
            this.Height = 500;
            Color[] colors = new Color[12] { Colors.Red, Colors.Green, Colors.Orange, Colors.Purple, Colors.Gray, Colors.Brown, Colors.DeepPink, Colors.DeepSkyBlue, Colors.DarkViolet, Colors.Black, Colors.LightBlue, Colors.DarkTurquoise };
            this.Foreground = colors.ToObservable();

            for (int i = 1; i < 5; i++)
            {
                this.Data.Add(random.Next(10));
            }
        }
        public PiePresenter(IEnumerable<double> data) : this()
        {
            this.RefreshData(data);
        }

        public void RefreshData(IEnumerable<double> data)
        {
            this.Data = new DoubleCollection(data);
        }
        private ObservableCollection<Color> _foreground = new ObservableCollection<Color>();
        [Display(Name = "显示颜色", GroupName = "常用,数据")]
        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public ObservableCollection<Color> Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }
    }


}
