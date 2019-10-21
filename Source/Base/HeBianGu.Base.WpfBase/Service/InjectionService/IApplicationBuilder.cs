using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public class ApplicationBuilder : IApplicationBuilder
    {

    }

    public interface IApplicationBuilder
    {
        
    }

    public static class ApplicationBuilderExtention
    {
        public static IApplicationBuilder UseTheme(this IApplicationBuilder builder, Action<ThemeService> theme)
        {
            //theme?.Invoke(ThemeService.Current);

            ThemeService.Current.InitTheme(theme);
            return builder;
        }
    }
}
