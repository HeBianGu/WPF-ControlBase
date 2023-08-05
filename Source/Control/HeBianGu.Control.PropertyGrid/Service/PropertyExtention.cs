// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace HeBianGu.Control.PropertyGrid
{

    public static class PropertyExtention
    {
        public static IPropertyItem Create(this PropertyInfo info, object obj)
        {
            PropertyItemTypeAttribute editor = info.GetCustomAttribute<PropertyItemTypeAttribute>();
            if (editor?.Type != null)
            {
                if (typeof(IPropertyItem).IsAssignableFrom(editor.Type))
                {
                    return Activator.CreateInstance(editor.Type, info, obj) as IPropertyItem;
                }
            }

            if (typeof(ICommand).IsAssignableFrom(info.PropertyType))
            {
                return new CommandPropertyItem(info, obj);
            }

            //  Do ：日期格式使用日期控件
            if (info.PropertyType == typeof(DateTime)) return new DateTimePropertyItem(info, obj);

            //  Do ：日期格式使用日期控件
            if (info.PropertyType == typeof(bool)) return new BoolPropertyItem(info, obj);
            //  Do ：日期格式使用日期控件
            if (info.PropertyType == typeof(bool?)) return new BoolNullablePropertyItem(info, obj);

            //  Do ：枚举使用枚举类型
            if (info.PropertyType.IsEnum) return new EnumPropertyItem(info, obj);

            //  Do ：首先有TypeConverter的用TypeConverter
            if (TextPropertyItem.IsTypeConverter(info)) return new TextPropertyItem(info, obj);

            if (TextPropertyItem.IsIConvertible(info)) return new TextPropertyItem(info, obj);

            //  Do ：其他基元类型
            else if (info.PropertyType.IsPrimitive || info.PropertyType == typeof(string)) return new TextPropertyItem(info, obj);

            if (typeof(IEnumerable).IsAssignableFrom(info.PropertyType) && info.PropertyType.IsGenericType)
            {
                Type[] args = info.PropertyType.GetGenericArguments();

                if (args.Count() == 1 && args.First().IsPrimitive)
                {
                    //  Do ：List<double>
                    return new PrimitiveListPropertyItem(info, obj);
                }
                else
                {    //  Do ：泛型集合
                    return new ListPropertyItem(info, obj);
                }
            }

            if (info.PropertyType.IsArray)
            {
                Type elementType = info.PropertyType.GetElementType();

                if (elementType.IsPrimitive || elementType == typeof(string) || elementType == typeof(DateTime))
                {
                    //  Do ：数组
                    return new PrimitiveArrayPropertyItem(info, obj);
                }
                else
                {
                    return new ArrayPropertyItem(info, obj);
                }

            }

            CustomValidationAttribute attr = info.GetCustomAttribute<CustomValidationAttribute>();

            if (attr == null || string.IsNullOrEmpty(attr.Method))
            {
                return new ObjectPropertyItem<object>(info, obj);
            }
            else
            {
                MethodInfo ms = obj.GetType().GetMethod(attr.Method);

                IEnumerable<object> source = ms.Invoke(obj, null) as IEnumerable<object>;

                if (source == null) return new ObjectPropertyItem<object>(info, obj);

                return new SelectSourcePropertyItem(info, obj);
            }
        }

        public static ObjectPropertyItem CreateByType(this PropertyInfo info, object obj)
        {
            if (info.PropertyType == typeof(int)) return new TextPropertyItem(info, obj);
            if (info.PropertyType == typeof(double)) return new TextPropertyItem(info, obj);

            //  Do ：日期格式使用日期控件
            if (info.PropertyType == typeof(DateTime)) return new DateTimePropertyItem(info, obj);

            //  Do ：日期格式使用日期控件
            if (info.PropertyType == typeof(bool)) return new BoolPropertyItem(info, obj);

            if (info.PropertyType.IsEnum) return new EnumPropertyItem(info, obj);

            if (TextPropertyItem.IsIConvertible(info)) return new TextPropertyItem(info, obj);

            if (TextPropertyItem.IsTypeConverter(info)) return new TextPropertyItem(info, obj);


            if (typeof(IEnumerable).IsAssignableFrom(info.PropertyType) && info.PropertyType.IsGenericType)
            {
                Type[] args = info.PropertyType.GetGenericArguments();

                if (args.Count() == 1 && args.First().IsPrimitive)
                {
                    //  Do ：List<double>
                    return new PrimitiveListPropertyItem(info, obj);
                }
                else
                {    //  Do ：泛型集合
                    return new ListPropertyItem(info, obj);
                }

                throw new ArgumentException();
            }

            if (info.PropertyType.IsArray)
            {
                Type elementType = info.PropertyType.GetElementType();

                if (elementType.IsPrimitive || elementType == typeof(string) || elementType == typeof(DateTime))
                {
                    //  Do ：数组
                    return new PrimitiveArrayPropertyItem(info, obj);
                }
                else
                {
                    return new ArrayPropertyItem(info, obj);
                }

            }

            CustomValidationAttribute attr = info.GetCustomAttribute<CustomValidationAttribute>();

            if (attr == null || string.IsNullOrEmpty(attr.Method))
            {
                return new ObjectPropertyItem<object>(info, obj);
            }
            else
            {
                MethodInfo ms = obj.GetType().GetMethod(attr.Method);

                IEnumerable<object> source = ms.Invoke(obj, null) as IEnumerable<object>;

                if (source == null) return new ObjectPropertyItem<object>(info, obj);

                return new SelectSourcePropertyItem(info, obj);
            }
        }

        public static T ChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static IPropertyItem CreateView(this PropertyInfo info, object obj)
        {
            PropertyItemTypeAttribute editor = info.GetCustomAttribute<PropertyItemTypeAttribute>();
            if (editor?.Type != null)
            {
                if (typeof(IPropertyViewItem).IsAssignableFrom(editor.Type))
                {
                    return Activator.CreateInstance(editor.Type, info, obj) as IPropertyViewItem;
                }
            }

            if (TextPropertyItem.IsTypeConverter(info))
                return new TextPropertyViewItem(info, obj);

            if (TextPropertyItem.IsIConvertible(info))
                return new TextPropertyViewItem(info, obj);

            if (info.PropertyType.IsClass && info.PropertyType != typeof(string))
            {
                return new ObjectPropertyItem<object>(info, obj);
            }
            return new TextPropertyViewItem(info, obj);

            //if (typeof(ICommand).IsAssignableFrom(info.PropertyType))
            //{
            //    return new CommandPropertyItem(info, obj);
            //}

            //if (info.PropertyType == typeof(DateTime)) return new DateTimePropertyItem(info, obj);

            ////  Do ：日期格式使用日期控件
            //if (info.PropertyType == typeof(bool)) return new BoolPropertyItem(info, obj);

            ////  Do ：枚举使用枚举类型
            //if (info.PropertyType.IsEnum) return new EnumPropertyItem(info, obj);

            ////  Do ：首先有TypeConverter的用TypeConverter
            //if (TextPropertyItem.IsTypeConverter(info)) return new TextPropertyItem(info, obj);

            //if (TextPropertyItem.IsIConvertible(info)) return new TextPropertyItem(info, obj);

            ////  Do ：其他基元类型
            //else if (info.PropertyType.IsPrimitive || info.PropertyType == typeof(string)) return new TextPropertyItem(info, obj);

            //if (typeof(IEnumerable).IsAssignableFrom(info.PropertyType) && info.PropertyType.IsGenericType)
            //{
            //    Type[] args = info.PropertyType.GetGenericArguments();

            //    if (args.Count() == 1 && args.First().IsPrimitive)
            //    {
            //        //  Do ：List<double>
            //        return new PrimitiveListPropertyItem(info, obj);
            //    }
            //    else
            //    {    //  Do ：泛型集合
            //        return new ListPropertyItem(info, obj);
            //    }
            //}

            //if (info.PropertyType.IsArray)
            //{
            //    Type elementType = info.PropertyType.GetElementType();

            //    if (elementType.IsPrimitive || elementType == typeof(string) || elementType == typeof(DateTime))
            //    {
            //        //  Do ：数组
            //        return new PrimitiveArrayPropertyItem(info, obj);
            //    }
            //    else
            //    {
            //        return new ArrayPropertyItem(info, obj);
            //    }

            //}

            //CustomValidationAttribute attr = info.GetCustomAttribute<CustomValidationAttribute>();

            //if (attr == null || string.IsNullOrEmpty(attr.Method))
            //{
            //    return new ObjectPropertyItem<object>(info, obj);
            //}
            //else
            //{
            //    MethodInfo ms = obj.GetType().GetMethod(attr.Method);

            //    IEnumerable<object> source = ms.Invoke(obj, null) as IEnumerable<object>;

            //    if (source == null) return new ObjectPropertyItem<object>(info, obj);

            //    return new SelectSourcePropertyItem(info, obj);
            //}
        }
    }
}
