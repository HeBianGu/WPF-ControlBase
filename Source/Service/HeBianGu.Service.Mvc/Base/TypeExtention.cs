// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Service.Mvc
{
    public static class TypeExtention
    {
        public static List<Type> GetAllTypeOfMatch(this Assembly assembly, Predicate<Type> match)
        {
            return assembly.GetTypes().Where(l => match(l)).ToList();
        }

        public static Type GetTypeOfMatch(this Assembly assembly, Predicate<Type> match)
        {
            return assembly.GetAllTypeOfMatch(match).FirstOrDefault();
        }

        public static Type GetTypeOfMatch(this IEnumerable<Assembly> assemblys, Predicate<Type> match)
        {
            foreach (Assembly assembly in assemblys)
            {
                Type find = assembly.GetAllTypeOfMatch(match).FirstOrDefault();

                if (find != null) return find;
            }

            return null;

        }



        public static Type GetTypeOfMatch<T>(this Assembly assembly, Predicate<Type> match = null)
        {
            return assembly.GetAllTypeOfMatch(l => typeof(T).IsAssignableFrom(l) && match(l)).FirstOrDefault();
        }

        public static Type GetTypeWithAttribute<TAttribute>(this Assembly assembly, Predicate<TAttribute> match) where TAttribute : Attribute
        {
            Predicate<Type> currentMatch = l =>
              {
                  TAttribute attribute = l.GetCustomAttribute(typeof(TAttribute)) as TAttribute;

                  if (attribute == null) return false;

                  return match(attribute);

              };
            return assembly.GetAllTypeOfMatch(currentMatch).FirstOrDefault();
        }
    }
}
