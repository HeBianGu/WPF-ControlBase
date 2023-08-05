// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public static class BrushKeys
    {
        #region - Background -
        public static ComponentResourceKey BackgroundDefault => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBackground.Default");

        public static ComponentResourceKey BackgroundRowIndex => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.RowIndex.BackGround");

        #endregion

        public static ComponentResourceKey ForegroundDefault => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Default");
        public static ComponentResourceKey ForegroundTitle => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Title");

        #region - Foreground -

        public static ComponentResourceKey ForegroundMouseOver => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.MouseOver");
        public static ComponentResourceKey ForegroundSelected => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Selected");
        public static ComponentResourceKey ForegroundAssist => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Assist");
        public static ComponentResourceKey ForegroundLink => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Link");
        public static ComponentResourceKey ForegroundWhite => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White");

        public static ComponentResourceKey ForegroundWhiteOpacity9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.9");
        public static ComponentResourceKey ForegroundWhiteOpacity8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.8");
        public static ComponentResourceKey ForegroundWhiteOpacity7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.7");
        public static ComponentResourceKey ForegroundWhiteOpacity6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.6");
        public static ComponentResourceKey ForegroundWhiteOpacity5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.5");
        public static ComponentResourceKey ForegroundWhiteOpacity4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.4");
        public static ComponentResourceKey ForegroundWhiteOpacity3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.3");
        public static ComponentResourceKey ForegroundWhiteOpacity2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.2");
        public static ComponentResourceKey ForegroundWhiteOpacity1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.1");

        #endregion

        #region - BorderBrush -

        public static ComponentResourceKey BorderBrushDefault => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Default");
        public static ComponentResourceKey BorderBrushTitle => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Title");
        public static ComponentResourceKey BorderBrushAssist => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Assist");

        #endregion


        #region - Accent -
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent");

        [AccentKey]
        public static ComponentResourceKey AccentMouseOverBackground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.MouseOver.Background");
        [AccentKey]
        public static ComponentResourceKey AccentMouseOverForeground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.MouseOver");

        [AccentKey]
        public static ComponentResourceKey AccentSelectBackground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Select.Background");
        [AccentKey]
        public static ComponentResourceKey AccentSelectForeground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Select");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.9");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.8");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.7");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.6");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.5");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.4");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.3");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.2");
        [AccentKey]
        public static ComponentResourceKey AccentOpacity1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent.Opacity.1");
        #endregion 

        #region - Dark - 
        public static ComponentResourceKey Dark10 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.10");
        public static ComponentResourceKey Dark9_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.9.5");
        public static ComponentResourceKey Dark9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.9");
        public static ComponentResourceKey Dark8_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.8.5");
        public static ComponentResourceKey Dark8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.8");
        public static ComponentResourceKey Dark7_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.7.5");
        public static ComponentResourceKey Dark7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.7");
        public static ComponentResourceKey Dark6_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.6.5");
        public static ComponentResourceKey Dark6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.6");
        public static ComponentResourceKey Dark5_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.5.5");
        public static ComponentResourceKey Dark5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.5");
        public static ComponentResourceKey Dark4_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.4.5");
        public static ComponentResourceKey Dark4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.4");
        public static ComponentResourceKey Dark3_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.3.5");
        public static ComponentResourceKey Dark3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.3");
        public static ComponentResourceKey Dark2_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.2.5");
        public static ComponentResourceKey Dark2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.2");
        public static ComponentResourceKey Dark1_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.1.5");
        public static ComponentResourceKey Dark1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.1");
        public static ComponentResourceKey Dark0_9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.9");
        public static ComponentResourceKey Dark0_8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.8");
        public static ComponentResourceKey Dark0_7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.7");
        public static ComponentResourceKey Dark0_6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.6");
        public static ComponentResourceKey Dark0_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.5");
        public static ComponentResourceKey Dark0_4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.4");
        public static ComponentResourceKey Dark0_3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.3");
        public static ComponentResourceKey Dark0_2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.2");
        public static ComponentResourceKey Dark0_1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.1");
        public static ComponentResourceKey Dark0 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0");
        #endregion 

        #region - Color -
        public static ComponentResourceKey LightGray => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightGray.Notice");
        public static ComponentResourceKey LightGrayOpacity5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightGray.NoticeOpacity.5");
        public static ComponentResourceKey Gray => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Gray.Notice");
        public static ComponentResourceKey GrayOpacity5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Gray.Notice.Opacity.5,");
        public static ComponentResourceKey Black => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Black.Notice");
        public static ComponentResourceKey Orange => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Orange.Notice");
        public static ComponentResourceKey Red => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Red.Notice");
        public static ComponentResourceKey Green => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Green.Notice");
        public static ComponentResourceKey Yellow => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Yellow.Notice");
        public static ComponentResourceKey Blue => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Blue.Notice");
        public static ComponentResourceKey Purple => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Purple.Notice");
        public static ComponentResourceKey Brown => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Brown.Notice");
        public static ComponentResourceKey LightBlue => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightBlue.Notice");
        public static ComponentResourceKey Pink => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Pink.Notice");

        public static ComponentResourceKey White => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Notice");
        public static ComponentResourceKey WhiteOpactiy9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.9");
        public static ComponentResourceKey WhiteOpactiy8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.8");
        public static ComponentResourceKey WhiteOpactiy7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.7");
        public static ComponentResourceKey WhiteOpactiy6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.6");
        public static ComponentResourceKey WhiteOpactiy5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.5");
        public static ComponentResourceKey WhiteOpactiy4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.4");
        public static ComponentResourceKey WhiteOpactiy3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.3");
        public static ComponentResourceKey WhiteOpactiy2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.2");
        public static ComponentResourceKey WhiteOpactiy1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.1");

        #endregion

        #region - System -
        public static ComponentResourceKey DialogCover => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dialog.Cover");

        #endregion

        public static ComponentResourceKey Tranparent => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Tranparent");
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AccentKeyAttribute : Attribute
    {

    }



    public static class OpacityKeys
    {
        public static ComponentResourceKey Watermark => new ComponentResourceKey(typeof(OpacityKeys), "S.Opacity.Watermark");
        public static ComponentResourceKey Disable => new ComponentResourceKey(typeof(OpacityKeys), "S.Opacity.Disable");
        public static ComponentResourceKey Readonly => new ComponentResourceKey(typeof(OpacityKeys), "S.Opacity.Readonly");
        public static ComponentResourceKey MouseOver => new ComponentResourceKey(typeof(OpacityKeys), "S.Opacity.MouseOver");
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

        public static ComponentResourceKey Circle => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Circle");
        public static ComponentResourceKey CircleLeft => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Circle.Left");
        public static ComponentResourceKey CircleRight => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Circle.Right");
        public static ComponentResourceKey CircleTop => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Circle.Top");
        public static ComponentResourceKey CircleBottom => new ComponentResourceKey(typeof(CornerRadiusKeys), "S.Window.Item.CornerRadius.Circle.Bottom");
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
        public static ComponentResourceKey DockRight => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.DockRight");
        public static ComponentResourceKey DockTop => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.DockTop");
        public static ComponentResourceKey DockBottom => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.DockBottom");
        public static ComponentResourceKey DockLeft => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.DockLeft");

        public static ComponentResourceKey UnderLine => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.UnderLine");
        public static ComponentResourceKey TopLine => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.TopLine");
        public static ComponentResourceKey LeftLine => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.LeftLine");
        public static ComponentResourceKey RightLine => new ComponentResourceKey(typeof(ThicknessKeys), "S.Thickness.RightLine");

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
        public static ComponentResourceKey DateFormat => new ComponentResourceKey(typeof(SystemKeys), "S.DateFormat.Date");
        public static ComponentResourceKey DateTimeFormat => new ComponentResourceKey(typeof(SystemKeys), "S.DateFormat.Time");
    }

    public static class DrawingKeys
    {

        public static ComponentResourceKey EventError
        {
            get
            {
                return new ComponentResourceKey(typeof(DrawingKeys), "S.Drawing.EventError");
            }
        }
    }

    public static class StoryBoardKeys
    {

        //public static ComponentResourceKey EventError=> new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Window.Load.Opacity");

        public static ComponentResourceKey OpacityForever => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.OpacityForever");
        public static ComponentResourceKey RotateForever => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.RotateForever");
        public static ComponentResourceKey RotateForever2 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.RotateForever.2");
        public static ComponentResourceKey RotateForever3 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.RotateForever.3");
        public static ComponentResourceKey RotateForever4 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.RotateForever.4");
        public static ComponentResourceKey RotateForever5 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.RotateForever.5");
        public static ComponentResourceKey OpacityForever0 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.OpacityForever.0");
        public static ComponentResourceKey ScaleTransformXY => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.ScaleTransform.XY");
        public static ComponentResourceKey ScaleTransformX => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.ScaleTransform.X");
        public static ComponentResourceKey ScaleTransformY => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.ScaleTransform.Y");
        public static ComponentResourceKey CloseOpacity => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Close.Opacity");
        public static ComponentResourceKey LoadScaleXY => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.Scale.XY");
        public static ComponentResourceKey LoadScaleX => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.Scale.X");
        public static ComponentResourceKey LoadScaleY => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.Scale.Y");
        public static ComponentResourceKey LoadOpacityRightToLeft => new ComponentResourceKey(typeof(StoryBoardKeys), ".Storyboard.Load.OpacityRightToLeft");
        public static ComponentResourceKey LoadOpacityLeftToRight => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.OpacityLeftToRight");
        public static ComponentResourceKey LoadOpacityDownToUp => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.OpacityDownToUp");
        public static ComponentResourceKey LoadWaitDownToUp => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.WaitDownToUp");
        public static ComponentResourceKey LoadOpacityUpToDwon => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.OpacityUpToDwon");
        public static ComponentResourceKey LoadFountain => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Load.Fountain");
        public static ComponentResourceKey SlowHide => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.SlowHide");
        public static ComponentResourceKey SlowShow => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.SlowShow");
        public static ComponentResourceKey ScaleRecovery => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Scale.Recovery");
        public static ComponentResourceKey ScaleRecovery_1_2 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Scale.Recovery.1.2");
        public static ComponentResourceKey ScaleRecovery_1_4 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Scale.Recovery.1.4");
        public static ComponentResourceKey ScaleEnlarge_1_2 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Scale.Enlarge.1.2");
        public static ComponentResourceKey ScaleEnlargeRecovery_1_2 => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Scale.Enlarge.1.2.Recovery");
        public static ComponentResourceKey FontSizeRebound => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.FontSize.Rebound");
        public static ComponentResourceKey ColorFlash => new ComponentResourceKey(typeof(StoryBoardKeys), "S.Storyboard.Color.Flash");
    }
}
