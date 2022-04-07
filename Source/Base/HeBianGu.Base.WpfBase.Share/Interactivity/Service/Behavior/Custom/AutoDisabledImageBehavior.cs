// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// Behavior which applies a grayscale effect to an image when it's
    /// parent Button or MenuItem is disabled.
    /// 图片控件 当不可用时设置成灰白
    /// </summary>
    public class AutoDisabledImageBehavior : Behavior<Image>
    {
        private FrameworkElement _owningControl;
        private ImageSource _originalSource;
        private bool _changingSource, _isEnabled;

        /// <summary>
        /// Attached property to allow this to be associated with an image in a Style setter.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(AutoDisabledImageBehavior),
                new PropertyMetadata(default(bool), OnIsActiveChanged));

        /// <summary>
        /// Attached property getter
        /// </summary>
        /// <param name="theImage"></param>
        /// <returns></returns>
        public static bool GetIsActive(Image theImage)
        {
            return (bool)theImage.GetValue(IsActiveProperty);
        }

        /// <summary>
        /// Attached property setter
        /// </summary>
        /// <param name="theImage"></param>
        /// <param name="value"></param>
        public static void SetIsActive(Image theImage, bool value)
        {
            theImage.SetValue(IsActiveProperty, value);
        }

        /// <summary>
        /// Dependency Property to back the owner type property.
        /// </summary>
        public static readonly DependencyProperty OwnerTypeProperty =
            DependencyProperty.Register("OwnerType", typeof(Type), typeof(AutoDisabledImageBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Owner type to look for (automatically scans for Button or MenuItem)
        /// </summary>
        public Type OwnerType
        {
            get { return (Type)GetValue(OwnerTypeProperty); }
            set { SetValue(OwnerTypeProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            _originalSource = AssociatedObject.Source;

            DependencyPropertyDescriptor dp = DependencyPropertyDescriptor.FromProperty(Image.SourceProperty, typeof(Image));
            if (dp != null)
                dp.AddValueChanged(AssociatedObject, OnSourceChanged);

            AssociatedObject.Loaded += OnImageLoaded;
            AssociatedObject.Unloaded += OnImageUnloaded;

            if (AssociatedObject.IsLoaded)
            {
                OnImageLoaded(null, null);
            }
        }


        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            if (_owningControl != null)
            {
                OnImageUnloaded(null, null);
            }

            DependencyPropertyDescriptor dp = DependencyPropertyDescriptor.FromProperty(Image.SourceProperty, typeof(Image));
            if (dp != null)
                dp.RemoveValueChanged(AssociatedObject, OnSourceChanged);

            AssociatedObject.Loaded -= OnImageLoaded;
            AssociatedObject.Unloaded -= OnImageUnloaded;

            _originalSource = null;

            base.OnDetaching();
        }

        /// <summary>
        /// This is called when the Image.Source property is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSourceChanged(object sender, EventArgs e)
        {
            // If we are not the ones changing the source then cache off the new source.
            if (!_changingSource)
            {
                _originalSource = AssociatedObject.Source;
                if (AssociatedObject.IsLoaded && _owningControl != null)
                    UpdateImageSource(_isEnabled);
            }
        }

        /// <summary>
        /// This attaches our event handlers by locating the proper parent and hooking
        /// into it's IsEnabled change notification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImageLoaded(object sender, RoutedEventArgs e)
        {
            Debug.Assert(_owningControl == null);

            if (OwnerType != null)
                _owningControl = (FrameworkElement)AssociatedObject.GetParent(OwnerType);
            else
                _owningControl = AssociatedObject.GetParent<ButtonBase>()
                                    ?? (FrameworkElement)AssociatedObject.GetParent<MenuItem>();

            if (_owningControl != null)
            {
                UpdateImageSource(_owningControl.IsEnabled);
                _owningControl.IsEnabledChanged += OnOwnerEnabledStateChanged;
            }
        }

        /// <summary>
        /// This detaches the event handlers and unhooks our parent control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImageUnloaded(object sender, RoutedEventArgs e)
        {
            if (_owningControl != null)
            {
                _owningControl.IsEnabledChanged -= OnOwnerEnabledStateChanged;
                _owningControl = null;
            }
        }

        /// <summary>
        /// This is invoked when the owner (Button, etc.) IsEnabled state has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOwnerEnabledStateChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateImageSource(Convert.ToBoolean(e.NewValue));
        }

        /// <summary>
        /// This changes our image state from normal to disabled
        /// </summary>
        /// <param name="isEnabled"></param>
        private void UpdateImageSource(bool isEnabled)
        {
            _changingSource = true;
            _isEnabled = isEnabled;

            try
            {
                if (_isEnabled)
                {
#if WPF_TOOLKIT
                    AssociatedObject.Source = _originalSource;
#else
                    AssociatedObject.SetCurrentValue(Image.SourceProperty, _originalSource);
#endif
                    AssociatedObject.OpacityMask = null;
                }
                else
                {
                    BitmapSource bs = _originalSource as BitmapSource;
                    if (bs == null)
                    {
                        try
                        {
                            // See if we can load an image from the URI
                            bs = new BitmapImage(new Uri(_originalSource.ToString()));
                        }
                        catch
                        {
                            // Unknown bitmap type
                            return;
                        }
                    }

                    FormatConvertedBitmap grayImage = new FormatConvertedBitmap(bs, PixelFormats.Gray32Float, null, 0);
#if WPF_TOOLKIT
                    AssociatedObject.Source = grayImage;
#else
                    AssociatedObject.SetCurrentValue(Image.SourceProperty, grayImage);
#endif
                    AssociatedObject.OpacityMask = new ImageBrush(bs);

                }
            }
            finally
            {
                _changingSource = false;
            }
        }

        /// <summary>
        /// This adds the behavior to an image through an attached property.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnIsActiveChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            BehaviorCollection bc = Interaction.GetBehaviors(dpo);
            if (Convert.ToBoolean(e.NewValue))
            {
                bc.Add(new AutoDisabledImageBehavior());
            }
            else
            {
                Behavior behavior = bc.FirstOrDefault(beh => beh.GetType() == typeof(AutoDisabledImageBehavior));
                if (behavior != null)
                    bc.Remove(behavior);
            }
        }
    }
}
