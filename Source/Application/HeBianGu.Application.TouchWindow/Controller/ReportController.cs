
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.Base.WpfBase;


namespace HeBianGu.Application.TouchWindow
{
    [Controller("Report")]
    internal class ReportController : Controller<ReportViewModel>
    {
        public async Task<IActionResult> Report(ObservableCollection<LinkActionEntity> link, bool runSumit = true)
        {
            this.ViewModel.RunNextEngine = runSumit;

            this.ViewModel.LinkActions = link;

            return await ViewAsync();
        }
    }
}
