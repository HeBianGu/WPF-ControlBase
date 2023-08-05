using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HeBianGu.Base.WpfBase
{
    public class mbc_db_modelbase : StringEntityBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public mbc_db_modelbase()
        {
            this.CDATE = DateTime.Now;
            this.UDATE = this.CDATE;
        }
        [Browsable(false)]
        [Display(Name = "创建时间")]
        [ReadOnly(true)]
        public DateTime CDATE { get; set; }

        private DateTime _UDATE;
        [Browsable(false)]
        [Display(Name = "修改时间")]
        [ReadOnly(true)]
        public DateTime UDATE
        {
            get { return _UDATE; }
            set
            {
                _UDATE = value;
                RaisePropertyChanged();
            }
        }

        [Browsable(false)]
        [Display(Name = "是否可用")]
        [ReadOnly(true)]
        public int ISENBLED { get; set; } = 1;

        #region - INotifyPropertyChanged -
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion

        #region - IDataErrorInfo -
        [NotMapped]
        [Browsable(false)]
        public string Error { get; private set; }

        [Browsable(false)]
        public string this[string columnName]
        {
            get
            {
                List<string> results = new List<string>();
                System.Reflection.PropertyInfo property = this.GetType().GetProperty(columnName);
                IEnumerable<ValidationAttribute> attrs = property.GetCustomAttributes(true)?.OfType<ValidationAttribute>();
                if (attrs == null || attrs.Count() == 0)
                    return string.Empty;
                DisplayAttribute display = property.GetCustomAttributes(true)?.OfType<DisplayAttribute>()?.FirstOrDefault();
                object value = property.GetValue(this);
                foreach (ValidationAttribute r in attrs)
                {
                    if (!r.IsValid(value))
                    {
                        results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? columnName : display.Name));
                    }
                }
                return this.Error = results.FirstOrDefault();
            }
        }
        #endregion

    }

}
