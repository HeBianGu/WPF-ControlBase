namespace HeBianGu.App.Manager
{
    //public class StudentViewModel : ValidationXmlViewModel<Student>
    //{

    //    public StudentViewModel() : base(new Student())
    //    {
    //    }

    //    public StudentViewModel(Student signl) : base(signl)
    //    {

    //    }
    //    #region - 属性 -

    //    private StringProperty _name;
    //    [XmlIndex("1")]
    //    public StringProperty Name
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _name = _name ?? this.CreateProperty<StringProperty>();
    //        }
    //        set
    //        {
    //            _name = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private XhtypeProperty _xh;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("2")]
    //    public XhtypeProperty Xh
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _xh = _xh ?? this.CreateProperty<XhtypeProperty>();
    //        }
    //        set
    //        {
    //            _xh = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    #endregion

    //    #region - 命令 -

    //    #endregion

    //    #region - 方法 -

    //    protected override void RelayMethod(object obj)
    //    {
    //        string command = obj.ToString();

    //        ////  Do：应用
    //        //if (command == "Button.Click.Sumit")
    //        //{
    //        //    List<string> errors;

    //        //    if (!this.ModelState(out errors) || !this.SelectItem.ModelState(out errors))
    //        //    {
    //        //        MessageBox.Show(errors?.FirstOrDefault());

    //        //    }
    //        //    else
    //        //    {
    //        //        MessageBox.Show("验证成功");
    //        //    }

    //        //    this.Model.Xh = this.SelectItem.GetModel<IXh>();

    //        //    string json = JsonConvert.SerializeObject(this.Model);

    //        //    Debug.WriteLine(json);

    //        //}
    //        ////  Do：取消
    //        //else if (command == "Button.Click.LoadDefault")
    //        //{

    //        //    this.LoadDefault();

    //        //    this.SelectItem.LoadDefault();

    //        //}
    //    }

    //    public override void LoadDefault()
    //    {
    //        base.LoadDefault();


    //        this.Xh?.LoadDefault();
    //    }

    //    #endregion
    //}


}
