// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class DatePickerKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Dynamic");

        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Default");
        public static ComponentResourceKey Label => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Label");
        public static ComponentResourceKey LabelClear => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.LabelClear");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Clear");

        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Single");
        public static ComponentResourceKey LabelSingle => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Single.Label");
        public static ComponentResourceKey LabelClearSingle => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Single.LabelClear");
        public static ComponentResourceKey ClearSingle => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Single.Clear");

        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Accent");
        public static ComponentResourceKey LabelAccent => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Accent.Label");
        public static ComponentResourceKey LabelClearAccent => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Accent.LabelClear");
        public static ComponentResourceKey ClearAccent => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Accent.Clear");

        public static ComponentResourceKey Circle => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Circle");
        public static ComponentResourceKey LabelCircle => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Circle.Label");
        public static ComponentResourceKey ClearCircle => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Circle.Clear");
        public static ComponentResourceKey LabelClearCircle => new ComponentResourceKey(typeof(DatePickerKeys), "S.DatePicker.Circle.LabelClear");
    }
}
