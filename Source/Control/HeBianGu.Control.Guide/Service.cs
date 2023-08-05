// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace HeBianGu.Control.Guide
{
    public class GuideAdornerService : IGuideAdornerService
    {
        public void HideAdorner(FrameworkElement element)
        {
            UIElement parent = GuideAdorner.GetGuideParent(element);
            if (parent == null) return;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(parent);
            if (layer == null) return;

            //  Do ：移除向导 
            Adorner[] finds = layer.GetAdorners(parent);
            if (finds == null) return;
            foreach (Adorner item in finds)
            {
                if (item is GuideAdorner toolTip)
                {
                    if (toolTip.Child == element)
                    {
                        layer.Remove(toolTip);
                        return;
                    }
                }
            }
        }

        public void ShowAdorner(FrameworkElement element)
        {
            UIElement parent = GuideAdorner.GetGuideParent(element);
            if (parent == null) return;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(parent);
            if (layer == null) return;

            Brush stroke = GuideAdorner.GetGuideStroke(element);
            Brush fill = GuideAdorner.GetGuideFill(element);
            object text = Cattach.GetGuideTitle(element);
            Point offset = GuideAdorner.GetGuideTextOffset(element);

            //  Do ：添加向导
            GuideAdorner adorner = new GuideAdorner(parent, element)
            {
                Stroke = stroke,
                Fill = fill,
                Text = text?.ToString(),
                TextOffset = offset
            };

            layer.Add(adorner);
        }
    }

    public interface IGuideAdornerService
    {
        void ShowAdorner(FrameworkElement element);
        void HideAdorner(FrameworkElement element);
    }

    public class GuideAdornerProxyer : ServiceInstance<IGuideAdornerService>
    {

    }
}
