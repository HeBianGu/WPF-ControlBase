using System;
using System.Collections.Generic;
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
        public static List<string> ModelState(object obj)
        {
            List<string> results = new List<string>();

            var propertys = obj.GetType().GetProperties();

            foreach (var item in propertys)
            {
                var collection = item.GetCustomAttributes<ValidationAttribute>()?.ToList();

                var value = item.GetValue(obj);

                //  Do：检验数据有效性
                if (results == null) continue;

                foreach (var r in collection)
                {
                    if (!r.IsValid(value))
                    {
                        results.Add(r.ErrorMessage);
                    }
                } 
            }

            return results;
        }
    }
}
