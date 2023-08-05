// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public partial class ControlTemplateKeys
    {
        public static ComponentResourceKey ControlIcon => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.Control.Icon");
        public static ComponentResourceKey ControlIconBackground => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.Control.Icon.Background");
        public static ComponentResourceKey ContentControlDefault => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.ContentControl.Default");
        public static ComponentResourceKey ContentControlIcon => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.ContentControl.Icon");
        public static ComponentResourceKey ContentControlTitle => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.ContentControl.Title");
        public static ComponentResourceKey ContentControlBox => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.ContentControl.Box");
        public static ComponentResourceKey ContentControlItemClose => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.ContentControl.Item.Close");
        public static ComponentResourceKey TextBoxError => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.TextBox.Error");
    }

    public class ShareKeys
    {
        public static ComponentResourceKey ClearTemplate => new ComponentResourceKey(typeof(ShareKeys), "S.Template.Clear");
        public static ComponentResourceKey CloseTemplate => new ComponentResourceKey(typeof(ShareKeys), "S.Template.Close");
        public static ComponentResourceKey TitleTemplate => new ComponentResourceKey(typeof(ShareKeys), "S.Template.Title");
    }
}
