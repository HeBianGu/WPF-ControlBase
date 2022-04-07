// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Control.Filter
{

    public class StringFilter : PropertyFilterBase<string>
    {
        public StringFilter()
        {

        }
        public StringFilter(PropertyInfo property) : base(property)
        {
            this.Operate = FilterOperate.SelectSource;
        }

        public StringFilter(PropertyInfo property, IEnumerable source) : base(property, source)
        {
            this.Operate = FilterOperate.SelectSource;
        }

        public override IFilter Copy()
        {
            StringFilter result = new StringFilter(this.Model) { Operate = this.Operate, Value = this.Value, Source = this.Source, SelectedSource = new ObservableCollection<string>(this.SelectedSource) };

            if (this.Operate == FilterOperate.SelectSource)
            {
                result.Value = $"<{string.Join(",", this.SelectedSource)}>";
            }

            return result;
        }

        public override bool IsMatch(object obj)
        {
            PropertyInfo p = obj.GetType().GetProperty(this.Name);

            if (p == null || !p.CanRead) return false;

            string v = p.GetValue(obj)?.ToString();

            if (this.Operate == FilterOperate.Contain)
            {
                return v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.UnContain)
            {
                return !v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.Equals)
            {
                return v == this.Value;
            }
            else if (this.Operate == FilterOperate.UnEquals)
            {
                return v != this.Value;
            }
            else if (this.Operate == FilterOperate.SelectSource)
            {
                //  Do ：没有勾选默认表示全选
                if (this.SelectedSource.Count == 0) return true;

                return this.SelectedSource.Any(l => l == v);
            }
            else if (this.Operate == FilterOperate.Setted)
            {
                return v != null;
            }
            else if (this.Operate == FilterOperate.Unset)
            {
                return v == null;
            }
            else
            {
                return false;
            }
        }
    }
}
