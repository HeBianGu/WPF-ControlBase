using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Defines an area within which you can explicitly position an infinite number of child elements by using coordinates that are relative to the <see cref="ZoomableCanvas"/> area.
    /// </summary>
    public class ZoomableCanvas : VirtualPanel, IScrollInfo
    {
        #region BoxProperty
        public static readonly DependencyProperty BoxProperty = DependencyProperty.Register("Box", typeof(Rect), typeof(ZoomableCanvas),
                                                            new FrameworkPropertyMetadata(Rect.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoxChanged), IsBoxValid);
        public Rect Box
        {
            get { return (Rect)GetValue(BoxProperty); }
            set { SetValue(BoxProperty, value); }
        }
        private static bool IsBoxValid(object value)
        {
            return true;
        }
        private static void OnBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region ApplyTransformProperty

        /// <summary>
        /// Identifies the <see cref="ApplyTransform"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ApplyTransformProperty = DependencyProperty.Register("ApplyTransform", typeof(bool), typeof(ZoomableCanvas), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange, OnApplyTransformChanged));

        /// <summary>
        /// Returns a transform applying the <see cref="Scale"/> and <see cref="Offset"/> when <see cref="ApplyTransform"/> is set to <c>true</c>.
        /// </summary>
        /// <param name="d">Dependency object whos value is being coerced.</param>
        /// <param name="value">The original uncoerced value.</param>
        /// <returns>A new transform if <see cref="ApplyTransform"/> is set to <c>true</c>; otherwise, <paramref name="value"/>.</returns>
        private static object CoerceRenderTransform(DependencyObject d, object value)
        {
            var canvas = d as ZoomableCanvas;
            if (canvas != null && canvas.ApplyTransform)
            {
                var transform = new TransformGroup();
                transform.Children.Add(new ScaleTransform());
                transform.Children.Add(new TranslateTransform());
                return transform;
            }

            return value;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="ApplyTransform"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnApplyTransformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(RenderTransformProperty);
        }

        /// <summary>
        /// Gets or sets whether to automatically apply a <see cref="ScaleTransform"/> to the canvas.
        /// </summary>
        /// <value><c>true</c> or <c>false</c>.  The default is <see cref="true"/>.</value>
        /// <remarks>
        /// The value of this dependency property is <c>true</c> by default, meaning that the <see cref="RenderTransform"/> property will contain a <see cref="Transform"/> that scales the canvas and its children automatically.
        /// This property can be set to <c>false</c> prevent the automatic transform.  This means that children are responsible for changing their appearance when the <see cref="Scale"/> property changes.
        /// Note that this property does not affect the <b>placement</b> of the elements; the children are automatically placed with the top-left corners of their elements at the appropriate positions on the screen, regardless of the value of <see cref="ApplyTransform"/>.
        /// <para>
        /// Children will usually do this by simply changing their <see cref="Width"/> and <see cref="Height "/> to become larger or smaller when the <see cref="Scale"/> property increases or decreases.
        /// This is useful when pen widths are important, such as an element surrounded with a <see cref="Border"/> with <see cref="Border.BorderThickness"/> set to <c>1.0</c>.
        /// If <see cref="ApplyTransform"/> is <c>true</c>, then as <see cref="Scale"/> decreases the shape will be scaled down and the border stroke will become thinner than one pixel, possibly too thin to see even with sub-pixel rendering.
        /// This is also true when drawing paths, edges of a graph, or any other element that uses <see cref="Pen"/> to draw lines and strokes.
        /// In these cases setting <see cref="ApplyTransform"/> to <c>false</c> and setting the <see cref="Shape"/>'s <see cref="Shape.Stretch"/> to <see cref="Stretch.Fill"/> while binding its <see cref="Width"/> and <see cref="Height"/> to a factor of <see cref="Scale"/> will often provide a better effect.
        /// </para>
        /// <para>
        /// Another reason to set this property to <c>false</c> is when elements change their representation or visual state based on the scale (also known as "semantic zoom").
        /// For example, imagine a canvas showing multiple thumbnails of spreadsheets and the relationships between their formulas and values.
        /// When <see cref="Scale"/> is set to <c>1.0</c> (the default value), each spreadsheet element might be fully interactive, editable, and showing all rows and columns.
        /// When zooming out, and <see cref="Scale"/> gets small enough that there is not enough room for each spreadsheet to show all of its rows and columns, it may change its representation into a bar chart or pie chart with axis values and a legend instead.
        /// When zooming even further out, and <see cref="Scale"/> gets small enough that there is not enough room for the axis and legend, it may simply remove the axis and legend to make more room for the graphical portion of the chart.
        /// Since the children of the canvas can be arbitary rich UIElements, they can dynamically change their representation and be interacted with at all levels of zoom.
        /// This is in sharp contrast to multi-scale-image approaches such as Silverlight's Deep Zoom since those scenarios are simply performing linear scale transformations on pre-computed static bitmaps.
        /// </para>
        /// </remarks>
        public bool ApplyTransform
        {
            get { return (bool)GetValue(ApplyTransformProperty); }
            set { SetValue(ApplyTransformProperty, value); }
        }

        #endregion

        #region ActualViewboxProperty

        /// <summary>
        /// Identifies the <see cref="ActualViewbox"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActualViewboxProperty = DependencyProperty.Register("ActualViewbox", typeof(Rect), typeof(ZoomableCanvas),
                                                                            new FrameworkPropertyMetadata(Rect.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnActualViewboxChanged, CoerceActualViewbox));
       
        /// <summary>
        /// Returns a <see cref="Rect"/> representing the area of the canvas that is currently being displayed.
        /// </summary>
        /// <param name="d">Dependency object whos value is being coerced.</param>
        /// <param name="value">The original uncoerced value.</param>
        /// <returns>A <see cref="Rect"/> representing the area of the canvas (in canvas coordinates) that is being displayed by this panel.</returns>
        private static object CoerceActualViewbox(DependencyObject d, object value)
        {
            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                var offset = canvas.Offset;
                var scale = canvas.Scale;
                var renderSize = canvas.RenderSize;
                value = new Rect(offset.X / scale, offset.Y / scale, renderSize.Width / scale, renderSize.Height / scale);
            }
            //Console.WriteLine("Rectangle = {0}", value.ToString());
            //Console.WriteLine("ViewboxProp = {0}, Viewbox = {1}", (Rect)canvas.GetValue(ViewboxProperty), canvas.Viewbox);
            //Console.WriteLine("ActualViewboxProp = {0}, ActualViewbox = {1}", (Rect)canvas.GetValue(ActualViewboxProperty), canvas.ActualViewbox);
            //d.SetValue(BoxProperty, ((Rect)value).X);
            return value;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="ActualViewbox"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnActualViewboxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                canvas.InvalidateReality();
            }
            var scrollInfo = d as IScrollInfo;
            if (scrollInfo != null && scrollInfo.ScrollOwner != null)
            {
                scrollInfo.ScrollOwner.InvalidateScrollInfo();
            }
            
        }

        /// <summary>
        /// Gets a <see cref="Rect"/> representing the area of the canvas that is currently being displayed by this panel.
        /// </summary>
        /// <value>A <see cref="Rect"/> representing the area of the canvas that is currently being displayed by this panel.</value>
        /// <remarks>
        /// The value of this property is automatically computed based on the <see cref="Scale"/>, <see cref="Offset"/>, and <see cref="RenderSize"/> of this panel.
        /// It is independent (and usually different) from the <see cref="Viewbox"/> dependency property.
        /// </remarks>
        /// <seealso cref="Viewbox"/>
        public Rect ActualViewbox
        {
            get { return (Rect)GetValue(ActualViewboxProperty); }
            set { SetValue(ActualViewboxProperty, value); }
        }

        #endregion

        #region ViewboxProperty

        /// <summary>
        /// Identifies the <see cref="Viewbox"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewboxProperty = DependencyProperty.Register("Viewbox", typeof(Rect), typeof(ZoomableCanvas), 
                                                                      new FrameworkPropertyMetadata(Rect.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnViewboxChanged), IsViewboxValid);

        /// <summary>
        /// Determines whether the value given is a valid value for the <see cref="Viewbox"/> dependency property.
        /// </summary>
        /// <param name="value">The potential value for the dependency property.</param>
        /// <returns><c>true</c> if the value is a valid value for the property; otherwise, <c>false</c>.</returns>
        private static bool IsViewboxValid(object value)
        {
            var viewbox = (Rect)value;
            return viewbox.IsEmpty
                //|| (viewbox.X.IsBetween(Double.NegativeInfinity, Double.PositiveInfinity)
                //&& viewbox.Y.IsBetween(Double.NegativeInfinity, Double.PositiveInfinity)
                //&& viewbox.Width.IsBetween(Double.Epsilon, Double.PositiveInfinity)
                //&& viewbox.Height.IsBetween(Double.Epsilon, Double.PositiveInfinity));
            || (viewbox.X.IsBetween(Double.MinValue, Double.MaxValue)
                && viewbox.Y.IsBetween(Double.MinValue, Double.MaxValue)
                && viewbox.Width.IsBetween(Double.Epsilon, Double.MaxValue)
                && viewbox.Height.IsBetween(Double.Epsilon, Double.MaxValue));
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="Viewbox"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnViewboxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(ScaleProperty);
            d.CoerceValue(OffsetProperty);
        }

        /// <summary>
        /// Gets or sets the portion of the canvas (in canvas coordinates) that should be attempted to be displayed by this panel.
        /// </summary>
        /// <value>A <see cref="Rect"/> specifying the portion of the canvas that should be displayed by this panel, or <see cref="Rect.Empty"/> when unspecified.  The default value is <see cref="Rect.Empty"/>.</value>
        /// <remarks>
        /// The area of the canvas shown by this panel can be controlled by either setting <see cref="Scale"/> and <see cref="Offset"/>, or by setting the <see cref="Viewbox"/>, <see cref="Stretch"/>, and <see cref="StretchDirection"/> properties.
        /// When <see cref="Viewbox"/> is set to anything other than <see cref="Rect.Empty"/>, the <see cref="Scale"/> and <see cref="Offset"/> will be automatically coerced to appropriate values according to the <see cref="Stretch"/> and <see cref="StretchDirection"/> properties.
        /// Note that the <see cref="Stretch"/> mode of <see cref="Stretch.Fill"/> is not supported, so unless the aspect ratio of <see cref="Viewbox"/> exactly matches the aspect ratio of <see cref="RenderSize"/> the actual area displayed will be more or less than <see cref="Viewbox"/>.
        /// The exact area that is displayed can be determined by the <see cref="ActualViewbox"/> property in this case.
        /// </remarks>
        /// <seealso cref="Stretch"/>
        /// <seealso cref="StretchDirection"/>
        /// <seealso cref="TileBrush.Viewbox"/>
        public Rect Viewbox
        {
            get { return (Rect)GetValue(ViewboxProperty); }
            set { SetValue(ViewboxProperty, value); }
        }

        #endregion

        #region StretchProperty

        /// <summary>
        /// Identifies the <see cref="Stretch"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ZoomableCanvas), new FrameworkPropertyMetadata(Stretch.Uniform, OnStretchChanged), IsStretchValid);

        /// <summary>
        /// Determines whether the value given is a valid value for the <see cref="Stretch"/> dependency property.
        /// </summary>
        /// <param name="value">The potential value for the dependency property.</param>
        /// <returns><c>true</c> if the value is a valid value for the property; otherwise, <c>false</c>.</returns>
        private static bool IsStretchValid(object value)
        {
            var stretch = (Stretch)value;
            return stretch == Stretch.None
                || stretch == Stretch.Uniform
                || stretch == Stretch.UniformToFill;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="Stretch"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(ScaleProperty);
            d.CoerceValue(OffsetProperty);
        }

        /// <summary>
        /// Gets or sets a value that specifies how the content of the canvas is displayed when <see cref="Viewbox"/> is set.
        /// </summary>
        /// <value>One of the <see cref="Stretch"/> values other than <see cref="Stretch.Fill"/>.  The default is <see cref="Stretch.Uniform"/>.</value>
        /// <remarks>
        /// Please see the documentation of <see cref="TileBrush.Stretch"/> for a detailed explanation of the effects of this property.
        /// The <see cref="Stretch"/> mode of <see cref="Stretch.Fill"/> is not supported, so unless the aspect ratio of <see cref="Viewbox"/> exactly matches the aspect ratio of <see cref="RenderSize"/> the actual area displayed will be more or less than <see cref="Viewbox"/>.
        /// The exact area that is displayed can be determined by the <see cref="ActualViewbox"/> property in this case.
        /// </remarks>
        /// <seealso cref="TileBrush.Stretch"/>
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        #endregion

        #region StretchDirectionProperty

        /// <summary>
        /// Identifies the <see cref="StretchDirection"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StretchDirectionProperty = DependencyProperty.Register("StretchDirection", typeof(StretchDirection), typeof(ZoomableCanvas), new FrameworkPropertyMetadata(StretchDirection.Both, OnStretchDirectionChanged), IsStretchDirectionValid);

        /// <summary>
        /// Determines whether the value given is a valid value for the <see cref="StretchDirection"/> dependency property.
        /// </summary>
        /// <param name="value">The potential value for the dependency property.</param>
        /// <returns><c>true</c> if the value is a valid value for the property; otherwise, <c>false</c>.</returns>
        private static bool IsStretchDirectionValid(object value)
        {
            var stretch = (StretchDirection)value;
            return stretch == StretchDirection.Both
                || stretch == StretchDirection.UpOnly
                || stretch == StretchDirection.DownOnly;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="StretchDirection"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnStretchDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(ScaleProperty);
            d.CoerceValue(OffsetProperty);
        }

        /// <summary>
        /// Gets or sets how setting the <see cref="Viewbox"/> property can affect the <see cref="Scale"/>.
        /// </summary>
        /// <value>One of the <see cref="StretchDirection"/> values.  The default is <see cref="StretchDirection.Both"/></value>
        /// <remarks>
        /// When setting the <see cref="Viewbox"/> property, the <see cref="Scale"/> and <see cref="Offset"/> properties are automatically coerced to the appropriate values according to the <see cref="Stretch"/> and <see cref="StretchDirection"/> properties, and any existing values of <see cref="Scale"/> and <see cref="Offset"/> will be overridden.
        /// However, when the value of <see cref="StretchDirection"/> is set to anything other than <see cref="StretchDirection.Both"/>, then the setting of the <see cref="Scale"/> property can limit the range of the automatically computed value.
        /// The exact area that is displayed can be determined by the <see cref="ActualViewbox"/> property in this case.
        /// </remarks>
        public StretchDirection StretchDirection
        {
            get { return (StretchDirection)GetValue(StretchDirectionProperty); }
            set { SetValue(StretchDirectionProperty, value); }
        }

        #endregion
        
        #region OffsetProperty

        /// <summary>
        /// Identifies the <see cref="Offset"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register("Offset", typeof(Point), typeof(ZoomableCanvas), new FrameworkPropertyMetadata(new Point(0.0, 0.0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnOffsetChanged, CoerceOffset), IsOffsetValid);

        /// <summary>
        /// Determines whether the value given is a valid value for the <see cref="Offset"/> dependency property.
        /// </summary>
        /// <param name="value">The potential value for the dependency property.</param>
        /// <returns><c>true</c> if the value is a valid value for the property; otherwise, <c>false</c>.</returns>
        private static bool IsOffsetValid(object value)
        {
            var point = (Point)value;
            return point.X.IsBetween(Double.MinValue, Double.MaxValue)
                && point.Y.IsBetween(Double.MinValue, Double.MaxValue);
        }

        /// <summary>
        /// Returns a <see cref="Point"/> representing top-left point of the canvas (in canvas coordinates) that is currently being displayed after taking <see cref="Viewbox"/> into account.
        /// </summary>
        /// <param name="d">Dependency object whos value is being coerced.</param>
        /// <param name="value">The original uncoerced value.</param>
        /// <returns>A <see cref="Point"/> representing top-left point of the canvas (in canvas coordinates) that is currently being displayed.</returns>
        private static object CoerceOffset(DependencyObject d, object value)
        {
            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                var viewbox = canvas.Viewbox;
                if (!viewbox.IsEmpty)
                {
                    var scale = canvas.Scale;
                    var renderSize = canvas.RenderSize;
                    value = new Point((viewbox.X + viewbox.Width / 2) * scale - renderSize.Width / 2, (viewbox.Y + viewbox.Height / 2) * scale - renderSize.Height / 2);
                }
            }

            return value;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="Offset"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(ActualViewboxProperty);
            Rect rect = (Rect)d.GetValue(ActualViewboxProperty);
            d.SetValue(BoxProperty, rect);
            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                canvas.OffsetOverride((Point)e.NewValue);
            }
        }

        /// <summary>
        /// Gets or sets (but see remarks) the top-left point of the area of the canvas (in canvas coordinates) that is being displayed by this panel.
        /// </summary>
        /// <value>A <see cref="Point"/> on the canvas (in canvas coordinates).  The default is <c>(0,0)</c>.</value>
        /// <remarks>
        /// This value controls the horizontal and vertical position of the canvas children relative to this panel.
        /// <para>
        /// For example, consider a child element which has its <see cref="Canvas.Left"/> and <see cref="Canvas.Top"/> set to <c>100</c> and <c>100</c>.
        /// Also assume that the value of <see cref="Scale"/> is set to <c>1.0</c> (the default value).
        /// </para>
        /// <para>
        /// By default, the value of <see cref="Offset"/> is <c>(0,0)</c> so the element will be displayed at 100 units to the right and 100 units down from the top-left corner of this panel, exactly how <see cref="Canvas"/> would display it.
        /// If the value of <see cref="Offset"/> is set to <c>(20,40)</c> then the element will be displayed 80 units to the right and 60 units down from the top-left corner of this panel.
        /// In other words, it will have appeared to "move" up by 20 units and left by 40 units.
        /// </para>
        /// <para>
        /// If the value of <see cref="Offset"/> is set to <c>(100,100)</c> then the top-left corner of the element will be displayed exactly in the top-left corner of this panel.
        /// Note that this is true regardless of the value of <see cref="Scale"/>!
        /// </para>
        /// <para>
        /// If the value of <see cref="Offset"/> is set to <c>(110,120)</c> then the top-left corner of the element will be displayed 10 units to the left and 20 units above this panel.
        /// In other words, if <see cref="ClipToBounds"/> is set to <c>true</c>, then the top-left corner of the element will not be visible.
        /// </para>
        /// <para>
        /// The value of <see cref="Offset"/> can also be negative, so if the value of <see cref="Offset"/> is set to <c>(-100,-100)</c> then the element will be displayed at 200 pixels to the right and 200 pixels down from the top-left corner of the panel.
        /// </para>
        /// <para>
        /// When the <see cref="Viewbox"/> property is set to a non-<see cref="Rect.Empty"/> value, the value of the <see cref="Offset"/> property will be automatically computed to match the <see cref="Viewbox"/>, <see cref="Stretch"/>, and <see cref="StretchDirection"/> properties.
        /// The value of the <see cref="Offset"/> property will contain the computed value (via the WPF dependency property coersion mechanism), and any attempts to set <see cref="Offset"/> to a different value will be ignored until <see cref="Viewbox"/> is set to <see cref="Rect.Empty"/> again.
        /// <para>
        /// </remarks>
        /// <seealso cref="Viewbox"/>
        public Point Offset
        {
            get { return (Point)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        #endregion

        #region ScaleProperty

        /// <summary>
        /// Identifies the <see cref="Scale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(ZoomableCanvas), 
                                                                    new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScaleChanged, CoerceScale), IsScaleValid);

        /// <summary>
        /// Determines whether the value given is a valid value for the <see cref="Scale"/> dependency property.
        /// </summary>
        /// <param name="value">The potential value for the dependency property.</param>
        /// <returns><c>true</c> if the value is a valid value for the property; otherwise, <c>false</c>.</returns>
        private static bool IsScaleValid(object value)
        {
            return ((double)value).IsBetween(Double.Epsilon, Double.MaxValue);
        }

        /// <summary>
        /// Returns a <see cref="Double"/> representing the scale of the content that is currently being displayed after taking <see cref="Viewbox"/>, <see cref="Stretch"/>, and <see cref="StretchDirection"/> into account.
        /// </summary>
        /// <param name="d">Dependency object whos value is being coerced.</param>
        /// <param name="value">The original uncoerced value.</param>
        /// <returns>A <see cref="Double"/> representing scale of the content that is currently being displayed.</returns>
        private static object CoerceScale(DependencyObject d, object value)
        {
            var scale = (double)value;

            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                var renderSize = canvas.RenderSize;
                if (renderSize.Width > 0 && renderSize.Height > 0)
                {
                    var viewbox = canvas.Viewbox;
                    if (!viewbox.IsEmpty)
                    {
                        switch (canvas.Stretch)
                        {
                            case Stretch.Uniform:
                                scale = Math.Min(renderSize.Width / viewbox.Width, renderSize.Height / viewbox.Height);
                                break;

                            case Stretch.UniformToFill:
                                scale = Math.Max(renderSize.Width / viewbox.Width, renderSize.Height / viewbox.Height);
                                break;
                        }

                        switch (canvas.StretchDirection)
                        {
                            case StretchDirection.DownOnly:
                                scale = scale.AtMost((double)value);
                                break;

                            case StretchDirection.UpOnly:
                                scale = scale.AtLeast((double)value);
                                break;
                        }
                    }
                }
            }

            return scale;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="Scale"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(ActualViewboxProperty);
            d.CoerceValue(OffsetProperty);
            //d.SetValue(BoxProperty, d.GetValue(ActualViewboxProperty));
            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                canvas.ScaleOverride((double)e.NewValue);
            }
        }

        /// <summary>
        /// Gets or sets (but see remarks) the scale at which the content of the canvas is being displayed.
        /// </summary>
        /// <value>A <see cref="Double"/> between <see cref="Double.Epsilon"/> and <see cref="Double.MaxValue"/>.  The default value is <c>1.0</c>.</value>
        /// <remarks>
        /// This value is what controls the zoom level of the canvas and the amount of the <see cref="ScaleTransform"/> when <see cref="ApplyTransform"/> is set to <c>true</c>.
        /// When <see cref="ApplyTransform"/> is set to <c>false</c>, this value still controls the positioning of the children (i.e. elements are placed closer together when zoomed out and farther apart when zoomed in), but the sizes of the children are unaffected.
        /// <para>
        /// For example, consider a child element which has its <see cref="Canvas.Left"/> and <see cref="Canvas.Top"/> set to <c>100</c> and <c>100</c>, with a <see cref="Width"/> of 50 and a <see cref="Height"/> of 50.
        /// Also assume that the value of <see cref="Offset"/> is set to <c>(0,0)</c> (the default value).
        /// </para>
        /// <para>
        /// By default, the value of <see cref="Scale"/> is <c>1.0</c> so the element will be displayed at 100 units to the right and 100 units down from the top-left corner of this panel, exactly how <see cref="Canvas"/> would display it.
        /// If the value of <see cref="Scale"/> is set to <c>0.8</c> then the top-left corner of the element will be displayed 80 units to the right and 80 units down from the top-left corner of this panel.
        /// If <see cref="ApplyTransform"/> is set to <c>true</c> (the default value), then the element will also be scaled down (shrunk) to 80% of its normal size, so that the bottom-right of the element will be 120 units to the right and 120 units down from the top-left corner of this panel.
        /// If <see cref="ApplyTransform"/> is set to <c>false</c>, then the element will remain its original size, resulting in the bottom-right of the element being 130 units to the right and 130 units down from the top-left corner of this panel.
        /// In other words, it will simply have appeared to "move" up by 20 units and left by 20 units without changing its size.
        /// This is not normally what a user would expect when "zooming out" (unless the element is some kind of floating label above the canvas), so it is expected that the children of the canvas will be responsible for changing their representation appropriately when <see cref="ApplyTransform"/> is set to <c>false</c>.
        /// </para>
        /// <para>
        /// When the <see cref="Viewbox"/> property is set to a non-<see cref="Rect.Empty"/> value, the value of the <see cref="Scale"/> property will be automatically computed to match the <see cref="Viewbox"/>, <see cref="Stretch"/>, and <see cref="StretchDirection"/> properties.
        /// The value of the <see cref="Scale"/> property will contain the computed value (via the WPF dependency property coersion mechanism), but and any attempts to set <see cref="Scale"/> to a different value will be ignored (if <see cref="StretchDirection"/> is set to <see cref="StretchDirection.Both"/>) until <see cref="Viewbox"/> is set to <see cref="Rect.Empty"/> again.
        /// <para>
        /// </remarks>
        /// <seealso cref="Viewbox"/>
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        #endregion

        #region RealizationLimitProperty

        /// <summary>
        /// Identifies the <see cref="RealizationLimit"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RealizationLimitProperty = DependencyProperty.Register("RealizationLimit", typeof(int), typeof(ZoomableCanvas), new FrameworkPropertyMetadata(int.MaxValue, OnRealizationLimitChanged), IsRealizationLimitValid);

        /// <summary>
        /// Determines whether the value given is a valid value for the <see cref="RealizationLimit"/> dependency property.
        /// </summary>
        /// <param name="value">The potential value for the dependency property.</param>
        /// <returns><c>true</c> if the value is a valid value for the property; otherwise, <c>false</c>.</returns>
        private static bool IsRealizationLimitValid(object value)
        {
            return (int)value >= 0;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="RealizationLimit"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnRealizationLimitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                canvas.InvalidateReality();
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of elements that will be instantiated on the canvas when <see cref="IsVirtualizing"/> is set to <see cref="true"/>.
        /// </summary>
        /// <value>An <see cref="Int32"/> between <c>0</c> and <see cref="Int32.MaxValue"/>.  The default is <see cref="Int32.MaxValue"/>.</value>
        /// <remarks>
        /// When the children of the canvas are being populated through an <see cref="ItemsControl"/>, visual elements will be instantiated for the first <see cref="RealizationLimit"/> items within the <see cref="ActualViewbox"/>.
        /// </remarks>
        public int RealizationLimit
        {
            get { return (int)GetValue(RealizationLimitProperty); }
            set { SetValue(RealizationLimitProperty, value); }
        }

        #endregion

        #region RealizationRateProperty

        /// <summary>
        /// Identifies the <see cref="RealizationRate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RealizationRateProperty = DependencyProperty.Register("RealizationRate", typeof(int), typeof(ZoomableCanvas), new FrameworkPropertyMetadata(int.MaxValue, OnRealizationRateChanged), IsRealizationRateValid);

        /// <summary>
        /// Determines whether the value given is a valid value for the <see cref="RealizationRate"/> dependency property.
        /// </summary>
        /// <param name="value">The potential value for the dependency property.</param>
        /// <returns><c>true</c> if the value is a valid value for the property; otherwise, <c>false</c>.</returns>
        private static bool IsRealizationRateValid(object value)
        {
            return (int)value >= 0;
        }

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="RealizationRate"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnRealizationRateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as ZoomableCanvas;
            if (canvas != null)
            {
                canvas.InvalidateReality();
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of elements that will be realized or virtualized at one time before yielding control back to the dispatcher when <see cref="IsVirtualizing"/> is set to <see cref="true"/>.
        /// </summary>
        /// <value>An <see cref="Int32"/> between <c>0</c> and <see cref="Int32.MaxValue"/>.  The default is <see cref="Int32.MaxValue"/>.</value>
        /// <remarks>
        /// By default, the value of this property is <see cref="Int32.MaxValue"/> which means that all realization and virtualization happens at once, at the time determined by <see cref="RealizationPriority"/>.
        /// The default behavior is optimized to realize all elements as quickly as possible, at the expensive of application responsiveness while the realization is happening.
        /// Setting the <see cref="RealizationPriority"/> to <see cref="DispatcherPriority.Input"/> and decreasing the <see cref="RealizationRate"/> will make the application feel more responsive but will take longer to realize all items.
        /// </remarks>
        public int RealizationRate
        {
            get { return (int)GetValue(RealizationRateProperty); }
            set { SetValue(RealizationRateProperty, value); }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Performs a one-time initialization of <see cref="ZoomableCanvas"/>-related metadata.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ZoomableCanvas()
        {
            RenderTransformProperty.OverrideMetadata(typeof(ZoomableCanvas), new FrameworkPropertyMetadata(null, CoerceRenderTransform));

            try
            {
                Canvas.TopProperty.OverrideMetadata(typeof(UIElement), new FrameworkPropertyMetadata(OnPositioningChanged));
                Canvas.LeftProperty.OverrideMetadata(typeof(UIElement), new FrameworkPropertyMetadata(OnPositioningChanged));
                Canvas.BottomProperty.OverrideMetadata(typeof(UIElement), new FrameworkPropertyMetadata(OnPositioningChanged));
                Canvas.RightProperty.OverrideMetadata(typeof(UIElement), new FrameworkPropertyMetadata(OnPositioningChanged));
            }
            catch (ArgumentException)
            {
                // These overrides crash in Expression Blend.
            }
        }

        /// <summary>
        /// Ensures coersion routines are invoked with their default values.
        /// </summary>
        public ZoomableCanvas()
        {
            CoerceValue(ScaleProperty);
            CoerceValue(OffsetProperty);
            CoerceValue(ActualViewboxProperty);
            CoerceValue(RenderTransformProperty);
        }

        #endregion

        #region Spatial Item Management

        /// <summary>
        /// Provides a two-dimensional index of items that can be quickly queried for all items that intersect a given rectangle.
        /// </summary>
        /// <remarks>
        /// When the <see cref="ZoomableCanvas"/> is hosting items for an <see cref="ItemsControl"/>, the <see cref="ItemsControl.ItemsSource"/> can implement this interface to greatly speed up virtualization in the canvas.
        /// If any of those conditions are not true, then the canvas must realize every item at least once in order to determine its bounds before it can virtualize it, and then once it is virtualized it will have no means of moving spontaneously back into view.
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "This interface is only used by ZoomableCanvas.")]
        public interface ISpatialItemsSource
        {
            /// <summary>
            /// Gets the entire extent of the index, which is typically the union of all bounding boxes of all items within the set.
            /// </summary>
            /// <remarks>
            /// This value is used when determining the extent of the scroll bars when the canvas is hosted in a scroll viewer.
            /// </remarks>
            Rect Extent { get; }

            /// <summary>
            /// Gets the set of items that intersect the given rectangle.
            /// </summary>
            /// <param name="rectangle">The area in which any intersecting items are returned.</param>
            /// <returns>A result set of all items that intersect the given rectangle.</returns>
            /// <remarks>
            /// The enumerator returned by this method is used lazily and sometimes only partially, meaning it should return quickly without computing the entire result set immediately for best results.
            /// </remarks>
            IEnumerable<int> Query(Rect rectangle);

            /// <summary>
            /// Occurs when the value of the <see cref="Extent"/> property has changed.
            /// </summary>
            event EventHandler ExtentChanged;

            /// <summary>
            /// Occurs when the results of the last query are no longer valid and should be re-queried.
            /// </summary>
            event EventHandler QueryInvalidated;
        }

        #region Private SpatialIndex Implementation

        /// <summary>
        /// Private implementation of <see cref="ISpatialItemsSource"/> when the items source is not one.
        /// </summary>
        /// <remarks>
        /// This class efficiently implements a spatial index by internally using a PriorityQuadTree data structure.
        /// </remarks>
        private class PrivateSpatialIndex : ISpatialItemsSource
        {
            /// <summary>
            /// Private class that holds an index/bounds pair.
            /// </summary>
            /// <remarks>
            /// A Tuple could have been used instead except that we want Index to be mutable.
            /// </remarks>
            private class SpatialItem
            {
                public SpatialItem()
                {
                    Index = -1;
                    Bounds = Rect.Empty;
                }

                public int Index
                {
                    get;
                    set;
                }

                public Rect Bounds
                {
                    get;
                    set;
                }

                public override string ToString()
                {
                    return "Item[" + Index + "].Bounds = " + Bounds;
                }
            }

            /// <summary>
            /// We use a PriorityQuadTree to implement our spatial index.
            /// </summary>
            private readonly PriorityQuadTree<SpatialItem> _tree = new PriorityQuadTree<SpatialItem>();

            /// <summary>
            /// This is a list of all of the spatial items in the index.
            /// </summary>
            private readonly List<SpatialItem> _items = new List<SpatialItem>();

            /// <summary>
            /// Holds the accurate extent of all item bounds in the index.  This may be different from _tree.Extent.
            /// </summary>
            private Rect _extent = Rect.Empty;

            /// <summary>
            /// Holds the last query used in order to know when to raise the <see cref="QueryInvalidated"/> event.
            /// </summary>
            private Rect _lastQuery = Rect.Empty;

            /// <summary>
            /// Occurs when the value of the <see cref="Extent"/> property has changed.
            /// </summary>
            public event EventHandler ExtentChanged;

            /// <summary>
            /// Occurs when the results of the last query are no longer valid and should be re-queried.
            /// </summary>
            public event EventHandler QueryInvalidated;

            /// <summary>
            /// Get a list of the items that intersect the given bounds.
            /// </summary>
            /// <param name="bounds">The bounds to test.</param>
            /// <returns>
            /// List of zero or more items that intersect the given bounds, returned in the order given by the priority assigned during Insert.
            /// </returns>
            public IEnumerable<int> Query(Rect bounds)
            {
                _lastQuery = bounds;
                return _tree.GetItemsIntersecting(bounds).Select(i => i.Index);
            }

            /// <summary>
            /// Gets the computed minimum required rectangle to contain all of the items in the index.  This property is also settable for efficiency the future extent of the items is known.
            /// </summary>
            public Rect Extent
            {
                get
                {
                    if (_extent.IsEmpty)
                    {
                        foreach (var item in _items)
                        {
                            _extent.Union(item.Bounds);
                        }
                    }
                    return _extent;
                }
            }

            /// <summary>
            /// Gets or sets the bounds for the item with the given <paramref name="index"/>.
            /// </summary>
            /// <param name="index">The index of the item.</param>
            /// <returns>The bounds of the item, or <see cref="Rect.Empty"/> if the bounds are unknown.</returns>
            /// <remarks>
            /// Items with bounnds of <see cref="Rect.Empty"/> are always returned first from any query.
            /// </remarks>
            public Rect this[int index]
            {
                get
                {
                    return _items[index].Bounds;
                }
                set
                {
                    var item = _items[index];
                    var bounds = item.Bounds;
                    if (bounds != value)
                    {
                        _extent = Rect.Empty;
                        _tree.Remove(item, bounds);
                        _tree.Insert(item, value, value.IsEmpty ? Double.PositiveInfinity : value.Width + value.Height);
                        item.Bounds = value;

                        if (ExtentChanged != null)
                        {
                            ExtentChanged(this, EventArgs.Empty);
                        }

                        if (QueryInvalidated != null && (bounds.IntersectsWith(_lastQuery) || value.IntersectsWith(_lastQuery)))
                        {
                            QueryInvalidated(this, EventArgs.Empty);
                        }
                    }
                }
            }

            /// <summary>
            /// Adds or inserts the given <see cref="count"/> of items at the given <see cref="index"/>.
            /// </summary>
            /// <param name="index">The index at which to insert the items.</param>
            /// <param name="count">The number of items to insert.</param>
            /// <remarks>
            /// All items are inserted with bounds of <see cref="Rect.Empty"/>, meaning they will be returned from all queries.
            /// </remarks>
            public void InsertRange(int index, int count)
            {
                var items = new SpatialItem[count];
                for (int i = 0; i < count; i++)
                {
                    items[i] = new SpatialItem();
                    items[i].Index = index + i;
                    _tree.Insert(items[i], Rect.Empty, Double.PositiveInfinity);
                }
                _items.InsertRange(index, items);

                if (QueryInvalidated != null)
                {
                    QueryInvalidated(this, EventArgs.Empty);
                }
            }

            /// <summary>
            /// Removes the given <see cref="count"/> of items at the given <see cref="index"/>.
            /// </summary>
            /// <param name="index">The index at which to remove from.</param>
            /// <param name="count">The number of items to remove.</param>
            public void RemoveRange(int index, int count)
            {
                for (int i = index; i < _items.Count; i++)
                {
                    if (i < index + count)
                    {
                        _tree.Remove(_items[i], _items[i].Bounds);
                    }
                    else
                    {
                        _items[i].Index = i - count;
                    }
                }
                // $$$ this is where the problems are caused when there is an index out of range. It has a 
                // slightly hack-ish fix in Virtual Panel (line 339)
                _items.RemoveRange(index, count);
                _extent = Rect.Empty;

                if (ExtentChanged != null)
                {
                    ExtentChanged(this, EventArgs.Empty);
                }

                if (QueryInvalidated != null)
                {
                    QueryInvalidated(this, EventArgs.Empty);
                }
            }

            /// <summary>
            /// Clears and resets the spatial index to hold the given <see cref="count"/> of items.
            /// </summary>
            /// <param name="count">The number of items within the index.</param>
            public void Reset(int count)
            {
                _extent = Rect.Empty;
                _items.Clear();
                InsertRange(0, count);

                if (ExtentChanged != null)
                {
                    ExtentChanged(this, EventArgs.Empty);
                }

                if (QueryInvalidated != null)
                {
                    QueryInvalidated(this, EventArgs.Empty);
                }
            }

            /// <summary>
            /// Optimizes the spatial index based on the current extent if optimization is warranted.
            /// </summary>
            public void Optimize()
            {
                var treeExtent = _tree.Extent;
                var realExtent = Extent;
                if (treeExtent.Top - realExtent.Top > treeExtent.Height ||
                    treeExtent.Left - realExtent.Left > treeExtent.Width ||
                    realExtent.Right - treeExtent.Right > treeExtent.Width ||
                    realExtent.Bottom - treeExtent.Bottom > treeExtent.Height)
                {
                    _tree.Extent = realExtent;

                    if (QueryInvalidated != null)
                    {
                        QueryInvalidated(this, EventArgs.Empty);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Two-dimentional spatial index of our data items.
        /// </summary>
        private ISpatialItemsSource SpatialIndex;

        /// <summary>
        /// Private implementation of <see cref="ISpatialItemsSource"/> when the items source does not provide one.
        /// </summary>
        private PrivateSpatialIndex PrivateIndex;

        /// <summary>
        /// Ordered list of realized items based on the order they are returned from the spatial index.
        /// </summary>
        private LinkedList<int> RealizedItems;

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="VirtualPanel.IsVirtualizing"/> property has changed.
        /// </summary>
        protected override void OnIsVirtualizingChanged(bool oldIsVirtualizing, bool newIsVirtualizing)
        {
            OnItemsReset();
            base.OnIsVirtualizingChanged(oldIsVirtualizing, newIsVirtualizing);
        }

        /// <summary>
        /// Refreshes our data when the <see cref="Panel.IsItemsHost"/> property has changed.
        /// </summary>
        protected override void OnIsItemsHostChanged(bool oldIsItemsHost, bool newIsItemsHost)
        {
            OnItemsReset();
            base.OnIsItemsHostChanged(oldIsItemsHost, newIsItemsHost);
        }

        /// <summary>
        /// Dispatches to specific methods to update the spatial index when the items have changed.
        /// </summary>
        protected override void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                OnItemsAdded(args.NewStartingIndex, args.NewItems);
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                OnItemsRemoved(args.OldStartingIndex, args.OldItems);
            }
            else if (args.Action == NotifyCollectionChangedAction.Reset)
            {
                OnItemsReset();
            }

            InvalidateReality();
            InvalidateExtent();
        }

        /// <summary>
        /// Updates our private spatial index when items are added to the item source.
        /// </summary>
        /// <param name="index">The index of the first item that was added.</param>
        /// <param name="items">The items that were added.</param>
        private void OnItemsAdded(int index, IList items)
        {
            if (PrivateIndex != null)
            {
                PrivateIndex.InsertRange(index, items.Count);
            }

            if (RealizedItems != null)
            {
                var item = RealizedItems.First;
                while (item != null)
                {
                    if (item.Value >= index)
                    {
                        item.Value += items.Count;
                    }
                    item = item.Next;
                }
            }
        }

        /// <summary>
        /// Updates our private spatial index when items are removed from the item source.
        /// </summary>
        /// <param name="index">The old index of the first item that was removed.</param>
        /// <param name="items">The items that were removed.</param>
        private void OnItemsRemoved(int index, IList items)
        {
            if (PrivateIndex != null)
            {
                PrivateIndex.RemoveRange(index, items.Count);
            }

            if (RealizedItems != null)
            {
                var item = RealizedItems.First;
                while (item != null)
                {
                    var next = item.Next;
                    if (item.Value >= index)
                    {
                        if (item.Value < index + items.Count)
                        {
                            RealizedItems.Remove(item);
                        }
                        else
                        {
                            item.Value -= items.Count;
                        }
                    }
                    item = next;
                }
            }
        }

        /// <summary>
        /// Resets and initializes our spatial indices when the items source has changed.
        /// </summary>
        private void OnItemsReset()
        {
            if (SpatialIndex != null)
            {
                SpatialIndex.ExtentChanged -= OnSpatialExtentChanged;
                SpatialIndex.QueryInvalidated -= OnSpatialQueryInvalidated;
            }

            RealizedItems = null;
            SpatialIndex = null;
            PrivateIndex = null;

            if (IsVirtualizing && IsItemsHost && ItemsOwner != null)
            {
                RealizedItems = new LinkedList<int>();
                SpatialIndex = ItemsOwner.ItemsSource as ISpatialItemsSource;
                if (SpatialIndex == null)
                {
                    PrivateIndex = new PrivateSpatialIndex();
                    PrivateIndex.Reset(ItemsOwner.Items != null ? ItemsOwner.Items.Count : 0);
                    SpatialIndex = PrivateIndex;
                }

                SpatialIndex.ExtentChanged += OnSpatialExtentChanged;
                SpatialIndex.QueryInvalidated += OnSpatialQueryInvalidated;
            }

            InvalidateReality();
            InvalidateExtent();
        }

        /// <summary>
        /// Invalidates reality when the last spatial query is no longer valid.
        /// </summary>
        /// <param name="sender">The spatial index.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSpatialQueryInvalidated(object sender, EventArgs e)
        {
            InvalidateReality();
        }

        /// <summary>
        /// Invalidates the extent when the spatial index extent has changed.
        /// </summary>
        /// <param name="sender">The spatial index.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSpatialExtentChanged(object sender, EventArgs e)
        {
            InvalidateExtent();
        }

        #endregion

        #region Virtualization

        /// <summary>
        /// Performs realization and virtualization in batches based on the <see cref="RealizationRate"/>. 
        /// </summary>
        /// <param name="items">The current items being hosted by this panel.</param>
        /// <param name="state">The previous return value of this method.</param>
        /// <returns>A non-<c>null</c> value if further realization is required; otherwise, <c>null</c>.</returns>
        protected override object RealizeOverride(IEnumerable items, object state)
        {
            var enumerator = state as IEnumerator ?? RealizeOverride();
            var rate = RealizationRate;
            while (rate-- > 0)
            {
                if (!enumerator.MoveNext())
                {
                    return null;
                }
            }
            return enumerator;
        }

        /// <summary>
        /// Realizes and virtualizes items based on the current viewbox.
        /// </summary>
        /// <returns>An enumerator which allows this method to continue realization where it left off.</returns>
        private IEnumerator RealizeOverride()
        {
            if (SpatialIndex != null)
            {
                // Optimize our private spatial index for the upcoming query.
                if (PrivateIndex != null)
                {
                    PrivateIndex.Optimize();
                }

                IEnumerable<int> query;

                if (IsVirtualizing)
                {
                    // Only realize the items within our viewbox.
                    var viewbox = ActualViewbox;
                    var limit = RealizationLimit;

                    // Buffer the viewbox so that panning by small amounts is smooth.
                    viewbox.Inflate(viewbox.Width / 10, viewbox.Height / 10);

                    // Query the index for all items that intersect our viewbox, up to our realization limit.
                    query = SpatialIndex.Query(viewbox).Take(limit);
                }
                else
                {
                    // Get all items.
                    query = SpatialIndex.Query(new Rect(Double.NegativeInfinity, Double.NegativeInfinity, Double.PositiveInfinity, Double.PositiveInfinity));
                }

                // We insert nodes at the head of the linked list in the order they are returned.
                LinkedListNode<int> lastNode = null;
                LinkedListNode<int> nextNode = RealizedItems.First;

                // Realize them.
                foreach (var index in query)
                {
                    // See if the item was already realized.
                    var node = RealizedItems.FindNext(lastNode, index);
                    if (node == null || node != nextNode)
                    {
                        if (node != null)
                        {
                            // If it was already realized further down the list, remove it from the list so we can add it later.
                            RealizedItems.Remove(node);
                        }
                        else
                        {
                            // If it was not already realized, realize it now and create a new node for it.
                            RealizeItem(index);
                            node = new LinkedListNode<int>(index);
                        }

                        // Insert the node after the last node, or at the front if this is the first.
                        if (lastNode == null)
                        {
                            RealizedItems.AddFirst(node);
                        }
                        else
                        {
                            RealizedItems.AddAfter(lastNode, node);
                        }
                    }

                    // Keep track of the last node of the query results.
                    lastNode = node;
                    nextNode = node.Next;

                    // Yield control for throttling.
                    yield return index;
                }

                // Virtualize any remaining items that are no longer part of our result set, backwards.
                nextNode = RealizedItems.Last;
                while (nextNode != lastNode)
                {
                    var node = nextNode;
                    nextNode = nextNode.Previous;

                    var index = node.Value;
                    var container = ContainerFromIndex(index);
                    if (container == null || (!container.IsMouseCaptureWithin && !container.IsKeyboardFocusWithin))
                    {
                        VirtualizeItem(index);
                        RealizedItems.Remove(node);
                    }

                    // Yield control for throttling.
                    yield return index;
                }
            }
        }

        #endregion

        #region Arrange Logic

        /// <summary>
        /// Updates the calculated <see cref="ActualViewbox"/> and the <see cref="Scale"/> and <see cref="Offset"/> when the size changes.
        /// </summary>
        /// <param name="sizeInfo">Size information about the render size.</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            CoerceValue(ScaleProperty);
            CoerceValue(OffsetProperty);
            CoerceValue(ActualViewboxProperty);

            base.OnRenderSizeChanged(sizeInfo);
        }

        /// <summary>
        /// Invalidates the arrangement of canvases when their children's positions change.
        /// </summary>
        /// <param name="d">Dependency object whos position has changed.</param>
        /// <param name="e">Event arguments related to the change.</param>
        private static void OnPositioningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(d);
            var zoomable = parent as ZoomableCanvas;
            if (zoomable != null)
            {
                zoomable.InvalidateArrange();
            }
            else
            {
                var regular = parent as Canvas;
                if (regular != null)
                {
                    regular.InvalidateArrange();
                }
            }
        }

        /// <summary>
        /// Gets the applied scale transform if <see cref="ApplyTransform"/> is set to <c>true</c>.
        /// </summary>
        private ScaleTransform AppliedScaleTransform
        {
            get
            {
                if (ApplyTransform)
                {
                    return (ScaleTransform)((TransformGroup)RenderTransform).Children[0];
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the applied translate transform if <see cref="ApplyTransform"/> is set to <c>true</c>.
        /// </summary>
        private TranslateTransform AppliedTranslateTransform
        {
            get
            {
                if (ApplyTransform)
                {
                    return (TranslateTransform)((TransformGroup)RenderTransform).Children[1];
                }
                return null;
            }
        }

        /// <summary>
        /// Scales the child elements of a <see cref="ZoomableCanvas"/> by applying a transform if <see cref="ApplyTransform"/> is <c>true</c>, or by calling <see cref="FrameworkElement.InvalidateArrange"/> otherwise.
        /// </summary>
        /// <param name="scale">The new scale of the canvas.</param>
        protected virtual void ScaleOverride(double scale)
        {
            var appliedTransform = AppliedScaleTransform;
            if (appliedTransform != null)
            {
                appliedTransform.ScaleX = scale;
                appliedTransform.ScaleY = scale;
            }
            else
            {
                InvalidateArrange();
            }
        }

        /// <summary>
        /// Offsets the child elements of a <see cref="ZoomableCanvas"/> by applying a transform if <see cref="ApplyTransform"/> is <c>true</c>, or by calling <see cref="FrameworkElement.InvalidateArrange"/> otherwise.
        /// </summary>
        /// <param name="offset">The new offset of the canvas.</param>
        protected virtual void OffsetOverride(Point offset)
        {
            var appliedTransform = AppliedTranslateTransform;
            if (appliedTransform != null)
            {
                appliedTransform.X = -offset.X;
                appliedTransform.Y = -offset.Y;
            }
            else
            {
                InvalidateArrange();
            }
        }

        /// <summary>
        /// Measures the child elements of a <see cref="ZoomableCanvas"/> in anticipation of arranging them during the <see cref="ArrangeOverride"/> pass.
        /// </summary>
        /// <param name="availableSize">An upper limit <see cref="Size"/> that should not be exceeded.</param>
        /// <returns>A <see cref="Size"/> that represents the size that is required to arrange child content.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size childConstraint = new Size(Double.PositiveInfinity, Double.PositiveInfinity);

            foreach (UIElement child in InternalChildren)
            {
                if (child != null)
                {
                    child.Measure(childConstraint);
                }
            }

            return new Size();
        }

        /// <summary>
        /// Arranges the content of a <see cref="ZoomableCanvas"/> element.
        /// </summary>
        /// <param name="finalSize">The size that this <see cref="ZoomableCanvas"/> element should use to arrange its child elements.</param>
        /// <returns>A <see cref="Size"/> that represents the arranged size of this <see cref="ZoomableCanvas"/> element and its descendants.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            bool applyTransform = ApplyTransform;
            Point offset = applyTransform ? new Point() : Offset;
            double scale = applyTransform ? 1.0 : Scale;

            ChildrenExtent = Rect.Empty;

            foreach (UIElement child in InternalChildren)
            {
                if (child != null)
                {
                    // Get bounds information from the element.
                    Rect bounds = new Rect(Canvas.GetLeft(child).GetValueOrDefault(), Canvas.GetTop(child).GetValueOrDefault(), child.DesiredSize.Width / scale, child.DesiredSize.Height / scale);

                    // If we are maintaining our own spatial wrapper then update its bounds.
                    if (PrivateIndex != null)
                    {
                        int index = IndexFromContainer(child); //TODO
                        Rect oldBounds = PrivateIndex[index];
                        const double tolerance = .0001; // The exact values during arrange can vary slightly.
                        if (Math.Abs(oldBounds.Top - bounds.Top) > tolerance ||
                            Math.Abs(oldBounds.Left - bounds.Left) > tolerance ||
                            Math.Abs(oldBounds.Width - bounds.Width) > tolerance ||
                            Math.Abs(oldBounds.Height - bounds.Height) > tolerance)
                        {
                            PrivateIndex[index] = bounds;
                        }
                    }

                    // Update the children extent for scrolling.
                    ChildrenExtent.Union(bounds);

                    // So far everything has been in canvas coordinates.  Here we adjust the result for the final call to Arrange.
                    bounds.X *= scale;
                    bounds.X -= offset.X;
                    bounds.Y *= scale;
                    bounds.Y -= offset.Y;
                    bounds.Width  = Math.Ceiling(bounds.Width*scale);
                    bounds.Height = Math.Ceiling(bounds.Height*scale);
                    

                    // WPF Arrange will crash if the values are too large.
                    bounds.X = bounds.X.AtLeast(Single.MinValue / 2);
                    bounds.Y = bounds.Y.AtLeast(Single.MinValue / 2);
                    bounds.Width = bounds.Width.AtMost(Single.MaxValue);
                    bounds.Height = bounds.Height.AtMost(Single.MaxValue);

                    child.Arrange(bounds);
                }
            }

            InvalidateExtent();

            return finalSize;
        }

        /// <summary>
        /// Returns a clipping geometry that indicates the area that will be clipped if the <see cref="UIElement.ClipToBounds"/> property is set to <c>true</c>. 
        /// </summary>
        /// <param name="layoutSlotSize">The available size of the element.</param>
        /// <returns>A <see cref="Geometry"/> that represents the area that is clipped if <see cref="UIElement.ClipToBounds"/> is <c>true</c>.</returns>
        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            // ZoomableCanvas only clips to bounds if ClipToBounds is set, no automatic clipping.
            return ClipToBounds ? new RectangleGeometry(new Rect(RenderSize)) : null;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Represents the extent of the instantiated UIElements calculated during <see cref="ArrangeOverride"/>.
        /// </summary>
        private Rect ChildrenExtent = Rect.Empty;

        /// <summary>
        /// Caches the calculated <see cref="Extent"/> based on the spatial index and arranged children of the canvas until <see cref="InvalidateExtent"/> is called.
        /// </summary>
        private Rect ComputedExtent = Rect.Empty;

        /// <summary>
        /// Gets the extent of the populated area of the canvas (in canvas coordinates).
        /// </summary>
        /// <remarks>
        /// This property is also used to determine the range of the scroll bars when the canvas is hosted within a <see cref="ScrollViewer"/>.
        /// </remarks>
        public virtual Rect Extent
        {
            get
            {
                if (ComputedExtent.IsEmpty)
                {
                    ComputedExtent = Rect.Union(ChildrenExtent, SpatialIndex != null ? SpatialIndex.Extent : Rect.Empty);
                }
                return ComputedExtent;
            }
        }

        /// <summary>
        /// Re-computes the <see cref="Extent"/> of items in the canvas and updates the parent scroll viewer if there is one.
        /// </summary>
        public void InvalidateExtent()
        {
            ComputedExtent = Rect.Empty;
            var owner = ((IScrollInfo)this).ScrollOwner;
            if (owner != null)
            {
                owner.InvalidateScrollInfo();
            }
        }

        /// <summary>
        /// Gets the current visual coordinates for a given <see cref="Point"/> on this <see cref="ZoomableCanvas"/>.
        /// </summary>
        /// <param name="canvasPoint">The <see cref="Point"/> in canvas coordinates.</param>
        /// <returns>The current position of the canvas point on the screen relative to the upper-left corner of this <see cref="ZoomableCanvas"/>.</returns>
        public Point GetVisualPoint(Point canvasPoint)
        {
            return (Point)(((Vector)canvasPoint * Scale) - (Vector)Offset);
        }

        /// <summary>
        /// Gets the point on the canvas that is currently represented by the given <see cref="Point"/> on the screen.
        /// </summary>
        /// <param name="screenPoint">The <see cref="Point"/> on the screen relative to the upper-left corner of this <see cref="ZoomableCanvas"/>.</param>
        /// <returns>The point on the canvas that corresponds to the given point on the screen.</returns>
        public Point GetCanvasPoint(Point screenPoint)
        {
            return (Point)(((Vector)Offset + (Vector)screenPoint) / Scale);
        }

        /// <summary>
        /// Returns the the point on the canvas at which the mouse cursor is currently located.
        /// </summary>
        public Point MousePosition
        {
            get
            {
                var position = Mouse.GetPosition(this);
                if (ApplyTransform)
                {
                    return position;
                }
                else
                {
                    return GetCanvasPoint(position);
                }
            }
        }

        #endregion

        #region IScrollInfo Implementation

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        ScrollViewer IScrollInfo.ScrollOwner
        {
            get;
            set;
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        bool IScrollInfo.CanHorizontallyScroll
        {
            get;
            set;
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        bool IScrollInfo.CanVerticallyScroll
        {
            get;
            set;
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        double IScrollInfo.ViewportHeight
        {
            get
            {
                return ActualViewbox.Height * Scale;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        double IScrollInfo.ViewportWidth
        {
            get
            {
                return ActualViewbox.Width * Scale;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        double IScrollInfo.ExtentHeight
        {
            get
            {
                return Math.Max(Math.Max(this.ActualViewbox.Bottom, this.Extent.Bottom) - Math.Min(this.ActualViewbox.Top, this.Extent.Top), 0.0) * Scale;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        double IScrollInfo.ExtentWidth
        {
            get
            {
                return Math.Max(Math.Max(this.ActualViewbox.Right, this.Extent.Right) - Math.Min(this.ActualViewbox.Left, this.Extent.Left), 0.0) * Scale;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        double IScrollInfo.HorizontalOffset
        {
            get
            {
                return Math.Max(this.ActualViewbox.X - Extent.X, 0.0) * this.Scale;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        double IScrollInfo.VerticalOffset
        {
            get
            {
                return Math.Max(this.ActualViewbox.Y - Extent.Y, 0.0) * this.Scale;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.LineDown()
        {
            ((IScrollInfo)this).SetVerticalOffset(((IScrollInfo)this).VerticalOffset + 16);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.LineLeft()
        {
            ((IScrollInfo)this).SetHorizontalOffset(((IScrollInfo)this).HorizontalOffset - 16);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.LineRight()
        {
            ((IScrollInfo)this).SetHorizontalOffset(((IScrollInfo)this).HorizontalOffset + 16);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.LineUp()
        {
            ((IScrollInfo)this).SetVerticalOffset(((IScrollInfo)this).VerticalOffset - 16);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.MouseWheelDown()
        {
            ((IScrollInfo)this).SetVerticalOffset(((IScrollInfo)this).VerticalOffset + 48);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.MouseWheelLeft()
        {
            ((IScrollInfo)this).SetHorizontalOffset(((IScrollInfo)this).HorizontalOffset - 48);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.MouseWheelRight()
        {
            ((IScrollInfo)this).SetHorizontalOffset(((IScrollInfo)this).HorizontalOffset + 48);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.MouseWheelUp()
        {
            ((IScrollInfo)this).SetVerticalOffset(((IScrollInfo)this).VerticalOffset - 48);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.PageDown()
        {
            ((IScrollInfo)this).SetVerticalOffset(((IScrollInfo)this).VerticalOffset + ((IScrollInfo)this).ViewportHeight);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.PageLeft()
        {
            ((IScrollInfo)this).SetHorizontalOffset(((IScrollInfo)this).HorizontalOffset - ((IScrollInfo)this).ViewportWidth);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.PageRight()
        {
            ((IScrollInfo)this).SetHorizontalOffset(((IScrollInfo)this).HorizontalOffset + ((IScrollInfo)this).ViewportWidth);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.PageUp()
        {
            ((IScrollInfo)this).SetVerticalOffset(((IScrollInfo)this).VerticalOffset - ((IScrollInfo)this).ViewportHeight);
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.SetHorizontalOffset(double offset)
        {
            offset = Math.Max(Math.Min(offset, ((IScrollInfo)this).ExtentWidth - ((IScrollInfo)this).ViewportWidth), 0.0);

            var viewbox = Viewbox;
            if (viewbox.IsEmpty)
            {
                Offset = new Point(Offset.X + offset - ((IScrollInfo)this).HorizontalOffset, Offset.Y);
            }
            else
            {
                viewbox.X += (offset - ((IScrollInfo)this).HorizontalOffset) / Scale;
                Viewbox = viewbox;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void IScrollInfo.SetVerticalOffset(double offset)
        {
            offset = Math.Max(Math.Min(offset, ((IScrollInfo)this).ExtentHeight - ((IScrollInfo)this).ViewportHeight), 0.0);

            var viewbox = Viewbox;
            if (viewbox.IsEmpty)
            {
                Offset = new Point(Offset.X, Offset.Y + offset - ((IScrollInfo)this).VerticalOffset);
            }
            else
            {
                viewbox.Y += (offset - ((IScrollInfo)this).VerticalOffset) / Scale;
                Viewbox = viewbox;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        Rect IScrollInfo.MakeVisible(Visual visual, Rect rectangle)
        {
            if (rectangle.IsEmpty || visual == null || !IsAncestorOf(visual))
            {
                return Rect.Empty;
            }

            rectangle = visual.TransformToAncestor(this).TransformBounds(rectangle);
            rectangle = RenderTransform.TransformBounds(rectangle);

            var width = ((IScrollInfo)this).ViewportWidth;
            var height = ((IScrollInfo)this).ViewportHeight;
            var left = -rectangle.X;
            var right = left + width - rectangle.Width;
            var top = -rectangle.Y;
            var bottom = top + height - rectangle.Height;
            var deltaX = left > 0 && right > 0 ? Math.Min(left, right) : left < 0 && right < 0 ? Math.Max(left, right) : 0.0;
            var deltaY = top > 0 && bottom > 0 ? Math.Min(top, bottom) : top < 0 && bottom < 0 ? Math.Max(top, bottom) : 0.0;

            var offset = Offset;
            offset.X -= deltaX;
            offset.Y -= deltaY;
            Offset = offset;

            rectangle.X += deltaX;
            rectangle.Y += deltaY;
            rectangle.Intersect(new Rect(0, 0, width, height));

            return rectangle;
        }

        #endregion
    }
}