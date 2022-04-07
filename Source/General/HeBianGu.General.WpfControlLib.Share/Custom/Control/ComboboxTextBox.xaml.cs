// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class ComboboxTextBox : ComboBox
    {
        static ComboboxTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboboxTextBox), new FrameworkPropertyMetadata(typeof(ComboboxTextBox)));
        }


        private ObservableCollection<object> _selectSource = new ObservableCollection<object>();



        public ObservableCollection<object> SelectSource
        {
            get { return (ObservableCollection<object>)GetValue(SelectSourceProperty); }
            set { SetValue(SelectSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectSourceProperty =
            DependencyProperty.Register("SelectSource", typeof(ObservableCollection<object>), typeof(ComboboxTextBox), new PropertyMetadata(new ObservableCollection<object>(), (d, e) =>
             {
                 ComboboxTextBox control = d as ComboboxTextBox;

                 if (control == null) return;

                 ObservableCollection<object> config = e.NewValue as ObservableCollection<object>;

             }));


        public string InputText
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register("InputText", typeof(string), typeof(ComboboxTextBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 ComboboxTextBox control = d as ComboboxTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;



             }));



    }


}
