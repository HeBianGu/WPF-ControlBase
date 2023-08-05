// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;

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

            if (!generaicType.IsGenericType)
            {
                if (generaicType.BaseType?.IsGenericType == true)
                {
                    generaicType = generaicType.BaseType;
                }
                else
                {
                    return false;
                }
            }

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

        public static string TryConvertToString(this object obj)
        {
            return obj.TryConvertToString(out string error);
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
            if (obj == null)
                return true;
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


        /// <summary> 模型有效信息验证 </summary>
        public static bool ModelStateDeep(this object obj, out string error)
        {
            error = null;
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            foreach (PropertyInfo item in propertys)
            {
                if (item.Name == "Item")
                    continue;
                List<ValidationAttribute> collection = item.GetCustomAttributes<ValidationAttribute>()?.ToList();
                object value = item.GetValue(obj);
                if (collection.Count > 0)
                {
                    foreach (ValidationAttribute r in collection)
                    {
                        if (!r.IsValid(value))
                        {
                            string display = item.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;
                            error = r.ErrorMessage ?? r.FormatErrorMessage(display ?? item.Name);
                            return false;
                        }
                    }
                    continue;
                }

                if (item.PropertyType.IsPrimitive)
                    continue;
                if (item.PropertyType == typeof(string))
                    continue;
                if (item.PropertyType == typeof(DateTime))
                    continue;
                if (item.PropertyType.IsEnum)
                    continue;

                if (value is IEnumerable objects)
                {
                    foreach (var current in objects)
                    {
                        if (item.PropertyType.IsPrimitive)
                            continue;
                        if (item.PropertyType == typeof(string))
                            continue;
                        if (item.PropertyType == typeof(DateTime))
                            continue;
                        if (item.PropertyType.IsEnum)
                            continue;
                        bool r = current.ModelStateDeep(out error);
                        if (r == false)
                            return false;
                    }
                    continue;
                }

                if (value.ModelStateDeep(out error) == false)
                    return false;
            }
            return true;
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


        public static T TryChangeType<T>(this object obj)
        {
            bool r = obj.TryChangeType<T>(out T result);
            return result;
        }
        public static bool TryChangeType<T>(this object obj, out T result)
        {
            result = default(T);
            Type rType = typeof(T);
            if (obj == null) return false;
            Type type = obj.GetType();

            if (obj is T)
            {
                result = (T)obj;
                return true;
            }
            if (typeof(IConvertible).IsAssignableFrom(rType) && typeof(IConvertible).IsAssignableFrom(type))
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                    return false;
                result = (T)Convert.ChangeType(obj, rType);
                return true;
            }

            var tConvert = rType.GetCustomAttribute<TypeConverterAttribute>();
            if (type == typeof(string) && tConvert != null)
            {
                var t = Type.GetType(tConvert.ConverterTypeName);
                TypeConverter typeConverter = Activator.CreateInstance(t) as TypeConverter;
                if (typeof(Freezable).IsAssignableFrom(typeof(T)))
                    result = Application.Current.Dispatcher.Invoke(() =>
                     {
                         return (T)typeConverter.ConvertFromString(obj.ToString());
                     });
                else
                    result = (T)typeConverter.ConvertFromString(obj.ToString());
                return true;
            }
            return false;
        }

        public static string GetDisplayName(this Type type)
        {
            return type.GetCustomAttribute<DisplayAttribute>()?.Name ?? type.Name;
        }

        public static object CloneBy(this object t, Predicate<PropertyInfo> predicate = null)
        {
            object n = Activator.CreateInstance(t.GetType());
            PropertyInfo[] ps = t.GetType().GetProperties();
            foreach (PropertyInfo p in ps)
            {
                if (p.CanWrite == false || p.CanRead == false)
                    continue;
                if (predicate?.Invoke(p) == false)
                    continue;

                if (p.PropertyType.IsPrimitive || p.PropertyType.IsValueType || p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(string))
                {
                    p.SetValue(n, p.GetValue(t));
                    continue;
                }
                //var tConvert = p.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
                //if (tConvert != null)
                //{
                //    var t = Type.GetType(tConvert.ConverterTypeName);
                //    TypeConverter typeConverter = Activator.CreateInstance(t) as TypeConverter;
                //    if (typeof(Freezable).IsAssignableFrom(typeof(T)))
                //        result = Application.Current.Dispatcher.Invoke(() =>
                //        {
                //            return (T)typeConverter.ConvertFromString(obj.ToString());
                //        });
                //    else
                //        result = (T)typeConverter.ConvertFromString(obj.ToString());
                //    return true;
                //}
                if (p.PropertyType.TryCreateInstance(out object nn))
                {
                    if (nn is IList list && p.GetValue(t) is IList old)
                    {
                        foreach (var item in old)
                        {
                            list.Add(item.CloneBy(predicate));
                        }
                    }
                    else
                    {
                        p.SetValue(n, nn.CloneBy(predicate));
                    }
                }
            }
            return n;
        }

        public static T CloneCast<T>(this T t, Predicate<PropertyInfo> predicate = null) where T : class
        {
            return t.CloneBy(predicate) as T;
        }

        public static object CloneXml(this object realObject)
        {
            return XmlSerialize.Instance.CloneXml(realObject);
        }

        public static void CopyPropertyValueFrom(this object to, object from, Predicate<PropertyInfo> predicate = null, Func<PropertyInfo, PropertyInfo, bool> firstOrDefault = null)
        {
            PropertyInfo[] toPs = to.GetType().GetProperties();
            PropertyInfo[] fromPs = from.GetType().GetProperties();
            foreach (PropertyInfo p in fromPs)
            {
                if (p.CanWrite == false)
                    continue;

                if (predicate?.Invoke(p) == false)
                    continue;

                PropertyInfo top = null;
                if (firstOrDefault == null)
                {
                    top = toPs.FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType);
                }
                else
                {
                    top = toPs.FirstOrDefault(x => firstOrDefault.Invoke(x, p));
                }

                if (top == null)
                    continue;

                if (top.CanWrite == false)
                    continue;

                if (p.PropertyType.IsPrimitive || p.PropertyType.IsValueType || p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(string))
                {
                    top.SetValue(to, p.GetValue(from));
                    continue;
                }

                if (p.PropertyType.TryCreateInstance(out object nn))
                {
                    //if (nn is IList list && p.GetValue(from) is IList old)
                    //{
                    //    foreach (var item in old)
                    //    {
                    //        list.Add(item.CloneBy(predicate));
                    //    }
                    //}
                    //else
                    //{
                    //    p.SetValue(from, nn);
                    //}
                    p.SetValue(from, nn);
                }
            }
        }
    }

    public static class ResourceExtension
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
