
using HeBianGu.Base.WpfBase;
using System.Windows.Controls;
using System.Windows.Documents.Serialization;
using System.Windows.Documents;
using System.Windows.Input;
using System.Printing;

namespace HeBianGu.Control.PrintBox
{
    [Displayer(Name = "打印设置", GroupName = SystemSetting.GroupControl)]
    public class PrintBoxSetting : LazySettingInstance<PrintBoxSetting>
    {
        private double _printableAreaWidth;
        /// <summary> 说明  </summary>
        public double PrintableAreaWidth
        {
            get { return _printableAreaWidth; }
            set
            {
                _printableAreaWidth = value;
                RaisePropertyChanged();
            }
        }


    }


    public class PrintDocumentViewer : DocumentViewer
    {
        protected override void OnPrintCommand()
        {
            var pq = LocalPrintServer.GetDefaultPrintQueue();
            var writer = PrintQueue.CreateXpsDocumentWriter(pq);
            var paginator = this.Document.DocumentPaginator;
            writer.Write(paginator);
        }

        //        protected override void OnPrintCommand()
        //        {
        //#if !DONOTREFPRINTINGASMMETA
        //            System.Windows.Xps.XpsDocumentWriter docWriter;
        //            System.Printing.PrintDocumentImageableArea ia = null;

        //            // Only one printing job is allowed.
        //            if (_documentWriter != null)
        //            {
        //                return;
        //            }

        //            if (this.Document != null)
        //            {
        //                // Show print dialog.
        //                docWriter = System.Printing.PrintQueue.CreateXpsDocumentWriter(ref ia);
        //                if (docWriter != null && ia != null)
        //                {
        //                    // Register for WritingCompleted event.
        //                    _documentWriter = docWriter;
        //                    _documentWriter.WritingCompleted += new WritingCompletedEventHandler(HandlePrintCompleted);
        //                    _documentWriter.WritingCancelled += new WritingCancelledEventHandler(HandlePrintCancelled);

        //                    // Since _documentWriter value is used to determine CanExecute state, we must invalidate that state.
        //                    CommandManager.InvalidateRequerySuggested();

        //                    // Write to the PrintQueue
        //                    if (_document is FixedDocumentSequence)
        //                    {
        //                        docWriter.WriteAsync(_document as FixedDocumentSequence);
        //                    }
        //                    else if (_document is FixedDocument)
        //                    {
        //                        docWriter.WriteAsync(_document as FixedDocument);
        //                    }
        //                    else
        //                    {
        //                        docWriter.WriteAsync(_document.DocumentPaginator);
        //                    }
        //                }
        //            }
        //#endif // DONOTREFPRINTINGASMMETA
        //        }
    }

}
