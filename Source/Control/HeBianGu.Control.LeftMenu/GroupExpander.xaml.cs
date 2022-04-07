// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.LeftMenu
{

    public class GroupExpander : ItemsControl//, ICommandSource
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(GroupExpander), "S.GroupExpander.Default");

        static GroupExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupExpander), new FrameworkPropertyMetadata(typeof(GroupExpander)));
        }

        #region - Command -


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(GroupExpander));



        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(GroupExpander));



        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(NotifyClassProperty); }
            set { SetValue(NotifyClassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotifyClassProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(GroupExpander));



        #endregion


        public object SelectItem
        {
            get { return GetValue(SelectItemProperty); }
            set { SetValue(SelectItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectItemProperty =
            DependencyProperty.Register("SelectItem", typeof(object), typeof(GroupExpander), new PropertyMetadata(default(object), (d, e) =>
             {
                 GroupExpander control = d as GroupExpander;

                 if (control == null) return;

                 if (e.NewValue == null)
                 {


                     control.SelectItem = e.OldValue;
                 }
                 else
                 {
                     if (e.OldValue == null) return;

                     control.SelectItem = null;
                 }

                 control.Command?.Execute(control.CommandParameter);

             }));
    }

    public class GroupObject : ObservableCollection<object>
    {
        private string _logo;
        /// <summary> 说明  </summary>
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
            }
        }

        private string _displayName;
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
            }
        }
    }
}


