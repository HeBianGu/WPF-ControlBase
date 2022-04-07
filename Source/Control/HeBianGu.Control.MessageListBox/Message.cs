// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Control.MessageListBox
{
    public abstract class MessageBase : NotifyPropertyChangedBase
    {
        public MessageBase()
        {
            this.DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private string _dateTime;
        /// <summary> 说明  </summary>
        public string DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                RaisePropertyChanged("DateTime");
            }
        }


        private string _title;
        /// <summary> 说明  </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }


        private string _data;
        /// <summary> 说明  </summary>
        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged("Data");
            }
        }
    }


    public class ErrorMessage : MessageBase
    {

        private Exception _exception;
        /// <summary> 说明  </summary>
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                RaisePropertyChanged("Exception");
            }
        }

    }

    public class InfoMessage : MessageBase
    {

    }

    public class TraceMessage : MessageBase
    {

    }
    public class WarnMessage : MessageBase
    {

    }

    public class FatalMessage : MessageBase
    {

    }

}
