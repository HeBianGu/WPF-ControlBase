// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{

    public partial class ProgressBarKeys
    {

        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Dynamic");

        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Accent");

        public static ComponentResourceKey Corner => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Corner");
        public static ComponentResourceKey Buzy => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Buzy");
        public static ComponentResourceKey RunningPoint => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.RunningPoint");

        public static ComponentResourceKey RunningWaitting => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.RunningWaitting");
        public static ComponentResourceKey WaittingPercent => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.WaittingPercent");
        public static ComponentResourceKey CriclePercent => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.CriclePercent");
    }
}
