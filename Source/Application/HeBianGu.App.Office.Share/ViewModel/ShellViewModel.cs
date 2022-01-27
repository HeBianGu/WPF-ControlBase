using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Office
{
    internal class ShellViewModel : NotifyPropertyChanged
    {

        protected override void Init()
        {

        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (command == "1")
            {

            }
            else if (command == "2")
            {

            }

        }
    }
}
