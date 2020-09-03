using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 拥有根据Model验证特性判断用户输入数据是否合法的绑定模型基类 </summary>
    public class ValidationAttributeViewModel<T> : ModelViewModel<T> where T : new()
    {
        public ValidationAttributeViewModel() : base()
        {

        }

        public ValidationAttributeViewModel(T t) : base(t)
        {

        }

        /// <summary> 获取指定名称实体字段数据 </summary>
        protected R CreateProperty<R>([CallerMemberName] string propertyName = "") where R : PropertyInfo
        {
            R value = Activator.CreateInstance<R>();

            //  Do ：值改变时检查数据，并更新实体字段
            value.TextChanged = l =>
            {
                this.RefreshProperty(value, propertyName);
            };

            var p = typeof(T).GetProperty(propertyName);

            var p_this = this.GetType().GetProperty(propertyName);

            string display = p.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;

            string display_this = p_this.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;

            value.Name = p.Name;

            value.DisplayName = display_this ?? display ?? value.Name;

            if (!p.CanRead)
            {
                value.Message = "只写字段不支持读取数据";
                return value;
            }

            //  Do ：判断数据验证
            var collection = p.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection != null)
            {
                foreach (var r in collection)
                {

                    if (!r.IsValid(value.Text))
                    {
                        value.Message = r.ErrorMessage ?? r.FormatErrorMessage(value.DisplayName);

                        //  Do ：保存实体验证失败
                        break;
                    }
                }
            }

            var collection_this = p_this.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection_this != null)
            {
                foreach (var r in collection_this)
                {

                    if (!r.IsValid(value.Text))
                    {
                        value.Message = r.ErrorMessage ?? r.FormatErrorMessage(value.DisplayName);

                        //  Do ：保存实体验证失败
                        break;
                    }
                }
            }

            value.Flag = p.GetCustomAttributes<RequiredAttribute>()?.FirstOrDefault() != null ? "*" : "";

            value.Text = p.GetValue(this.Model)?.ToString();

            return value;
        }


        /// <summary> 设置指定名称实体字段数据  </summary>
        bool RefreshProperty<R>(R value, [CallerMemberName] string propertyName = "") where R : PropertyInfo
        {
            var p = typeof(T).GetProperty(propertyName);

            var p_this = this.GetType().GetProperty(propertyName);

            if (!p.CanWrite)
            {
                value.Message = "只读字段不可以修改";
                return false;
            }

            value.Message = null;

            //  Do ：判断数据验证
            var collection = p.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection != null)
            {
                foreach (var r in collection)
                {
                    if (!r.IsValid(value.Text))
                    {
                        string display = p.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;

                        value.Message = r.ErrorMessage ?? r.FormatErrorMessage(display ?? p.Name);

                        //  Do ：保存实体验证失败
                        return false;
                    }
                }
            }

            var collection_this = p_this.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection_this != null)
            {
                foreach (var r in collection_this)
                {

                    if (!r.IsValid(value.Text))
                    {
                        value.Message = r.ErrorMessage ?? r.FormatErrorMessage(value.DisplayName);

                        //  Do ：保存实体验证失败
                        break;
                    }
                }
            }

            p.SetValue(this.Model, value.ConvertToObject(value.Text));

            return true;
        }

        /// <summary> 检查数据状态 </summary>
        public bool ModelState(out List<string> errors)
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
    }
}
