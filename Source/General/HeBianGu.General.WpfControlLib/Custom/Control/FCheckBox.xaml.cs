﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// FCheckBox.xaml 的交互逻辑
    /// </summary>
    [Obsolete("废弃不用了  直接用 CheckBox")]
    public partial class FCheckBox : CheckBox
    {
        static FCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FCheckBox), new FrameworkPropertyMetadata(typeof(FCheckBox)));
        }
    }
}
