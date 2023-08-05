// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows.Controls;

namespace HeBianGu.Control.Message
{
    /// <summary>
    /// Interaction logic for SampleMessageDialog.xaml
    /// </summary>
    public partial class PercentProgressDialog : UserControl, IPercentProgress
    {
        public PercentProgressDialog()
        {
            InitializeComponent();
        }

        public int Value
        {
            set
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.progress.Value = value;
                });
            }
        }

    }



}
