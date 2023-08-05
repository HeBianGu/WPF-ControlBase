// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Shape
{
    public abstract class ShapeBase : System.Windows.Shapes.Shape
    {
        public ShapeBase()
        {
            this.Width = 100;
            this.Height = 60;
            this.Fill = Application.Current.TryFindResource(BrushKeys.BackgroundDefault) as Brush;
            this.Stroke = Application.Current.TryFindResource(BrushKeys.ForegroundDefault) as Brush;
            this.Stretch = Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return CreateGeometry();
            }
        }

        protected abstract Geometry CreateGeometry();
    }

}
