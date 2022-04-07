using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;
using System.Windows;

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
            Success = false;

            Message = string.Empty;

            IsCancel = false;

            System.Reflection.PropertyInfo[] ps = GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo item in ps)
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

            Message = "用户已取消获取数据";
        }

        /// <summary> 开始测量 获取结果 </summary>
        public virtual async Task<object> Start()
        {
            Message = "连接成功,测量中...";

            await Task.Delay(2000);

            if (new Random().Next(1, 8) == 2)
            {
                Message = "获取数据失败，请检查...";
                return null;
            }

            if (IsCancel)
            {
                Message = "用户已取消获取数据";
                return null;

            }

            Message = "测量完成";

            return new object();
        }

        /// <summary> 初始化设备 </summary>
        public virtual async Task<bool> BeginInit()
        {
            Message = "正在连接设备...";

            await Task.Delay(2000);

            if (new Random().Next(1, 8) == 2)
            {
                Message = "设备连接失败，请检查...";
                return false;
            }

            return true;
        }

        public virtual bool IsAvailable()
        {
            System.Reflection.PropertyInfo[] ps = GetType().GetProperties();

            //  Do ：检验所有DataValueEntity是否都可用
            foreach (System.Reflection.PropertyInfo item in ps)
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
