using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageListBox;
using System;

namespace HeBianGu.App.Creator
{
    class LogService : ILogService
    {
        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();

        public void Debug(params string[] messages)
        {
            throw new NotImplementedException();
        }

        public void Error(params Exception[] messages)
        {
            foreach (var item in messages)
            {
                ShellViewModel?.Errors.Add(new ErrorMessage() { Title = "运行错误", Data = item.Message, Exception = item });
            }
        }

        public void Error(params string[] messages)
        {
            foreach (var item in messages)
            {
                ShellViewModel?.Errors.Add(new ErrorMessage() { Title = "运行错误", Data = item });
            }
        }

        public void Fatal(params string[] messages)
        {
            throw new NotImplementedException();
        }

        public void Fatal(params Exception[] messages)
        {

        }

        public void Info(params string[] messages)
        {
            foreach (var item in messages)
            {
                ShellViewModel?.Infos.Add(new InfoMessage() { Title = "运行完成", Data = item, });
            }
        }

        public void Trace(params string[] messages)
        {
         
        }

        public void Warn(params string[] messages)
        {
        
        }
    }
}
