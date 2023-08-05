// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net.Cache;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Converters;
using System.Windows.Media.Media3D;
using System.Xaml;
using System.Xaml.Schema;

namespace HeBianGu.Control.PropertyGrid
{

    public class TypeCTestModel
    {
        public Thickness Thickness { get; set; }

        [TypeConverter(typeof(Int32Converter))]
        public int[] Int32Converter { get; set; }

        public DoubleCollection DoubleCollection { get; set; }

        public Int32Collection Int32Collection { get; set; }

        [TypeConverter(typeof(Int16Converter))]
        public int Int16Converter { get; set; }

        [TypeConverter(typeof(DoubleConverter))]
        public double DoubleConverter { get; set; }

        [TypeConverter(typeof(BooleanConverter))]
        public bool BooleanConverter { get; set; }


        [TypeConverter(typeof(ByteConverter))]
        public int ByteConverter { get; set; }


        [TypeConverter(typeof(CharConverter))]
        public char CharConverter { get; set; }

        [TypeConverter(typeof(CollectionConverter))]
        public double[] CollectionConverter { get; set; }

        [TypeConverter(typeof(ComponentConverter))]
        public Component ComponentConverter { get; set; }

        [TypeConverter(typeof(CultureInfoConverter))]
        public CultureInfo CultureInfoConverter { get; set; }


        [TypeConverter(typeof(DateTimeConverter))]
        public DateTime DateTimeConverter { get; set; }

        [TypeConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset DateTimeOffsetConverter { get; set; }

        [TypeConverter(typeof(DecimalConverter))]
        public Decimal DecimalConverter { get; set; }

        [TypeConverter(typeof(EnumConverter))]
        public TestType EnumConverter { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public object ExpandableObjectConverter { get; set; }

        [TypeConverter(typeof(GuidConverter))]
        public Guid GuidConverter { get; set; }

        [TypeConverter(typeof(MultilineStringConverter))]
        public string MultilineStringConverter { get; set; }

        [TypeConverter(typeof(NullableConverter))]
        public int? NullableConverter { get; set; }

        [TypeConverter(typeof(ReferenceConverter))]
        public object ReferenceConverter { get; set; }

        [TypeConverter(typeof(SByteConverter))]
        public sbyte SByteConverter { get; set; }

        [TypeConverter(typeof(SingleConverter))]
        public float SingleConverter { get; set; }

        [TypeConverter(typeof(StringConverter))]
        public string StringConverter { get; set; }

        [TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan TimeSpanConverter { get; set; }

        [TypeConverter(typeof(TypeConverter))]
        public Type TypeConverter { get; set; }

        [TypeConverter(typeof(TypeListConverter))]
        public List<Type> TypeListConverter { get; set; }

        //[TypeConverter(typeof(LogConverter))]
        //public System.Diagnostics.EventLog LogConverter { get; set; }

        //[TypeConverter(typeof(ColorConverter))]
        //public Color ColorConverter { get; set; }

        //[TypeConverter(typeof(System.Drawing.FontConverter))]
        //public System.Drawing.Font FontConverter { get; set; }


        //[TypeConverter(typeof(System.Drawing.IconConverter))]
        //public System.Drawing.Icon IconConverter { get; set; }

        //[TypeConverter(typeof(System.Drawing.ImageConverter))]
        //public System.Drawing.Image ImageConverter { get; set; }

        //[TypeConverter(typeof(System.Drawing.ImageFormatConverter))]
        //public System.Drawing.Imaging.ImageFormat ImageFormatConverter { get; set; }

        [TypeConverter(typeof(PointConverter))]
        public Point PointConverter { get; set; }

        //[TypeConverter(typeof(MarginsConverter))]
        //public Margins MarginsConverter { get; set; }

        [TypeConverter(typeof(UriTypeConverter))]
        public Uri UriTypeConverter { get; set; }
    }

    public class ControlTypeConverterModel
    {
        [TypeConverter(typeof(DataGridLengthConverter))]
        public DataGridLength DataGridLengthConverter { get; set; }

        //[TypeConverter(typeof(StringCollectionConverter))]
        //public Uri StringCollectionConverter { get; set; }

        [TypeConverter(typeof(VirtualizationCacheLengthConverter))]
        public VirtualizationCacheLength VirtualizationCacheLengthConverter { get; set; }

        [TypeConverter(typeof(CornerRadiusConverter))]
        public CornerRadius CornerRadiusConverter { get; set; }

        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
        public CultureInfo CultureInfoIetfLanguageTagConverter { get; set; }

        [TypeConverter(typeof(DeferrableContentConverter))]
        public DeferrableContent DeferrableContentConverter { get; set; }

        [TypeConverter(typeof(DialogResultConverter))]
        public bool? DialogResultConverter { get; set; }

        [TypeConverter(typeof(DurationConverter))]
        public Duration DurationConverter { get; set; }

        [TypeConverter(typeof(DynamicResourceExtensionConverter))]
        public DynamicResourceExtension DynamicResourceExtensionConverter { get; set; }

        [TypeConverter(typeof(ExpressionConverter))]
        public Expression ExpressionConverter { get; set; }

        [TypeConverter(typeof(FigureLengthConverter))]
        public FigureLength FigureLengthConverter { get; set; }

        [TypeConverter(typeof(FontSizeConverter))]
        public string FontSizeConverter { get; set; }

        [TypeConverter(typeof(FontStretchConverter))]
        public FontStretch FontStretchConverter { get; set; }

        [TypeConverter(typeof(FontStyleConverter))]
        public FontStyle FontStyleConverter { get; set; }

        [TypeConverter(typeof(FontWeightConverter))]
        public FontWeight FontWeightConverter { get; set; }

        [TypeConverter(typeof(GridLengthConverter))]
        public GridLength GridLengthConverter { get; set; }

        [TypeConverter(typeof(CommandConverter))]
        public ICommand CommandConverter { get; set; }

        [TypeConverter(typeof(CursorConverter))]
        public Cursor CursorConverter { get; set; }

        [TypeConverter(typeof(InputScopeConverter))]
        public InputScope InputScopeConverter { get; set; }

        [TypeConverter(typeof(InputScopeNameConverter))]
        public InputScopeName InputScopeNameConverter { get; set; }

        [TypeConverter(typeof(KeyConverter))]
        public Key KeyConverter { get; set; }

        [TypeConverter(typeof(ModifierKeysConverter))]
        public ModifierKeys ModifierKeysConverter { get; set; }

        [TypeConverter(typeof(MouseActionConverter))]
        public MouseAction MouseActionConverter { get; set; }

        [TypeConverter(typeof(MouseGestureConverter))]
        public MouseGesture MouseGestureConverter { get; set; }

        [TypeConverter(typeof(Int32RectConverter))]
        public Int32Rect Int32RectConverter { get; set; }

        [TypeConverter(typeof(KeySplineConverter))]
        public KeySpline KeySplineConverter { get; set; }

        [TypeConverter(typeof(KeyTimeConverter))]
        public KeyTime KeyTimeConverter { get; set; }

        /// <summary>
        /// * ATUO 等支持
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public double LengthConverter { get; set; }

        [TypeConverter(typeof(ComponentResourceKeyConverter))]
        public ComponentResourceKey ComponentResourceKeyConverter { get; set; }

        [TypeConverter(typeof(DependencyPropertyConverter))]
        public DependencyProperty DependencyPropertyConverter { get; set; }

        [TypeConverter(typeof(EventSetterHandlerConverter))]
        public EventSetter EventSetterHandlerConverter { get; set; }

        //[TypeConverter(typeof(NameReferenceConverter))]
        //public NameReference NameReferenceConverter { get; set; }

        //[TypeConverter(typeof(ResourceReferenceExpressionConverter))]
        //public ResourceReferenceExpression ResourceReferenceExpressionConverter { get; set; }

        [TypeConverter(typeof(RoutedEventConverter))]
        public RoutedEvent RoutedEventConverter { get; set; }

        [TypeConverter(typeof(SetterTriggerConditionValueConverter))]
        public Setter SetterTriggerConditionValueConverter { get; set; }

        [TypeConverter(typeof(TemplateKeyConverter))]
        public TemplateKey TemplateKeyConverter { get; set; }

        [TypeConverter(typeof(XmlLanguageConverter))]
        public XmlLanguage XmlLanguageConverter { get; set; }

        [TypeConverter(typeof(RepeatBehaviorConverter))]
        public RepeatBehavior RepeatBehaviorConverter { get; set; }

        [TypeConverter(typeof(BrushConverter))]
        public Brush BrushConverter { get; set; }

        [TypeConverter(typeof(CacheModeConverter))]
        public CacheMode CacheModeConverter { get; set; }

        [TypeConverter(typeof(BoolIListConverter))]
        public List<bool> BoolIListConverter { get; set; }

        [TypeConverter(typeof(CharIListConverter))]
        public List<char> CharIListConverter { get; set; }


        [TypeConverter(typeof(DoubleIListConverter))]
        public List<double> DoubleIListConverter { get; set; }

        [TypeConverter(typeof(PointIListConverter))]
        public List<Point> PointIListConverter { get; set; }

        [TypeConverter(typeof(UShortIListConverter))]
        public List<ushort> UShortIListConverter { get; set; }

        [TypeConverter(typeof(FontFamilyConverter))]
        public FontFamily FontFamilyConverter { get; set; }

        [TypeConverter(typeof(UriTypeConverter))]
        public Uri UriTypeConverter { get; set; }

        /// <summary>
        /// 重点看下
        /// </summary>
        [TypeConverter(typeof(GeometryConverter))]
        public Geometry GeometryConverter { get; set; } = new EllipseGeometry(new Rect(0, 0, 100, 200));

        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource ImageSourceConverter { get; set; }

        [TypeConverter(typeof(MatrixConverter))]
        public Matrix MatrixConverter { get; set; }

        [TypeConverter(typeof(Matrix3DConverter))]
        public Matrix3D Matrix3DConverter { get; set; }

        [TypeConverter(typeof(Point3DCollectionConverter))]
        public Point3DCollection Point3DCollectionConverter { get; set; }

        [TypeConverter(typeof(Point3DConverter))]
        public Point3D Point3DConverter { get; set; }

        [TypeConverter(typeof(Point4DConverter))]
        public Point4D Point4DConverter { get; set; }

        [TypeConverter(typeof(QuaternionConverter))]
        public Quaternion QuaternionConverter { get; set; }

        [TypeConverter(typeof(Rect3DConverter))]
        public Rect3D Rect3DConverter { get; set; }

        [TypeConverter(typeof(Size3DConverter))]
        public Size3D Size3DConverter { get; set; }

        [TypeConverter(typeof(Vector3DCollectionConverter))]
        public Vector3DCollection Vector3DCollectionConverter { get; set; }

        [TypeConverter(typeof(Vector3DConverter))]
        public Vector3D Vector3DConverter { get; set; }

        [TypeConverter(typeof(PathFigureCollectionConverter))]
        public PathFigureCollection PathFigureCollectionConverter { get; set; }

        [TypeConverter(typeof(PixelFormatConverter))]
        public PixelFormat PixelFormatConverter { get; set; }

        [TypeConverter(typeof(PointCollectionConverter))]
        public PointCollection PointCollectionConverter { get; set; }

        [TypeConverter(typeof(RequestCachePolicyConverter))]
        public RequestCachePolicy RequestCachePolicyConverter { get; set; }

        [TypeConverter(typeof(TransformConverter))]
        public Transform TransformConverter { get; set; }

        [TypeConverter(typeof(VectorCollectionConverter))]
        public VectorCollection VectorCollectionConverter { get; set; }

        [TypeConverter(typeof(NullableBoolConverter))]
        public bool? NullableBoolConverter { get; set; }

        [TypeConverter(typeof(PointConverter))]
        public Point PointConverter { get; set; }

        [TypeConverter(typeof(PropertyPathConverter))]
        public PropertyPath PropertyPathConverter { get; set; }

        [TypeConverter(typeof(RectConverter))]
        public Rect RectConverter { get; set; }

        [TypeConverter(typeof(SizeConverter))]
        public Size SizeConverter { get; set; }

        [TypeConverter(typeof(StrokeCollectionConverter))]
        public StrokeCollection StrokeCollectionConverter { get; set; }

        [TypeConverter(typeof(TemplateBindingExpressionConverter))]
        public TemplateBindingExpression TemplateBindingExpressionConverter { get; set; }

        [TypeConverter(typeof(TemplateBindingExtensionConverter))]
        public TemplateBindingExtension TemplateBindingExtensionConverter { get; set; }

        [TypeConverter(typeof(TextDecorationCollectionConverter))]
        public TextDecoration TextDecorationCollectionConverter { get; set; }

        [TypeConverter(typeof(ThicknessConverter))]
        public Thickness ThicknessConverter { get; set; }

        [TypeConverter(typeof(VectorConverter))]
        public Vector VectorConverter { get; set; }

        [TypeConverter(typeof(XamlTypeTypeConverter))]
        public XamlType XamlTypeTypeConverter { get; set; }
    }

    public enum TestType
    {
        Default = 0, X, Y, Z
    }
}
