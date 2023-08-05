// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace HeBianGu.Control.PrintBox
{
    public class DocumentViewPresenter : DisplayerViewModelBase
    {
        private IDocumentPaginatorSource _document;
        /// <summary> 说明  </summary>
        public IDocumentPaginatorSource Document
        {
            get { return _document; }
            set
            {
                _document = value;
                RaisePropertyChanged();
            }
        }
    }
}
