// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class TextBlockKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Default");
        public static ComponentResourceKey Icon => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Icon");
        public static ComponentResourceKey Title => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Title");
        public static ComponentResourceKey Binding => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Binding");

        public static ComponentResourceKey FontSize18 => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.FontSize.18");
        public static ComponentResourceKey FontSize16 => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.FontSize.16");
        public static ComponentResourceKey FontSize14 => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.FontSize.14");

        public static ComponentResourceKey AnimationFontSize => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.FontSize");
        public static ComponentResourceKey AnimationFontSizeBounceEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.FontSize.BounceEase");
        public static ComponentResourceKey AnimationFontSizeBackEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.FontSize.BackEase");
        public static ComponentResourceKey AnimationFontSizeElasticEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.FontSize.ElasticEase");
        public static ComponentResourceKey AnimationOpacity => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Opacity");
        public static ComponentResourceKey AnimationOpacityBounceEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Opacity.BounceEase");
        public static ComponentResourceKey AnimationOpacityBackEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Opacity.BackEase");
        public static ComponentResourceKey AnimationOpacityCubicEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Opacity.CubicEase");
        public static ComponentResourceKey ColorAnimation => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Opacity.ElasticEase");
        public static ComponentResourceKey AnimationOpacityElasticEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Opacity.BounceEase");
        public static ComponentResourceKey AnimationColor => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Color");
        public static ComponentResourceKey AnimationColorElasticEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Color.ElasticEase");
        public static ComponentResourceKey AnimationColorCubicEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Color.CubicEase");
        public static ComponentResourceKey AnimationColorBounceEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Color.BounceEase");
        public static ComponentResourceKey AnimationColorBackEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Color.BackEase");
        public static ComponentResourceKey AnimationAngle => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Angle");
        public static ComponentResourceKey AnimationAngleBounceEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Angle.BounceEase");
        public static ComponentResourceKey AnimationAngleBackEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Angle.BackEase");
        public static ComponentResourceKey AnimationAngleElasticEase => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Angle.ElasticEase");

        public static ComponentResourceKey AnimationTranslateX => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Translate.X");
        public static ComponentResourceKey AnimationTranslateXToLeft => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Translate.X.ToLeft");
        public static ComponentResourceKey AnimationTranslateXToRight => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Translate.X.ToRight");
        public static ComponentResourceKey AnimationTranslateY => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Translate.Y");
        public static ComponentResourceKey AnimationTranslateYToBottom => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Translate.Y.ToBottom");
        public static ComponentResourceKey AnimationTranslateYToTop => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Animation.Translate.Y.ToTop");

    }
}
