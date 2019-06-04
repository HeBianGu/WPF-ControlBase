#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/21 18:35:28 
 * 文件名：XConverter 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
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
