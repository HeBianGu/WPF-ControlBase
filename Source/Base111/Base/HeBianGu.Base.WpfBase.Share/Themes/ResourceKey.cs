using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public static class BrushKeys
    {

        #region - Background -
        public static ComponentResourceKey BackgroundDefaultKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBackground.Default");

        public static ComponentResourceKey BackgroundRowIndexKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.RowIndex.BackGround");

        #endregion

        public static ComponentResourceKey ForegroundDefaultKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Default");
        public static ComponentResourceKey ForegroundTitleKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Title");

        #region - Foreground -

        public static ComponentResourceKey ForegroundMouseOverKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.MouseOver");
        public static ComponentResourceKey ForegroundSelectedKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Selected");
        public static ComponentResourceKey ForegroundAssistKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Assist");
        public static ComponentResourceKey ForegroundLinkKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Link");
        public static ComponentResourceKey ForegroundWhiteKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White");

        public static ComponentResourceKey ForegroundWhiteOpacity9Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.9");
        public static ComponentResourceKey ForegroundWhiteOpacity8Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.8");
        public static ComponentResourceKey ForegroundWhiteOpacity7Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.7");
        public static ComponentResourceKey ForegroundWhiteOpacity6Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.6");
        public static ComponentResourceKey ForegroundWhiteOpacity5Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.5");
        public static ComponentResourceKey ForegroundWhiteOpacity4Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.4");
        public static ComponentResourceKey ForegroundWhiteOpacity3Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.3");
        public static ComponentResourceKey ForegroundWhiteOpacity2Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.2");
        public static ComponentResourceKey ForegroundWhiteOpacity1Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.1");

        #endregion

        #region - BorderBrush -

        public static ComponentResourceKey BorderBrushDefaultKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Default");
        public static ComponentResourceKey BorderBrushTitleKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Title");
        public static ComponentResourceKey BorderBrushAssistKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Assist");

        #endregion


        #region - Accent -
        public static ComponentResourceKey AccentKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent");

        public static ComponentResourceKey AccentMouseOverBackgroundKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.MouseOver.Background");
        public static ComponentResourceKey AccentMouseOverForegroundKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.MouseOver");
        public static ComponentResourceKey AccentOpacity9Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.9");
        public static ComponentResourceKey AccentOpacity8Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.8");
        public static ComponentResourceKey AccentOpacity7Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.7");
        public static ComponentResourceKey AccentOpacity6Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.6");

        public static ComponentResourceKey AccentOpacity5Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.5");
        public static ComponentResourceKey AccentOpacity4Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.4");
        public static ComponentResourceKey AccentOpacity3Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.3");

        public static ComponentResourceKey AccentOpacity2Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.2");
        public static ComponentResourceKey AccentOpacity1Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.1");
        #endregion


        #region - Dark - 
        public static ComponentResourceKey Dark10Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.10");
        public static ComponentResourceKey Dark9Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.9");
        public static ComponentResourceKey Dark8Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.8");
        public static ComponentResourceKey Dark7Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.7");
        public static ComponentResourceKey Dark6Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.6");
        public static ComponentResourceKey Dark5Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.5");
        public static ComponentResourceKey Dark4Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.4");
        public static ComponentResourceKey Dark3Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.3");
        public static ComponentResourceKey Dark2Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.2");
        public static ComponentResourceKey Dark1Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.1");

        #endregion


        #region - Color -
        public static ComponentResourceKey LightGrayKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightGray.Notice");
        public static ComponentResourceKey LightGrayOpacity5Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightGray.NoticeOpacity.5");
        public static ComponentResourceKey GrayKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Gray.Notice");
        public static ComponentResourceKey GrayOpacity5Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Gray.Notice.Opacity.5,");
        public static ComponentResourceKey BlackKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Black.Notice");
        public static ComponentResourceKey OrangeKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Orange.Notice");
        public static ComponentResourceKey RedKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Red.Notice");
        public static ComponentResourceKey GreenKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Green.Notice");
        public static ComponentResourceKey WhiteKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Notice");
        public static ComponentResourceKey WhiteOpactiy9Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.9");
        public static ComponentResourceKey WhiteOpactiy8Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.8");
        public static ComponentResourceKey WhiteOpactiy7Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.7");
        public static ComponentResourceKey WhiteOpactiy6Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.6");
        public static ComponentResourceKey WhiteOpactiy5Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.5");
        public static ComponentResourceKey WhiteOpactiy4Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.4");
        public static ComponentResourceKey WhiteOpactiy3Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.3");
        public static ComponentResourceKey WhiteOpactiy2Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.2");
        public static ComponentResourceKey WhiteOpactiy1Key => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.1");

        #endregion

        #region - System -
        public static ComponentResourceKey DialogCoverKey => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dialog.Cover");

        #endregion 
    }

    public static class OpacityKeys
    {
        public static ComponentResourceKey Watermark => new ComponentResourceKey(typeof(OpacityKeys), "S.Opacity.Watermark");

        public static ComponentResourceKey Disable => new ComponentResourceKey(typeof(OpacityKeys), "S.Opacity.Disable");

        public static ComponentResourceKey Readonly => new ComponentResourceKey(typeof(OpacityKeys), "S.Opacity.Readonly");

    }


    public static class FontSizeKeys
    {
        public static ComponentResourceKey Header => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header");
        public static ComponentResourceKey Header1 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.1");
        public static ComponentResourceKey Header2 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.2");
        public static ComponentResourceKey Header3 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.3");
        public static ComponentResourceKey Header4 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.4");
        public static ComponentResourceKey Header5 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.5");
        public static ComponentResourceKey Header6 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.6");
        public static ComponentResourceKey Header7 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.7");
        public static ComponentResourceKey Header8 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.8");
        public static ComponentResourceKey Header9 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.9");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Default");

        public static ComponentResourceKey Small => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Small");

        public static ComponentResourceKey Large => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Large");
        public static ComponentResourceKey Fixed => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Fixed");
    }


    public static class CornerRadiusKeys
    {
        public static ComponentResourceKey Circle => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Circle");
        public static ComponentResourceKey Value => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Value");
        public static ComponentResourceKey CornerRadius => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius");
        public static ComponentResourceKey Left => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Left");
        public static ComponentResourceKey Right => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Right");
        public static ComponentResourceKey Top => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Top");
        public static ComponentResourceKey Bottom => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Bottom");
        public static ComponentResourceKey LeftTop => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.LeftTop");
        public static ComponentResourceKey RightTop => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.RightTop");

        public static ComponentResourceKey LeftBottom => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.LeftBottom");
        public static ComponentResourceKey RightBottom => new ComponentResourceKey(typeof(CornerRadiusKeys), ".Window.Item.CornerRadius.RightBottom");
    }


    public static class IconSizeKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(IconSizeKeys), "S.IconSize.Default");
        public static ComponentResourceKey Small => new ComponentResourceKey(typeof(IconSizeKeys), "S.IconSize.Small");
        public static ComponentResourceKey Normal => new ComponentResourceKey(typeof(IconSizeKeys), "S.IconSize.Normal");
        public static ComponentResourceKey Large => new ComponentResourceKey(typeof(IconSizeKeys), "S.IconSize.Large");


    }

    public static class ThicknessKeys
    {
        public static ComponentResourceKey Margin_10_0 => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.Margin.10.0");
        public static ComponentResourceKey Margin_0_10 => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.Margin.0.10");
        public static ComponentResourceKey Margin_20_20 => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.Margin.20.20");
        public static ComponentResourceKey Margin_0_20 => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.Margin.0.20");
        public static ComponentResourceKey Margin_20_0 => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.Margin.20.0");
        public static ComponentResourceKey Padding_10_6 => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.Padding.10.6");
        public static ComponentResourceKey Margin_2_0 => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.Margin.2.0");


    }

    public static class EffectShadowKeys
    {
        public static ComponentResourceKey Color => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.Color.Default");
        public static ComponentResourceKey Window => new ComponentResourceKey(typeof(EffectShadowKeys), "S.DropShadowEffect.Window");
        public static ComponentResourceKey Default1 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.1");
        public static ComponentResourceKey Default2 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.2");
        public static ComponentResourceKey Default3 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.3");
        public static ComponentResourceKey Default4 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.4");
        public static ComponentResourceKey Default5 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.5");

        public static ComponentResourceKey Accent1 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.Accent.1");
        public static ComponentResourceKey Accent2 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.Accent.2");
        public static ComponentResourceKey Accent3 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.Accent.3");
        public static ComponentResourceKey Accent4 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.Accent.4");
        public static ComponentResourceKey Accent5 => new ComponentResourceKey(typeof(EffectShadowKeys), "S.EffectShadow.Accent.5");

    }

    public static class SystemKeys
    {
        public static ComponentResourceKey TransformGroup => new ComponentResourceKey(typeof(SystemKeys), "S.TransformGroup.Default");

        public static ComponentResourceKey FontFamily => new ComponentResourceKey(typeof(SystemKeys), "S.FontFamily.Default");

        public static ComponentResourceKey ColorAccent => new ComponentResourceKey(typeof(SystemKeys), "S.Color.Accent");

        public static ComponentResourceKey ItemHeight => new ComponentResourceKey(typeof(SystemKeys), "S.Window.Item.Height");
        public static ComponentResourceKey ItemWidth => new ComponentResourceKey(typeof(SystemKeys), "S.Window.Item.Width");
        public static ComponentResourceKey TitleWidth => new ComponentResourceKey(typeof(SystemKeys), "S.Window.Title.Width");
        public static ComponentResourceKey RowHeight => new ComponentResourceKey(typeof(SystemKeys), "S.Window.Row.Height");
        public static ComponentResourceKey ColumnWidth => new ComponentResourceKey(typeof(SystemKeys), "S.Window.Column.Width");


        //public static ComponentResourceKey Height
        //{
        //    get
        //    {
        //        return new ComponentResourceKey(typeof(SystemKeys), "Height");
        //    }
        //}

    }

    //public static class BrushSource
    //{ 

    //    public static ComponentResourceKey Height
    //    {
    //        get
    //        {
    //            return new ComponentResourceKey(typeof(BrushSource), "Height");
    //        }
    //    }
    //}

}
