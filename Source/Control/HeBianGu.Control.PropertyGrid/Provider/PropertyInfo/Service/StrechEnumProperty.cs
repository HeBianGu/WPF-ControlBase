using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HeBianGu.Control.PropertyGrid
{
    public class StrechEnumProperty: EnumProperty<Stretch>
    {

    }


    public class MyEnumProperty : EnumProperty<MyEnum>
    {

    }
    public enum MyEnum
    {
        选择输出=0,不选择数据
    }
}
