// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HeBianGu.Control.Message
{
    /// <summary>
    /// Implements a <see cref="Snackbar"/> inspired by the Material Design specs (https://material.google.com/components/snackbars-toasts.html).
    /// </summary>
    [ContentProperty(nameof(Message))]
    public class Snackbar : System.Windows.Controls.Control
    {
        private const string ActivateStoryboardName = "ActivateStoryboard";
        private const string DeactivateStoryboardName = "DeactivateStoryboard";

        private Action _messageQueueRegistrationCleanUp = null;

        static Snackbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(typeof(Snackbar)));
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message), typeof(SnackbarMessage), typeof(Snackbar), new PropertyMetadata(default(SnackbarMessage)));

        public SnackbarMessage Message
        {
            get { return (SnackbarMessage)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageQueueProperty = DependencyProperty.Register(
            nameof(MessageQueue), typeof(SnackbarMessageQueue), typeof(Snackbar), new PropertyMetadata(default(SnackbarMessageQueue), MessageQueuePropertyChangedCallback));

        private static void MessageQueuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            Snackbar snackbar = (Snackbar)dependencyObject;
            (snackbar._messageQueueRegistrationCleanUp ?? (() => { }))();
            SnackbarMessageQueue messageQueue = dependencyPropertyChangedEventArgs.NewValue as SnackbarMessageQueue;
            snackbar._messageQueueRegistrationCleanUp = messageQueue?.Pair(snackbar);
        }

        public SnackbarMessageQueue MessageQueue
        {
            get { return (SnackbarMessageQueue)GetValue(MessageQueueProperty); }
            set { SetValue(MessageQueueProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive), typeof(bool), typeof(Snackbar), new PropertyMetadata(default(bool), IsActivePropertyChangedCallback));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public event RoutedPropertyChangedEventHandler<bool> IsActiveChanged
        {
            add { AddHandler(IsActiveChangedEvent, value); }
            remove { RemoveHandler(IsActiveChangedEvent, value); }
        }

        public static readonly RoutedEvent IsActiveChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(IsActiveChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>),
                typeof(Snackbar));

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Snackbar instance = d as Snackbar;
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(
                (bool)e.OldValue,
                (bool)e.NewValue)
            { RoutedEvent = IsActiveChangedEvent };
            instance?.RaiseEvent(args);
        }

        public static readonly RoutedEvent DeactivateStoryboardCompletedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(DeactivateStoryboardCompleted),
                RoutingStrategy.Bubble,
                typeof(SnackbarMessageEventArgs),
                typeof(Snackbar));

        public event RoutedPropertyChangedEventHandler<SnackbarMessage> DeactivateStoryboardCompleted
        {
            add { AddHandler(DeactivateStoryboardCompletedEvent, value); }
            remove { RemoveHandler(DeactivateStoryboardCompletedEvent, value); }
        }

        private static void OnDeactivateStoryboardCompleted(
            IInputElement snackbar, SnackbarMessage message)
        {
            SnackbarMessageEventArgs args = new SnackbarMessageEventArgs(DeactivateStoryboardCompletedEvent, message);
            snackbar.RaiseEvent(args);
        }

        public TimeSpan ActivateStoryboardDuration { get; private set; }

        public TimeSpan DeactivateStoryboardDuration { get; private set; }

        public static readonly DependencyProperty ActionButtonStyleProperty = DependencyProperty.Register(
            nameof(ActionButtonStyle), typeof(System.Windows.Style), typeof(Snackbar), new PropertyMetadata(default(System.Windows.Style)));

        public System.Windows.Style ActionButtonStyle
        {
            get { return (System.Windows.Style)GetValue(ActionButtonStyleProperty); }
            set { SetValue(ActionButtonStyleProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            //we regards to notification of deactivate storyboard finishing,
            //we either build a storyboard in code and subscribe to completed event, 
            //or take the not 100% proof of the storyboard duration from the storyboard itself
            //...HOWEVER...we can both methods result can work under the same public API so 
            //we can flip the implementation if this version doesnt pan out

            //(currently we have no even on the activate animation; don't 
            // need it just now, but it would mirror the deactivate)

            ActivateStoryboardDuration = GetStoryboardResourceDuration(ActivateStoryboardName);
            DeactivateStoryboardDuration = GetStoryboardResourceDuration(DeactivateStoryboardName);

            base.OnApplyTemplate();
        }

        private TimeSpan GetStoryboardResourceDuration(string resourceName)
        {
            Storyboard storyboard = Template.Resources.Contains(resourceName)
                ? (Storyboard)Template.Resources[resourceName]
                : null;

            return storyboard != null && storyboard.Duration.HasTimeSpan
                ? storyboard.Duration.TimeSpan
                : new Func<TimeSpan>(() =>
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Warning, no Duration was specified at root of storyboard '{resourceName}'.");
                    return TimeSpan.Zero;
                })();
        }

        private static void IsActivePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            OnIsActiveChanged(dependencyObject, dependencyPropertyChangedEventArgs);

            if ((bool)dependencyPropertyChangedEventArgs.NewValue) return;

            Snackbar snackbar = (Snackbar)dependencyObject;
            if (snackbar.Message == null) return;

            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Tag = new Tuple<Snackbar, SnackbarMessage>(snackbar, snackbar.Message),
                Interval = snackbar.DeactivateStoryboardDuration
            };
            dispatcherTimer.Tick += DeactivateStoryboardDispatcherTimerOnTick;
            dispatcherTimer.Start();
        }

        private static void DeactivateStoryboardDispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            DispatcherTimer dispatcherTimer = (DispatcherTimer)sender;
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= DeactivateStoryboardDispatcherTimerOnTick;
            Tuple<Snackbar, SnackbarMessage> source = (Tuple<Snackbar, SnackbarMessage>)dispatcherTimer.Tag;
            OnDeactivateStoryboardCompleted(source.Item1, source.Item2);
        }
    }



}