// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Panel
{
    using LayoutUpdateEventHandler = EventHandler;

    public class AreaDefinition
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }

        public AreaDefinition(int row, int column, int rowSpan, int columnSpan)
        {
            this.Row = row;
            this.Column = column;
            this.RowSpan = rowSpan;
            this.ColumnSpan = columnSpan;
        }
    }

    public class NamedAreaDefinition : AreaDefinition
    {
        public string Name { get; set; }

        public NamedAreaDefinition(string name, int row, int column, int rowSpan, int columnSpan)
            : base(row, column, rowSpan, columnSpan)
        {
            this.Name = name;
        }
    }

    internal struct GridLengthDefinition
    {
        public GridLength GridLength;
        public double? Min;
        public double? Max;
    }

    public static class GridAreaAttach
    {
        public static Orientation GetAutoFillOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(AutoFillOrientationProperty);
        }
        public static void SetAutoFillOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(AutoFillOrientationProperty, value);
        }
        // Using a DependencyProperty as the backing store for AutoFillOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoFillOrientationProperty =
            DependencyProperty.RegisterAttached("AutoFillOrientation", typeof(Orientation), typeof(GridAreaAttach), new PropertyMetadata(Orientation.Horizontal));


        public static bool GetAutoFillChildren(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoFillChildrenProperty);
        }
        public static void SetAutoFillChildren(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoFillChildrenProperty, value);
        }
        // Using a DependencyProperty as the backing store for AutoFillChildren.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoFillChildrenProperty =
            DependencyProperty.RegisterAttached("AutoFillChildren", typeof(bool), typeof(GridAreaAttach), new PropertyMetadata(false, OnAutoFillChildrenChanged));

        private static void OnAutoFillChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            bool isEnabled = (bool)e.NewValue;
            if (grid == null) { return; }

            if (isEnabled)
            {
                LayoutUpdateEventHandler layoutUpdateCallback = CreateLayoutUpdateHandler(grid);
                grid.LayoutUpdated += layoutUpdateCallback;
                SetLayoutUpdatedCallback(grid, layoutUpdateCallback);

                AutoFill(grid);
            }
            else
            {
                LayoutUpdateEventHandler callback = GetLayoutUpdatedCallback(grid);
                grid.LayoutUpdated -= callback;

                ClearAutoFill(grid);
            }
        }

        private static LayoutUpdateEventHandler CreateLayoutUpdateHandler(Grid grid)
        {
            int prevCount = 0;
            int prevColumn = grid.ColumnDefinitions.Count;
            int prevRow = grid.RowDefinitions.Count;
            Orientation prevOrientation = GetAutoFillOrientation(grid);

            LayoutUpdateEventHandler layoutUpdateCallback = new LayoutUpdateEventHandler((sender, args) =>
            {
                int count = grid.Children.Count;
                int column = grid.ColumnDefinitions.Count;
                int row = grid.RowDefinitions.Count;
                Orientation orientation = GetAutoFillOrientation(grid);

                if (count != prevCount ||
                    column != prevColumn ||
                    row != prevRow ||
                    orientation != prevOrientation)
                {
                    AutoFill(grid);
                    prevCount = count;
                    prevColumn = column;
                    prevRow = row;
                    prevOrientation = orientation;
                }
            });

            return layoutUpdateCallback;
        }

        public static LayoutUpdateEventHandler GetLayoutUpdatedCallback(DependencyObject obj)
        {
            return (LayoutUpdateEventHandler)obj.GetValue(LayoutUpdatedCallbackProperty);
        }
        private static void SetLayoutUpdatedCallback(DependencyObject obj, LayoutUpdateEventHandler value)
        {
            obj.SetValue(LayoutUpdatedCallbackProperty, value);
        }
        // Using a DependencyProperty as the backing store for LayoutUpdatedCallback.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutUpdatedCallbackProperty =
            DependencyProperty.RegisterAttached("LayoutUpdatedCallback", typeof(LayoutUpdateEventHandler), typeof(GridAreaAttach), new PropertyMetadata(null));

        private static void AutoFill(Grid grid)
        {
            bool isEnabled = GetAutoFillChildren(grid);
            int rowCount = grid.RowDefinitions.Count;
            int columnCount = grid.ColumnDefinitions.Count;
            Orientation orientation = GetAutoFillOrientation(grid);

            if (!isEnabled || rowCount == 0 || columnCount == 0) return;

            bool[,] area = new bool[rowCount, columnCount];

            List<FrameworkElement> autoLayoutList = new List<FrameworkElement>();
            // Grid内の位置固定要素のチェック
            foreach (FrameworkElement child in grid.Children)
            {
                AreaDefinition region = GetAreaNameRegion(child) ?? GetAreaRegion(child);
                bool isFixed = region != null;

                if (isFixed)
                {
                    int row = region.Row;
                    int column = region.Column;
                    int rowSpan = region.RowSpan;
                    int columnSpan = region.ColumnSpan;

                    for (int i = row; i < row + rowSpan; i++)
                        for (int j = column; j < column + columnSpan; j++)
                        {
                            if (columnCount <= j || rowCount <= i) { continue; }
                            area[i, j] = true;
                        }
                }
                else
                {
                    autoLayoutList.Add(child);
                }

            }

            int count = 0;
            int numOfCell = rowCount * columnCount;
            bool isHorizontal = orientation == Orientation.Horizontal;
            bool isOverflow = false;
            foreach (FrameworkElement child in autoLayoutList)
            {
                if (child.Visibility == Visibility.Collapsed)
                {
                    continue;
                }

                while (true)
                {
                    int x = isHorizontal ? count % columnCount : count / rowCount;
                    int y = isHorizontal ? count / columnCount : count % rowCount;
                    bool canArrange = isOverflow ? true : !area[y, x];
                    if (canArrange)
                    {
                        Grid.SetRow(child, y);
                        Grid.SetColumn(child, x);
                        Grid.SetRowSpan(child, 1);
                        Grid.SetColumnSpan(child, 1);
                    }

                    if (count + 1 < numOfCell)
                    {
                        count++;
                    }
                    else
                    {
                        isOverflow = true;
                    }

                    if (canArrange)
                    {
                        break;
                    }
                }

            }
        }

        private static void ClearAutoFill(Grid grid)
        {
            foreach (FrameworkElement child in grid.Children)
            {
                child.ClearValue(Grid.RowProperty);
                child.ClearValue(Grid.ColumnProperty);
                child.ClearValue(Grid.RowSpanProperty);
                child.ClearValue(Grid.ColumnSpanProperty);

                UpdateItemPosition(child);
            }
        }

        public static string GetColumnDefinition(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumnDefinitionProperty);
        }

        public static void SetColumnDefinition(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnDefinitionProperty, value);
        }
        // Using a DependencyProperty as the backing store for ColumnDefinition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnDefinitionProperty =
            DependencyProperty.RegisterAttached("ColumnDefinition", typeof(string), typeof(GridAreaAttach), new PropertyMetadata(null, OnColumnDefinitionChanged));

        private static void OnColumnDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            string param = e.NewValue as string;

            InitializeColumnDefinition(grid, param);

            string template = GetTemplateArea(grid);
            if (template != null)
            {
                InitializeTemplateArea(grid, template);
            }
        }

        private static void InitializeColumnDefinition(Grid grid, string param)
        {
            if (grid == null || param == null)
            {
                return;
            }

            grid.ColumnDefinitions.Clear();

            IEnumerable<string> list = param.Split(',')
                            .Select(o => o.Trim());

            foreach (string item in list)
            {
                GridLengthDefinition def = StringToGridLengthDefinition(item);
                ColumnDefinition columnDefinition = new ColumnDefinition() { Width = def.GridLength };
                if (def.Max != null) { columnDefinition.MaxWidth = def.Max.Value; }
                if (def.Min != null) { columnDefinition.MinWidth = def.Min.Value; }
                grid.ColumnDefinitions.Add(columnDefinition);
            }
        }

        public static string GetRowDefinition(DependencyObject obj)
        {
            return (string)obj.GetValue(RowDefinitionProperty);
        }
        public static void SetRowDefinition(DependencyObject obj, string value)
        {
            obj.SetValue(RowDefinitionProperty, value);
        }
        // Using a DependencyProperty as the backing store for RowDefinition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowDefinitionProperty =
            DependencyProperty.RegisterAttached("RowDefinition", typeof(string), typeof(GridAreaAttach), new PropertyMetadata(null, OnRowDefinitionChanged));

        private static void OnRowDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            string param = e.NewValue as string;

            InitializeRowDefinition(grid, param);

            string template = GetTemplateArea(grid);
            if (template != null)
            {
                InitializeTemplateArea(grid, template);
            }
        }

        private static void InitializeRowDefinition(Grid grid, string param)
        {
            if (grid == null || param == null)
            {
                return;
            }

            grid.RowDefinitions.Clear();

            IEnumerable<string> list = param.Split(',')
                            .Select(o => o.Trim());

            foreach (string item in list)
            {
                GridLengthDefinition def = StringToGridLengthDefinition(item);
                RowDefinition rowDefinition = new RowDefinition() { Height = def.GridLength };
                if (def.Max != null) { rowDefinition.MaxHeight = def.Max.Value; }
                if (def.Min != null) { rowDefinition.MinHeight = def.Min.Value; }
                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        public static IList<NamedAreaDefinition> GetAreaDefinitions(DependencyObject obj)
        {
            return (IList<NamedAreaDefinition>)obj.GetValue(AreaDefinitionsProperty);
        }
        private static void SetAreaDefinitions(DependencyObject obj, IList<NamedAreaDefinition> value)
        {
            obj.SetValue(AreaDefinitionsProperty, value);
        }
        // Using a DependencyProperty as the backing store for AreaDefinitions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaDefinitionsProperty =
            DependencyProperty.RegisterAttached("AreaDefinitions", typeof(IList<NamedAreaDefinition>), typeof(GridAreaAttach), new PropertyMetadata(null));

        public static string GetTemplateArea(DependencyObject obj)
        {
            return (string)obj.GetValue(TemplateAreaProperty);
        }
        public static void SetTemplateArea(DependencyObject obj, string value)
        {
            obj.SetValue(TemplateAreaProperty, value);
        }
        // Using a DependencyProperty as the backing store for TemplateArea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateAreaProperty =
            DependencyProperty.RegisterAttached("TemplateArea", typeof(string), typeof(GridAreaAttach), new PropertyMetadata(null, OnTemplateAreaChanged));

        private static void OnTemplateAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            string param = e.NewValue as string;

            if (d == null)
            {
                return;
            }

            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            InitializeRowDefinition(grid, GetRowDefinition(grid));
            InitializeColumnDefinition(grid, GetColumnDefinition(grid));

            if (param != null)
            {
                InitializeTemplateArea(grid, param);
            }
        }

        private static void InitializeTemplateArea(Grid grid, string param)
        {
            IEnumerable<string[]> columns = param.Split(new[] { '\n', '/' })
                               .Select(o => o.Trim())
                               .Where(o => !string.IsNullOrWhiteSpace(o))
                               .Select(o => o.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            // 行×列数のチェック
            int num = columns.FirstOrDefault().Count();
            bool isValidRowColumn = columns.All(o => o.Count() == num);
            if (!isValidRowColumn)
            {
                // Invalid Row Columns...
                throw new ArgumentException("Invalid Row/Column definition.");
            }

            int rowShortage = columns.Count() - grid.RowDefinitions.Count;
            for (int i = 0; i < rowShortage; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            int columnShortage = num - grid.ColumnDefinitions.Count;
            for (int i = 0; i < columnShortage; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            IList<NamedAreaDefinition> areaList = ParseAreaDefinition(columns);
            SetAreaDefinitions(grid, areaList);

            foreach (FrameworkElement child in grid.Children)
            {
                UpdateItemPosition(child);
            }
        }

        private static IList<NamedAreaDefinition> ParseAreaDefinition(IEnumerable<string[]> columns)
        {
            List<NamedAreaDefinition> result = new List<NamedAreaDefinition>();

            var flatten = columns.SelectMany(
                    (item, index) => item.Select((o, xIndex) => new { row = index, column = xIndex, name = o })
                );

            var groups = flatten.GroupBy(o => o.name);
            foreach (var group in groups)
            {
                int left = group.Min(o => o.column);
                int top = group.Min(o => o.row);
                int right = group.Max(o => o.column);
                int bottom = group.Max(o => o.row);

                bool isValid = true;
                for (int y = top; y <= bottom; y++)
                    for (int x = left; x <= right; x++)
                    {
                        isValid = isValid && group.Any(o => o.column == x && o.row == y);
                    }

                if (!isValid)
                {
                    throw new ArgumentException($"\"{group.Key}\" is invalid area definition.");
                }

                result.Add(new NamedAreaDefinition(group.Key, top, left, bottom - top + 1, right - left + 1));
            }

            return result;
        }

        private static GridLengthDefinition StringToGridLengthDefinition(string source)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"(^[^\(\)]+)(?:\((.*)-(.*)\))?");
            System.Text.RegularExpressions.Match m = r.Match(source);

            string length = m.Groups[1].Value;
            string min = m.Groups[2].Value;
            string max = m.Groups[3].Value;

            double temp;
            GridLengthDefinition result = new GridLengthDefinition()
            {
                GridLength = StringToGridLength(length),
                Min = double.TryParse(min, out temp) ? temp : (double?)null,
                Max = double.TryParse(max, out temp) ? temp : (double?)null
            };

            return result;
        }

        private static GridLength StringToGridLength(string source)
        {
            TypeConverter glc = TypeDescriptor.GetConverter(typeof(GridLength));
            return (GridLength)glc.ConvertFromString(source);
        }

        public static string GetAreaName(DependencyObject obj)
        {
            return (string)obj.GetValue(AreaNameProperty);
        }
        public static void SetAreaName(DependencyObject obj, string value)
        {
            obj.SetValue(AreaNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for AreaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaNameProperty =
            DependencyProperty.RegisterAttached("AreaName", typeof(string), typeof(GridAreaAttach), new PropertyMetadata(null, OnAreaNameChanged));

        private static void OnAreaNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement ctrl = d as FrameworkElement;

            if (ctrl == null)
            {
                return;
            }

            UpdateItemPosition(ctrl);

            Grid grid = ctrl.Parent as Grid;
            bool isAutoFill = GetAutoFillChildren(grid);
            if (isAutoFill)
            {
                AutoFill(grid);
            }
        }

        public static string GetArea(DependencyObject obj)
        {
            return (string)obj.GetValue(AreaProperty);
        }
        public static void SetArea(DependencyObject obj, string value)
        {
            obj.SetValue(AreaProperty, value);
        }
        // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaProperty =
            DependencyProperty.RegisterAttached("Area", typeof(string), typeof(GridAreaAttach), new PropertyMetadata(null, OnAreaChanged));

        private static void OnAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement ctrl = d as FrameworkElement;

            if (d == null)
            {
                return;
            }

            UpdateItemPosition(ctrl);

            Grid grid = ctrl.Parent as Grid;
            if (grid == null)
            {
                return;
            }

            bool isAutoFill = GetAutoFillChildren(grid);
            if (isAutoFill)
            {
                AutoFill(grid);
            }
        }

        private static void UpdateItemPosition(FrameworkElement element)
        {
            AreaDefinition area = GetAreaNameRegion(element) ?? GetAreaRegion(element);
            if (area != null)
            {
                Grid.SetRow(element, area.Row);
                Grid.SetColumn(element, area.Column);
                Grid.SetRowSpan(element, area.RowSpan);
                Grid.SetColumnSpan(element, area.ColumnSpan);
            }
        }

        private static AreaDefinition GetAreaNameRegion(FrameworkElement element)
        {
            string name = GetAreaName(element);
            Grid grid = element.Parent as Grid;
            if (grid == null || name == null) { return null; }
            IList<NamedAreaDefinition> areaList = GetAreaDefinitions(grid);
            if (areaList == null) { return null; }

            NamedAreaDefinition area = areaList.FirstOrDefault(o => o.Name == name);
            if (area == null) { return null; }

            return new AreaDefinition(area.Row, area.Column, area.RowSpan, area.ColumnSpan);
        }

        private static AreaDefinition GetAreaRegion(FrameworkElement element)
        {
            string param = GetArea(element);
            if (param == null) { return null; }

            List<int> list = param.Split(',')
                .Select(o => o.Trim())
                .Select(o => int.Parse(o))
                .ToList();

            // Row, Column, RowSpan, ColumnSpan
            if (list.Count() != 4) { return null; }

            return new AreaDefinition(list[0], list[1], list[2], list[3]);
        }
    }
}
