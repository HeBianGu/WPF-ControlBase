using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Service.MarkupExtension
{
    public class GetDateTimeExtension : GetValueExtensionBase
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DateTime.TryParse(this.Value, out DateTime result))
            {
                return result;
            }
            return DateTime.MinValue;
        }
    }
}
