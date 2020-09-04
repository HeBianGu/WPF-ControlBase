using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyMessageWindow : NotifyWindow
    {
        public NotifyMessageWindow()
        {
            InitializeComponent();

            //this.Loaded += (l, k) =>
            //  {
            //      this.Height = SystemParameters.WorkArea.Height;
            //  };
        }


        //public ObservableCollection<MessageBase> Source
        //{
        //    get { return (ObservableCollection<MessageBase>)GetValue(SourceProperty); }
        //    set { SetValue(SourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SourceProperty =
        //    DependencyProperty.Register("Source", typeof(ObservableCollection<MessageBase>), typeof(NotifyMessageWindow), new PropertyMetadata(new ObservableCollection<MessageBase>(), (d, e) =>
        //    {
        //        NotifyMessageWindow control = d as NotifyMessageWindow;

        //        if (control == null) return;

        //        //ProgressBarState config = e.NewValue as ProgressBarState;

        //    }));

        public void Add(MessageBase message)
        {
            this.contrainer.Add(message);
        }
    }
}
