using System.Threading.Tasks;

namespace HeBianGu.App.Touch
{
    /// <summary> "血压", "Loyout", "Pressure", "\xe600" </summary>
    internal class PressureLinkActionEntity : LinkActionEntity
    {
        public PressureLinkActionEntity()
        {
            DisplayName = "血压";
            Controller = "Loyout";
            Action = "Pressure";
            Logo = "\xe600";
        }


        private DataValueEntity _dbp = new DataValueEntity() { Name = "收缩压（mmHg）", Range = "参考范围：90 - 120" };
        /// <summary> 脂肪率  </summary>
        public DataValueEntity Dbp
        {
            get { return _dbp; }
            set
            {
                _dbp = value;
                RaisePropertyChanged("Dbp");
            }
        }

        private DataValueEntity _sbp = new DataValueEntity() { Name = "舒张压（mmHg）", Range = "参考范围：60 - 80" };
        /// <summary> 脂肪率  </summary>
        public DataValueEntity Sbp
        {
            get { return _sbp; }
            set
            {
                _sbp = value;
                RaisePropertyChanged("Sbp");
            }
        }

        private DataValueEntity _pulse = new DataValueEntity() { Name = "脉率（次/分）", Range = "参考范围：60 - 100" };
        /// <summary> 脂肪率  </summary>
        public DataValueEntity Pulse
        {
            get { return _pulse; }
            set
            {
                _pulse = value;
                RaisePropertyChanged("Pulse");
            }
        }

        public override async Task<object> Start()
        {
            object result = await base.Start();

            if (result == null) return null;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Dbp.SetValue("117", "#27DB20");
                Sbp.SetValue("89", "#FCA814");
                Pulse.SetValue("87", "#27DB20");
            });

            return result;
        }
    }
}
