using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace HeBianGu.Control.PropertyGrid
{
    public class FPropertyGrid : ItemsControl
    {

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(FPropertyGrid), new PropertyMetadata(default(string), (d, e) =>
             {
                 FPropertyGrid control = d as FPropertyGrid;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public object Object
        {
            get { return (object)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register("Object", typeof(object), typeof(FPropertyGrid), new PropertyMetadata((l, k) =>
        {
            FPropertyGrid control = l as FPropertyGrid;

            control.LoadProperty(k.NewValue);

        }));


        void LoadProperty(object obj)
        {
            if (obj == null)
            {
                this.ItemsSource = null;

                return;
            }

            List<ValidationPropertyBase> vs = new List<ValidationPropertyBase>();

            Action<object> action = null;

            action = l =>
            {
                if (l == null) return;

                var ps = l.GetType().GetProperties();

                foreach (var item in ps)
                {
                    //  Do ：直接属性
                    if (item.PropertyType.IsSubclassOf(typeof(ValidationPropertyBase)))
                    {
                        vs.Add(item.GetValue(l) as ValidationPropertyBase);
                    }

                    //  Do ：递归属性
                    var ats = item.GetCustomAttributes(typeof(AutoMergeAttribute), false);

                    if (ats != null && ats.Count() > 0)
                    {
                        action?.Invoke(item.GetValue(l));
                    }

                    //  ToDo：基础属性处理

                }
            };

            action?.Invoke(obj);

            this.RefreshData(vs.OrderBy(l=>l.Index)?.ToList());

            //  Do ：加载默认值
            vs.ForEach(l => l.LoadDefault());
        }


        public bool AutoMerge { get; set; }

        public bool UseGroup
        {
            get { return (bool)GetValue(UseGroupProperty); }
            set { SetValue(UseGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseGroupProperty =
            DependencyProperty.Register("UseGroup", typeof(bool), typeof(FPropertyGrid), new PropertyMetadata(true, (d, e) =>
             {
                 FPropertyGrid control = d as FPropertyGrid;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 control.LoadProperty(control.Object);


             }));


        void RefreshData(List<ValidationPropertyBase> from)
        {
            var groups = from.GroupBy(l => l.Group);

            if (!this.UseGroup)
            {
                this.ItemsSource = from;
                return;
            }

            //if (groups.Count() == 1)
            //{
            //    this.ItemsSource = from;
            //}

            List<ValidationPropertyGroup> datas = new List<ValidationPropertyGroup>();

            foreach (var item in groups)
            {
                ValidationPropertyGroup vps = new ValidationPropertyGroup();

                vps.Name = item.Key;

                foreach (var it in item)
                {
                    vps.Add(it);
                }
                datas.Add(vps);
            }

            this.ItemsSource = datas;

        }

    }

    public class ValidationPropertyGroup : List<ValidationPropertyBase>
    {
        public string Name { get; set; }
    }
}
