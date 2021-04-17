using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public class ObjectPropertyFactory
    {
        /// <summary> 根据属性创建实体类型 </summary>
        public static ObjectPropertyItem Create(PropertyInfo info, object obj)
        {

            var editor = info.GetCustomAttribute<EditorAttribute>();

            if(editor!=null)
            {
                if(editor.EditorTypeName== "Combobox")
                {
                    return new ComboboxPropertyItem(info, obj);
                }
            }

            if (info.PropertyType == typeof(int))
            {
                return new IntPropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(string))
            {
                return new StringPropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(DateTime))
            {
                return new DateTimePropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(double))
            {
                return new DoublePropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(bool))
            {
                return new BoolPropertyItem(info, obj);
            }

            return null;
        }

        /// <summary> 模型有效信息验证 </summary>
        public static bool ModelState(object obj, out List<string> errors)
        {
            errors = new List<string>();

            var propertys = obj.GetType().GetProperties();

            foreach (var item in propertys)
            {
                var collection = item.GetCustomAttributes<ValidationAttribute>()?.ToList();

                var value = item.GetValue(obj);

                foreach (var r in collection)
                {
                    if (!r.IsValid(value))
                    {
                        string display = item.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;

                        errors.Add(r.ErrorMessage ?? r.FormatErrorMessage(display ?? item.Name));
                    }
                }
            }

            return errors.Count == 0;
        }
    }
}
