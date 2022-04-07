// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    ///     波浪进度条
    /// </summary>
    [TemplatePart(Name = ElementWave, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementClip, Type = typeof(FrameworkElement))]
    public class WaveProgressBar : RangeBase
    {
        private const string ElementWave = "PART_Wave";

        private const string ElementClip = "PART_Clip";

        private FrameworkElement _waveElement;

        private const double TranslateTransformMinY = -20;

        private double _translateTransformYRange;

        private TranslateTransform _translateTransform;

        public WaveProgressBar() => Loaded += (s, e) => UpdateWave(Value);

        static WaveProgressBar()
        {
            FocusableProperty.OverrideMetadata(typeof(WaveProgressBar),
                new FrameworkPropertyMetadata(false));
            MaximumProperty.OverrideMetadata(typeof(WaveProgressBar),
                new FrameworkPropertyMetadata(100.0));
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            UpdateWave(newValue);
        }

        private void UpdateWave(double value)
        {
            if (_translateTransform == null || Math.Abs(Maximum) < 1E-06) return;
            double scale = 1 - value / Maximum;
            double y = _translateTransformYRange * scale + TranslateTransformMinY;
            _translateTransform.Y = y;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _waveElement = GetTemplateChild(ElementWave) as FrameworkElement;
            FrameworkElement clipElement = GetTemplateChild(ElementClip) as FrameworkElement;

            if (_waveElement != null && clipElement != null)
            {
                _translateTransform = new TranslateTransform
                {
                    Y = clipElement.Height
                };
                _translateTransformYRange = clipElement.Height - TranslateTransformMinY;
                _waveElement.RenderTransform = new TransformGroup
                {
                    Children = { _translateTransform }
                };
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(WaveProgressBar), new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty ShowTextProperty = DependencyProperty.Register(
            "ShowText", typeof(bool), typeof(WaveProgressBar), new PropertyMetadata(true));

        public bool ShowText
        {
            get => (bool)GetValue(ShowTextProperty);
            set => SetValue(ShowTextProperty, value);
        }

        public static readonly DependencyProperty WaveFillProperty = DependencyProperty.Register(
            "WaveFill", typeof(Brush), typeof(WaveProgressBar), new PropertyMetadata(default(Brush)));

        public Brush WaveFill
        {
            get => (Brush)GetValue(WaveFillProperty);
            set => SetValue(WaveFillProperty, value);
        }

        public static readonly DependencyProperty WaveThicknessProperty = DependencyProperty.Register(
            "WaveThickness", typeof(double), typeof(WaveProgressBar), new PropertyMetadata(0.0));

        public double WaveThickness
        {
            get => (double)GetValue(WaveThicknessProperty);
            set => SetValue(WaveThicknessProperty, value);
        }

        public static readonly DependencyProperty WaveStrokeProperty = DependencyProperty.Register(
            "WaveStroke", typeof(Brush), typeof(WaveProgressBar), new PropertyMetadata(default(Brush)));

        public Brush WaveStroke
        {
            get => (Brush)GetValue(WaveStrokeProperty);
            set => SetValue(WaveStrokeProperty, value);
        }
    }
}