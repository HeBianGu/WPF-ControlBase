// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public sealed class XConverter
    {
        public static BooleanToVisibilityConverter BooleanToVisibilityConverter { get; } = new Lazy<BooleanToVisibilityConverter>().Value;
        public static FalseToVisibilityConverter FalseToVisibilityConverter { get; } = new Lazy<FalseToVisibilityConverter>().Value;
        public static VisibilityToBoolenConverter VisibilityToBoolenConverter { get; } = new Lazy<VisibilityToBoolenConverter>().Value;
        public static VisibilityConverter VisibilityConverter { get; } = new Lazy<VisibilityConverter>().Value;
        public static ThicknessToDoubleConverter ThicknessToDoubleConverter { get; } = new Lazy<ThicknessToDoubleConverter>().Value;
        public static PercentToAngleConverter PercentToAngleConverter { get; } = new Lazy<PercentToAngleConverter>().Value;
        public static TrueToFalseConverter TrueToFalseConverter { get; } = new Lazy<TrueToFalseConverter>().Value;
        public static TrueToFalseMultiConverter TrueToFalseMultiConverter { get; } = new Lazy<TrueToFalseMultiConverter>().Value;
        public static PressedSizeConverter PressedSizeConverter { get; } = new Lazy<PressedSizeConverter>().Value;
        public static MouseOverSizeConverter MouseOverSizeConverter { get; } = new Lazy<MouseOverSizeConverter>().Value;
        public static VisibilityEmptyConverter VisibilityEmptyConverter { get; } = new Lazy<VisibilityEmptyConverter>().Value;
        public static VisibilityStringConverter VisibilityStringConverter { get; } = new Lazy<VisibilityStringConverter>().Value;
        public static VisibilityWithOutStringConverter VisibilityWithOutStringConverter { get; } = new Lazy<VisibilityWithOutStringConverter>().Value;
        public static CornerRadiusToDouble CornerRadiusToDouble { get; } = new Lazy<CornerRadiusToDouble>().Value;
        public static ComboBoxAutoSelectionConverter ComboBoxAutoSelectionConverter { get; } = new Lazy<ComboBoxAutoSelectionConverter>().Value;
        public static ColorToBrushConverter ColorToBrushConverter { get; } = new Lazy<ColorToBrushConverter>().Value;
        public static BrushRoundConverter BrushRoundConverter { get; } = new Lazy<BrushRoundConverter>().Value;
        public static MathMultipleConverter MathMultipleConverter { get; } = new Lazy<MathMultipleConverter>().Value;
        public static DrawerOffsetConverter DrawerOffsetConverter { get; } = new Lazy<DrawerOffsetConverter>().Value;
        public static StringSplitFirstConverter StringSplitFirstConverter { get; } = new Lazy<StringSplitFirstConverter>().Value;
        public static StringReplaceConverter StringReplaceConverter { get; } = new Lazy<StringReplaceConverter>().Value;
        public static IsEqualConverter IsEqualConverter { get; } = new Lazy<IsEqualConverter>().Value;
        public static IsMultiValueEqualConverter IsMultiValueEqualConverter { get; } = new Lazy<IsMultiValueEqualConverter>().Value;
        public static ByteToImageSourceConverter ByteToImageSourceConverter { get; } = new Lazy<ByteToImageSourceConverter>().Value;
        public static Number2PercentageConverter Number2PercentageConverter { get; } = new Lazy<Number2PercentageConverter>().Value;
        public static IsLastItemInContainerConverter IsLastItemInContainerConverter { get; } = new Lazy<IsLastItemInContainerConverter>().Value;
        public static CollpseLastItemInContainerConverter CollpseLastItemInContainerConverter { get; } = new Lazy<CollpseLastItemInContainerConverter>().Value;
        public static IsFirstItemInContainerConverter IsFirstItemInContainerConverter { get; } = new Lazy<IsFirstItemInContainerConverter>().Value;
        public static ItemPanelConverter ItemPanelConverter { get; } = new Lazy<ItemPanelConverter>().Value;
        public static CircleProgressBarConverter CircleProgressBarConverter { get; } = new Lazy<CircleProgressBarConverter>().Value;
        public static DoubleRoundConverter DoubleRoundConverter { get; } = new Lazy<DoubleRoundConverter>().Value;
        public static OpacityProgressBarConverter OpacityProgressBarConverter { get; } = new Lazy<OpacityProgressBarConverter>().Value;
        public static MultiComboboxSelectConverter MultiComboboxSelectConverter { get; } = new Lazy<MultiComboboxSelectConverter>().Value;
        public static VisibilityContainWithOutStringConverter VisibilityContainWithOutStringConverter { get; } = new Lazy<VisibilityContainWithOutStringConverter>().Value;
        public static VisibilityContainWithStringConverter VisibilityContainWithStringConverter { get; } = new Lazy<VisibilityContainWithStringConverter>().Value;
        public static MathMultiplicationConverter MathMultiplicationConverter { get; } = new Lazy<MathMultiplicationConverter>().Value;
        public static MathAdditionConverter MathAdditionConverter { get; } = new Lazy<MathAdditionConverter>().Value;

        public static MathMultipleConverter MathAddConverter { get; } = new MathMultipleConverter() { Operation = MathOperation.Add };
        public static MathMultipleConverter MathSubtractConverter { get; } = new MathMultipleConverter() { Operation = MathOperation.Subtract };
        public static MathMultipleConverter MathMultiplyConverter { get; } = new MathMultipleConverter() { Operation = MathOperation.Multiply };
        public static MathMultipleConverter MathDivideConverter { get; } = new MathMultipleConverter() { Operation = MathOperation.Divide };


        public static MillisecondsTimeSpanConverter MillisecondsTimeSpanConverter { get; } = new Lazy<MillisecondsTimeSpanConverter>().Value;
        public static TicksToTimeSpanDisplayConverter TicksToTimeSpanDisplay { get; } = new Lazy<TicksToTimeSpanDisplayConverter>().Value;

        public static TimeSpanSplitPointConverter TimeSpanSplitPointConverter { get; } = new Lazy<TimeSpanSplitPointConverter>().Value;
        public static NullOrEmptyTofalseConverter NullOrEmptyTofalseConverter { get; } = new Lazy<NullOrEmptyTofalseConverter>().Value;
        public static IconToImageSourceConverter IconToImageSourceConverter { get; } = new Lazy<IconToImageSourceConverter>().Value;
        public static BoolAndConverter BoolAndConverter { get; } = new Lazy<BoolAndConverter>().Value;
        public static BoolOrConverter BoolOrConverter { get; } = new Lazy<BoolOrConverter>().Value;
        public static NullToEmptyConverter NullToEmptyConverter { get; } = new Lazy<NullToEmptyConverter>().Value;
        public static ByteSizeDisplayConverter ByteSizeDisplayConverter { get; } = new Lazy<ByteSizeDisplayConverter>().Value;
        public static GetTypeDisplayConverter GetTypeDisplayConverter { get; } = new Lazy<GetTypeDisplayConverter>().Value;
        public static ToTypeConverter GetTypeConverter { get; } = new Lazy<ToTypeConverter>().Value;
        public static IsAssignableFromConverter IsAssignableFromConverter { get; } = new Lazy<IsAssignableFromConverter>().Value;
        public static FindResourceConverter FindResourceConverter { get; } = new Lazy<FindResourceConverter>().Value;
        public static DoubleTextConverter DoubleTextConverter { get; } = new Lazy<DoubleTextConverter>().Value;
        public static WriteLineObjectConverter WriteLineObjectConverter { get; } = new Lazy<WriteLineObjectConverter>().Value;
        public static DisplayMapConverter DisplayMapConverter { get; } = new Lazy<DisplayMapConverter>().Value;
        public static StringToImageSourceConverter StringToImageSourceConverter { get; } = new Lazy<StringToImageSourceConverter>().Value;
        public static IntToSexConverter IntToSexConverter { get; } = new Lazy<IntToSexConverter>().Value;
        public static DateTimeToAgeConverter DateTimeToAgeConverter { get; } = new Lazy<DateTimeToAgeConverter>().Value;
        public static BoolNullToBoolConverter BoolNullToBoolConverter { get; } = new Lazy<BoolNullToBoolConverter>().Value;
        public static GetDirectoryNameConverter GetDirectoryNameConverter { get; } = new Lazy<GetDirectoryNameConverter>().Value;
        public static GetFileNameWithoutExtensionConverter GetFileNameWithoutExtensionConverter { get; } = new Lazy<GetFileNameWithoutExtensionConverter>().Value;
        public static GetExtensionConverter GetExtensionConverter { get; } = new Lazy<GetExtensionConverter>().Value;
        public static SecondsToTimeSpanDisplayConverter SecondsToTimeSpanDisplayConverter { get; } = new Lazy<SecondsToTimeSpanDisplayConverter>().Value;
        public static DateTimeToStringConverter DateTimeToStringConverter { get; } = new Lazy<DateTimeToStringConverter>().Value;
        public static LessThanConverter LessThan { get; } = new Lazy<LessThanConverter>().Value;
        public static GreaterThanConverter GreaterThan { get; } = new Lazy<GreaterThanConverter>().Value;
        public static ComparaConverter Compara { get; } = new Lazy<ComparaConverter>().Value;
        public static GetFileLengthDisplayConverter GetFileLengthDisplay { get; } = new Lazy<GetFileLengthDisplayConverter>().Value;
        public static NullToCollapsedConverter NullToCollapsed { get; } = new Lazy<NullToCollapsedConverter>().Value;
    }
}
