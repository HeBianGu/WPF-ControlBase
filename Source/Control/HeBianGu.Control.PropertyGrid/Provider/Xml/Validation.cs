using HeBianGu.Common.LocalConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Control.PropertyGrid
{

    public class PropertyRotes
    {
        public List<Property> Propertys { get; set; } = new List<Property>();
    }

    public static class PropertyService
    {
        static XmlProvider xmlProvider = new XmlProvider(); 
        public static Property GetProperty(this PropertyRotes source,string id)
        {
          return  source.Propertys.FirstOrDefault(l=>l.Id==id);
        }

        public static Property GetProperty(this string file, string id)
        {
           var rote= xmlProvider.Load<PropertyRotes>(file);

            return rote.GetProperty(id);
        }

        public static List<Validation> GetValidations(this Property source)
        {
            List<Validation> result = new List<Validation>();
            
            if(source.Range!=null)
                result.Add(source.Range);
            if (source.Required != null)
                result.Add(source.Required);
            if (source.RegularExpression != null)
                result.Add(source.RegularExpression);
            if (source.MaxLength != null)
                result.Add(source.MaxLength);

            return result;
        }


        public static UnitGroup GetUnitGroup(this UnitGroups source, string id)
        {
            return source.FirstOrDefault(l => l.Id == id);
        }

        public static UnitGroup GetUnitGroup(this string file, string id)
        {
            var rote = xmlProvider.Load<UnitGroups>(file);

            return rote.GetUnitGroup(id);
        }
    }

    public class Property:ValidationGroup
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("Default")]
        public string Default { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Desc")]
        public string Desc { get; set; }

        [XmlAttribute("ItemSource")]
        public string ItemSource { get; set; }

        [XmlAttribute("Index")]
        public string Index { get; set; }

        [XmlAttribute("Group")]
        public string Group { get; set; }

        public ValidationGroup ItemValidations { get; set; }
    }

    public class ValidationGroup
    {
        public Range Range { get; set; }

        public Required Required { get; set; }

        public RegularExpression RegularExpression { get; set; }

        public MaxLength MaxLength { get; set; }

        protected List<Validation> GetValidations()
        {
            List<Validation> result = new List<Validation>();

            if (this.Range != null)
                result.Add(this.Range);
            if (this.Required != null)
                result.Add(this.Required);
            if (this.RegularExpression != null)
                result.Add(this.RegularExpression);
            if (this.MaxLength != null)
                result.Add(this.MaxLength);

            return result;
        }

        public bool IsValid(string value, string display, out string error)
        {
            error = null;

            List<Validation> result = this.GetValidations();

            foreach (var r in result)
            {
                if (!r.IsValid(value))
                {
                    error = r.ErrorMessage ?? r.FormatErrorMessage(display);

                    //  Do ：保存实体验证失败
                    return false;
                }
            }

            return true;
        }
    }

    //public enum DataType
    //{
    //    String=0,Double,Int,Bool,StringArray,DoubleArray, IntArray, BoolArray
    //}

    public abstract class Validation
    {
        [XmlAttribute("Error")]
        public string ErrorMessage { get; set; }

        public abstract bool IsValid(object obj);


        public virtual string FormatErrorMessage(string message)
        {
           return string.Format("{0}参数不合法", message);
        }
    }

    public class Range : Validation
    {
        [XmlAttribute("Min")]
        public double Min { get; set; }

        [XmlAttribute("Max")]
        public double Max { get; set; }

        public override bool IsValid(object obj)
        {
            if (obj == null) return false;

             if(double.TryParse(obj.ToString(), out double r))
            {
                return r <= this.Max && r >= this.Min;
            }

            return false;
        }

        public override string FormatErrorMessage(string message)
        {
            return string.Format($"注:{message} 不在有效范围[{this.Min} - {this.Max}]");
        }
    }


    public class Required : Validation
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }


        public override bool IsValid(object obj)
        {
            return !string.IsNullOrEmpty(obj?.ToString());
        }

        public override string FormatErrorMessage(string message)
        {
            return string.Format($"{message}参数不能为空");
        }
    }

    public class RegularExpression : Validation
    {
        [XmlAttribute("Pattern")]
        public string Pattern { get; set; }


        public override bool IsValid(object obj)
        {
            return Regex.IsMatch(obj?.ToString(), this.Pattern);
        }

        public override string FormatErrorMessage(string message)
        {
            return string.Format($"{message}参数不能匹配");
        }
    }

    public class MaxLength : Validation
    {
        [XmlAttribute("Value")]
        public int Value { get; set; }


        public override bool IsValid(object obj)
        {
            if (obj == null) return false;

            return obj.ToString().Length <= this.Value;
        }

        public override string FormatErrorMessage(string message)
        {
            return string.Format($"{message}参数长度超过最大长度{this.Value}");
        }
    }

    public class DoubleType : Validation
    {
        public override bool IsValid(object obj)
        {
            return double.TryParse(obj?.ToString(),out double d);
        }

        public override string FormatErrorMessage(string message)
        {
            return string.Format($"参数 {message} 不是double的有效参数");
        }
    }
    public class BoolType : Validation
    {
        public override bool IsValid(object obj)
        {
            return bool.TryParse(obj?.ToString(), out bool d);
        }

        public override string FormatErrorMessage(string message)
        {
            return string.Format($"参数 {message} 不是bool的有效参数");
        }
    }

    public class IntType : Validation
    {
        public override bool IsValid(object obj)
        {
            return int.TryParse(obj?.ToString(), out int d);
        }

        public override string FormatErrorMessage(string message)
        {
            return string.Format($"参数 {message} 不是int的有效参数");
        }
    }

}
