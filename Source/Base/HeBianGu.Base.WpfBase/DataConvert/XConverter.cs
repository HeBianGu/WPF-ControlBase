using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public sealed class XConverter
    {
        public static BooleanToVisibilityConverter BooleanToVisibilityConverter
        {
            get { return new BooleanToVisibilityConverter(); }
        }

        public static FalseToVisibilityConverter FalseToVisibilityConverter
        {
            get { return new FalseToVisibilityConverter(); }
        }

        public static VisibilityToBoolenConverter VisibilityToBoolenConverter
        {
            get { return new VisibilityToBoolenConverter(); }
        }

        public static VisibilityConverter VisibilityConverter
        {
            get { return new VisibilityConverter(); }
        }


        
        //public static BackgroundToForegroundConverter BackgroundToForegroundConverter
        //{
        //    get { return Singleton<BackgroundToForegroundConverter>.GetInstance(); }
        //}


        public static ThicknessToDoubleConverter ThicknessToDoubleConverter
        {
            get { return new ThicknessToDoubleConverter(); }
        }
        public static PercentToAngleConverter PercentToAngleConverter
        {
            get { return new PercentToAngleConverter(); }
        }

        public static TrueToFalseConverter TrueToFalseConverter
        {
            get { return new TrueToFalseConverter(); }
        }

        public static TrueToFalseMultiConverter TrueToFalseMultiConverter
        {
            get { return new TrueToFalseMultiConverter(); }
        }
        

        public static PressedSizeConverter PressedSizeConverter
        {
            get { return new PressedSizeConverter(); }
        }

        public static MouseOverSizeConverter MouseOverSizeConverter
        {
            get { return new MouseOverSizeConverter(); }
        }

        public static VisibilityEmptyConverter VisibilityEmptyConverter
        {
            get { return new VisibilityEmptyConverter(); }
        }
        public static VisibilityStringConverter VisibilityStringConverter
        {
            get { return new VisibilityStringConverter(); }
        }
        public static VisibilityWithOutStringConverter VisibilityWithOutStringConverter
        {
            get { return new VisibilityWithOutStringConverter(); }
        }

        
        public static CornerRadiusToDouble CornerRadiusToDouble
        {
            get { return new CornerRadiusToDouble(); }
        }


        public static ComboBoxAutoSelectionConverter ComboBoxAutoSelectionConverter
        {
            get { return new ComboBoxAutoSelectionConverter(); }
        }


        public static ColorToBrushConverter ColorToBrushConverter
        {
            get { return new ColorToBrushConverter(); }
        }

        public static BrushRoundConverter BrushRoundConverter
        {
            get { return new BrushRoundConverter(); }
        }
        public static MathMultipleConverter MathMultipleConverter
        {
            get { return new MathMultipleConverter(); }
        }

        public static DrawerOffsetConverter DrawerOffsetConverter
        {
            get { return new DrawerOffsetConverter(); }
        }

        public static StringSplitFirstConverter StringSplitFirstConverter
        {
            get { return new StringSplitFirstConverter(); }
        }

        public static StringReplaceConverter StringReplaceConverter
        {
            get { return new StringReplaceConverter(); }
        }
        public static IsEqualConverter IsEqualConverter
        {
            get { return new IsEqualConverter(); }
        }

        public static IsMultiValueEqualConverter IsMultiValueEqualConverter
        {
            get { return new IsMultiValueEqualConverter(); }
        }


        public static ByteToImageSourceConverter ByteToImageSourceConverter
        {
            get { return new ByteToImageSourceConverter(); }
        }

        public static Number2PercentageConverter Number2PercentageConverter
        {
            get { return new Number2PercentageConverter(); }
        }

        public static IsLastItemInContainerConverter IsLastItemInContainerConverter
        {
            get { return new IsLastItemInContainerConverter(); }
        }

        public static IsFirstItemInContainerConverter IsFirstItemInContainerConverter
        {
            get { return new IsFirstItemInContainerConverter(); }
        }


        public static ItemPanelConverter ItemPanelConverter
        {
            get { return new ItemPanelConverter(); }
        }
        

        public static CircleProgressBarConverter CircleProgressBarConverter
        {
            get { return new CircleProgressBarConverter(); }
        }

        public static DoubleRoundConverter DoubleRoundConverter
        {
            get { return new DoubleRoundConverter(); }
        }

        public static OpacityProgressBarConverter OpacityProgressBarConverter
        {
            get { return new OpacityProgressBarConverter(); }
        }


        public static MultiComboboxSelectConverter MultiComboboxSelectConverter
        {
            get { return new MultiComboboxSelectConverter(); }
        }

        public static VisibilityContainWithOutStringConverter VisibilityContainWithOutStringConverter
        {
            get { return new VisibilityContainWithOutStringConverter(); }
        }
        public static VisibilityContainWithStringConverter VisibilityContainWithStringConverter
        {
            get { return new VisibilityContainWithStringConverter(); }
        }

        public static MathMultiplicationConverter MathMultiplicationConverter
        {
            get { return new MathMultiplicationConverter(); }
        }

        public static MathAdditionConverter MathAdditionConverter
        {
            get { return new MathAdditionConverter(); }
        }

        public static TimeSpanConverter TimeSpanConverter
        {
            get { return new TimeSpanConverter(); }
        }
        public static TimeSpanSplitPointConverter TimeSpanSplitPointConverter
        {
            get { return new TimeSpanSplitPointConverter(); }
        }

        public static NullOrEmptyTofalseConverter NullOrEmptyTofalseConverter
        {
            get { return new NullOrEmptyTofalseConverter(); }
        }

        public static IconToImageSourceConverter IconToImageSourceConverter
        {
            get { return new IconToImageSourceConverter(); }
        }

        public static BoolAndConverter BoolAndConverter
        {
            get { return new BoolAndConverter(); }
        }

        public static BoolOrConverter BoolOrConverter
        {
            get { return new BoolOrConverter(); }
        }

        public static NullToEmptyConverter NullToEmptyConverter
        {
            get { return new NullToEmptyConverter(); }
        }

        public static ByteSizeDisplayConverter ByteSizeDisplayConverter
        {
            get { return new ByteSizeDisplayConverter(); }
        }
        
        //public static SelectTextConverter SelectTextConverter
        //{
        //    get { return Singleton<SelectTextConverter>.GetInstance(); }
        //}

        //public static DataVisibleTextConverter DataVisibleTextConverter
        //{
        //    get { return Singleton<DataVisibleTextConverter>.GetInstance(); }
        //}

        //public static DataVisibleTextConverterFalse DataVisibleTextConverterFalse
        //{
        //    get { return Singleton<DataVisibleTextConverterFalse>.GetInstance(); }
        //}

        //public static PressedSizeConverter PressedSizeConverter
        //{
        //    get { return Singleton<PressedSizeConverter>.GetInstance(); }
        //}


        //public static PassWordConverter PassWordConverter
        //{
        //    get { return Singleton<PassWordConverter>.GetInstance(); }
        //}


    }
}
