// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows.Media;

namespace HeBianGu.Control.Shape
{
    public class RunwayShape : ShapeBase
    {
        protected override Geometry CreateGeometry()
        {
            return GeometryFactory.CreateRunway(this.Width,this.Height);
        }
    }
}
