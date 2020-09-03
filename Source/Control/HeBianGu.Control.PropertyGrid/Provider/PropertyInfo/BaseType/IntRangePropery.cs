using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> Int类型范围输入 </summary>
    public class IntRangePropery : RangeProperyBase<int>
    {
        public override int ConvertToValue(object value)
        {
            return Convert.ToInt32(value?.ToString());
        }

        public override void LoadProperty(Property u, string defaultName = null)
        {
            base.LoadProperty(u);

            this.Min = (int)u.Range.Min;
            this.Max = (int)u.Range.Max;
            this.Step = 1;
        }

        public override bool IsTypeOf()
        {
            return int.TryParse(this.Text, out int d);
        }
    }
}
