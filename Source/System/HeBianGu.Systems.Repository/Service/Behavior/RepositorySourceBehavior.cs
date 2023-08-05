// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Systems.Repository
{
    public abstract class RepositoryButtonBehaviorBase : ItemsSourceButtonBehaviorBase
    {
        //public Type ModelType
        //{
        //    get { return (Type)GetValue(ModelTypeProperty); }
        //    set { SetValue(ModelTypeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ModelTypeProperty =
        //    DependencyProperty.Register("ModelType", typeof(Type), typeof(RepositoryButtonBehaviorBase), new FrameworkPropertyMetadata(default(Type), (d, e) =>
        //    {
        //        RepositoryButtonBehaviorBase control = d as RepositoryButtonBehaviorBase;

        //        if (control == null) return;

        //        if (e.OldValue is Type o)
        //        {

        //        }

        //        if (e.NewValue is Type n)
        //        {

        //        }

        //    }));

        protected virtual Type GetModelType()
        {
            Type type = this.GetViewModelType();

            Type vmType = typeof(IModelViewModel<>).MakeGenericType(type);

            return vmType.IsAssignableFrom(type) ? vmType.GetGenericArguments()?.FirstOrDefault() : type;
        }


        protected virtual Type GetViewModelType()
        {
            return this.ItemsSource.GetGenericArgumentType();
        }

        public virtual async Task<T> InvokeMethod<T>(string methodName, params object[] parameters)
        {
            return await this.GetModelType().InvokeMethod<T>(methodName, parameters);

        }
    }

    public class AddRepositoryButtonBehavior : RepositoryButtonBehaviorBase
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AddRepositoryButtonBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 AddRepositoryButtonBehavior control = d as AddRepositoryButtonBehavior;

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
            Type type = this.GetModelType();

            if (type == null) return;
            if (this.ItemsSource == null) return;

            object instance = Activator.CreateInstance(type);

            bool r = await PropertyGrid.Show(instance, null, this.Title);

            if (r == false) return;

            bool result = await this.InvokeMethod<bool>("Add", instance);

            if (result == false)
            {
                MessageProxy.Snacker.ShowTime("添加失败");
                return;
            }

            Type vmType = this.GetViewModelType();

            if (vmType != type)
            {
                object vm = Activator.CreateInstance(vmType, instance);
                this.ItemsSource.Add(instance);
            }
            else
            {
                this.ItemsSource.Add(instance);
            }
        }
    }

    public class ClearRepositoryButtonBehavior : RepositoryButtonBehaviorBase
    {
        protected override async void OnClick()
        {
            if (this.ItemsSource == null) return;

            bool result = await this.InvokeMethod<bool>("DeleteAll");

            if (result == false)
            {
                MessageProxy.Snacker.ShowTime("清空失败");
                return;
            }

            this.ItemsSource.Clear();
        }
    }

    public class EditRepositoryButtonBehavior : RepositoryButtonBehaviorBase
    {
        public object Item
        {
            get { return GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(object), typeof(EditRepositoryButtonBehavior), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 EditRepositoryButtonBehavior control = d as EditRepositoryButtonBehavior;

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
            DependencyProperty.Register("Title", typeof(string), typeof(EditRepositoryButtonBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                EditRepositoryButtonBehavior control = d as EditRepositoryButtonBehavior;

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
            bool r = await PropertyGrid.Show(this.Item, null, this.Title);

            if (r == false) return;

            bool result = await this.InvokeMethod<bool>("Save");

            if (result == false)
            {
                MessageProxy.Snacker.ShowTime("保存失败");
                return;
            }

            //  Do ：更新逻辑
        }
    }

    public class RemoveAllCheckedRepositoryButtonBehavior : RepositoryButtonBehaviorBase
    {
        public ListBox ListBox
        {
            get { return (ListBox)GetValue(ListBoxProperty); }
            set { SetValue(ListBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListBoxProperty =
            DependencyProperty.Register("ListBox", typeof(ListBox), typeof(RemoveAllCheckedRepositoryButtonBehavior), new FrameworkPropertyMetadata(default(ListBox), (d, e) =>
            {
                RemoveAllCheckedRepositoryButtonBehavior control = d as RemoveAllCheckedRepositoryButtonBehavior;

                if (control == null) return;

                if (e.OldValue is ListBox o)
                {

                }

                if (e.NewValue is ListBox n)
                {

                }

            }));

        protected override async void OnClick()
        {
            if (this.ListBox == null) return;

            Type modelType = this.GetModelType();

            Type vmType = this.GetViewModelType();

            Type listType = typeof(List<>).MakeGenericType(vmType);

            IList list = Activator.CreateInstance(listType) as IList;

            foreach (object item in this.ListBox.Items)
            {
                DependencyObject find = this.ListBox.ItemContainerGenerator.ContainerFromItem(item);

                if (find is ListBoxItem listBoxItem)
                {
                    if (!listBoxItem.IsSelected) continue;

                    list.Add(item);
                }
            }

            Array array = Array.CreateInstance(modelType, list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                if (modelType == vmType)
                {

                    array.SetValue(list[i], i);
                }
                else
                {
                    System.Reflection.PropertyInfo model = vmType.GetProperty("Model");
                    array.SetValue(model, i);
                }
            }

            bool result = await this.InvokeMethod<bool>("Delete", array);

            if (result == false)
            {
                MessageProxy.Snacker.ShowTime("删除失败");
                return;
            }

            if (this.ItemsSource is IList l)
            {
                foreach (object item in list)
                {
                    l.Remove(item);
                }
            }


        }
    }

    public class RemoveRepositoryButtonBehavior : RepositoryButtonBehaviorBase
    {
        public object Item
        {
            get { return GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(object), typeof(RemoveRepositoryButtonBehavior), new FrameworkPropertyMetadata(default(object), (d, e) =>
            {
                RemoveRepositoryButtonBehavior control = d as RemoveRepositoryButtonBehavior;

                if (control == null) return;

                if (e.OldValue is object o)
                {

                }

                if (e.NewValue is object n)
                {

                }

            }));

        protected override async void OnClick()
        {
            Array array = Array.CreateInstance(this.Item.GetType(), 1);

            array.SetValue(Item, 0);

            bool result = await this.InvokeMethod<bool>("Delete", array);

            if (result == false)
            {
                MessageProxy.Snacker.ShowTime("删除失败");
                return;
            }

            this.ItemsSource.Remove(this.Item);
        }
    }

}