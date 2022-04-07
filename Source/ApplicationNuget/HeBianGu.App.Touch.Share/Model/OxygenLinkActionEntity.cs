using System.Threading.Tasks;

namespace HeBianGu.App.Touch
{
    /// <summary> "血氧", "Loyout", "Oxygen", "\xe649" </summary>
    internal class OxygenLinkActionEntity : LinkActionEntity
    {
        public OxygenLinkActionEntity()
        {
            DisplayName = "血氧";
            Controller = "Loyout";
            Action = "Oxygen";
            Logo = "\xe649";
        }


        private DataValueEntity _oxygen = new DataValueEntity() { Name = "血氧饱和度（%）", Range = "94 - 100" };
        /// <summary> 脂肪率  </summary>
        public DataValueEntity Oxygen
        {
            get { return _oxygen; }
            set
            {
                _oxygen = value;
                RaisePropertyChanged("Oxygen");
            }
        }

        public override async Task<object> Start()
        {
            object result = await base.Start();

            if (result == null) return null;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Oxygen.SetValue("97", "#27DB20");
            });

            return result;
        }
    }
}
