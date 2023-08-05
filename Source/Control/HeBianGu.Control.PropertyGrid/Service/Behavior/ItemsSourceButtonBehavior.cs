// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
    public class AddItemWithPropertyGridButtonBehavior : AddItemButtonBehaviorBase
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AddItemWithPropertyGridButtonBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 AddItemWithPropertyGridButtonBehavior control = d as AddItemWithPropertyGridButtonBehavior;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        protected override async void OnClick()
        {
            object instance = this.CreateNewItem();

            bool r = await PropertyGrid.Show(instance, null, this.Title);

            if (r == false) return;

            this.ItemsSource.Add(instance);
        }
    }

    public class EditItemWithPropertyGridButtonBehavior : ItemsSourceButtonBehaviorBase
    {
        public object Item
        {
            get { return GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(object), typeof(EditItemWithPropertyGridButtonBehavior), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 EditItemWithPropertyGridButtonBehavior control = d as EditItemWithPropertyGridButtonBehavior;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }

             }));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(EditItemWithPropertyGridButtonBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                EditItemWithPropertyGridButtonBehavior control = d as EditItemWithPropertyGridButtonBehavior;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));
        public bool UseSave
        {
            get { return (bool)GetValue(UseSaveProperty); }
            set { SetValue(UseSaveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSaveProperty =
            DependencyProperty.Register("UseSave", typeof(bool), typeof(EditItemWithPropertyGridButtonBehavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 EditItemWithPropertyGridButtonBehavior control = d as EditItemWithPropertyGridButtonBehavior;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        protected override async void OnClick()
        {
            if (this.UseSave)
            {
                await PropertyGrid.Show(this.Item);
                return;
            }


            if (this.Item.GetType().TryCreateInstance(out object instance))
            {
                bool r = await PropertyGrid.Show(instance, null, this.Title);

                if (r == false) return;

                //  ToDo：保存逻辑

                this.Item = instance;
            }
        }
    }

    public class DetialItemWithPropertyGridButtonBehavior : ItemsSourceButtonBehaviorBase
    {
        public object Item
        {
            get { return GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(object), typeof(DetialItemWithPropertyGridButtonBehavior), new FrameworkPropertyMetadata(default(object), (d, e) =>
            {
                DetialItemWithPropertyGridButtonBehavior control = d as DetialItemWithPropertyGridButtonBehavior;

                if (control == null) return;

                if (e.OldValue is object o)
                {

                }

                if (e.NewValue is object n)
                {

                }

            }));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DetialItemWithPropertyGridButtonBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 DetialItemWithPropertyGridButtonBehavior control = d as DetialItemWithPropertyGridButtonBehavior;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));



        protected override async void OnClick()
        {
            await PropertyGrid.Show(this.Item, null, this.Title, x => x.UsePropertyView = true);

        }
    }

}