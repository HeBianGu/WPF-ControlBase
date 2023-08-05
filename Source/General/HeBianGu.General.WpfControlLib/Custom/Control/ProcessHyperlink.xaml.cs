// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace HeBianGu.General.WpfControlLib
{
    public class ProcessHyperlink : Hyperlink
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ProcessHyperlink), "S.ProcessHyperlink.Default");
        public static ComponentResourceKey AccentKey => new ComponentResourceKey(typeof(ProcessHyperlink), "S.ProcessHyperlink.Accent");

        static ProcessHyperlink()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProcessHyperlink), new FrameworkPropertyMetadata(typeof(ProcessHyperlink)));
        }

        public ProcessHyperlink()
        {
            RequestNavigate += OnRequestNavigate;
        }

        public ProcessHyperlink(Inline childInline) : base(childInline)
        {
            RequestNavigate += OnRequestNavigate;
        }

        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {

            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            //Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
