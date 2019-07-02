using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Linq
{

    /// <summary> 基础类型扩展 </summary>
    public static class ObjectExtend
    {

        /// <summary> 判断是否为空 </summary>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary> 判断是否不为空 </summary>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary> 判断是否为空 </summary>
        public static bool IsNullOrEmpty(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

        /// <summary> 判断是否不为空 </summary>
        public static bool NotNullOrEmpty(this string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }

        /// <summary> 拆箱 </summary>
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        /// <summary> 是否只指定类型 </summary>
        public static bool Is<T>(this object obj)
        {
            return obj is T;
        }

        /// <summary> 转换成指定类型 </summary>
        public static T As<T>(this object obj) where T : class
        {
            if (obj is T)
            {
                return null;
            }
            else
            {
                return (T)obj;
            }
        }

        /// <summary> 转换成指定类型 </summary>
        public static void Do<T>(this T obj, Action<T> action) where T : class
        {
            if (obj.IsNotNull())
            {
                action(obj);
            }
        }

        /// <summary> 获取特性 指定参数的类型的特性 </summary>
        public static T GetAttributeInfo<T>(this object o) where T : Attribute
        {
            Type t = o.GetType();

            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }

        /// <summary> 获取特性 指定参数的类型的特性 </summary>
        public static T GetAttribute<T>(this Enum e) where T : Attribute
        {
            FieldInfo field = e.GetType().GetField(e.ToString());
            var desc = Attribute.GetCustomAttribute(field, typeof(T)) as T;

            return desc;

        }

        /// <summary> 获取特性 指定参数的字段特性 </summary>
        public static T GetAttribute<T>(this MemberInfo o) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(o, typeof(T));
        }

        /// <summary> 深复制 反射机制 </summary> 
        public static T DeepCopy<T>(this T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());

            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopy(field.GetValue(obj)));
                }
                catch
                {

                }
            }

            return (T)retval;
        }


        /// <summary> Debug下执行方法 </summary>
        public static void DoWhenDebug(this Action act)
        {
#if (DEBUG)
            act();
#else

#endif

        }

        /// <summary> Release下执行方法 </summary>
        public static void DoWhenRelease(this Action act)
        {
#if (!DEBUG)
            act();
#else

#endif
        }


        /// <summary> 反序列化字段 （把名称相同的字段赋值过来 ）isUpper=true 不区分大小写 </summary>
        public static void DeSeralize(this object obj, object v, bool isUpper = false)
        {
            FieldInfo[] fields = v.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            var thisfields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).ToList();

            foreach (FieldInfo field in fields)
            {
                var f = thisfields.Find(l => l.Name == field.Name);

                if (isUpper)
                {
                    f = thisfields.Find(l => l.Name.ToUpper() == field.Name.ToUpper());
                }

                if (f == null) continue;

                if (f.FieldType != field.FieldType) return;

                f.SetValue(obj, field.GetValue(v));
            }
        }

        /// <summary> 反序列化字段 （把名称相同的字段赋值过来 ）isUpper=true 不区分大小写 </summary>
        public static void CopyFromObj(this object obj, object v, bool isUpper = false)
        {
            PropertyInfo[] fields = v.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            var thisfields = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).ToList();

            foreach (PropertyInfo field in fields)
            {
                var f = thisfields.Find(l => l.Name == field.Name);

                if (isUpper)
                {
                    f = thisfields.Find(l => l.Name.ToUpper() == field.Name.ToUpper());
                }

                if (f == null) continue;

                if (f.PropertyType != field.PropertyType) continue;

                f.SetValue(obj, field.GetValue(v));
            }
        }

        /// <summary> 反序列化字段 （把名称相同的字段赋值过来 ）isUpper=true 不区分大小写 </summary>
        public static void CopyFromObjExtend(this object obj, object v, bool isUpper = false)
        {
            PropertyInfo[] fields = v.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            var thisfields = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).ToList();

            foreach (PropertyInfo field in fields)
            {
                var f = thisfields.Find(l => l.Name == field.Name);

                if (isUpper)
                {
                    f = thisfields.Find(l => l.Name.ToUpper() == field.Name.ToUpper());
                }

                if (f == null) continue;

                object o = field.GetValue(v);

                Func<object, object> func = l =>
                {
                    if (l is IList)
                    {
                        return l;
                    }
                    else if (l is double)
                    {
                        double d = l.ToString().ToDouble();

                        if (d == 0) return "";

                        return l.ToString();
                    }
                    else if (l is int)
                    {
                        double d = l.ToString().ToDouble();

                        if (d == 0) return "";

                        return l.ToString();
                    }
                    else if (l is string)
                    {
                        string d = l.ToString();

                        if (d.Trim() == "0|" || d.Trim() == "0") return "";

                        return l.IsNotNull() ? l.ToString().Replace("\u0001", "|") : l;
                    }
                    else
                    {
                        return l;
                    }
                };

                if (o == null) continue;

                System.Diagnostics.Debug.WriteLine(f.PropertyType.FullName);
                System.Diagnostics.Debug.WriteLine(func(o).GetType().FullName);

                if (f.PropertyType.FullName == func(o).GetType().FullName)
                {
                    f.SetValue(obj, func(o));
                }

            }
        }


        /// <summary> 反序列化字段 （把名称相同的字段赋值过来 ）match匹配规则 convert转换类型规则  </summary>
        public static void CopyFromObj(this object n, object o, Func<PropertyInfo, PropertyInfo, bool> match, Func<object, object> convert)
        {

            PropertyInfo[] fields = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            var np = n.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).ToList();

            foreach (PropertyInfo op in fields)
            {
                var find = np.Find(l => match(l, op));

                if (find == null) continue;

                var old = op.GetValue(o);

                object c = convert(old);

                if (c is string)
                {
                    find.SetValue(n, convert(old));
                }
            }
        }


        /// <summary> 反序列化字段 （把名称相同的字段赋值过来,统一转换成string类型 ）isUpper=true 不区分大小写   </summary>
        public static void CopyFromObjToString(this object n, object o, bool isUpper = false)
        {
            Func<PropertyInfo, PropertyInfo, bool> match = (l, k) =>
              {
                  return isUpper ? l.Name.ToUpper() == k.Name.ToUpper() : l.Name == k.Name;
              };

            Func<object, object> convert = l =>
              {
                  return l.IsNotNull() ? l.ToString() : l;
              };

            CopyFromObj(n, o, match, convert);
        }

        /// <summary> 反序列化字段 （把名称相同的字段赋值过来) 名称相同类型不同执行递归拷贝 </summary>
        public static void CopyFromObjDeep(this object obj, object v, bool isUpper = false)
        {

            PropertyInfo[] fields = v.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            var thisfields = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).ToList();

            foreach (PropertyInfo field in fields)
            {
                var f = thisfields.Find(l => l.Name == field.Name);

                if (isUpper)
                {
                    f = thisfields.Find(l => l.Name.ToUpper() == field.Name.ToUpper());
                }

                if (f == null) continue;


                if (f.PropertyType != field.PropertyType)
                {
                    var fv = field.GetValue(v);

                    var ov = f.GetValue(obj);

                    // Do ：列表特殊处理 
                    if (fv != null && fv is IList && ov is IList)
                    {
                        if (ov.GetType().IsGenericType)
                        {
                            IList collection = fv as IList;

                            IList objcollection = ov as IList;

                            foreach (var item in collection)
                            {
                                var o = Activator.CreateInstance(ov.GetType().GetGenericArguments()[0]);

                                o.CopyFromObjDeep(item);

                                objcollection.Add(o);
                            }
                        }
                    }
                    else
                    {
                        if (fv != null)
                        {
                            // Do ：基本类型直接转换成string 
                            if (field.PropertyType.IsPrimitive)
                            {
                                if (fv.ToString() == "0")
                                {
                                    f.SetValue(obj, null);
                                }
                                else
                                {
                                    f.SetValue(obj, fv.ToString());
                                }

                            }
                            else
                            {
                                // Do ：递归非基本类型实例化赋值 
                                var n = Activator.CreateInstance(f.PropertyType);
                                n.CopyFromObjDeep(fv);
                                f.SetValue(obj, n);
                            }
                        }
                        else
                        {
                            f.SetValue(obj, null);
                        }
                    }
                }
                else
                {
                    f.SetValue(obj, field.GetValue(v));
                }
            }
        }


        /// <summary> 序列化 </summary>
        public static string SerializeXml(this object o)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(o.GetType());
            try
            {
                //序列化对象
                xml.Serialize(Stream, o);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        /// <summary> 反序列化 </summary>
        /// <param name="type"> 类型 </param>
        /// <param name="xml"> XML字符串 </param>
        /// <returns> 结果 </returns>
        public static T SerializeDe<T>(this string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    return (T)xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

       
    }
}
