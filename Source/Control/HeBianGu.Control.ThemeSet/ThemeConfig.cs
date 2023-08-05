// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows.Media;

namespace HeBianGu.Control.ThemeSet
{
    public class ThemeConfig : LazySettingInstance<ThemeConfig>
    {
        public Color AccentColor { get; set; }

        //public Color ForegroundColor { get; set; }

        public FontSize FontSize { get; set; }

        public double ItemCornerRadius { get; set; }

        public double ItemHeight { get; set; }

        public double ItemWidth { get; set; }

        public Language Language { get; set; }

        //public double LargeFontSize { get; set; }

        public double DefaultFontSize { get; set; }

        public double RowHeight { get; set; }

        //public double SmallFontSize { get; set; }

        public ThemeType ThemeType { get; set; }

        public int AnimalSpeed { get; set; }

        public int AccentColorSelectType { get; set; }

        public bool IsUseAnimal { get; set; }

        public int Version { get; set; }

        public AccentBrushType AccentBrushType { get; set; }
    }
}