// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows.Controls;

namespace HeBianGu.Control.Message
{
    /// <summary>
    /// Interaction logic for SampleMessageDialog.xaml
    /// </summary>
    public partial class StringProgressDialog : UserControl, IStringProgress
    {
        public StringProgressDialog()
        {
            InitializeComponent();
        }

        public string MessageStr
        {
            set
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Message.Content = value; ;
                });

            }
        }

    }


}
