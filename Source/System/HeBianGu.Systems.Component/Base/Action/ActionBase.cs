// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Component
{
    public abstract class ActionBase : NotifyPropertyChanged, IAction, ICloneable
    {
        public ActionBase()
        {
            this.Name = this.GetType().Name;
        }
        [Browsable(false)]
        public ILogService Log => ServiceRegistry.Instance.GetInstance<ILogService>();

        [Browsable(false)]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        private string _displayName;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public string Name
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }


        private ActionState _state = ActionState.Ready;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public ActionState State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }


        private string _message;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private bool _isBuzy;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }

        private Exception _exception;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                RaisePropertyChanged("Exception");
            }
        }

        [Browsable(false)]
        protected Random Random { get; } = new Random();

        public object Clone()
        {
            return Activator.CreateInstance(this.GetType());
        }

        protected virtual IActionResult OK()
        {
            return new ActionResult() { State = ResultState.OK };
        }

        protected virtual IActionResult Bad()
        {
            return new ActionResult() { State = ResultState.Bad };
        }

        protected virtual IActionResult Error()
        {
            return new ActionResult() { State = ResultState.Error };
        }

        [Display(Name = "执行")]
        public RelayCommand InvokeCommand => new RelayCommand(async l => await this.TryInvokeAsync(this));

        public abstract IActionResult Invoke(IAction previors);

        public virtual async Task<IActionResult> InvokeAsync(IAction previors)
        {
            return await Task.Run(() =>
              {
                  return this.Invoke(previors);
              });
        }
        public virtual async Task<IActionResult> TryInvokeAsync(IAction previors)
        {
            try
            {
                this.State = ActionState.Running;

                this.IsBuzy = true;

                this.Log.Info($"正在执行<{this.GetType().Name}>:{this.Name}");

                IActionResult result = await InvokeAsync(previors);

                this.Log.Info($"执行完成<{this.GetType().Name}>:{this.Name}");

                this.State = ActionState.Success;

                return result;
            }
            catch (Exception ex)
            {
                this.State = ActionState.Error;
                this.Exception = ex;
                this.Message = ex.Message;

                this.Log.Info($"执行错误<{this.GetType().Name}>:{this.Name} {this.Message}");
                this.Log.Error($"执行错误<{this.GetType().Name}>:{this.Name} {this.Message}");
                this.Log.Error(ex);


                return this.Error();
            }
            finally
            {
                this.IsBuzy = false;
            }
        }

        public virtual void Clear()
        {
        }
    }
}
