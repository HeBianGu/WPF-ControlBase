using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Disk
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller<LoyoutViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Near()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Explorer()
        {
            this.ViewModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            return await ViewAsync();
        }

        public async Task<IActionResult> Space()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Share()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Image()
        {
            this.ViewModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            return await ViewAsync(nameof(Explorer));
        }

        public async Task<IActionResult> Video()
        {
            this.ViewModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            return await ViewAsync(nameof(Explorer));
        }
        public async Task<IActionResult> Music()
        {
            this.ViewModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            return await ViewAsync(nameof(Explorer));
        }

        public async Task<IActionResult> Document()
        {
            this.ViewModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return await ViewAsync(nameof(Explorer));
        }

        public async Task<IActionResult> Recent()
        {
            this.ViewModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            return await ViewAsync(nameof(Explorer));
        }
    }
}
