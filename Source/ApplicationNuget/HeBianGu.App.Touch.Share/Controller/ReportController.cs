
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HeBianGu.App.Touch
{
    [Controller("Report")]
    internal class ReportController : Controller<ReportViewModel>
    {
        public async Task<IActionResult> Report(ObservableCollection<LinkActionEntity> link, bool runSumit = true)
        {
            ViewModel.RunNextEngine = runSumit;

            ViewModel.LinkActions = link;

            return await ViewAsync();
        }
    }
}
