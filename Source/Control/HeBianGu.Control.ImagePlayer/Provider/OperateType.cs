using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary> 图片操作模式 </summary>
    public enum OperateType
    {
        /// <summary> 默认样式 </summary>
        Default=0,
        /// <summary> 标记框 </summary>
        Sign,
        /// <summary> 标记框放大 </summary>
        Enlarge,
        /// <summary> 气泡放大 </summary>
        Bubble,
             /// <summary> 气泡放大 </summary>
        None

    }
}
