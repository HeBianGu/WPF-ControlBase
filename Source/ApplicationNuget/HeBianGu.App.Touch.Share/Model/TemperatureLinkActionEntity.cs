using System.Threading.Tasks;

namespace HeBianGu.App.Touch
{
    /// <summary> "体温", "Loyout", "Temperature", "\xe635" </summary>
    internal class TemperatureLinkActionEntity : LinkActionEntity
    {
        public TemperatureLinkActionEntity()
        {
            DisplayName = "体温";
            Controller = "Loyout";
            Action = "Temperature";
            Logo = "\xe635";
        }

        private DataValueEntity _temperature = new DataValueEntity() { Name = "体温（℃）", Range = "参考范围：36 - 37" };
        /// <summary> 脂肪率  </summary>
        public DataValueEntity Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                RaisePropertyChanged("Temperature");
            }
        }

        public override async Task<object> Start()
        {
            object result = await base.Start();

            if (result == null) return null;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Temperature.SetValue("35.5", "#27DB20");
            });

            return result;
        }
    }
}
