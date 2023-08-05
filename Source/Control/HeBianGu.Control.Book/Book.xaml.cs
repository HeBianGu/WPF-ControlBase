// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.Book
{
    public partial class Book : ItemsControl
    {
        public Book()
        {
            Width = 300;
            Height = 200;
            InitializeComponent();
        }

        static Book()
        {
            DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(BookDisplayMode), typeof(Book), new PropertyMetadata(BookDisplayMode.Normal, new PropertyChangedCallback(OnDisplayModeChanged)));
            CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(BookCurrentPage), typeof(Book), new PropertyMetadata(BookCurrentPage.RightSheet, new PropertyChangedCallback(OnCurrentPageChanged)));
        }

        private static void OnDisplayModeChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            BookDisplayMode mode = (BookDisplayMode)args.NewValue;
            Book book = source as Book;
            if (mode == BookDisplayMode.Normal)
            {
                //book.translate.X = 0;
                //book.scale.ScaleX = 1;
                //book.scale.ScaleY = 1;
                book.translate.BeginAnimation(TranslateTransform.XProperty, null);
                book.scale.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            }
            else
            {
                if (book.CurrentPage == BookCurrentPage.LeftSheet)
                    book.AnimateToLeftSheet();
                else
                    book.AnimateToRightSheet();
            }
        }

        private static void OnCurrentPageChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            BookCurrentPage currentPage = (BookCurrentPage)args.NewValue;//5-1-a-s-p-x
            Book b = source as Book;
            if (currentPage == BookCurrentPage.LeftSheet)
                b.AnimateToRightSheet();
            else
                b.AnimateToLeftSheet();
        }

        public static DependencyProperty DisplayModeProperty;
        public static DependencyProperty CurrentPageProperty;

        private PageStatus _status = PageStatus.None;
        private DataTemplate defaultDataTemplate;

        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            BookPage bp0 = GetTemplateChild("sheet0") as BookPage;
            BookPage bp1 = GetTemplateChild("sheet1") as BookPage;

            if ((bp0 == null) || (bp1 == null))
                return;

            defaultDataTemplate = (DataTemplate)Resources["defaultDataTemplate"];
            Read<PageStatus> GetStatus = delegate () { return _status; };
            Action<PageStatus> SetStatus = delegate (PageStatus ps) { _status = ps; };
            bp0.GetStatus += GetStatus;
            bp0.SetStatus += SetStatus;
            bp1.GetStatus += GetStatus;
            bp1.SetStatus += SetStatus;

            RefreshSheetsContent();
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (CheckCurrentSheetIndex())
            {
                CurrentSheetIndex = GetItemsCount() / 2;
            }
            else
                RefreshSheetsContent();
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            if (CheckCurrentSheetIndex())
            {
                CurrentSheetIndex = GetItemsCount() / 2;
            }
            else
                RefreshSheetsContent();
        }

        internal object GetPage(int index)
        {
            if ((index >= 0) && (index < Items.Count))
                return Items[index];

            Canvas c = new Canvas();
            //c.Background = Brushes.White;

            return c;
        }

        private int _currentSheetIndex = 0;

        public void AnimateToNextPage(bool fromTop, int duration)
        {
            if (CurrentSheetIndex + 1 <= GetItemsCount() / 2)
            {
                BookPage bp0 = GetTemplateChild("sheet0") as BookPage;
                BookPage bp1 = GetTemplateChild("sheet1") as BookPage;

                if ((bp0 == null) || (bp1 == null))
                    return;

                Canvas.SetZIndex(bp0, 0);
                Canvas.SetZIndex(bp1, 1);
                bp1.AutoTurnPage(fromTop ? CornerOrigin.TopRight : CornerOrigin.BottomRight, duration);
            }
        }

        public void AnimateToPreviousPage(bool fromTop, int duration)
        {
            if (CurrentSheetIndex > 0)
            {
                BookPage bp0 = GetTemplateChild("sheet0") as BookPage;
                BookPage bp1 = GetTemplateChild("sheet1") as BookPage;

                if ((bp0 == null) || (bp1 == null))
                    return;

                Canvas.SetZIndex(bp1, 0);
                Canvas.SetZIndex(bp0, 1);
                bp0.AutoTurnPage(fromTop ? CornerOrigin.TopLeft : CornerOrigin.BottomLeft, duration);
            }
        }

        public int CurrentSheetIndex
        {
            get { return _currentSheetIndex; }
            set
            {
                if (_status != PageStatus.None) return;

                if (_currentSheetIndex != value)
                {
                    if ((value >= 0) && (value <= GetItemsCount() / 2))
                    {
                        _currentSheetIndex = value;
                        RefreshSheetsContent();
                    }
                    else
                        throw new Exception("Index out of bounds");
                }
            }
        }

        protected virtual bool CheckCurrentSheetIndex()
        {
            return CurrentSheetIndex > (GetItemsCount() / 2);
        }

        public int GetItemsCount()
        {
            if (ItemsSource != null)
            {
                if (ItemsSource is ICollection)
                    return (ItemsSource as ICollection).Count;
                int count = 0;
                foreach (object o in ItemsSource) count++;
                return count;
            }
            return Items.Count;
        }

        private void RefreshSheetsContent()
        {
            BookPage bp0 = GetTemplateChild("sheet0") as BookPage;
            if (bp0 == null)
                return;

            BookPage bp1 = GetTemplateChild("sheet1") as BookPage;
            if (bp1 == null)
                return;

            ContentPresenter sheet0Page0Content = bp0.FindName("page0") as ContentPresenter;
            ContentPresenter sheet0Page1Content = bp0.FindName("page1") as ContentPresenter;
            ContentPresenter sheet0Page2Content = bp0.FindName("page2") as ContentPresenter;

            ContentPresenter sheet1Page0Content = bp1.FindName("page0") as ContentPresenter;
            ContentPresenter sheet1Page1Content = bp1.FindName("page1") as ContentPresenter;
            ContentPresenter sheet1Page2Content = bp1.FindName("page2") as ContentPresenter;

            Visibility bp0Visibility = Visibility.Visible;
            Visibility bp1Visibility = Visibility.Visible;

            bp1.IsTopRightCornerEnabled = true;
            bp1.IsBottomRightCornerEnabled = true;

            Visibility sheet0Page0ContentVisibility = Visibility.Visible;
            Visibility sheet0Page1ContentVisibility = Visibility.Visible;
            Visibility sheet0Page2ContentVisibility = Visibility.Visible;
            Visibility sheet1Page0ContentVisibility = Visibility.Visible;
            Visibility sheet1Page1ContentVisibility = Visibility.Visible;
            Visibility sheet1Page2ContentVisibility = Visibility.Visible;

            DataTemplate dt = ItemTemplate;
            if (dt == null)
                dt = defaultDataTemplate;

            sheet0Page0Content.ContentTemplate = dt;
            sheet0Page1Content.ContentTemplate = dt;
            sheet0Page2Content.ContentTemplate = dt;
            sheet1Page0Content.ContentTemplate = dt;
            sheet1Page1Content.ContentTemplate = dt;
            sheet1Page2Content.ContentTemplate = dt;

            sheet0Page2ContentVisibility = _currentSheetIndex == 1 ? Visibility.Hidden : Visibility.Visible;
            int count = GetItemsCount();
            int sheetCount = count / 2;
            bool isOdd = (count % 2) == 1;

            if (_currentSheetIndex == sheetCount)
            {
                if (isOdd)
                {
                    bp1.IsTopRightCornerEnabled = false;
                    bp1.IsBottomRightCornerEnabled = false;
                }
                else
                    bp1Visibility = Visibility.Hidden;
            }

            if (_currentSheetIndex == sheetCount - 1)
            {
                if (!isOdd)
                    sheet1Page2ContentVisibility = Visibility.Hidden;
            }

            if (_currentSheetIndex == 0)
            {
                sheet0Page0Content.Content = null;
                sheet0Page1Content.Content = null;
                sheet0Page2Content.Content = null;
                bp0.IsEnabled = false;
                bp0Visibility = Visibility.Hidden;
            }
            else
            {
                sheet0Page0Content.Content = GetPage(2 * (CurrentSheetIndex - 1) + 1);
                sheet0Page1Content.Content = GetPage(2 * (CurrentSheetIndex - 1));
                sheet0Page2Content.Content = GetPage(2 * (CurrentSheetIndex - 1) - 1);
                bp0.IsEnabled = true;
            }

            sheet1Page0Content.Content = GetPage(2 * CurrentSheetIndex);
            sheet1Page1Content.Content = GetPage(2 * CurrentSheetIndex + 1);
            sheet1Page2Content.Content = GetPage(2 * CurrentSheetIndex + 2);

            bp0.Visibility = bp0Visibility;
            bp1.Visibility = bp1Visibility;

            sheet0Page0Content.Visibility = sheet0Page0ContentVisibility;
            sheet0Page1Content.Visibility = sheet0Page1ContentVisibility;
            sheet0Page2Content.Visibility = sheet0Page2ContentVisibility;
            sheet1Page0Content.Visibility = sheet1Page0ContentVisibility;
            sheet1Page1Content.Visibility = sheet1Page1ContentVisibility;
            sheet1Page2Content.Visibility = sheet1Page2ContentVisibility;

        }

        private void OnLeftMouseDown(object sender, MouseButtonEventArgs args)
        {
            BookPage bp0 = GetTemplateChild("sheet0") as BookPage;
            BookPage bp1 = GetTemplateChild("sheet1") as BookPage;
            Canvas.SetZIndex(bp0, 1);
            Canvas.SetZIndex(bp1, 0);
        }
        private void OnRightMouseDown(object sender, MouseButtonEventArgs args)
        {
            BookPage bp0 = GetTemplateChild("sheet0") as BookPage;
            BookPage bp1 = GetTemplateChild("sheet1") as BookPage;
            Canvas.SetZIndex(bp0, 0);
            Canvas.SetZIndex(bp1, 1);
        }
        private void OnLeftPageTurned(object sender, RoutedEventArgs args)
        {
            CurrentSheetIndex--;
        }
        private void OnRightPageTurned(object sender, RoutedEventArgs args)
        {
            CurrentSheetIndex++;
        }

        public BookDisplayMode DisplayMode
        {
            get { return (BookDisplayMode)GetValue(Book.DisplayModeProperty); }
            set
            {
                if (!GetValue(Book.DisplayModeProperty).Equals(value))
                    SetValue(Book.DisplayModeProperty, value);
            }
        }

        public BookCurrentPage CurrentPage
        {
            get { return (BookCurrentPage)GetValue(Book.CurrentPageProperty); }
            set
            {
                if (!GetValue(Book.CurrentPageProperty).Equals(value))
                    SetValue(Book.CurrentPageProperty, value);
            }
        }

        private void AnimateToLeftSheet()
        {
            DoubleAnimation translateAnimation = new DoubleAnimation(Width / 4, new Duration(TimeSpan.FromMilliseconds(500)));

            BezierSegment bs =
                new BezierSegment(new Point(0, 1), new Point(1, 0.5), new Point(2, 1), true);

            PathGeometry path = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(0, 1);
            figure.Segments.Add(bs);
            figure.IsClosed = false;
            path.Figures.Add(figure);

            DoubleAnimationUsingPath scaleAnimation = new DoubleAnimationUsingPath();
            scaleAnimation.PathGeometry = path;
            scaleAnimation.Source = PathAnimationSource.Y;
            scaleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            translate.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }

        private void AnimateToRightSheet()
        {
            DoubleAnimation translateAnimation = new DoubleAnimation(-Width / 4, new Duration(TimeSpan.FromMilliseconds(500)));

            BezierSegment bs =
                new BezierSegment(new Point(0, 1), new Point(1, 0.5), new Point(2, 1), true);

            PathGeometry path = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(0, 1);
            figure.Segments.Add(bs);
            figure.IsClosed = false;
            path.Figures.Add(figure);

            DoubleAnimationUsingPath scaleAnimation = new DoubleAnimationUsingPath();
            scaleAnimation.PathGeometry = path;
            scaleAnimation.Source = PathAnimationSource.Y;
            scaleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            translate.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }

        public void MoveToNextPage()
        {
            if (CurrentPage == BookCurrentPage.LeftSheet)
                AnimateToNextPage(false, 500);
            CurrentPage =
                CurrentPage == BookCurrentPage.LeftSheet ?
                    BookCurrentPage.RightSheet : BookCurrentPage.LeftSheet;
        }

        public void MoveToPreviousPage()
        {
            if (CurrentPage == BookCurrentPage.RightSheet)
                AnimateToPreviousPage(false, 500);
            CurrentPage =
                CurrentPage == BookCurrentPage.LeftSheet ?
                    BookCurrentPage.RightSheet : BookCurrentPage.LeftSheet;
        }
    }
    public enum BookDisplayMode { Normal, ZoomOnPage }
    public enum BookCurrentPage { LeftSheet, RightSheet }
}
