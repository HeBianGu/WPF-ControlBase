using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow.ViewModel
{
    public class GridViewModel : NotifyPropertyChanged
    {

        private string _total = "0";
        /// <summary> 说明  </summary>
        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged("Total");
            }
        }


        private string _pageCount = "0";
        /// <summary> 说明  </summary>
        public string PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                RaisePropertyChanged("PageCount");
            }
        }


        private string _currentIndex = "0";
        /// <summary> 说明  </summary>
        public string CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                RaisePropertyChanged("CurrentIndex");
            }
        }



        private bool _canMatchLisData;
        /// <summary> 说明  </summary>
        public bool CanMathcLisData
        {
            get { return _canMatchLisData; }
            set
            {
                _canMatchLisData = value;
                RaisePropertyChanged("CanMathcLisData");
            }
        }


        private DateTime? _startSelectedDate;
        /// <summary> 说明  </summary>
        public DateTime? StartSelectedDate
        {
            get { return _startSelectedDate; }
            set
            {
                _startSelectedDate = value;
                RaisePropertyChanged("StartSelectedDate");
            }
        }

        private int _selectType = 1;
        /// <summary> 说明  </summary>
        public int SelectType
        {
            get { return _selectType; }
            set
            {
                _selectType = value;
                RaisePropertyChanged("SelectType");
            }
        }


        private DateTime? _endSelectedDate;
        /// <summary> 说明  </summary>
        public DateTime? EndSelectedDate
        {
            get { return _endSelectedDate; }
            set
            {
                _endSelectedDate = value;
                RaisePropertyChanged("EndSelectedDate");
            }
        }


        private string _searchName;
        /// <summary> 说明  </summary>
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                RaisePropertyChanged("SearchName");
            }
        }


        private ObservableCollection<UpLoadItem> _collection = new ObservableCollection<UpLoadItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<UpLoadItem> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        Random random = new Random();
        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
            //  Do：取消
            else if (command == "init")
            {
                //  Do：初始化开始和结束日期
                this.StartSelectedDate = DateTime.Now.AddYears(-1);
                this.EndSelectedDate = DateTime.Now;

            }
            //  Do：取消
            else if (command == "SearchHealthData")
            {

                this.CurrentIndex = "1";

                await this.RefreshPage();

                MessageService.ShowSnackMessageWithNotice($"查询完成，共计{this.Total}条");

            }
            //  Do：取消
            else if (command == "UploadFirstPage")
            {

                if (this.CurrentIndex == "1")
                {
                    MessageService.ShowSnackMessageWithNotice("已经是第一页");
                    return;
                }

                this.CurrentIndex = "1";

                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "UploadPreviousPage")
            {

                if (this.CurrentIndex == "1")
                {
                    MessageService.ShowSnackMessageWithNotice("已经是第一页");
                    return;
                }

                this.CurrentIndex = (int.Parse(this.CurrentIndex) - 1).ToString();


                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "UploadNextPage")
            {

                if (this.CurrentIndex == this.PageCount)
                {
                    MessageService.ShowSnackMessageWithNotice("已经是最后一页");
                    return;
                }

                this.CurrentIndex = (int.Parse(this.CurrentIndex) + 1).ToString();

                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "UploadLastPage")
            {

                if (this.CurrentIndex == this.PageCount)
                {
                    MessageService.ShowSnackMessageWithNotice("已经是最后一页");
                    return;
                }

                this.CurrentIndex = this.PageCount;

                await this.RefreshPage();

            }
            //  Do：取消
            else if (command == "UpLoadData")
            {
                await MessageService.ShowPercentProgress(l =>
                {

                    for (int i = 1; i <= 100; i++)
                    {
                        l.Value = i;
                        Thread.Sleep(50);
                    }

                    Thread.Sleep(500);

                });


                if (random.Next(2) == 1)
                {
                    await MessageService.ShowSumitMessge("上传错误，请检查！");
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("上传成功，合计20条");
                }
            }

            //  Do：取消
            else if (command == "SaveData")
            {

                await MessageService.ShowStringProgress(l =>
                {

                    for (int i = 1; i <= 100; i++)
                    {
                        l.MessageStr = $"正在提交当前页第{i}份数据,共100份";

                        Thread.Sleep(50);
                    }

                    Thread.Sleep(500);
                });


                if (random.Next(2) == 1)
                {
                    await MessageService.ShowSumitMessge("上传错误，请检查！");
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("上传成功，合计20条");
                }
            }
        }


        async Task RefreshPage()
        {
            List<UpLoadItem> result = null;

            string err = string.Empty;

            uint total = 0;

            await MessageService.ShowWaittingMessge(() =>
            {
                System.Diagnostics.Debug.WriteLine(this.StartSelectedDate);

                Thread.Sleep(500);

                result = AssemblyDomain.Instance.GetGridList(out err, out total,
                    this.StartSelectedDate?.ToString("yyyy-MM-dd"),
                    this.EndSelectedDate?.ToString("yyyy-MM-dd"),
                    this.SearchName, this.SelectType == 0 ? 1 : 0,
                    (uint)(int.Parse(this.CurrentIndex)),
                    22);
            });

            if (result == null)
            {
                await MessageService.ShowSumitMessge(err);
            }
            else
            {

               

                this.Collection.Clear();

                foreach (var item in result)
                {
                    this.Collection.Add(item);
                }


                this.Total = total.ToString();

                this.PageCount = ((total % 15) == 0 ? total / 15 : total / 15 + 1).ToString();

                this.CanMathcLisData = true;

            }
        }

    }




    public class UpLoadItem : Base.WpfBase.NotifyPropertyChanged
    {

        private string _index;
        /// <summary> 说明  </summary>
        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged("Index");
            }
        }


        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }


        private string _path;
        /// <summary> 说明  </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged("Path");
            }
        }



        private string _type;
        /// <summary> 说明  </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged("Type");
            }
        }


        private string _size;
        /// <summary> 说明  </summary>
        public string Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged("Size");
            }
        }


        private string _span;
        /// <summary> 说明  </summary>
        public string Span
        {
            get { return _span; }
            set
            {
                _span = value;
                RaisePropertyChanged("Span");
            }
        }


        private string _time;
        /// <summary> 说明  </summary>
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged("Time");
            }
        }


        private string _tag;
        /// <summary> 说明  </summary>
        public string Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                RaisePropertyChanged("Tag");
            }
        }

    }
}
