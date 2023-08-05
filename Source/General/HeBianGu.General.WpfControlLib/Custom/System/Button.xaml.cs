// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace HeBianGu.General.WpfControlLib
{
    public class ButtonKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Default");
        public static ComponentResourceKey Icon => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Icon");
        public static ComponentResourceKey Label => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Label");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Single");
        public static ComponentResourceKey LabelSingle => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Single.Label");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Accent");
        public static ComponentResourceKey LabelAccent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Accent.Label");
        public static ComponentResourceKey Transparent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Transparent");
        public static ComponentResourceKey Link => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Link");
        public static ComponentResourceKey Circle => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Circle");
        public static ComponentResourceKey CircleTransparent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Circle.Transparent");
        public static ComponentResourceKey AccentBackTransparent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Transparent.AccentBack");
        public static ComponentResourceKey BorderBrushTransparent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Transparent.BorderBrush");
        public static ComponentResourceKey MouseOver => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.MouseOver");
        public static ComponentResourceKey Close => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Close");
        public static ComponentResourceKey CloseFlag => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Close.Flag");
        public static ComponentResourceKey MinimizeWindow => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Window.Minimize");
        public static ComponentResourceKey MaximizeWindow => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Window.Maximize");
        public static ComponentResourceKey RestoreWindow => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Window.Restore");
        public static ComponentResourceKey HideWindow => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Window.Hide");
        public static ComponentResourceKey CloseWindow => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Window.Close");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Clear");
        public static ComponentResourceKey AddTransparent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Add.Transparent");
        public static ComponentResourceKey AddTransparentBorder => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Add.Border");
        public static ComponentResourceKey Delete => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Delete");
        public static ComponentResourceKey Edit => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Edit");
        public static ComponentResourceKey Detial => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Detial");
        public static ComponentResourceKey Add => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Add");
        public static ComponentResourceKey Set => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Set");
        public static ComponentResourceKey CloseMouseOver => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Close.MouseOver");
        public static ComponentResourceKey CloseTransparent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Close.Transparent");
        public static ComponentResourceKey Left => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Left");
        public static ComponentResourceKey Right => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Right");
        public static ComponentResourceKey Start => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Start");
        public static ComponentResourceKey Stop => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Stop");
        public static ComponentResourceKey VerticalToolBar => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Vertical.ToolBar");
        public static ComponentResourceKey SetTransparent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Transparent.Set");
        public static ComponentResourceKey AddCircle => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Add.Circle");
        public static ComponentResourceKey DeleteCircle => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Delete.Circle");
        public static ComponentResourceKey SumitCircle => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Sumit.Circle");
        public static ComponentResourceKey CloseCircle => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Close.Circle");


    }


    public static partial class Extention
    {
        public static void UseButton(this IApplicationBuilder service, Action<ButtonSetting> action = null)
        {
            action?.Invoke(ButtonSetting.Instance);
            SystemSetting.Instance?.Add(ButtonSetting.Instance);
        }
    }

    [Displayer(Name = "按钮", GroupName = SystemSetting.GroupControl)]
    public class ButtonSetting : LazySettingInstance<ButtonSetting>
    {
        private EffectType _effectType;
        [Display(Name = "Effect")]
        [DefaultValue(EffectType.None)]
        public EffectType EffectType
        {
            get { return _effectType; }
            set
            {
                _effectType = value;
                RaisePropertyChanged();
            }
        }

        private double _minWidth = 100;
        [Display(Name = "最小宽度")]
        [DefaultValue(100.0)]
        public double MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
                RaisePropertyChanged();
            }
        }


        //private double? _mouseOverOpacity = 0.2;
        ///// <summary> 说明  </summary>
        //public double? MouseOverOpactiy
        //{
        //    get { return _mouseOverOpacity; }
        //    set
        //    {
        //        _mouseOverOpacity = value;
        //        RaisePropertyChanged();
        //    }
        //}

    }




}
