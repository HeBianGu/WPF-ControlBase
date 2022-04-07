// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Systems.Operation
{
    /// <summary> 说明</summary>
    public class Operation : NotifyPropertyChangedBase, IOperation
    {
        private string _id = Guid.NewGuid().ToString();
        /// <summary> 说明  </summary>
        [Display(Name = "ID")]
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }


        private bool _isChecked;
        /// <summary> 说明  </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
            }
        }


        private IUser _user;
        /// <summary> 说明  </summary>
        [Display(Name = "用户")]
        public IUser User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        private string _operationType;
        /// <summary> 说明  </summary>
        [Display(Name = "操作类型")]
        public string OperationType
        {
            get { return _operationType; }
            set
            {
                _operationType = value;
                RaisePropertyChanged();
            }
        }

        private string _type;
        /// <summary> 说明  </summary>
        [Display(Name = "日志类型")]
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        private string _result;
        /// <summary> 说明  </summary>
        [Display(Name = "结果")]
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged();
            }
        }

        private string _message;
        /// <summary> 说明  </summary>
        [Display(Name = "消息")]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _dateTime = DateTime.Now;
        /// <summary> 说明  </summary>
        [Display(Name = "时间")]
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                RaisePropertyChanged();
            }
        }

    }

}
