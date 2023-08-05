// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
            this.Operate = FilterOperate.Equals;

        }

        public StringFilter(PropertyInfo property, IEnumerable source) : base(property, source)
        {
            this.Operate = FilterOperate.Equals;
        }

        private bool _ordinalIgnoreCase;
        public bool OrdinalIgnoreCase
        {
            get { return _ordinalIgnoreCase; }
            set
            {
                _ordinalIgnoreCase = value;
                RaisePropertyChanged();
            }
        }

        public override IFilter Copy()
        {
            StringFilter result = new StringFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value, Source = this.Source, SelectedSource = new ObservableCollection<string>(this.SelectedSource) };

            if (this.Operate == FilterOperate.SelectSource)
            {
                result.Value = $"<{string.Join(",", this.SelectedSource)}>";
            }

            return result;
        }

        public override bool IsMatch(object obj)
        {

            PropertyInfo p = obj.GetType().GetProperty(this.Name);
            if (p == null || !p.CanRead)
                return false;

            string v = p.GetValue(obj)?.ToString();
            if (this.Operate == FilterOperate.Contain)
            {
                if (string.IsNullOrEmpty(this.Value))
                    return true;
                return this.OrdinalIgnoreCase ? v.IndexOf(this.Value, StringComparison.OrdinalIgnoreCase) >= 0 : v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.UnContain)
            {
                if (string.IsNullOrEmpty(this.Value))
                    return true;
                return this.OrdinalIgnoreCase ? !(v.IndexOf(this.Value, StringComparison.OrdinalIgnoreCase) >= 0) : !v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.Equals)
            {
                return string.Compare(v, this.Value, this.OrdinalIgnoreCase) == 0;
            }
            else if (this.Operate == FilterOperate.UnEquals)
            {
                return string.Compare(v, this.Value, this.OrdinalIgnoreCase) != 0;
            }
            else if (this.Operate == FilterOperate.SelectSource)
            {
                //  Do ：没有勾选默认表示全选
                if (this.SelectedSource.Count == 0)
                    return true;

                return this.SelectedSource.Any(l => l == v);
            }
            else if (this.Operate == FilterOperate.Setted)
            {
                return v != null;
            }
            else if (this.Operate == FilterOperate.StartWith)
            {
                return this.OrdinalIgnoreCase ? v.StartsWith(this.Value, StringComparison.OrdinalIgnoreCase) : v.StartsWith(this.Value);
            }
            else if (this.Operate == FilterOperate.EndWith)
            {
                return this.OrdinalIgnoreCase ? v.EndsWith(this.Value, StringComparison.OrdinalIgnoreCase) : v.EndsWith(this.Value);
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
