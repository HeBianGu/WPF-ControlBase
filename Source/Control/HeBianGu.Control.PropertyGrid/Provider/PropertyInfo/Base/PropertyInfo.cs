using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public abstract class PropertyInfo : ValidationProperty
    {
        public override bool IsValidation(Property source)
        {
            return base.IsValidation(source);
        }

        public override void LoadDefault()
        {
            base.LoadDefault();

            this.Text = this.Default;
        }

        public virtual object ConvertToObject(string value)
        {
            return value;
        }
    }
}
