// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;

namespace HeBianGu.Control.Filter
{

    public class IntFilter : MathValueFilter<int>
    {
        public IntFilter()
        {

        }

        public IntFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilter Copy()
        {
            return new IntFilter(this.Model) { Operate = this.Operate, Value = this.Value };
        }
    }
}
