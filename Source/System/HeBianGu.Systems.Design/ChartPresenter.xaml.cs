// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Chart2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace HeBianGu.Systems.Design
{
    public class PieDesignPresenter : PiePresenter, IDesignPresenter, IDisplayDoublesData
    {
    
    }

    public class RadarDesignPresenter : RadarPresenter, IDesignPresenter, IDisplayDoublesData
    {
      
    }
    [Displayer(Name = "柱状图", Icon = Icons.Clock)]
    public class BarDesignPresenter : LineDesignPresenter, IDesignPresenter, IDisplayDoublesData
    {
        public BarDesignPresenter()
        {

        }
        public BarDesignPresenter(IEnumerable<double> data) : base(data)
        {

        }
    }

    
    public class LineDesignPresenter : LinePresenter, IDesignPresenter, IDisplayDoublesData
    {
        public LineDesignPresenter()
        {

        }
        public LineDesignPresenter(IEnumerable<double> data) : base(data)
        {

        }
    }
}
