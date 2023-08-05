// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Systems.Survey
{
    public class Survey : NotifyPropertyChangedBase
    {
        public Survey()
        {
            {
                Qusition qusition = new Qusition() { Title = "请问您的年龄", AllowMulti = true, Columns = 1 };
                qusition.Collection.Add(new QusitionItem() { Text = "25岁以下" });
                qusition.Collection.Add(new QusitionItem() { Text = "26-35岁" });
                qusition.Collection.Add(new QusitionItem() { Text = "36-45岁" });
                qusition.Collection.Add(new QusitionItem() { Text = "46-55岁" });
                qusition.Collection.Add(new QusitionItem() { Text = "55岁以上" });
                qusition.Collection.Add(new QusitionOtherItem());
                Qusitions.Add(qusition);
            }

            {
                Qusition qusition = new Qusition() { Title = "请问您每月可支配的收入大概为", AllowMulti = true };
                qusition.Collection.Add(new QusitionItem() { Text = "500元以下" });
                qusition.Collection.Add(new QusitionItem() { Text = "500-1000元" });
                qusition.Collection.Add(new QusitionItem() { Text = "1000-1500元" });
                qusition.Collection.Add(new QusitionItem() { Text = "1500-2000元" });
                qusition.Collection.Add(new QusitionItem() { Text = "2000元以上" });
                qusition.Collection.Add(new QusitionOtherItem());
                Qusitions.Add(qusition);
            }

            {
                Qusition qusition = new Qusition() { Title = "您觉得产品定价应该在什么范围", AllowMulti = true, Columns = 1 };
                qusition.Collection.Add(new QusitionItem() { Text = "200元以下" });
                qusition.Collection.Add(new QusitionItem() { Text = "200-300元" });
                qusition.Collection.Add(new QusitionItem() { Text = "300-400元" });
                qusition.Collection.Add(new QusitionItem() { Text = "400-500元" });
                qusition.Collection.Add(new QusitionItem() { Text = "500元以上" });
                qusition.Collection.Add(new QusitionOtherItem());
                Qusitions.Add(qusition);
            }

            {
                Qusition qusition = new Qusition() { Title = "如果您考虑购买该产品，您希望在哪里买到，是否希望专业人员给您做详细介绍" };
                qusition.Collection.Add(new QusitionItem() { Text = "药店" });
                qusition.Collection.Add(new QusitionItem() { Text = "电脑城" });
                qusition.Collection.Add(new QusitionItem() { Text = "医疗器械专营店" });
                qusition.Collection.Add(new QusitionItem() { Text = "医院" });
                qusition.Collection.Add(new QusitionOtherItem());
                Qusitions.Add(qusition);
            }

        }
        private ObservableCollection<Qusition> _qusitions = new ObservableCollection<Qusition>();
        public ObservableCollection<Qusition> Qusitions
        {
            get { return _qusitions; }
            set
            {
                _qusitions = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand PreviousCommand => new RelayCommand(
            x => this.SelectedIndex--,
            x => this.SelectedIndex > 0);

        public RelayCommand NextCommand => new RelayCommand(
            x => this.SelectedIndex++,
            x => this.SelectedIndex < this.Qusitions.Count - 1 && this.Qusitions[this.SelectedIndex].IsValid());

        public RelayCommand SumitCommand => new RelayCommand(x =>
        {
            if (x is Window window)
            {
                Task.Run(() =>
                {
                    string json = this.ToJson();
                });
                window.Close();
            }
            else
            {
                MessageProxy.Container.Close();
            }
            MessageProxy.Snacker.ShowTime("感谢您的调查问卷");
        }, x => this.Qusitions.All(l => l.IsValid()));

        public string ToJson()
        {
            //SurveyRoot surveyRoot = new SurveyRoot();
            //foreach (Qusition qusition in this.Qusitions)
            //{
            //    SurveysItem surveysItem = new SurveysItem();
            //    surveysItem.Question = qusition.Title;
            //    surveysItem.Answers = qusition.Collection.Where(x => x.IsSelected).Select(x => x.Text).ToList();
            //    surveyRoot.Surveys.Add(surveysItem);
            //}
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //return jss.Serialize(surveyRoot);

            return null;
        }
    }

    public class Qusition : NotifyPropertyChangedBase
    {
        private bool _allowMulti;
        public bool AllowMulti
        {
            get { return _allowMulti; }
            set
            {
                _allowMulti = value;
                RaisePropertyChanged();
            }
        }

        private int _columns = 1;
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }





        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<QusitionItem> _collection = new ObservableCollection<QusitionItem>();
        public ObservableCollection<QusitionItem> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        public bool IsValid()
        {
            return this.Collection.Any(x => x.IsSelected);
        }

    }

    public class QusitionOtherItem : QusitionItem
    {

    }

    public class QusitionItem : NotifyPropertyChangedBase
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

    }

    //public class AddValueConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is Int32 v)
    //        {
    //            return v + 1;
    //        }
    //        return value;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class SurveysItem
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
    }

    public class SurveyRoot
    {
        public List<SurveysItem> Surveys { get; } = new List<SurveysItem>();
    }
}
