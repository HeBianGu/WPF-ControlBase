using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{

    /// <summary> double类型范围输入 </summary>
    public class DoubleRangePropery : RangeProperyBase<double>
    {
        public override void LoadProperty(Property u, string defaultName = null)
        {

            base.LoadProperty(u);

            this.Min = u.Range.Min;
            this.Max = u.Range.Max;
            this.Step = 1.0;

            AddValueCommand = new RelayCommand(l => Add());
            MulValueCommand = new RelayCommand(l => Mul());
        }


        public RelayCommand AddValueCommand { get; set; }

        public RelayCommand  MulValueCommand { get; set; }

        public void Add()
        {
            this.Text = (Convert.ToDouble(this.Text) + this.Step).ToString();

        }

        public void Mul()
        {
            this.Text = (Convert.ToDouble(this.Text) - this.Step).ToString();
        }

        public override double ConvertToValue(object obj)
        {
            return Convert.ToDouble(obj?.ToString());
        }

        public override bool IsTypeOf()
        {
            return double.TryParse(this.Text,out double d);
        }
    }
}
