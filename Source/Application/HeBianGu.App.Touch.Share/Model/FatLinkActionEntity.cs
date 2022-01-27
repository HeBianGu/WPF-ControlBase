using System.Threading.Tasks;

namespace HeBianGu.App.Touch
{
    /// <summary> "脂肪", "Loyout", "Fat", "\xe630" </summary>
    internal class FatLinkActionEntity : LinkActionEntity
    {
        public FatLinkActionEntity()
        {
            this.DisplayName = "脂肪";
            this.Controller = "Loyout";
            this.Action = "Fat";
            this.Logo = "\xe630";
        }


        private DataValueEntity _fat = new DataValueEntity() { Name = "脂肪量（kg）", Range = "参考范围：11.9 - 17.0" };
        /// <summary> 脂肪量  </summary>
        public DataValueEntity Fat
        {
            get { return _fat; }
            set
            {
                _fat = value;
                RaisePropertyChanged("Fat");
            }
        }

        private DataValueEntity _rate = new DataValueEntity() { Name = "脂肪率（%）", Range = "参考范围：11.9 - 17.0" };
        /// <summary> 脂肪率  </summary>
        public DataValueEntity Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                RaisePropertyChanged("Rate");
            }
        }

        private DataValueEntity _basic = new DataValueEntity() { Name = "基础代谢（KCAL/日）", Range = "参考范围：11.9 - 17.0" };
        /// <summary> 基础代谢  </summary>
        public DataValueEntity Basic
        {
            get { return _basic; }
            set
            {
                _basic = value;
                RaisePropertyChanged("Basic");
            }
        }

        public override async Task<object> Start()
        {
            var result = await base.Start();

            if (result == null) return null;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.Fat.SetValue("12.3", "#27DB20");
                this.Rate.SetValue("17.8", "#27DB20");
                this.Basic.SetValue("1253", "#27DB20");
            });

            return result;
        }

    }
}
