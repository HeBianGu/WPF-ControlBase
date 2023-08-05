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
        public static ComponentResourceKey HandyDynamic => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Handy.Dynamic");
        public static ComponentResourceKey Handy => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Handy");
        public static ComponentResourceKey HandySingle => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Handy.Single");
        public static ComponentResourceKey HandyAccent => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Handy.Accent");
        public static ComponentResourceKey HandyStripe => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Handy.Stripe");

        public static ComponentResourceKey LabelHandy => new ComponentResourceKey(typeof(ProgressBarKeys), "S.ProgressBar.Handy.Label");
    }
}
