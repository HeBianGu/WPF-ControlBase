using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public class Sprocket : Control
    {
        #region Constants

        private const double DEFAULT_INTERVAL = 60;
        private static readonly Color DEFAULT_TICK_COLOR = Color.FromArgb((byte)255, (byte)58, (byte)58, (byte)58);
        private const double DEFAULT_TICK_WIDTH = 3;
        private const int DEFAULT_TICK_COUNT = 12;
        private const double MINIMUM_INNER_RADIUS = 5;
        private const double MINIMUM_OUTER_RADIUS = 8;
        private readonly Size MINIMUM_CONTROL_SIZE = new Size(28, 28);
        private const double MINIMUM_PEN_WIDTH = 2;
        private const double DEFAULT_START_ANGLE = 270;
        private const double INNER_RADIUS_FACTOR = 0.175;
        private const double OUTER_RADIUS_FACTOR = 0.3125;
        // The Lower limit of the Alpha value (The spokes will be shown in 
        // alpha values ranging from 255 to m_AlphaLowerLimit)
        private const double ALPHA_LOWER_LIMIT = 5;
        private const double DEFAULT_PROGRESS_ALPHA = 10;
        private const double DEFAULT_PROGRESS = 0.0;
        #endregion

        #region Enums

        /// <summary>
        /// Defines the Direction of Rotation
        /// </summary>
        public enum Direction
        {
            CLOCKWISE,
            ANTICLOCKWISE
        }

        #endregion

        #region Structs

        /// <summary>
        /// Stores the details of each spoke
        /// </summary>
        struct Spoke
        {
            public Point StartPoint;
            public Point EndPoint;

            public Spoke(Point pt1, Point pt2)
            {
                StartPoint = pt1;
                EndPoint = pt2;
            }
        }

        #endregion

        #region Fields

        Point centerPoint = new Point();
        double innerRadius = 0;
        double outerRadius = 0;
        double alphaChange = 0;
        double angleIncrement = 0;
        double renderStartAngle = 0;
        System.Timers.Timer renderTimer = null;
        List<Spoke> spokes = null;

        #endregion

        #region Dependency Properties

        #region Interval

        /// <summary>
        /// Interval Dependency Property
        /// </summary>
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(double), typeof(Sprocket),
                new FrameworkPropertyMetadata(DEFAULT_INTERVAL,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              new PropertyChangedCallback(OnIntervalChanged)));

        /// <summary>
        /// Gets or sets the Interval property. This dependency property 
        /// indicates duration at which the timer for rotation should fire.
        /// </summary>
        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Interval property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket sprocket = (Sprocket)d;
            double oldInterval = (double)e.OldValue;
            double newInterval = sprocket.Interval;
            sprocket.OnIntervalChanged(oldInterval, newInterval);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Interval property.
        /// </summary>
        /// <param name="oldInterval">Old Value</param>
        /// <param name="newInterval">New Value</param>
        protected virtual void OnIntervalChanged(double oldInterval, double newInterval)
        {
            if (renderTimer != null)
            {
                bool isEnabled = renderTimer.Enabled;
                renderTimer.Enabled = false;
                renderTimer.Interval = newInterval;
                renderTimer.Enabled = isEnabled;
            }
        }

        #endregion

        #region IsIndeterminate

        /// <summary>
        /// IsIndeterminate Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(Sprocket),
                new FrameworkPropertyMetadata(true,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              (new PropertyChangedCallback(OnIsIndeterminateChanged))));

        /// <summary>
        /// Gets or sets the IsIndeterminate property. This dependency property 
        /// indicates whether the SprocketControl's progress is indeterminate or not.
        /// </summary>
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        /// <summary>
        /// Handles changes to the IsIndeterminate property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket target = (Sprocket)d;
            bool oldIsIndeterminate = (bool)e.OldValue;
            bool newIsIndeterminate = target.IsIndeterminate;
            target.OnIsIndeterminateChanged(oldIsIndeterminate, newIsIndeterminate);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the IsIndeterminate property.
        /// </summary>
        /// <param name="oldIsIndeterminate">Old Value</param>
        /// <param name="newIsIndeterminate">New Value</param>
        protected virtual void OnIsIndeterminateChanged(bool oldIsIndeterminate, bool newIsIndeterminate)
        {
            if (oldIsIndeterminate != newIsIndeterminate)
            {
                if (newIsIndeterminate)
                {
                    // Start the renderTimer
                    Start();
                }
                else
                {
                    // Stop the renderTimer
                    Stop();
                    InvalidateVisual();
                }
            }
        }

        #endregion

        #region Progress

        /// <summary>
        /// Progress Dependency Property
        /// </summary>
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(Sprocket),
                new FrameworkPropertyMetadata(DEFAULT_PROGRESS,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              new PropertyChangedCallback(OnProgressChanged),
                                              new CoerceValueCallback(CoerceProgress)));

        /// <summary>
        /// Gets or sets the Progress property. This dependency property 
        /// indicates the progress percentage.
        /// </summary>
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        /// <summary>
        /// Coerces the Progress value so that it stays in the range 0-100
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="value">New Value</param>
        /// <returns>Coerced Value</returns>
        private static object CoerceProgress(DependencyObject d, object value)
        {
            double progress = (double)value;

            if (progress < 0.0)
            {
                return 0.0;
            }
            else if (progress > 100.0)
            {
                return 100.0;
            }

            return value;
        }

        /// <summary>
        /// Handles changes to the Progress property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket sprocket = (Sprocket)d;
            double oldProgress = (double)e.OldValue;
            double newProgress = sprocket.Progress;
            sprocket.OnProgressChanged(oldProgress, newProgress);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Progress property.
        /// </summary>
        /// <param name="oldProgress">Old Value</param>
        /// <param name="newProgress">New Value</param>
        protected virtual void OnProgressChanged(double oldProgress, double newProgress)
        {
            InvalidateVisual();
        }

        #endregion

        #region Rotation

        /// <summary>
        /// Rotation Dependency Property
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(Direction), typeof(Sprocket),
                new FrameworkPropertyMetadata(Direction.CLOCKWISE,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              new PropertyChangedCallback(OnRotationChanged)));

        /// <summary>
        /// Gets or sets the Rotation property. This dependency property 
        /// indicates the direction of Rotation of the SprocketControl.
        /// </summary>
        public Direction Rotation
        {
            get { return (Direction)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Rotation property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnRotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket sprocket = (Sprocket)d;
            Direction oldRotation = (Direction)e.OldValue;
            Direction newRotation = sprocket.Rotation;
            sprocket.OnRotationChanged(oldRotation, newRotation);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Rotation property.
        /// </summary>
        /// <param name="oldRotation">Old Value</param>
        /// <param name="newRotation">New Value</param>
        protected virtual void OnRotationChanged(Direction oldRotation, Direction newRotation)
        {
            // Recalculate the spoke points
            CalculateSpokesPoints();
        }

        #endregion

        #region StartAngle

        /// <summary>
        /// StartAngle Dependency Property
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(Sprocket),
                new FrameworkPropertyMetadata(DEFAULT_START_ANGLE,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              new PropertyChangedCallback(OnStartAngleChanged)));

        /// <summary>
        /// Gets or sets the StartAngle property. This dependency property 
        /// indicates the angle at which the first spoke (with max opacity) is drawn.
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        /// <summary>
        /// Handles changes to the StartAngle property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnStartAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket sprocket = (Sprocket)d;
            double oldStartAngle = (double)e.OldValue;
            double newStartAngle = sprocket.StartAngle;
            sprocket.OnStartAngleChanged(oldStartAngle, newStartAngle);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the StartAngle property.
        /// </summary>
        /// <param name="oldStartAngle">Old Value</param>
        /// <param name="newStartAngle">New Value</param>
        protected virtual void OnStartAngleChanged(double oldStartAngle, double newStartAngle)
        {
            // Recalculate the spoke points
            CalculateSpokesPoints();
        }

        #endregion

        #region TickColor

        /// <summary>
        /// TickColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty TickColorProperty =
            DependencyProperty.Register("TickColor", typeof(Color), typeof(Sprocket),
                new FrameworkPropertyMetadata(DEFAULT_TICK_COLOR,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              new PropertyChangedCallback(OnTickColorChanged)));

        /// <summary>
        /// Gets or sets the TickColor property. This dependency property 
        /// indicates the color of the Spokes in the SprocketControl.
        /// </summary>
        public Color TickColor
        {
            get { return (Color)GetValue(TickColorProperty); }
            set { SetValue(TickColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the TickColor property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnTickColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket sprocket = (Sprocket)d;
            Color oldTickColor = (Color)e.OldValue;
            Color newTickColor = sprocket.TickColor;
            sprocket.OnTickColorChanged(oldTickColor, newTickColor);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the TickColor property.
        /// </summary>
        /// <param name="oldTickColor">Old Value</param>
        /// <param name="newTickColor">New Value</param>
        protected virtual void OnTickColorChanged(Color oldTickColor, Color newTickColor)
        {
            InvalidateVisual();
        }

        #endregion

        #region TickCount

        /// <summary>
        /// TickCount Dependency Property
        /// </summary>
        public static readonly DependencyProperty TickCountProperty =
            DependencyProperty.Register("TickCount", typeof(int), typeof(Sprocket),
                new FrameworkPropertyMetadata(DEFAULT_TICK_COUNT,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              new PropertyChangedCallback(OnTickCountChanged),
                                              new CoerceValueCallback(CoerceTickCount)));

        /// <summary>
        /// Gets or sets the TickCount property. This dependency property 
        /// indicates the number of spokes of the SprocketControl.
        /// </summary>
        public int TickCount
        {
            get { return (int)GetValue(TickCountProperty); }
            set { SetValue(TickCountProperty, value); }
        }

        /// <summary>
        /// Coerces the TickCount value to an acceptable value
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="value">New Value</param>
        /// <returns>Coerced Value</returns>
        private static object CoerceTickCount(DependencyObject d, object value)
        {
            int count = (int)value;

            if (count <= 0)
            {
                return DEFAULT_TICK_COUNT;
            }

            return value;
        }

        /// <summary>
        /// Handles changes to the TickCount property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnTickCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket sprocket = (Sprocket)d;
            int oldTickCount = (int)e.OldValue;
            int newTickCount = sprocket.TickCount;
            sprocket.OnTickCountChanged(oldTickCount, newTickCount);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the TickCount property.
        /// </summary>
        /// <param name="oldTickCount">Old Value</param>
        /// <param name="newTickCount">New Value</param>
        protected virtual void OnTickCountChanged(int oldTickCount, int newTickCount)
        {
            // Recalculate the spoke points
            CalculateSpokesPoints();
        }

        #endregion

        #region TickStyle

        /// <summary>
        /// TickStyle Dependency Property
        /// </summary>
        public static readonly DependencyProperty TickStyleProperty =
            DependencyProperty.Register("TickStyle", typeof(PenLineCap), typeof(Sprocket),
                new FrameworkPropertyMetadata(PenLineCap.Round, FrameworkPropertyMetadataOptions.AffectsRender, (new PropertyChangedCallback(OnTickStyleChanged))));

        /// <summary>
        /// Gets or sets the TickStyle property. This dependency property 
        /// indicates the style of the ends of each tick.
        /// </summary>
        public PenLineCap TickStyle
        {
            get { return (PenLineCap)GetValue(TickStyleProperty); }
            set { SetValue(TickStyleProperty, value); }
        }

        /// <summary>
        /// Handles changes to the TickStyle property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnTickStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket sprocket = (Sprocket)d;
            PenLineCap oldTickStyle = (PenLineCap)e.OldValue;
            PenLineCap newTickStyle = sprocket.TickStyle;
            sprocket.OnTickStyleChanged(oldTickStyle, newTickStyle);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the TickStyle property.
        /// </summary>
        /// <param name="oldTickStyle">Old Value</param>
        /// <param name="newTickStyle">New Value</param>
        protected virtual void OnTickStyleChanged(PenLineCap oldTickStyle, PenLineCap newTickStyle)
        {
            InvalidateVisual();
        }

        #endregion

        #region TickWidth

        /// <summary>
        /// TickWidth Dependency Property
        /// </summary>
        public static readonly DependencyProperty TickWidthProperty =
            DependencyProperty.Register("TickWidth", typeof(double), typeof(Sprocket),
                new FrameworkPropertyMetadata(DEFAULT_TICK_WIDTH,
                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                              new PropertyChangedCallback(OnTickWidthChanged),
                                              new CoerceValueCallback(CoerceTickWidth)));

        /// <summary>
        /// Gets or sets the TickWidth property. This dependency property 
        /// indicates the width of each spoke in the SprocketControl.
        /// </summary>
        public double TickWidth
        {
            get { return (double)GetValue(TickWidthProperty); }
            set { SetValue(TickWidthProperty, value); }
        }

        /// <summary>
        /// Coerces the TickWidth value so that it stays above 0.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="value">New Value</param>
        /// <returns>Coerced Value</returns>
        private static object CoerceTickWidth(DependencyObject d, object value)
        {
            double progress = (double)value;

            if (progress < 0.0)
            {
                return DEFAULT_TICK_WIDTH;
            }

            return value;
        }
        /// <summary>
        /// Handles changes to the TickWidth property.
        /// </summary>
        /// <param name="d">SprocketControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnTickWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Sprocket target = (Sprocket)d;
            double oldTickWidth = (double)e.OldValue;
            double newTickWidth = target.TickWidth;
            target.OnTickWidthChanged(oldTickWidth, newTickWidth);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the TickWidth property.
        /// </summary>
        /// <param name="oldTickWidth">Old Value</param>
        /// <param name="newTickWidth">New Value</param>
        protected virtual void OnTickWidthChanged(double oldTickWidth, double newTickWidth)
        {
            InvalidateVisual();
        }

        #endregion

        #endregion

        #region Construction

        /// <summary>
        /// Ctor
        /// </summary>
        public Sprocket()
        {
            //InitializeComponent();

            renderTimer = new System.Timers.Timer(this.Interval);
            renderTimer.Elapsed += new ElapsedEventHandler(OnRenderTimerElapsed);

            // Set the minimum size of the SprocketControl
            this.MinWidth = MINIMUM_CONTROL_SIZE.Width;
            this.MinWidth = MINIMUM_CONTROL_SIZE.Height;

            // Calculate the spoke points based on the current size
            CalculateSpokesPoints();

            this.Loaded += (s, e) =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (IsIndeterminate)
                        Start();
                }));
            };
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Start the Tick Control rotation
        /// </summary>
        private void Start()
        {
            if ((renderTimer != null) && (!renderTimer.Enabled))
            {
                renderTimer.Interval = this.Interval;
                renderTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Stop the Tick Control rotation
        /// </summary>
        private void Stop()
        {
            if (renderTimer != null)
            {
                renderTimer.Enabled = false;
            }
        }

        /// <summary>
        /// Converts Degrees to Radians
        /// </summary>
        /// <param name="degrees">Degrees</param>
        /// <returns>Radians</returns>
        private double ConvertDegreesToRadians(double degrees)
        {
            return ((Math.PI / (double)180) * degrees);
        }

        /// <summary>
        /// Calculate the Spoke Points and store them
        /// </summary>
        private void CalculateSpokesPoints()
        {
            spokes = new List<Spoke>();

            // Calculate the angle between adjacent spokes
            angleIncrement = (360 / (double)TickCount);
            // Calculate the change in alpha between adjacent spokes
            alphaChange = (int)((255 - ALPHA_LOWER_LIMIT) / (double)TickCount);

            // Set the start angle for rendering
            renderStartAngle = StartAngle;

            // Calculate the location around which the spokes will be drawn
            double width = (this.Width < this.Height) ? this.Width : this.Height;
            centerPoint = new Point(this.Width / 2, this.Height / 2);
            // Calculate the inner and outer radii of the control. The radii should not be less than the
            // Minimum values
            innerRadius = (int)(width * INNER_RADIUS_FACTOR);
            if (innerRadius < MINIMUM_INNER_RADIUS)
                innerRadius = MINIMUM_INNER_RADIUS;
            outerRadius = (int)(width * OUTER_RADIUS_FACTOR);
            if (outerRadius < MINIMUM_OUTER_RADIUS)
                outerRadius = MINIMUM_OUTER_RADIUS;

            double angle = 0;

            for (int i = 0; i < TickCount; i++)
            {
                Point pt1 = new Point(innerRadius * (float)Math.Cos(ConvertDegreesToRadians(angle)), innerRadius * (float)Math.Sin(ConvertDegreesToRadians(angle)));
                Point pt2 = new Point(outerRadius * (float)Math.Cos(ConvertDegreesToRadians(angle)), outerRadius * (float)Math.Sin(ConvertDegreesToRadians(angle)));

                // Create a spoke based on the points generated
                Spoke spoke = new Spoke(pt1, pt2);
                // Add the spoke to the List
                spokes.Add(spoke);

                // If it is not it Indeterminate state, 
                // ensure that the spokes are drawn in clockwise manner
                if (!IsIndeterminate)
                {
                    angle += angleIncrement;
                }
                else
                {
                    if (Rotation == Direction.CLOCKWISE)
                    {
                        angle -= angleIncrement;
                    }
                    else if (Rotation == Direction.ANTICLOCKWISE)
                    {
                        angle += angleIncrement;
                    }
                }
            }
        }

        #endregion

        #region Overrides

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            // Calculate the spoke points based on the new size
            CalculateSpokesPoints();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (spokes == null)
                return;

            TranslateTransform translate = new TranslateTransform(centerPoint.X, centerPoint.Y);
            dc.PushTransform(translate);
            RotateTransform rotate = new RotateTransform(renderStartAngle);
            dc.PushTransform(rotate);

            byte alpha = (byte)255;

            // Get the number of spokes that can be drawn with zero transparency
            int progressSpokes = (int)Math.Floor((Progress * TickCount) / 100.0);

            // Render the spokes
            for (int i = 0; i < TickCount; i++)
            {
                if (!IsIndeterminate)
                {
                    if (progressSpokes > 0)
                        alpha = (byte)(i < progressSpokes ? 255 : DEFAULT_PROGRESS_ALPHA);
                    else
                        alpha = (byte)DEFAULT_PROGRESS_ALPHA;
                }

                Pen p = new Pen(new SolidColorBrush(Color.FromArgb(alpha, this.TickColor.R, this.TickColor.G, this.TickColor.B)), TickWidth);
                p.StartLineCap = p.EndLineCap = TickStyle;
                dc.DrawLine(p, spokes[i].StartPoint, spokes[i].EndPoint);

                if (IsIndeterminate)
                {
                    alpha -= (byte)alphaChange;
                    if (alpha < ALPHA_LOWER_LIMIT)
                        alpha = (byte)ALPHA_LOWER_LIMIT;
                }
            }

            // Perform a reverse Rotation and Translation to obtain the original Transformation
            dc.Pop();
            dc.Pop();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Elapsed event of the renderTimer
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        void OnRenderTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Rotation == Direction.CLOCKWISE)
                {
                    renderStartAngle += angleIncrement;

                    if (renderStartAngle >= 360)
                        renderStartAngle -= 360;
                }
                else if (Rotation == Direction.ANTICLOCKWISE)
                {
                    renderStartAngle -= angleIncrement;

                    if (renderStartAngle <= -360)
                        renderStartAngle += 360;
                }

                // Force re-rendering of control
                InvalidateVisual();
            }));
        }

        #endregion
    }
}
