using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
   public static class ElementStoryBoardExtention
    {
        /// <summary> 执行动画 </summary>
        public static void BegionDoubleStoryBoard(this UIElement element,double start,double end,double duration,string propertyName)
        {
            var engine = DoubleStoryboardEngine.Create(start, end, duration, propertyName);

            engine.Start(element);

        }
    }
}
