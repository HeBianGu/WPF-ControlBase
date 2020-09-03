using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 用于带下拉选项和子项的配置属性 </summary>
    public class ObjectSelectProperty<T> : PropertyInfo<T> where T : class, IObjectSelectProperty
    {
        protected List<T> _matchs = new List<T>();

        public override T ConvertToValue(object obj)
        {
            var find = _matchs.FirstOrDefault(l => l.DisplayName == obj.ToString());

            if (find == null)
            {
                throw new ArgumentException("没有找到匹配类型: " + obj?.ToString());
            }

            return find;
        }

        public override object ConvertToObject(string value)
        {
            return this.Current?.GetModel();
        }

        public override void LoadProperty(Property config, string defaultName = null)
        {
            base.LoadProperty(config, defaultName);
        }

        public override bool IsTypeOf()
        {
            return true;
        }

        public override void LoadValue(object obj)
        {
            base.LoadValue(obj);

            if (obj == null)
            {
                //  Do ：为空加载默认值
                this.LoadDefault();
            }
            else
            {
                //  Do ：不为空加载实体上数据
                var find = _matchs.FirstOrDefault(l => l.GetType().BaseType.GenericTypeArguments?.FirstOrDefault() == obj.GetType());

                if (find == null)
                {
                    throw new ArgumentException("没有找到对应类型的ViewModel" + obj?.ToString());
                }

                this.Source.Remove(find);

                var current = Activator.CreateInstance(find.GetType(), obj) as T;

                this.Source.Add(current);

                this.Current = current;
            }
        }

        public override void LoadDefault()
        {
            base.LoadDefault();

            this.Current = this.Source?.FirstOrDefault(l => l.DisplayName == this.Default);
        }

        protected override void CurrentChanged()
        {
            this.TextChanged?.Invoke(this.Current?.DisplayName);
        }
    }
}
