// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class GroupBoxKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Default");
        public static ComponentResourceKey Close => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Close");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Single");
        public static ComponentResourceKey CloseSingle => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Single.Close");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Accent");
        public static ComponentResourceKey CloseAccent => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Accent.Close");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Clear");
        public static ComponentResourceKey System => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.System");
        public static ComponentResourceKey UnderLine => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.UnderLine");
        public static ComponentResourceKey Title => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.Title");
        public static ComponentResourceKey ShowCode => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.ShowCode");
        public static ComponentResourceKey HeaderBottom => new ComponentResourceKey(typeof(GroupBoxKeys), "S.GroupBox.HeaderBottom");
        public static ComponentResourceKey ToolBar => new ComponentResourceKey(typeof(GroupBoxKeys), "S.Group.ToolBar");
    }
}
