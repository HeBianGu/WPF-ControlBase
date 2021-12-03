using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    [TemplatePart(Name = "PART_ContentBorder", Type = typeof(Border))]
    [TemplatePart(Name = "PART_RootGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_ContentGrid", Type = typeof(Grid))]
    public class ToggleSwitch : ToggleButton
    {
        #region Fields

        Grid rootGrid = null;
        Border contentBorder = null;
        Grid contentGrid = null;
        double contentBorderMargin;

        #endregion

        #region Constants

        private const double DEFAULT_THUMB_WIDTH = 40.0;
        private const double MIN_THUMB_WIDTH = 10.0;
        private const double MAX_THUMB_WIDTH = 90.0;

        #endregion

        #region Dependency Properties

        #region CheckedText

        /// <summary>
        /// CheckedText Dependency Property
        /// </summary>
        public static readonly DependencyProperty CheckedTextProperty =
            DependencyProperty.Register("CheckedText", typeof(string), typeof(ToggleSwitch),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the CheckedText property. This dependency property 
        /// indicates the on text.
        /// </summary>
        public string CheckedText
        {
            get { return (string)GetValue(CheckedTextProperty); }
            set { SetValue(CheckedTextProperty, value); }
        }

        #endregion

        #region CheckedBackground

        /// <summary>
        /// CheckedBackground Dependency Property
        /// </summary>
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(ToggleSwitch),
                new PropertyMetadata(Brushes.White));

        /// <summary>
        /// Gets or sets the CheckedBackground property. This dependency property 
        /// indicates Background of the Checked Text.
        /// </summary>
        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        #endregion

        #region CheckedForeground

        /// <summary>
        /// CheckedForeground Dependency Property
        /// </summary>
        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.Register("CheckedForeground", typeof(Brush), typeof(ToggleSwitch),
                new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets or sets the CheckedForeground property. This dependency property 
        /// indicates Foreground of the Checked Text.
        /// </summary>
        public Brush CheckedForeground
        {
            get { return (Brush)GetValue(CheckedForegroundProperty); }
            set { SetValue(CheckedForegroundProperty, value); }
        }

        #endregion

        #region CornerRadius

        /// <summary>
        /// CornerRadius Dependency Property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ToggleSwitch),
                new PropertyMetadata(new CornerRadius()));

        /// <summary>
        /// Gets or sets the CornerRadius property. This dependency property 
        /// indicates the corner radius of the outer most border of the ToggleSwitch.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        #endregion

        #region TargetColumnInternal

        /// <summary>
        /// TargetColumnInternal Dependency Property
        /// </summary>
        public static readonly DependencyProperty TargetColumnInternalProperty =
            DependencyProperty.Register("TargetColumnInternal", typeof(int), typeof(ToggleSwitch),
                new FrameworkPropertyMetadata((new PropertyChangedCallback(OnTargetColumnInternalChanged))));

        /// <summary>
        /// Gets or sets the TargetColumnInternal property. This dependency property 
        /// indicates the target column to which the contentborder moves when the control is in unchecked state.
        /// This property is used internally.
        /// </summary>
        public int TargetColumnInternal
        {
            get { return (int)GetValue(TargetColumnInternalProperty); }
            set { SetValue(TargetColumnInternalProperty, value); }
        }

        /// <summary>
        /// Handles changes to the TargetColumnInternal property.
        /// </summary>
        /// <param name="d">ToggleSwitch</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnTargetColumnInternalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToggleSwitch ts = (ToggleSwitch)d;
            int oldTargetColumnInternal = (int)e.OldValue;
            int newTargetColumnInternal = ts.TargetColumnInternal;
            ts.OnTargetColumnInternalChanged(oldTargetColumnInternal, newTargetColumnInternal);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the TargetColumnInternal property.
        /// </summary>
        /// <param name="oldTargetColumnInternal">Old Value</param>
        /// <param name="newTargetColumnInternal">New Value</param>
        protected virtual void OnTargetColumnInternalChanged(int oldTargetColumnInternal, int newTargetColumnInternal)
        {

        }

        #endregion

        #region ThumbBackground

        /// <summary>
        /// ThumbBackground Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThumbBackgroundProperty =
            DependencyProperty.Register("ThumbBackground", typeof(Brush), typeof(ToggleSwitch),
                new PropertyMetadata((Brushes.Black)));

        /// <summary>
        /// Gets or sets the ThumbBackground property. This dependency property 
        /// indicates the Background of the Thumb.
        /// </summary>
        public Brush ThumbBackground
        {
            get { return (Brush)GetValue(ThumbBackgroundProperty); }
            set { SetValue(ThumbBackgroundProperty, value); }
        }

        #endregion

        #region ThumbBorderBrush

        /// <summary>
        /// ThumbBorderBrush Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThumbBorderBrushProperty =
            DependencyProperty.Register("ThumbBorderBrush", typeof(Brush), typeof(ToggleSwitch),
                new PropertyMetadata(Brushes.Gray));

        /// <summary>
        /// Gets or sets the ThumbBorderBrush property. This dependency property 
        /// indicates the BorderBrush of the Thumb.
        /// </summary>
        public Brush ThumbBorderBrush
        {
            get { return (Brush)GetValue(ThumbBorderBrushProperty); }
            set { SetValue(ThumbBorderBrushProperty, value); }
        }

        #endregion

        #region ThumbBorderThickness

        /// <summary>
        /// ThumbBorderThickness Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThumbBorderThicknessProperty =
            DependencyProperty.Register("ThumbBorderThickness", typeof(Thickness), typeof(ToggleSwitch),
                new PropertyMetadata(new Thickness()));

        /// <summary>
        /// Gets or sets the ThumbBorderThickness property. This dependency property 
        /// indicates the BorderThickness of the Thumb.
        /// </summary>
        public Thickness ThumbBorderThickness
        {
            get { return (Thickness)GetValue(ThumbBorderThicknessProperty); }
            set { SetValue(ThumbBorderThicknessProperty, value); }
        }

        #endregion

        #region ThumbCornerRadius

        /// <summary>
        /// ThumbCornerRadius Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThumbCornerRadiusProperty =
            DependencyProperty.Register("ThumbCornerRadius", typeof(CornerRadius), typeof(ToggleSwitch),
                new PropertyMetadata(new CornerRadius()));

        /// <summary>
        /// Gets or sets the ThumbCornerRadius property. This dependency property 
        /// indicates the corner radius of the Thumb.
        /// </summary>
        public CornerRadius ThumbCornerRadius
        {
            get { return (CornerRadius)GetValue(ThumbCornerRadiusProperty); }
            set { SetValue(ThumbCornerRadiusProperty, value); }
        }

        #endregion

        #region ThumbGlowColor

        /// <summary>
        /// ThumbGlowColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThumbGlowColorProperty =
            DependencyProperty.Register("ThumbGlowColor", typeof(Color), typeof(ToggleSwitch),
                new PropertyMetadata(Colors.LawnGreen));

        /// <summary>
        /// Gets or sets the ThumbGlowColor property. This dependency property 
        /// indicates the GlowColor of the Thumb.
        /// </summary>
        public Color ThumbGlowColor
        {
            get { return (Color)GetValue(ThumbGlowColorProperty); }
            set { SetValue(ThumbGlowColorProperty, value); }
        }

        #endregion

        #region ThumbShineCornerRadius

        /// <summary>
        /// ThumbShineCornerRadius Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThumbShineCornerRadiusProperty =
            DependencyProperty.Register("ThumbShineCornerRadius", typeof(CornerRadius), typeof(ToggleSwitch),
                new FrameworkPropertyMetadata(new CornerRadius()));

        /// <summary>
        /// Gets or sets the ThumbShineCornerRadius property. This dependency property 
        /// indicates the corner radius of the shine over the thumb.
        /// </summary>
        public CornerRadius ThumbShineCornerRadius
        {
            get { return (CornerRadius)GetValue(ThumbShineCornerRadiusProperty); }
            set { SetValue(ThumbShineCornerRadiusProperty, value); }
        }

        #endregion

        #region ThumbWidth

        /// <summary>
        /// ThumbWidth Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThumbWidthProperty =
            DependencyProperty.Register("ThumbWidth", typeof(double), typeof(ToggleSwitch),
                new FrameworkPropertyMetadata(DEFAULT_THUMB_WIDTH,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnThumbWidthChanged),
                    new CoerceValueCallback(CoerceThumbWidth)));

        /// <summary>
        /// Gets or sets the ThumbWidth property. This dependency property 
        /// indicates the width  of the Thumb as a percentage of the total width of the ToggleSwitch.
        /// </summary>
        public double ThumbWidth
        {
            get { return (double)GetValue(ThumbWidthProperty); }
            set { SetValue(ThumbWidthProperty, value); }
        }

        /// <summary>
        /// Coerces the Thumb Width to an acceptable value
        /// </summary>
        /// <param name="d">Dependency Object</param>
        /// <param name="value">Value</param>
        /// <returns>Coerced Value</returns>
        private static object CoerceThumbWidth(DependencyObject d, object value)
        {
            double percentage = (double)value;

            if (percentage < MIN_THUMB_WIDTH)
            {
                return MIN_THUMB_WIDTH;
            }

            if (percentage > MAX_THUMB_WIDTH)
            {
                return MAX_THUMB_WIDTH;
            }

            return percentage;
        }

        /// <summary>
        /// Handles changes to the ThumbWidth property.
        /// </summary>
        /// <param name="d">ToggleSwitch</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnThumbWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToggleSwitch ts = (ToggleSwitch)d;
            double oldThumbWidth = (double)e.OldValue;
            double newThumbWidth = ts.ThumbWidth;
            ts.OnThumbWidthChanged(oldThumbWidth, newThumbWidth);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the ThumbWidth property.
        /// </summary>
        /// <param name="oldThumbWidth">Old Value</param>
        /// <param name="newThumbWidth">New Value</param>
        protected virtual void OnThumbWidthChanged(double oldThumbWidth, double newThumbWidth)
        {
            CalculateLayout();
        }

        #endregion

        #region UncheckedBackground

        /// <summary>
        /// UncheckedBackground Dependency Property
        /// </summary>
        public static readonly DependencyProperty UncheckedBackgroundProperty =
            DependencyProperty.Register("UncheckedBackground", typeof(Brush), typeof(ToggleSwitch),
                new PropertyMetadata(Brushes.White));

        /// <summary>
        /// Gets or sets the UncheckedBackground property. This dependency property 
        /// indicates the Background of the Unchecked Text.
        /// </summary>
        public Brush UncheckedBackground
        {
            get { return (Brush)GetValue(UncheckedBackgroundProperty); }
            set { SetValue(UncheckedBackgroundProperty, value); }
        }

        #endregion

        #region UncheckedForeground

        /// <summary>
        /// UncheckedForeground Dependency Property
        /// </summary>
        public static readonly DependencyProperty UncheckedForegroundProperty =
            DependencyProperty.Register("UncheckedForeground", typeof(Brush), typeof(ToggleSwitch),
                new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets or sets the UncheckedForeground property. This dependency property 
        /// indicates the Foreground of the Unchecked Text.
        /// </summary>
        public Brush UncheckedForeground
        {
            get { return (Brush)GetValue(UncheckedForegroundProperty); }
            set { SetValue(UncheckedForegroundProperty, value); }
        }

        #endregion

        #region UncheckedText

        /// <summary>
        /// UncheckedText Dependency Property
        /// </summary>
        public static readonly DependencyProperty UncheckedTextProperty =
            DependencyProperty.Register("UncheckedText", typeof(string), typeof(ToggleSwitch),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the UncheckedText property. This dependency property 
        /// indicates the off text.
        /// </summary>
        public string UncheckedText
        {
            get { return (string)GetValue(UncheckedTextProperty); }
            set { SetValue(UncheckedTextProperty, value); }
        }

        #endregion

        #endregion

        #region Construction

        //static ToggleSwitch()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        //}

        #endregion

        #region Overrides

        /// <summary>
        /// Override which is called when the template is applied
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Get all the controls in the template
            GetTemplateParts();

            // Calculate the layout of the ToggleSwitch contents
            CalculateLayout();
        }

        /// <summary>
        /// Handler for the event raised when the size of the ToggleSwitch changes
        /// </summary>
        /// <param name="sizeInfo">Size Changed Info</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            // Calculate the margin of the content border
            CalculateContentBorderMargin();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the required controls in the template
        /// </summary>
        protected void GetTemplateParts()
        {
            // PART_RootGrid
            rootGrid = GetChildControl<Grid>("PART_RootGrid");
            // PART_ContentBorder
            contentBorder = GetChildControl<Border>("PART_ContentBorder");
            // PART_ContentGrid
            contentGrid = GetChildControl<Grid>("PART_ContentGrid");
        }

        /// <summary>
        /// Generic method to get a control from the template
        /// </summary>
        /// <typeparam name="T">Type of the control</typeparam>
        /// <param name="ctrlName">Name of the control in the template</param>
        /// <returns>Control</returns>
        protected T GetChildControl<T>(string ctrlName) where T : DependencyObject
        {
            T ctrl = GetTemplateChild(ctrlName) as T;
            return ctrl;
        }

        /// <summary>
        /// Calculates the width and margin of the contents of the ContentBorder
        /// The following calculation is used: (Here p is the value of ThumbWidth)
        /// p = [10, 90]
        /// Margin = 1 - p
        /// Left = (1 - p)/(2 - p)
        /// Right = (1 - p)/(2 - p)
        /// Center = 0.03
        /// CenterLeft = 0.485 - Left
        /// CenterRight = 0.485 - Left
        /// </summary>
        private void CalculateLayout()
        {
            if ((rootGrid == null) || (contentGrid == null))
                return;

            // Convert the ThumbWidth value to a percentage
            double thumbPercentage = ThumbWidth / 100.0;
            // Calculate the percentage of Total width available for the content
            double contentPercentage = 1 - thumbPercentage;

            if (thumbPercentage <= 0.5)
            {
                // Calculate the width of the RootGrid Columns
                rootGrid.ColumnDefinitions[0].Width = new GridLength(thumbPercentage, GridUnitType.Star);
                rootGrid.ColumnDefinitions[1].Width = new GridLength(1.0 - (2 * thumbPercentage), GridUnitType.Star);
                rootGrid.ColumnDefinitions[2].Width = new GridLength(thumbPercentage, GridUnitType.Star);

                // Adjust the thumb
                TargetColumnInternal = 2;
                Grid.SetColumnSpan(contentBorder, 1);
            }
            else
            {
                // Calculate the width of the RootGrid Columns
                rootGrid.ColumnDefinitions[0].Width = new GridLength(contentPercentage, GridUnitType.Star);
                rootGrid.ColumnDefinitions[1].Width = new GridLength(1.0 - (2 * contentPercentage), GridUnitType.Star);
                rootGrid.ColumnDefinitions[2].Width = new GridLength(contentPercentage, GridUnitType.Star);

                // Adjust the thumb
                TargetColumnInternal = 1;
                Grid.SetColumnSpan(contentBorder, 2);
            }

            // Calculate the width of the ContentGrid Columns
            double leftRight = (1 - thumbPercentage) / (2 - thumbPercentage);
            double centerLeftRight = 0.485 - leftRight;
            double center = 0.03;
            contentGrid.ColumnDefinitions[0].Width = new GridLength(leftRight, GridUnitType.Star);
            contentGrid.ColumnDefinitions[1].Width = new GridLength(centerLeftRight, GridUnitType.Star);
            contentGrid.ColumnDefinitions[2].Width = new GridLength(center, GridUnitType.Star);
            contentGrid.ColumnDefinitions[3].Width = new GridLength(centerLeftRight, GridUnitType.Star);
            contentGrid.ColumnDefinitions[4].Width = new GridLength(leftRight, GridUnitType.Star);

            contentBorderMargin = contentPercentage;

            CalculateContentBorderMargin();

            InvalidateVisual();
        }

        /// <summary>
        /// Calculates the margin of the contentBorder
        /// </summary>
        private void CalculateContentBorderMargin()
        {
            if (contentBorder != null)
            {
                // Change the margin of the content border so that its size is (1 + contentBorderMargin) times the width of
                // the Toggle switch

                if (double.IsNaN(this.Width) || double.IsNaN(this.Height)) return;

                contentBorder.Margin = new Thickness(-(this.Width * contentBorderMargin), 0, -(this.Width * contentBorderMargin), 0);
            }
        }

        #endregion
    }

    public class ClipBorder : Border
    {
        #region Fields

        private Geometry clipRect = null;
        private object oldClip;

        #endregion

        #region Overrides

        protected override void OnRender(DrawingContext dc)
        {
            OnApplyChildClip();
            base.OnRender(dc);
        }

        public override UIElement Child
        {
            get
            {
                return base.Child;
            }
            set
            {
                if (this.Child != value)
                {
                    if (this.Child != null)
                    {
                        // Restore original clipping of the old child
                        this.Child.SetValue(UIElement.ClipProperty, oldClip);
                    }

                    if (value != null)
                    {
                        // Store the current clipping of the new child
                        oldClip = value.ReadLocalValue(UIElement.ClipProperty);
                    }
                    else
                    {
                        // If we dont set it to null we could leak a Geometry object
                        oldClip = null;
                    }

                    base.Child = value;
                }
            }
        }

        #endregion 

        #region Helpers

        protected virtual void OnApplyChildClip()
        {
            UIElement child = this.Child;
            if (child != null)
            {
                // Get the geometry of a rounded rectangle border based on the BorderThickness and CornerRadius
                clipRect = GeometryHelper.GetRoundRectangle(new Rect(Child.RenderSize), this.BorderThickness, this.CornerRadius);
                child.Clip = clipRect;
            }
        }

        #endregion
    }

    public static class GeometryHelper
    {
        /// <summary>
        /// Gets the RoundRectangle Geometry for a rectangle having a
        /// given thickness and corner radius.
        /// </summary>
        /// <param name="baseRect">Base Rectangle</param>
        /// <param name="thickness">Border thickness</param>
        /// <param name="cornerRadius">CornerRadius</param>
        /// <returns>Geometry</returns>
        public static Geometry GetRoundRectangle(Rect baseRect, Thickness thickness, CornerRadius cornerRadius)
        {
            // Normalizing the corner radius
            if (cornerRadius.TopLeft < Double.Epsilon)
                cornerRadius.TopLeft = 0.0;
            if (cornerRadius.TopRight < Double.Epsilon)
                cornerRadius.TopRight = 0.0;
            if (cornerRadius.BottomLeft < Double.Epsilon)
                cornerRadius.BottomLeft = 0.0;
            if (cornerRadius.BottomRight < Double.Epsilon)
                cornerRadius.BottomRight = 0.0;

            // Taking the border thickness into account
            double leftHalf = thickness.Left * 0.5;
            if (leftHalf < Double.Epsilon)
                leftHalf = 0.0;
            double topHalf = thickness.Top * 0.5;
            if (topHalf < Double.Epsilon)
                topHalf = 0.0;
            double rightHalf = thickness.Right * 0.5;
            if (rightHalf < Double.Epsilon)
                rightHalf = 0.0;
            double bottomHalf = thickness.Bottom * 0.5;
            if (bottomHalf < Double.Epsilon)
                bottomHalf = 0.0;

            //baseRect = new Rect(baseRect.Location.X, baseRect.Location.Y, baseRect.Width + leftHalf + rightHalf, baseRect.Height + topHalf + bottomHalf);

            double tolerance = baseRect.Height < baseRect.Width ? baseRect.Height / baseRect.Width : baseRect.Width / baseRect.Height;

            // Create the rectangles for the corners that needs to be curved in the base rectangle
            // TopLeft Rectangle
            Rect topLeftRect = new Rect(baseRect.Location.X - tolerance,
                                        baseRect.Location.Y - tolerance,
                                        Math.Max(0.0, cornerRadius.TopLeft - leftHalf),
                                        Math.Max(0.0, cornerRadius.TopLeft - topHalf));
            // TopRight Rectangle
            Rect topRightRect = new Rect(baseRect.Location.X + baseRect.Width - cornerRadius.TopRight + rightHalf + tolerance,
                                         baseRect.Location.Y - tolerance,
                                         Math.Max(0.0, cornerRadius.TopRight - rightHalf),
                                         Math.Max(0.0, cornerRadius.TopRight - topHalf));
            // BottomRight Rectangle
            Rect bottomRightRect = new Rect(baseRect.Location.X + baseRect.Width - cornerRadius.BottomRight + rightHalf + tolerance,
                                            baseRect.Location.Y + baseRect.Height - cornerRadius.BottomRight + bottomHalf + tolerance,
                                            Math.Max(0.0, cornerRadius.BottomRight - rightHalf),
                                            Math.Max(0.0, cornerRadius.BottomRight - bottomHalf));
            // BottomLeft Rectangle
            Rect bottomLeftRect = new Rect(baseRect.Location.X - tolerance,
                                           baseRect.Location.Y + baseRect.Height - cornerRadius.BottomLeft + bottomHalf + tolerance,
                                           Math.Max(0.0, cornerRadius.BottomLeft - leftHalf),
                                           Math.Max(0.0, cornerRadius.BottomLeft - bottomHalf));

            // Adjust the width of the TopLeft and TopRight rectangles so that they are proportional to the width of the baseRect
            if (topLeftRect.Right > topRightRect.Left)
            {
                double newWidth = (topLeftRect.Width / (topLeftRect.Width + topRightRect.Width)) * baseRect.Width;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, newWidth, topLeftRect.Height);
                topRightRect = new Rect(baseRect.Left + newWidth, topRightRect.Location.Y, Math.Max(0.0, baseRect.Width - newWidth), topRightRect.Height);
            }

            // Adjust the height of the TopRight and BottomRight rectangles so that they are proportional to the height of the baseRect
            if (topRightRect.Bottom > bottomRightRect.Top)
            {
                double newHeight = (topRightRect.Height / (topRightRect.Height + bottomRightRect.Height)) * baseRect.Height;
                topRightRect = new Rect(topRightRect.Location.X, topRightRect.Location.Y, topRightRect.Width, newHeight);
                bottomRightRect = new Rect(bottomRightRect.Location.X, baseRect.Top + newHeight, bottomRightRect.Width, Math.Max(0.0, baseRect.Height - newHeight));
            }

            // Adjust the width of the BottomLeft and BottomRight rectangles so that they are proportional to the width of the baseRect
            if (bottomRightRect.Left < bottomLeftRect.Right)
            {
                double newWidth = (bottomLeftRect.Width / (bottomLeftRect.Width + bottomRightRect.Width)) * baseRect.Width;
                bottomLeftRect = new Rect(bottomLeftRect.Location.X, bottomLeftRect.Location.Y, newWidth, bottomLeftRect.Height);
                bottomRightRect = new Rect(baseRect.Left + newWidth, bottomRightRect.Location.Y, Math.Max(0.0, baseRect.Width - newWidth), bottomRightRect.Height);
            }

            // Adjust the height of the TopLeft and BottomLeft rectangles so that they are proportional to the height of the baseRect
            if (bottomLeftRect.Top < topLeftRect.Bottom)
            {
                double newHeight = (topLeftRect.Height / (topLeftRect.Height + bottomLeftRect.Height)) * baseRect.Height;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, topLeftRect.Width, newHeight);
                bottomLeftRect = new Rect(bottomLeftRect.Location.X, baseRect.Top + newHeight, bottomLeftRect.Width, Math.Max(0.0, baseRect.Height - newHeight));
            }

            StreamGeometry roundedRectGeometry = new StreamGeometry();

            using (StreamGeometryContext context = roundedRectGeometry.Open())
            {
                // Begin from the Bottom of the TopLeft Arc and proceed clockwise
                context.BeginFigure(topLeftRect.BottomLeft, true, true);
                // TopLeft Arc
                context.ArcTo(topLeftRect.TopRight, topLeftRect.Size, 0, false, SweepDirection.Clockwise, true, true);
                // Top Line
                context.LineTo(topRightRect.TopLeft, true, true);
                // TopRight Arc
                context.ArcTo(topRightRect.BottomRight, topRightRect.Size, 0, false, SweepDirection.Clockwise, true, true);
                // Right Line
                context.LineTo(bottomRightRect.TopRight, true, true);
                // BottomRight Arc
                context.ArcTo(bottomRightRect.BottomLeft, bottomRightRect.Size, 0, false, SweepDirection.Clockwise, true, true);
                // Bottom Line
                context.LineTo(bottomLeftRect.BottomRight, true, true);
                // BottomLeft Arc
                context.ArcTo(bottomLeftRect.TopLeft, bottomLeftRect.Size, 0, false, SweepDirection.Clockwise, true, true);

                context.Close();
            }

            return roundedRectGeometry;
        }
    }
}
