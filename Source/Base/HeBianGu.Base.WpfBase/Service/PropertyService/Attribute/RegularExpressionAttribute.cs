//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace HeBianGu.Base.WpfBase
//{
//    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
//    public class RegularExpressionAttribute : ValidationAttribute
//    {
//        Regex _regex;

//        public RegularExpressionAttribute(string pattern)
//        {
//            _regex = new Regex(pattern);

//            Pattern = pattern;

//            this.ErrorMessage = "数据不匹配";
//        }

//        public string Pattern { get; }


//        public override bool IsValid(object value)
//        {
//            if (value == null) return false;

//            return _regex.IsMatch(value.ToString());
//        }
//    }
//}
