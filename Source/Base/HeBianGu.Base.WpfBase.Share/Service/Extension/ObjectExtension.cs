// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;

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

            ConstructorInfo find = type.GetConstructor(new Type[] { });

            if (find != null)
            {
                instance = Activator.CreateInstance(type);
                return true;
            }

            ConstructorInfo[] constructors = type.GetConstructors();

            foreach (ConstructorInfo cconstructor in constructors)
            {
                ParameterInfo[] parameters = cconstructor.GetParameters();

                List<object> ps = new List<object>();

                foreach (ParameterInfo parameter in parameters)
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

            Type generaicType = enumerable.GetType();

            if (!generaicType.IsGenericType) return false;

            Type[] args = generaicType.GetGenericArguments();

            if (args.Count() != 1) return false;

            return args[0].TryCreateInstance(out instance);
        }
        /// <summary>
        /// 创建泛型集合的类型
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Type GetGenericArgumentType(this IEnumerable enumerable)
        {
            if (enumerable == null) return null;

            Type generaicType = enumerable.GetType();

            if (!generaicType.IsGenericType) return null;

            Type[] args = generaicType.GetGenericArguments();

            return args?.First();
        }


        /// <summary>
        /// 创建泛型集合的实例
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object Addtem(this IEnumerable enumerable)
        {
            if (enumerable is IList list)
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

                TypeConverterAttribute typeConvert = type.GetCustomAttribute<TypeConverterAttribute>();

                if (typeConvert != null)
                {
                    Type t = Type.GetType(typeConvert.ConverterTypeName);

                    ConstructorInfo constructor = t.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);

                    if (constructor != null)
                    {
                        TypeConverter instance = Activator.CreateInstance(t) as TypeConverter;

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

            Type type = obj.GetType();

            TypeConverterAttribute typeConvert = type.GetCustomAttribute<TypeConverterAttribute>();

            if (typeConvert != null)
            {
                Type t = Type.GetType(typeConvert.ConverterTypeName);

                ConstructorInfo constructor = t.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);

                if (constructor != null)
                {
                    TypeConverter instance = Activator.CreateInstance(t) as TypeConverter;

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

        /// <summary> 模型有效信息验证 </summary>
        public static bool ModelState(this object obj, out List<string> errors)
        {
            errors = new List<string>();

            PropertyInfo[] propertys = obj.GetType().GetProperties();

            foreach (PropertyInfo item in propertys)
            {
                List<ValidationAttribute> collection = item.GetCustomAttributes<ValidationAttribute>()?.ToList();

                if (collection.Count == 0)
                    continue;

                object value = item.GetValue(obj);

                foreach (ValidationAttribute r in collection)
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

        public static bool IsMacth(this object obj, Func<PropertyInfo, object, bool> match)
        {
            PropertyInfo[] ps = obj.GetType().GetProperties();

            foreach (PropertyInfo p in ps)
            {
                if (!p.CanRead) continue;

                if (match?.Invoke(p, obj) == true) return true;
            }

            return false;
        }

        public static bool IsMacth(this object obj, string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return true;

            Func<PropertyInfo, object, bool> match = (p, o) =>
              {
                  if (p.PropertyType.IsValueType || p.PropertyType == typeof(string))
                  {
                      return p.GetValue(obj)?.ToString()?.Contains(searchText) == true;
                  }

                  return false;
              };
            return obj.IsMacth(match);
        }
    }

    public static class ResourceExtention
    {
        public static List<DictionaryEntry> FindResources(this ResourceDictionary resource, Predicate<DictionaryEntry> predicate = null)
        {
            List<DictionaryEntry> result = new List<DictionaryEntry>();

            Action<ResourceDictionary> action = null;

            action = l =>
            {

                foreach (ResourceDictionary m in l.MergedDictionaries)
                {
                    action(m);
                }

                try
                {
                    foreach (DictionaryEntry item in l.OfType<DictionaryEntry>())
                    {
                        if (predicate?.Invoke(item) == true)
                            result.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    //  ToDo：后面有时间把有异常的内容干掉 
                    System.Diagnostics.Debug.WriteLine("FindResources:" + ex);

                }
            };

            action.Invoke(resource);

            return result;
        }

        public static IEnumerable<T> FindResources<T>(this ResourceDictionary resource, Predicate<DictionaryEntry> predicate = null) where T : class
        {
            List<DictionaryEntry> entries = resource?.FindResources(predicate);

            if (entries == null) yield break;

            foreach (DictionaryEntry entrie in entries)
            {
                if (entrie.Value is T t)
                    yield return t;
            }
        }
    }
}
