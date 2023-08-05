// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class CalendarKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(CalendarKeys), "S.Calendar.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(CalendarKeys), "S.Calendar.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(CalendarKeys), "S.Calendar.Accent");
    }
}
