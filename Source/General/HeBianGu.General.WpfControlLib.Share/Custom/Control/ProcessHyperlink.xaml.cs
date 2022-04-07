// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace HeBianGu.General.WpfControlLib
{
    public class ProcessHyperlink : Hyperlink
    {
        public ProcessHyperlink()
        {
            RequestNavigate += OnRequestNavigate;
        }

        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
