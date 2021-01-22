using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary>
    ///矩形图集合
    /// </summary>
   public class RectangleLayer : Collection<IRectangleStroke>
    {
        /// <summary>
        /// 集合图形是否可见
        /// </summary>
        public bool IsVisible
        {
            set
            {
                foreach (var item in this)
                {
                    item.Visibility = value?System.Windows.Visibility.Visible:System.Windows.Visibility.Collapsed;
                }
            }
        }
    }
}
