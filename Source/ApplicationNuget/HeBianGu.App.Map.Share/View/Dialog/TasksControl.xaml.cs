using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Map.View.Dialog
{
    /// <summary>
    /// TasksControl.xaml 的交互逻辑
    /// </summary>
    public partial class TasksControl : UserControl
    {
        public TasksControl()
        {
            InitializeComponent();
        }

        public TaskViewModel Model
        {
            get { return (TaskViewModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(TaskViewModel), typeof(TasksControl), new PropertyMetadata(default(TaskViewModel), (d, e) =>
            {
                TasksControl control = d as TasksControl;

                if (control == null) return;

                TestViewModel config = e.NewValue as TestViewModel;

            }));
    }
}
