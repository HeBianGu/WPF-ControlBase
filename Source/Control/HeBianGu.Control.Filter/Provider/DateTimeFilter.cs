// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Reflection;

namespace HeBianGu.Control.Filter
{
    public class DateTimeFilter : MathValueFilter<DateTime>
    {
        public DateTimeFilter()
        {

        }
        public DateTimeFilter(PropertyInfo property) : base(property)
        {
            this.Value = DateTime.Now;
        }


        private bool _onlyDate = true;
        /// <summary> 仅比较日期 </summary>
        public bool OnlyDate
        {
            get { return _onlyDate; }
            set
            {
                _onlyDate = value;
                RaisePropertyChanged("OnlyDate");
            }
        }

        public override IFilter Copy()
        {
            return new DateTimeFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }

        public override DateTime ConvertValue()
        {
            return this.OnlyDate ? this.Value.Date : this.Value;
        }

    }
}
