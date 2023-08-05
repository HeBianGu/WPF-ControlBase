// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Operation
{

    public interface IOparationViewPresenterOption : ITreeViewItemPresenterOption
    {

    }

    [Displayer(Name = "操作记录", GroupName = SystemSetting.GroupBase, Icon = "\xe6e7", Description = "应用此功能可以查看最近的操作记录")]
    public class OparationViewPresenter : TreeViewItemPresenter<OparationViewPresenter, IOparationViewPresenter>, IOparationViewPresenter, IOparationViewPresenterOption
    {
        //private int _count;
        //[DefaultValue(50)]
        //[Display(Name = "保存条数")]
        //public int Count
        //{
        //    get { return _count; }
        //    set
        //    {
        //        _count = value;
        //        RaisePropertyChanged();
        //    }
        //}  

        private DateRepositoryViewModel<hi_dd_operation> _viewModel = new DateRepositoryViewModel<hi_dd_operation>();
        [Browsable(false)]
        [XmlIgnore]
        public DateRepositoryViewModel<hi_dd_operation> ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                RaisePropertyChanged();
            }
        }

        public async void Log(string title, string message = null, [CallerMemberName] string type = null, bool result = true, OperationType operationType = OperationType.Default)
        {
            hi_dd_operation operation = new hi_dd_operation()
            {
                Message = message,
                Title = title,
                OperationType = operationType,
                Result = result,
                Type = type,
            };
            operation.UserID = Loginner.Instance?.User?.ID;
            operation.UserName = Loginner.Instance?.User?.Account;
            await this.ViewModel.Add(operation);
        }
    }

    public class Operation : SelectViewModel<hi_dd_operation>
    {
        public Operation() : base(new hi_dd_operation())
        {

        }
        public Operation(hi_dd_operation model) : base(model)
        {

        }
    }
}
