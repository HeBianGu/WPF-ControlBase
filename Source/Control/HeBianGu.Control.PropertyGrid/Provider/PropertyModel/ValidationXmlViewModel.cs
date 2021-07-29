using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 拥有根据Model验证特性判断用户输入数据是否合法的绑定模型基类 </summary>
    public class ValidationXmlViewModel<T> : ModelViewModel<T>, IValidationViewModel where T : new()
    {
        string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "config.xml");

        string unitPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "unit.xml");

        public ValidationXmlViewModel(T t) : base(t)
        {

        }

        /// <summary> 获取指定名称实体字段数据 </summary>
        protected R CreateProperty<R>([CallerMemberName] string propertyName = "") where R : PropertyInfo
        {
            R property = Activator.CreateInstance<R>();

            //  Do ：值改变时检查数据，并更新实体字段
            property.TextChanged = l =>
            {
                this.RefreshProperty(property, propertyName);
            };

            var p = typeof(T).GetProperty(propertyName);

            if(p==null)
            {
                throw new Exception("请在Model中添加字段"+property.Name+" - 类型"+ typeof(R));
            }

            if (!p.CanRead)
            {
                property.Message = "只写字段不支持读取数据";
                return property;
            }


            Property config = this.GetConifg(propertyName);

            UnitGroup ug = this.GetUnit(propertyName);

            property.LoadProperty(config, p.Name);

            property.LoadUnit(ug);

            var value = p.GetValue(this.Model);

            //property.LoadDefault(config.Default);

            property.Default = config.Default;
            //value.Value = config.Default;

            property.LoadValue(value);

            return property;
        }


        /// <summary> 设置指定名称实体字段数据  </summary>
        bool RefreshProperty<R>(R value, [CallerMemberName] string propertyName = "") where R : PropertyInfo
        {
            var p = typeof(T).GetProperty(propertyName);

            if (!p.CanWrite || !p.CanRead)
            {
                value.Message = "只读/只写字段访问";
                return false;
            }

            value.Message = null;

            Property config = this.GetConifg(propertyName);

            if(value.IsValidation(config))
            {
                p.SetValue(this.Model, value.ConvertToObject(value.Text));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary> 检查数据状态 </summary>
        public virtual bool ModelState(out List<string> errors)
        {
            errors = new List<string>();

            var ps = this.GetType().GetProperties();

            foreach (var item in ps)
            {
                if (item.PropertyType.IsSubclassOf(typeof(ValidationPropertyBase)))
                {
                    var v = item.GetValue(this) as ValidationPropertyBase;

                    if (!string.IsNullOrEmpty(v.Message))
                    {
                        errors.Add(v.Message);
                    }
                }
            }

            return errors.Count == 0;
        }

        Property GetConifg(string propertyName)
        {
            string index = this.GetType().GetProperty(propertyName).GetCustomAttributes<XmlIndexAttribute>()?.FirstOrDefault()?.ID;

            var result= PropertyService.GetProperty(this.configPath, index);

            return result;
        }

        UnitGroup GetUnit(string propertyName)
        {
            string unitindex = this.GetType().GetProperty(propertyName).GetCustomAttributes<XmlUnitAttribute>()?.FirstOrDefault()?.ID;

            return PropertyService.GetUnitGroup(unitPath, unitindex);
        }

        /// <summary> 全部设置成默认值 </summary>
        public virtual void LoadDefault()
        {
            foreach (var item in this.GetType().GetProperties())
            {
                if(item.PropertyType.IsSubclassOf(typeof(ValidationPropertyBase)))
                {
                    var v = item.GetValue(this) as ValidationPropertyBase;

                    v.LoadDefault();
                }
            }
        }

        /// <summary> 获取当前承载Model </summary>
        public object GetModel()
        {
            return this.Model;
        }

        /// <summary> 当前显示名称 </summary>
        public string DisplayName { get; set; }
    }

    public interface IValidationViewModel
    {

    }
}
