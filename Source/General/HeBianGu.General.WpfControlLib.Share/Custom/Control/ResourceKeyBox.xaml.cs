using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class ResourceKeyBox : ListBox
    {
        static ResourceKeyBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResourceKeyBox), new FrameworkPropertyMetadata(typeof(ResourceKeyBox)));
        }

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(ResourceKeyBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
             {
                 ResourceKeyBox control = d as ResourceKeyBox;

                 if (control == null) return;

                 if (e.OldValue is Type o)
                 {

                 }

                 if (e.NewValue is Type n)
                 {
                     control.RefreshData();
                 }
             }));

        void RefreshData()
        {
            var properties = this.Type.GetProperties(BindingFlags.Static | BindingFlags.Public);

            List<ResourceKeyModel> source = new List<ResourceKeyModel>();

            foreach (var item in properties)
            {
                var key = item.GetValue(null) as ComponentResourceKey;
                if (key == null) continue;
                ResourceKeyModel resourceKey = new ResourceKeyModel(key);
                resourceKey.Name = $"h:{this.Type.Name }.{item.Name}";
                resourceKey.Display = "{DynamicResource {x:Static " + resourceKey.Name + "}}";
                source.Add(resourceKey);
            }

            this.ItemsSource = source;
        }
    }

    public class ResourceKeyItem : ContentControl
    {
        public ComponentResourceKey Key
        {
            get { return (ComponentResourceKey)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(ComponentResourceKey), typeof(ResourceKeyItem), new FrameworkPropertyMetadata(default(ComponentResourceKey), (d, e) =>
            {
                ResourceKeyItem control = d as ResourceKeyItem;

                if (control == null) return;

                if (e.OldValue is ComponentResourceKey o)
                {

                }

                if (e.NewValue is ComponentResourceKey n)
                {
                    control.RefreshData();
                }

            }));

        void RefreshData()
        {
            var find = Application.Current.TryFindResource(this.Key);

            if (find is Style style)
            {
                this.Content = Activator.CreateInstance(style.TargetType);

                if (this.Content is FrameworkElement framework)
                {
                    framework.Style = style;
                }

                if (this.Content is ItemsControl items)
                {
                    if (items.Items.Count == 0 && items.ItemsSource == null)
                    {
                        items.ItemsSource = Enumerable.Range(0, 5).Select(L => Student.Random());

                       if(this.Content is DataGrid grid)
                        {
                            grid.AutoGenerateColumns = true;
                        }
                    }
                }
                else if (this.Content is ContentControl content)
                {
                    if (this.Content is Button) return;

                    content.Content = this.Key.ResourceId;
                }
                else if (this.Content is TextBlock textBlock)
                {
                    textBlock.Text = this.Key.ResourceId.ToString();
                }
                else if (this.Content is Hyperlink hyperlink)
                {
                    this.Content = new Hyperlink(new Run() { Text = this.Key.ResourceId.ToString() });
                }


                Cattach.SetTitle(this.Content as DependencyObject, "Title");
            }
        }
    }

    /// <summary> 说明</summary>
    public class ResourceKeyModel : ModelViewModel<ComponentResourceKey>
    {
        public ResourceKeyModel(ComponentResourceKey component) : base(component)
        {

        }
        #region - 属性 -

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }


        private string _display;
        /// <summary> 说明  </summary>
        public string Display
        {
            get { return _display; }
            set
            {
                _display = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }

    public class TypeListResourceKeyBox : ListBox
    {

    }
}
