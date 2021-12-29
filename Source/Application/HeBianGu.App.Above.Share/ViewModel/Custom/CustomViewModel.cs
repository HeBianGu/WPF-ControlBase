using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;

namespace HeBianGu.App.Above
{
    [ViewModel("Custom")]
    internal class CustomViewModel : MvcViewModelBase
    {
        private Student _student = new Student();
        /// <summary> 说明  </summary>
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                RaisePropertyChanged("Student");
            }
        }


        private Config _config;
        /// <summary> 说明  </summary>
        public Config Config
        {
            get { return _config; }
            set
            {
                _config = value;
                RaisePropertyChanged("Config");
            }
        }

        private PropertyModel _propertyModel = new PropertyModel();
        /// <summary> 说明  </summary>
        public PropertyModel PropertyModel
        {
            get { return _propertyModel; }
            set
            {
                _propertyModel = value;
                RaisePropertyChanged("PropertyModel");
            }
        }

        private PropertyListModel _propertyListModel = new PropertyListModel();
        /// <summary> 说明  </summary>
        public PropertyListModel PropertyListModel
        {
            get { return _propertyListModel; }
            set
            {
                _propertyListModel = value;
                RaisePropertyChanged("PropertyListModel");
            }
        }



        protected override void Init()
        {
            Config testConfig = new Config();

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "PropertyConfig.xml");


            testConfig = testConfig.LoadFromFile(filePath);

            this.Config = testConfig;
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
