// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class GroupItemKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(GroupItemKeys), "S.GroupItem.Default");
        public static ComponentResourceKey GroupTitle => new ComponentResourceKey(typeof(GroupItemKeys), "S.GroupItem.Group.Title");
        public static ComponentResourceKey Transparent => new ComponentResourceKey(typeof(GroupItemKeys), "S.GroupItem.Transparent");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(GroupItemKeys), "S.GroupItem.Clear");
    }
}
