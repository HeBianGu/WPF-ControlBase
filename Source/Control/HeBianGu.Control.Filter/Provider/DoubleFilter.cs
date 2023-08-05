// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;

namespace HeBianGu.Control.Filter
{
    public class DoubleFilter : MathValueFilter<double>
    {
        public DoubleFilter()
        {

        }
        public DoubleFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilter Copy()
        {
            return new DoubleFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }

}
