// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ImageKeys
    {
        public static ComponentResourceKey Logo => new ComponentResourceKey(typeof(ImageKeys), "S.Image.Logo");
        public static ComponentResourceKey Material_Silky => new ComponentResourceKey(typeof(ImageKeys), "S.Image.Material.Silky");
        public static ComponentResourceKey Material_Silky1 => new ComponentResourceKey(typeof(ImageKeys), "S.Image.Material.Silky1");
    }

    public class ImageSourceKeys
    {
        public static ComponentResourceKey Logo => new ComponentResourceKey(typeof(ImageSourceKeys), "S.ImageSource.Logo");
        //public static ComponentResourceKey Material_Silky => new ComponentResourceKey(typeof(ImageSourceKeys), "S.Image.Material.Silky");
        //public static ComponentResourceKey Material_Silky1 => new ComponentResourceKey(typeof(ImageSourceKeys), "S.Image.Material.Silky1");
    }
}
