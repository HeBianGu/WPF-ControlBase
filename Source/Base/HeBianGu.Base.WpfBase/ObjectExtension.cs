using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 尝试用构造函数递归创建实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool TryCreateInstance(this Type type, out object instance)
        {
            if (type.IsValueType)
            {
                instance = Activator.CreateInstance(type);
                return true;
            }

            var find = type.GetConstructor(new Type[] { });

            if (find != null)
            {
                instance = Activator.CreateInstance(type);
                return true;
            }

            var constructors = type.GetConstructors();

            foreach (var cconstructor in constructors)
            {
                var parameters = cconstructor.GetParameters();

                List<object> ps = new List<object>();

                foreach (var parameter in parameters)
                {
                    if (!parameter.ParameterType.TryCreateInstance(out object pInstance))
                    {
                        break;
                    }
                    ps.Add(pInstance);
                }

                if (ps.Count() != parameters.Count()) continue;

                instance = Activator.CreateInstance(type, ps.ToArray());
                return true;
            }
            instance = null;
            return false;
        }

        /// <summary>
        /// 创建泛型集合的实例
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool TryCreateItem(this IEnumerable enumerable, out object instance)
        {
            instance = null;

            if (enumerable == null) return false;

            var generaicType = enumerable.GetType();

            if (!generaicType.IsGenericType) return false;

            var args = generaicType.GetGenericArguments();

            if (args.Count() != 1) return false;

            return args[0].TryCreateInstance(out instance);
        }

        /// <summary>
        /// 创建泛型集合的实例
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object Addtem(this IEnumerable enumerable)
        {
            if(enumerable is IList list)
            {
                if (list.TryCreateItem(out object item))
                {
                    list.Add(item);
                    return item;
                } 
            }

            return null;
        }

        public static object TryConvertFromString(this Type type, string txt, out string error)
        {
            try
            {
                error = null;

                var typeConvert = type.GetCustomAttribute<TypeConverterAttribute>();

                if (typeConvert != null)
                {
                    var t = Type.GetType(typeConvert.ConverterTypeName);

                    var constructor = t.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);

                    if (constructor != null)
                    {
                        var instance = Activator.CreateInstance(t) as TypeConverter;

                        return instance.ConvertFrom(null, System.Globalization.CultureInfo.CurrentUICulture, txt);
                    }
                }

                if (typeof(IConvertible).IsAssignableFrom(type))
                {
                    return Convert.ChangeType(txt, type);
                }

                error = "未识别转换方法";
                return null;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public static string TryConvertToString(this object obj, out string error)
        {
            error = null;

            if (obj == null) return null;

            var type = obj.GetType();

            var typeConvert = type.GetCustomAttribute<TypeConverterAttribute>();

            if (typeConvert != null)
            {
                var t = Type.GetType(typeConvert.ConverterTypeName);

                var constructor = t.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);

                if (constructor != null)
                {
                    var instance = Activator.CreateInstance(t) as TypeConverter;

                    return instance.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, obj);
                }
            }

            if (obj is IConvertible convertible)
            {
                return Convert.ChangeType(obj, typeof(string))?.ToString();
            }

            error = "未识别转换方法";
            return null;
        }
    }
}
