//using HeBianGu.Control.PropertyGrid;

namespace HeBianGu.App.Manager
{
    //public class TztypeProperty : ObjectSelectProperty<IZTypeViewModelBase>
    //{
    //    public TztypeProperty()
    //    {
    //        _matchs.Add(new CWViewModel());
    //        _matchs.Add(new AMViewModel());
    //        _matchs.Add(new ASKViewModel());
    //    }
    //}

    ///// <summary> 调制参数配置</summary> 
    //public interface IZTypeViewModelBase : IObjectSelectProperty
    //{
    //    bool ModelState(out List<string> errors);

    //    void LoadDefault();
    //}

    //public abstract class TZTypeViewModelBase<T> : ValidationXmlViewModel<T>, IZTypeViewModelBase where T : ITz, new()
    //{
    //    public TZTypeViewModelBase():base(new T())
    //    {

    //    }

    //    public TZTypeViewModelBase(T model) : base(model)
    //    {

    //    }

    //    private UnitDoublePropery _outfre;
    //    /// <summary> 输出频率  </summary>
    //    [XmlIndex("1.1")]
    //    [XmlUnit("1")]
    //    public UnitDoublePropery OutFre
    //    {
    //        get
    //        {
    //            return _outfre = _outfre ?? this.CreateProperty<UnitDoublePropery>();
    //        }
    //        set
    //        {
    //            _outfre = value;
    //            RaisePropertyChanged("OutFre");
    //        }
    //    }
    //}

    //public class CWViewModel : TZTypeViewModelBase<CW>
    //{
    //    public CWViewModel()
    //    {
    //        this.DisplayName = "北京大学";
    //    }

    //    public CWViewModel(CW model) : base(model)
    //    {
    //        this.DisplayName = "北京大学";
    //    }
    //}
    //public class AMViewModel : TZTypeViewModelBase<AM>
    //{
    //    public AMViewModel()
    //    {
    //        this.DisplayName = "吉林大学";
    //    }

    //    public AMViewModel(AM model) : base(model)
    //    {
    //        this.DisplayName = "吉林大学";
    //    }

    //    private DoubleProperty _tzFre;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.2.1")]
    //    public DoubleProperty TzFre
    //    {
    //        get { return _tzFre = _tzFre ?? this.CreateProperty<DoubleProperty>(); }
    //        set
    //        {
    //            _tzFre = value;
    //            RaisePropertyChanged("TzFre");
    //        }
    //    }


    //    private DoubleProperty _tzDeep;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.2.2")]
    //    public DoubleProperty TzDeep
    //    {
    //        get { return _tzDeep = _tzDeep ?? this.CreateProperty<DoubleProperty>(); }
    //        set
    //        {
    //            _tzDeep = value;
    //            RaisePropertyChanged("TzDeep");
    //        }
    //    }
    //}
    //public class ASKViewModel : TZTypeViewModelBase<ASK>
    //{
    //    public ASKViewModel()
    //    {
    //        this.DisplayName = "南京航空航天大学";
    //    }

    //    public ASKViewModel(ASK model) : base(model)
    //    {
    //        this.DisplayName = "南京航空航天大学";
    //    }

    //    private UnitDoublePropery _xhSpeed;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.3.1")]
    //    [XmlUnit("2")]
    //    public UnitDoublePropery XhSpeed
    //    {
    //        get { return _xhSpeed = _xhSpeed ?? this.CreateProperty<UnitDoublePropery>(); }
    //        set
    //        {
    //            _xhSpeed = value;
    //            RaisePropertyChanged("XhSpeed");
    //        }
    //    }

    //    private DoubleProperty _tzDeep;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.3.2")]
    //    public DoubleProperty TzDeep
    //    {
    //        get { return _tzDeep = _tzDeep ?? this.CreateProperty<DoubleProperty>(); }
    //        set
    //        {
    //            _tzDeep = value;
    //            RaisePropertyChanged("TzDeep");
    //        }
    //    }

    //    private StrechEnumProperty _sendType;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.3.3")]
    //    public StrechEnumProperty SendType
    //    {
    //        get { return _sendType = _sendType ?? this.CreateProperty<StrechEnumProperty>(); }
    //        set
    //        {
    //            _sendType = value;
    //            RaisePropertyChanged("SendType");
    //        }
    //    }



    //    private DoubleProperty _xzyz;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.3.4")]
    //    public DoubleProperty Xzyz
    //    {
    //        get { return _xzyz = _xzyz ?? this.CreateProperty<DoubleProperty>(); }
    //        set
    //        {
    //            _xzyz = value;
    //            RaisePropertyChanged("Xzyz");
    //        }
    //    }

    //    private MyEnumProperty _selectType;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.3.5")]
    //    public MyEnumProperty SelectType
    //    {
    //        get { return _selectType = _selectType ?? this.CreateProperty<MyEnumProperty>(); }
    //        set
    //        {
    //            _selectType = value;
    //            RaisePropertyChanged("SelectType");
    //        }
    //    }


    //    PathPropery _savePath;
    //    [XmlIndex("1.3.6")]
    //    public PathPropery SavePath
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _savePath = _savePath ?? this.CreateProperty<PathPropery>();
    //        }
    //        set
    //        {
    //            _savePath = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private UpDown_DoubleRangePropery _height;
    //    [XmlIndex("1.3.7")]
    //    public UpDown_DoubleRangePropery Height
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _height = _height ?? this.CreateProperty<UpDown_DoubleRangePropery>();
    //        }
    //        set
    //        {
    //            _height = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private UpDown_DoubleRangePropery _age;
    //    [XmlIndex("1.3.8")]
    //    public UpDown_DoubleRangePropery Age
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _age = _age ?? this.CreateProperty<UpDown_DoubleRangePropery>();
    //        }
    //        set
    //        {
    //            _age = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private BoolProperty _online;
    //    [XmlIndex("1.3.9")]
    //    public BoolProperty Online
    //    {
    //        get
    //        {
    //            //  Do ：只有第一遍从实体里面加载数据
    //            return _online = _online ?? this.CreateProperty<BoolProperty>();
    //        }
    //        set
    //        {
    //            _online = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private IntsProperty _mls;
    //    /// <summary> 说明  </summary>
    //    [XmlIndex("1.3.10")]
    //    public IntsProperty Mls
    //    {
    //        get { return _mls = _mls ?? this.CreateProperty<IntsProperty>(); }
    //        set
    //        {
    //            _mls = value;
    //            RaisePropertyChanged("Mls");
    //        }
    //    }

    //}

}
