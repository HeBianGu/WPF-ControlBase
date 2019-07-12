using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
   public sealed class DisplayAttribute : Attribute
    { 
        public DisplayAttribute(string positionalString)
        {
            this.Name = positionalString;
             
        }

        public string Name
        {
            get;
        } 
    }
}
