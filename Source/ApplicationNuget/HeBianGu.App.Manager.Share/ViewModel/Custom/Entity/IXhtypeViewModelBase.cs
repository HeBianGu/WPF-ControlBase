namespace HeBianGu.App.Manager
{
    //public class XhtypeProperty : ObjectSelectProperty<IXhtypeViewModelBase>
    //{
    //    public XhtypeProperty()
    //    {
    //        _matchs.Add(new CDViewModel());
    //        _matchs.Add(new BJViewModel());
    //    }

    //    public override void LoadDefault()
    //    {
    //        base.LoadDefault();

    //        this._matchs?.ForEach(l=>l.LoadDefault());
    //    }
    //}

    ///// <summary> 信号类型配置 </summary>
    //public interface IXhtypeViewModelBase: IObjectSelectProperty
    //{
    //    bool ModelState(out List<string> errors);

    //    void LoadDefault();
    //}

    //public class BJViewModel : ValidationXmlViewModel<CD>, IXhtypeViewModelBase
    //{
    //    public BJViewModel(CD model) : base(model)
    //    {
    //        this.DisplayName = "北京";
    //    }
    //    public BJViewModel():base(new CD())
    //    {
    //        this.DisplayName = "北京";
    //    }
    //    private TztypeProperty _tz;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("2.1")]
    //    public TztypeProperty Tz
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _tz = _tz ?? this.CreateProperty<TztypeProperty>();
    //        }
    //        set
    //        {
    //            _tz = value;
    //            RaisePropertyChanged();
    //        }
    //    }


    //}

    //public class CDViewModel : ValidationXmlViewModel<BJ>, IXhtypeViewModelBase
    //{
    //    public CDViewModel(BJ model) :base(model)
    //    {
    //        this.DisplayName = "成都";
    //    }
    //    public CDViewModel():base(new BJ())
    //    {
    //        this.DisplayName = "成都";
    //    }
    //    private TztypeProperty _tz;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("2.2")]
    //    public TztypeProperty Tz
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _tz = _tz ?? this.CreateProperty<TztypeProperty>();
    //        }
    //        set
    //        {
    //            _tz = value;
    //            RaisePropertyChanged();
    //        }
    //    }  
    //}
}
