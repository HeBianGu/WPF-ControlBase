using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.DiskWindow
{
    public interface IXh
    {

    }

    public class BJ : IXh
    {
        public ITz Tz { get; set; }
    }

    public class CD : IXh
    {
        public ITz Tz { get; set; }
    }
}
