using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Application.AboveWindow
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    {

        private ObservableCollection<FComboBoxItemModel> _comboBoxItems = new ObservableCollection<FComboBoxItemModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<FComboBoxItemModel> ComboBoxItems
        {
            get { return _comboBoxItems; }
            set
            {
                _comboBoxItems = value;
                RaisePropertyChanged("ComboBoxItems");
            }
        }

        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }

        protected override void Init()
        {
            for (int i = 0; i < 60; i++)
            {
                ComboBoxItems.Add(new FComboBoxItemModel()
                {
                    Header = "ComboBoxItem" + (ComboBoxItems.Count + 1),
                    Value = (ComboBoxItems.Count + 1),
                    CanDelete = true
                });
            }


            for (int i = 0; i < 50; i++)
            {
                this.Students.Add(Student.Random());
            }
        }


        protected override void Loaded(string args)
        {

        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Sumit")
            {

            }

            //  Do：等待消息
            else if (command == "Cancel")
            {

            }
        }

    }
}
