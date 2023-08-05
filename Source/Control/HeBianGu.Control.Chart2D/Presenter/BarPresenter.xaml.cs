// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;

namespace HeBianGu.Control.Chart2D
{
    [Displayer(Name = "柱状图", Icon = "\xe7e6")]
    public class BarPresenter : LinePresenter
    {
        public BarPresenter()
        {

        }
        public BarPresenter(IEnumerable<double> data) : base(data)
        {

        }
    }
}
