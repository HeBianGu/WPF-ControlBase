using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;

namespace HeBianGu.App.Touch
{
    /// <summary> 测量具体项目基类</summary>
    internal abstract class LinkActionEntity : LinkAction, IMeasure
    {
        #region - 属性 -

        private bool _success;
        /// <summary> 是否测量成功 用于下标状态显示  </summary>
        public bool Success
        {
            get { return _success; }
            set
            {
                _success = value;
                RaisePropertyChanged("Success");
            }
        }
        private Visibility _visbleNext = Visibility.Visible;
        /// <summary> 下一项是否可见  </summary>
        public Visibility VisbleNext
        {
            get { return _visbleNext; }
            set
            {
                _visbleNext = value;
                RaisePropertyChanged("VisbleNext");
            }
        }

        private string _message;
        /// <summary> 运行提示消息  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        /// <summary> 提交后清理方法 </summary>
        public void Clear()
        {
            this.Success = false;

            this.Message = string.Empty;

            this.IsCancel = false;

            var ps = this.GetType().GetProperties();

            foreach (var item in ps)
            {
                if (item.GetValue(this) is DataValueEntity entity)
                {
                    entity.Value = null;
                    entity.IsBuzy = true;
                }
            }
        }

        public bool IsCancel { get; private set; }

        public void Stop()
        {
            IsCancel = true;

            this.Message = "用户已取消获取数据";
        }

        /// <summary> 开始测量 获取结果 </summary>
        public virtual async Task<object> Start()
        {
            this.Message = "连接成功,测量中...";

            await Task.Delay(2000);

            if (new Random().Next(1, 8) == 2)
            {
                this.Message = "获取数据失败，请检查...";
                return null;
            }

            if (this.IsCancel)
            {
                this.Message = "用户已取消获取数据";
                return null;

            }

            this.Message = "测量完成";

            return new object();
        }

        /// <summary> 初始化设备 </summary>
        public virtual async Task<bool> BeginInit()
        {
            this.Message = "正在连接设备...";

            await Task.Delay(2000);

            if (new Random().Next(1, 8) == 2)
            {
                this.Message = "设备连接失败，请检查...";
                return false;
            }

            return true;
        }

        public virtual bool IsAvailable()
        {
            var ps = this.GetType().GetProperties();

            //  Do ：检验所有DataValueEntity是否都可用
            foreach (var item in ps)
            {
                if (item.GetValue(this) is DataValueEntity entity)
                {
                    if (!entity.IsAvailable()) return false;
                }
            }

            return true;
        }

        #endregion
    }
}
