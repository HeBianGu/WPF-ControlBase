// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Documents;

namespace HeBianGu.Service.Converter
{
    public sealed class ConverterService
    {
        #region - System -

        public static AlternationConverter AlternationConverter => new AlternationConverter();

        public static BooleanToVisibilityConverter BooleanToVisibilityConverter => new BooleanToVisibilityConverter();

        public static BorderGapMaskConverter BorderGapMaskConverter => new BorderGapMaskConverter();

        public static MenuScrollingVisibilityConverter MenuScrollingVisibilityConverter => new MenuScrollingVisibilityConverter();

        public static RibbonScrollButtonVisibilityConverter RibbonScrollButtonVisibilityConverter => new RibbonScrollButtonVisibilityConverter();

        public static RibbonWindowSmallIconConverter RibbonWindowSmallIconConverter => new RibbonWindowSmallIconConverter();

        public static ZoomPercentageConverter ZoomPercentageConverter => new ZoomPercentageConverter();

        #endregion

        #region - File -

        public static FilePathToIconConverter FilePathToIconConverter => new FilePathToIconConverter();

        public static FilePathToImageSourceConverter FilePathToImageSourceConverter => new FilePathToImageSourceConverter();

        #endregion

        public static ExpressionOfParameterConverter ExpressionOfParameterConverter => new ExpressionOfParameterConverter();


    }
}
