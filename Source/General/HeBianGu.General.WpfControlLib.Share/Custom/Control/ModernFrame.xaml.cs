// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> 自定义导航框架 </summary>
    public class ModernFrame : ContentControl
    {
        /// <summary>
        /// Identifies the KeepAlive attached dependency property.
        /// </summary>
        public static readonly DependencyProperty KeepAliveProperty = DependencyProperty.RegisterAttached("KeepAlive", typeof(bool?), typeof(ModernFrame), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the KeepContentAlive dependency property.
        /// </summary>
        public static readonly DependencyProperty KeepContentAliveProperty = DependencyProperty.Register("KeepContentAlive", typeof(bool), typeof(ModernFrame), new PropertyMetadata(true, OnKeepContentAliveChanged));

        /// <summary>
        /// Identifies the ContentLoader dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(ModernFrame), new PropertyMetadata(new DefaultContentLoader(), OnContentLoaderChanged));

        private static readonly DependencyPropertyKey IsLoadingContentPropertyKey = DependencyProperty.RegisterReadOnly("IsLoadingContent", typeof(bool), typeof(ModernFrame), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the IsLoadingContent dependency property.
        /// </summary>
        public static readonly DependencyProperty IsLoadingContentProperty = IsLoadingContentPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the Source dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Uri), typeof(ModernFrame), new PropertyMetadata(OnSourceChanged));

        /// <summary>
        /// Occurs when navigation to a content fragment begins.
        /// </summary>
        public event EventHandler<FragmentNavigationEventArgs> FragmentNavigation;

        /// <summary>
        /// Occurs when a new navigation is requested.
        /// </summary>
        /// <remarks>
        /// The navigating event is also raised when a parent frame is navigating. This allows for cancelling parent navigation.
        /// </remarks>
        public event EventHandler<NavigatingCancelEventArgs> Navigating;

        /// <summary>
        /// Occurs when navigation to new content has completed.
        /// </summary>
        public event EventHandler<NavigationEventArgs> Navigated;

        /// <summary>
        /// Occurs when navigation has failed.
        /// </summary>
        public event EventHandler<NavigationFailedEventArgs> NavigationFailed;

        private Stack<Uri> history = new Stack<Uri>();

        private Dictionary<Uri, object> contentCache = new Dictionary<Uri, object>();

        // list of registered frames in sub tree
        private List<WeakReference<ModernFrame>> childFrames = new List<WeakReference<ModernFrame>>();

        private CancellationTokenSource tokenSource;
        private bool isNavigatingHistory;
        private bool isResetSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernFrame"/> class.
        /// </summary>
        public ModernFrame()
        {
            this.DefaultStyleKey = typeof(ModernFrame);

            // associate application and navigation commands with this instance
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseBack, OnBrowseBack, OnCanBrowseBack));
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.GoToPage, OnGoToPage, OnCanGoToPage));
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.Refresh, OnRefresh, OnCanRefresh));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopy, OnCanCopy));

            this.Loaded += OnLoaded;
        }

        private static void OnKeepContentAliveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernFrame)o).OnKeepContentAliveChanged((bool)e.NewValue);
        }

        private void OnKeepContentAliveChanged(bool keepAlive)
        {
            // clear content cache
            this.contentCache.Clear();
        }

        private static void OnContentLoaderChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                // null values for content loader not allowed
                throw new ArgumentNullException("ContentLoader");
            }
        }

        private static void OnSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernFrame)o).OnSourceChanged((Uri)e.OldValue, (Uri)e.NewValue);
        }

        private void OnSourceChanged(Uri oldValue, Uri newValue)
        {
            // if resetting source or old source equals new, don't do anything
            if (this.isResetSource || newValue != null && newValue.Equals(oldValue))
            {
                return;
            }

            // handle fragment navigation
            string newFragment = null;
            Uri oldValueNoFragment = NavigationHelper.RemoveFragment(oldValue);
            Uri newValueNoFragment = NavigationHelper.RemoveFragment(newValue, out newFragment);

            if (newValueNoFragment != null && newValueNoFragment.Equals(oldValueNoFragment))
            {
                // fragment navigation
                FragmentNavigationEventArgs args = new FragmentNavigationEventArgs
                {
                    Fragment = newFragment
                };

                OnFragmentNavigation(this.Content as IContent, args);
            }
            else
            {
                NavigationType navType = this.isNavigatingHistory ? NavigationType.Back : NavigationType.New;

                // only invoke CanNavigate for new navigation
                if (!this.isNavigatingHistory && !CanNavigate(oldValue, newValue, navType))
                {
                    return;
                }

                Navigate(oldValue, newValue, navType);
            }
        }
        private bool CanNavigate(Uri oldValue, Uri newValue, NavigationType navigationType)
        {
            NavigatingCancelEventArgs cancelArgs = new HeBianGu.General.WpfControlLib.NavigatingCancelEventArgs
            {
                Frame = this,
                Source = newValue,
                IsParentFrameNavigating = true,
                NavigationType = navigationType,
                Cancel = false,
            };
            OnNavigating(this.Content as IContent, cancelArgs);

            // check if navigation cancelled
            if (cancelArgs.Cancel)
            {
                Debug.WriteLine("Cancelled navigation from '{0}' to '{1}'", oldValue, newValue);

                if (this.Source != oldValue)
                {
                    // enqueue the operation to reset the source back to the old value
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        this.isResetSource = true;
                        SetCurrentValue(SourceProperty, oldValue);
                        this.isResetSource = false;
                    }));
                }
                return false;
            }

            return true;
        }

        private void Navigate(Uri oldValue, Uri newValue, NavigationType navigationType)
        {
            Debug.WriteLine("Navigating from '{0}' to '{1}'", oldValue, newValue);

            // set IsLoadingContent state
            SetValue(IsLoadingContentPropertyKey, true);

            // cancel previous load content task (if any)
            // note: no need for thread synchronization, this code always executes on the UI thread
            if (this.tokenSource != null)
            {
                this.tokenSource.Cancel();
                this.tokenSource = null;
            }

            // push previous source onto the history stack (only for new navigation types)
            if (oldValue != null && navigationType == NavigationType.New)
            {
                this.history.Push(oldValue);
            }

            object newContent = null;

            if (newValue != null)
            {
                // content is cached on uri without fragment
                Uri newValueNoFragment = NavigationHelper.RemoveFragment(newValue);

                if (navigationType == NavigationType.Refresh || !this.contentCache.TryGetValue(newValueNoFragment, out newContent))
                {
                    CancellationTokenSource localTokenSource = new CancellationTokenSource();
                    this.tokenSource = localTokenSource;
                    // load the content (asynchronous!)
                    TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                    Task<object> task = this.ContentLoader.LoadContentAsync(newValue, this.tokenSource.Token);

                    task.ContinueWith(t =>
                    {
                        try
                        {
                            if (t.IsCanceled || localTokenSource.IsCancellationRequested)
                            {
                                Debug.WriteLine("Cancelled navigation to '{0}'", newValue);
                            }
                            else if (t.IsFaulted)
                            {
                                // raise failed event
                                NavigationFailedEventArgs failedArgs = new NavigationFailedEventArgs
                                {
                                    Frame = this,
                                    Source = newValue,
                                    Error = t.Exception.InnerException,
                                    Handled = false
                                };

                                OnNavigationFailed(failedArgs);

                                // if not handled, show error as content
                                newContent = failedArgs.Handled ? null : failedArgs.Error;

                                SetContent(newValue, navigationType, newContent, true);
                            }
                            else
                            {
                                newContent = t.Result;
                                if (ShouldKeepContentAlive(newContent))
                                {
                                    // keep the new content in memory
                                    this.contentCache[newValueNoFragment] = newContent;
                                }

                                SetContent(newValue, navigationType, newContent, false);
                            }
                        }
                        finally
                        {
                            // clear global tokenSource to avoid a Cancel on a disposed object
                            if (this.tokenSource == localTokenSource)
                            {
                                this.tokenSource = null;
                            }

                            // and dispose of the local tokensource
                            localTokenSource.Dispose();
                        }
                    }, scheduler);
                    return;
                }
            }

            // newValue is null or newContent was found in the cache
            SetContent(newValue, navigationType, newContent, false);
        }

        private void SetContent(Uri newSource, NavigationType navigationType, object newContent, bool contentIsError)
        {
            IContent oldContent = this.Content as IContent;

            // assign content
            this.Content = newContent;

            // do not raise navigated event when error
            if (!contentIsError)
            {
                NavigationEventArgs args = new NavigationEventArgs
                {
                    Frame = this,
                    Source = newSource,
                    Content = newContent,
                    NavigationType = navigationType
                };

                OnNavigated(oldContent, newContent as IContent, args);
            }

            // set IsLoadingContent to false
            SetValue(IsLoadingContentPropertyKey, false);

            if (!contentIsError)
            {
                // and raise optional fragment navigation events
                string fragment;
                NavigationHelper.RemoveFragment(newSource, out fragment);
                if (fragment != null)
                {
                    // fragment navigation
                    FragmentNavigationEventArgs fragmentArgs = new FragmentNavigationEventArgs
                    {
                        Fragment = fragment
                    };

                    OnFragmentNavigation(newContent as IContent, fragmentArgs);
                }
            }
        }

        private IEnumerable<ModernFrame> GetChildFrames()
        {
            WeakReference<ModernFrame>[] refs = this.childFrames.ToArray();
            foreach (WeakReference<ModernFrame> r in refs)
            {
                bool valid = false;
                ModernFrame frame;

#if NET4
                if (r.IsAlive) {
                    frame = (ModernFrame)r.Target;
#else
                if (r.TryGetTarget(out frame))
                {
#endif
                    // check if frame is still an actual child (not the case when child is removed, but not yet garbage collected)
                    if (NavigationHelper.FindFrame(null, frame) == this)
                    {
                        valid = true;
                        yield return frame;
                    }
                }

                if (!valid)
                {
                    this.childFrames.Remove(r);
                }
            }
        }

        private void OnFragmentNavigation(IContent content, FragmentNavigationEventArgs e)
        {
            // invoke optional IContent.OnFragmentNavigation
            if (content != null)
            {
                content.OnFragmentNavigation(e);
            }

            // raise the FragmentNavigation event
            if (FragmentNavigation != null)
            {
                FragmentNavigation(this, e);
            }
        }

        private void OnNavigating(IContent content, NavigatingCancelEventArgs e)
        {
            // first invoke child frame navigation events
            foreach (ModernFrame f in GetChildFrames())
            {
                f.OnNavigating(f.Content as IContent, e);
            }

            e.IsParentFrameNavigating = e.Frame != this;

            // invoke IContent.OnNavigating (only if content implements IContent)
            if (content != null)
            {
                content.OnNavigatingFrom(e);
            }

            // raise the Navigating event
            if (Navigating != null)
            {
                Navigating(this, e);
            }
        }

        private void OnNavigated(IContent oldContent, IContent newContent, NavigationEventArgs e)
        {
            // invoke IContent.OnNavigatedFrom and OnNavigatedTo
            if (oldContent != null)
            {
                oldContent.OnNavigatedFrom(e);
            }
            if (newContent != null)
            {
                newContent.OnNavigatedTo(e);
            }

            // raise the Navigated event
            if (Navigated != null)
            {
                Navigated(this, e);
            }
        }

        private void OnNavigationFailed(NavigationFailedEventArgs e)
        {
            if (NavigationFailed != null)
            {
                NavigationFailed(this, e);
            }
        }

        /// <summary>
        /// Determines whether the routed event args should be handled.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <remarks>This method prevents parent frames from handling routed commands.</remarks>
        private bool HandleRoutedEvent(CanExecuteRoutedEventArgs args)
        {
            DependencyObject originalSource = args.OriginalSource as DependencyObject;

            if (originalSource == null)
            {
                return false;
            }
            return originalSource.AncestorsAndSelf().OfType<ModernFrame>().FirstOrDefault() == this;
        }

        private void OnCanBrowseBack(object sender, CanExecuteRoutedEventArgs e)
        {
            // only enable browse back for source frame, do not bubble
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.history.Count > 0;
            }
        }

        private void OnCanCopy(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.Content != null;
            }
        }

        private void OnCanGoToPage(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = e.Parameter is String || e.Parameter is Uri;
            }
        }

        private void OnCanRefresh(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.Source != null;
            }
        }

        private void OnBrowseBack(object target, ExecutedRoutedEventArgs e)
        {
            if (this.history.Count > 0)
            {
                Uri oldValue = this.Source;
                Uri newValue = this.history.Peek();     // do not remove just yet, navigation may be cancelled

                if (CanNavigate(oldValue, newValue, NavigationType.Back))
                {
                    this.isNavigatingHistory = true;
                    SetCurrentValue(SourceProperty, this.history.Pop());
                    this.isNavigatingHistory = false;
                }
            }
        }

        private void OnGoToPage(object target, ExecutedRoutedEventArgs e)
        {
            Uri newValue = NavigationHelper.ToUri(e.Parameter);
            SetCurrentValue(SourceProperty, newValue);
        }

        private void OnRefresh(object target, ExecutedRoutedEventArgs e)
        {
            if (CanNavigate(this.Source, this.Source, NavigationType.Refresh))
            {
                Navigate(this.Source, this.Source, NavigationType.Refresh);
            }
        }

        private void OnCopy(object target, ExecutedRoutedEventArgs e)
        {
            // copies the string representation of the current content to the clipboard
            Clipboard.SetText(this.Content.ToString());
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ModernFrame parent = NavigationHelper.FindFrame(NavigationHelper.FrameParent, this);
            if (parent != null)
            {
                parent.RegisterChildFrame(this);
            }
        }

        private void RegisterChildFrame(ModernFrame frame)
        {
            // do not register existing frame
            if (!GetChildFrames().Contains(frame))
            {
#if NET4
                var r = new WeakReference(frame);
#else
                WeakReference<ModernFrame> r = new WeakReference<ModernFrame>(frame);
#endif
                this.childFrames.Add(r);
            }
        }

        /// <summary>
        /// Determines whether the specified content should be kept alive.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool ShouldKeepContentAlive(object content)
        {
            DependencyObject o = content as DependencyObject;
            if (o != null)
            {
                bool? result = GetKeepAlive(o);

                // if a value exists for given content, use it
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            // otherwise let the ModernFrame decide
            return this.KeepContentAlive;
        }

        /// <summary>
        /// Gets a value indicating whether to keep specified object alive in a ModernFrame instance.
        /// </summary>
        /// <param name="o">The target dependency object.</param>
        /// <returns>Whether to keep the object alive. Null to leave the decision to the ModernFrame.</returns>
        public static bool? GetKeepAlive(DependencyObject o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            return (bool?)o.GetValue(KeepAliveProperty);
        }

        /// <summary>
        /// Sets a value indicating whether to keep specified object alive in a ModernFrame instance.
        /// </summary>
        /// <param name="o">The target dependency object.</param>
        /// <param name="value">Whether to keep the object alive. Null to leave the decision to the ModernFrame.</param>
        public static void SetKeepAlive(DependencyObject o, bool? value)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            o.SetValue(KeepAliveProperty, value);
        }

        /// <summary>
        /// Gets or sets a value whether content should be kept in memory.
        /// </summary>
        public bool KeepContentAlive
        {
            get { return (bool)GetValue(KeepContentAliveProperty); }
            set { SetValue(KeepContentAliveProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content loader.
        /// </summary>
        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is currently loading content.
        /// </summary>
        public bool IsLoadingContent
        {
            get { return (bool)GetValue(IsLoadingContentProperty); }
        }

        /// <summary>
        /// Gets or sets the source of the current content.
        /// </summary>
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }

    /// <summary>
    /// Identifies the types of navigation that are supported.
    /// </summary>
    public enum NavigationType
    {
        /// <summary>
        /// Navigating to new content.
        /// </summary>
        New,
        /// <summary>
        /// Navigating back in the back navigation history.
        /// </summary>
        Back,
        /// <summary>
        /// Reloading the current content.
        /// </summary>
        Refresh
    }

    /// <summary>
    /// The contract for loading content.
    /// </summary>
    public interface IContentLoader
    {
        /// <summary>
        /// Asynchronously loads content from specified uri.
        /// </summary>
        /// <param name="uri">The content uri.</param>
        /// <param name="cancellationToken">The token used to cancel the load content task.</param>
        /// <returns>The loaded content.</returns>
        Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Provides helper function for navigation.
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// Identifies the current frame.
        /// </summary>
        public const string FrameSelf = "_self";

        /// <summary>
        /// Identifies the top frame.
        /// </summary>
        public const string FrameTop = "_top";

        /// <summary>
        /// Identifies the parent of the current frame.
        /// </summary>
        public const string FrameParent = "_parent";

        /// <summary>
        /// Finds the frame identified with given name in the specified context.
        /// </summary>
        /// <param name="name">The frame name.</param>
        /// <param name="context">The framework element providing the context for finding a frame.</param>
        /// <returns>The frame or null if the frame could not be found.</returns>
        public static ModernFrame FindFrame(string name, FrameworkElement context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // collect all ancestor frames
            ModernFrame[] frames = context.AncestorsAndSelf().OfType<ModernFrame>().ToArray();

            if (name == null || name == FrameSelf)
            {
                // find first ancestor frame
                return frames.FirstOrDefault();
            }
            if (name == FrameParent)
            {
                // find parent frame
                return frames.Skip(1).FirstOrDefault();
            }
            if (name == FrameTop)
            {
                // find top-most frame
                return frames.LastOrDefault();
            }

            // find ancestor frame having a name matching the target
            ModernFrame frame = frames.FirstOrDefault(f => f.Name == name);

            if (frame == null)
            {
                // find frame in context scope
                frame = context.FindName(name) as ModernFrame;

                if (frame == null)
                {
                    // find frame in scope of ancestor frame content
                    ModernFrame parent = frames.FirstOrDefault();
                    if (parent != null && parent.Content != null)
                    {
                        FrameworkElement content = parent.Content as FrameworkElement;
                        if (content != null)
                        {
                            frame = content.FindName(name) as ModernFrame;
                        }
                    }
                }
            }

            return frame;
        }

        /// <summary>
        /// Removes the fragment from specified uri and return it.
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <returns>The uri without the fragment, or the uri itself if no fragment is found</returns>
        public static Uri RemoveFragment(Uri uri)
        {
            string fragment;
            return RemoveFragment(uri, out fragment);
        }

        /// <summary>
        /// Removes the fragment from specified uri and returns the uri without the fragment and the fragment itself.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <param name="fragment">The fragment, null if no fragment found</param>
        /// <returns>The uri without the fragment, or the uri itself if no fragment is found</returns>
        public static Uri RemoveFragment(Uri uri, out string fragment)
        {
            fragment = null;

            if (uri != null)
            {
                string value = uri.OriginalString;

                int i = value.IndexOf('#');
                if (i != -1)
                {
                    fragment = value.Substring(i + 1);
                    uri = new Uri(value.Substring(0, i), uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
                }
            }

            return uri;
        }

        /// <summary>
        /// Tries to cast specified value to a uri. Either a uri or string input is accepted.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Uri ToUri(object value)
        {
            Uri uri = value as Uri;
            if (uri == null)
            {
                string uriString = value as string;
                if (uriString == null || !Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out uri))
                {
                    return null; // no valid uri found
                }
            }
            return uri;
        }

        /// <summary>
        /// Tries to parse a uri with parameters from given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="targetName">Name of the target.</param>
        /// <returns></returns>
        public static bool TryParseUriWithParameters(object value, out Uri uri, out string parameter, out string targetName)
        {
            uri = value as Uri;
            parameter = null;
            targetName = null;

            if (uri == null)
            {
                string valueString = value as string;
                return TryParseUriWithParameters(valueString, out uri, out parameter, out targetName);
            }

            return true;
        }

        /// <summary>
        /// Tries to parse a uri with parameters from given string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="targetName">Name of the target.</param>
        /// <returns></returns>
        public static bool TryParseUriWithParameters(string value, out Uri uri, out string parameter, out string targetName)
        {
            uri = null;
            parameter = null;
            targetName = null;

            if (value == null)
            {
                return false;
            }

            // parse uri value for optional parameter and/or target, eg 'cmd://foo|parameter|target'
            string uriString = value;
            string[] parts = uriString.Split(new char[] { '|' }, 3);
            if (parts.Length == 3)
            {
                uriString = parts[0];
                parameter = Uri.UnescapeDataString(parts[1]);
                targetName = Uri.UnescapeDataString(parts[2]);
            }
            else if (parts.Length == 2)
            {
                uriString = parts[0];
                parameter = Uri.UnescapeDataString(parts[1]);
            }

            return Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out uri);
        }
    }

    /// <summary>
    /// Defines the optional contract for content loaded in a ModernFrame.
    /// </summary>
    public interface IContent
    {
        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        void OnFragmentNavigation(FragmentNavigationEventArgs e);
        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        void OnNavigatedFrom(NavigationEventArgs e);
        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        void OnNavigatedTo(NavigationEventArgs e);
        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        /// <remarks>The method is also invoked when parent frames are about to navigate.</remarks>
        void OnNavigatingFrom(NavigatingCancelEventArgs e);
    }

    /// <summary>
    /// Loads XAML files using Application.LoadComponent.
    /// </summary>
    public class DefaultContentLoader
        : IContentLoader
    {
        /// <summary>
        /// Asynchronously loads content from specified uri.
        /// </summary>
        /// <param name="uri">The content uri.</param>
        /// <param name="cancellationToken">The token used to cancel the load content task.</param>
        /// <returns>The loaded content.</returns>
        public Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                throw new InvalidOperationException();
            }

            // scheduler ensures LoadContent is executed on the current UI thread
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(uri), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        /// <summary>
        /// Loads the content from specified uri.
        /// </summary>
        /// <param name="uri">The content uri</param>
        /// <returns>The loaded content.</returns>
        protected virtual object LoadContent(Uri uri)
        {
            // don't do anything in design mode
            if (ModernUIHelper.IsInDesignMode)
            {
                return null;
            }
            return Application.LoadComponent(uri);
        }
    }

    /// <summary>
    /// Provides various common helper methods.
    /// </summary>
    public static class ModernUIHelper
    {
        private static bool? isInDesignMode;

        /// <summary>
        /// Determines whether the current code is executed in a design time environment such as Visual Studio or Blend.
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                if (!isInDesignMode.HasValue)
                {
                    isInDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());
                }
                return isInDesignMode.Value;
            }
        }


        ///// <summary>
        ///// Gets the DPI awareness of the current process.
        ///// </summary>
        ///// <returns>
        ///// The DPI awareness of the current process
        ///// </returns>
        ///// <exception cref="System.ComponentModel.Win32Exception"></exception>
        //public static ProcessDpiAwareness GetDpiAwareness()
        //{
        //    if (OSVersionHelper.IsWindows8Point1OrGreater)
        //    {
        //        ProcessDpiAwareness value;
        //        var result = NativeMethods.GetProcessDpiAwareness(IntPtr.Zero, out value);
        //        if (result != NativeMethods.S_OK)
        //        {
        //            throw new Win32Exception(result);
        //        }

        //        return value;
        //    }
        //    if (OSVersionHelper.IsWindowsVistaOrGreater)
        //    {
        //        // use older Win32 API to query system DPI awareness
        //        return NativeMethods.IsProcessDPIAware() ? ProcessDpiAwareness.SystemDpiAware : ProcessDpiAwareness.DpiUnaware;
        //    }

        //    // assume WPF default
        //    return ProcessDpiAwareness.SystemDpiAware;
        //}

        /// <summary>
        /// Attempts to set the DPI awareness of this process to PerMonitorDpiAware
        /// </summary>
        /// <returns>A value indicating whether the DPI awareness has been set to PerMonitorDpiAware.</returns>
        /// <remarks>
        /// <para>
        /// For this operation to succeed the host OS must be Windows 8.1 or greater, and the initial
        /// DPI awareness must be set to DpiUnaware (apply [assembly:DisableDpiAwareness] to application assembly).
        /// </para>
        /// <para>
        /// When the host OS is Windows 8 or lower, an attempt is made to set the DPI awareness to SystemDpiAware (= WPF default). This
        /// effectively revokes the [assembly:DisableDpiAwareness] attribute if set.
        /// </para>
        ///// </remarks>
        //public static bool TrySetPerMonitorDpiAware()
        //{
        //    var awareness = GetDpiAwareness();

        //    // initial awareness must be DpiUnaware
        //    if (awareness == ProcessDpiAwareness.DpiUnaware)
        //    {
        //        if (OSVersionHelper.IsWindows8Point1OrGreater)
        //        {
        //            return NativeMethods.SetProcessDpiAwareness(ProcessDpiAwareness.PerMonitorDpiAware) == NativeMethods.S_OK;
        //        }

        //        // use older Win32 API to set the awareness to SystemDpiAware
        //        return NativeMethods.SetProcessDPIAware() == NativeMethods.S_OK;
        //    }

        //    // return true if per monitor was already enabled
        //    return awareness == ProcessDpiAwareness.PerMonitorDpiAware;
        //}

    }

    /// <summary>
    /// Provides data for fragment navigation events.
    /// </summary>
    public class FragmentNavigationEventArgs
        : EventArgs
    {
        /// <summary>
        /// Gets the uniform resource identifier (URI) fragment.
        /// </summary>
        public string Fragment { get; internal set; }
    }

    /// <summary>
    /// Provides data for the <see cref="IContent.OnNavigatingFrom" /> method and the <see cref="ModernFrame.Navigating"/> event.
    /// </summary>
    public class NavigatingCancelEventArgs : NavigationBaseEventArgs
    {
        /// <summary>
        /// Gets a value indicating whether the frame performing the navigation is a parent frame or the frame itself.
        /// </summary>
        public bool IsParentFrameNavigating { get; internal set; }

        /// <summary>
        /// Gets a value that indicates the type of navigation that is occurring.
        /// </summary>
        public NavigationType NavigationType { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether the event should be canceled.
        /// </summary>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// Provides data for the <see cref="ModernFrame.NavigationFailed"/> event.
    /// </summary>
    public class NavigationFailedEventArgs : NavigationBaseEventArgs
    {
        /// <summary>
        /// Gets the error from the failed navigation.
        /// </summary>
        public Exception Error { get; internal set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the failure event has been handled.
        /// </summary>
        /// <remarks>
        /// When not handled, the error is displayed in the ModernFrame raising the NavigationFailed event.
        /// </remarks>
        public bool Handled { get; set; }
    }

    /// <summary>
    /// Provides data for frame navigation events.
    /// </summary>
    public class NavigationEventArgs : NavigationBaseEventArgs
    {
        /// <summary>
        /// Gets a value that indicates the type of navigation that is occurring.
        /// </summary>
        public NavigationType NavigationType { get; internal set; }

        /// <summary>
        /// Gets the content of the target being navigated to.
        /// </summary>
        public object Content { get; internal set; }
    }

    /// <summary>
    /// Defines the base navigation event arguments.
    /// </summary>
    public abstract class NavigationBaseEventArgs
        : EventArgs
    {
        /// <summary>
        /// Gets the frame that raised this event.
        /// </summary>
        public ModernFrame Frame { get; internal set; }

        /// <summary>
        /// Gets the source uri for the target being navigated to.
        /// </summary>
        public Uri Source { get; internal set; }
    }


}
