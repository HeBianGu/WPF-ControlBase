// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.PropertyGrid
{
    public class TransitionPropertyItem : ItemsSourcePropertyItem<TransitionsHost>, ILoadDefaultPropertyItem
    {
        public TransitionPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            this.LoadDefault();
        }

        public void LoadDefault()
        {
            //var transitons = TransitionService.GetAll();

            //foreach (var item in transitons)
            //{
            //    var host = new TransitionsHost();
            //    host.Transitions.Add(item);
            //    host.Name = item.GetType().Name;
            //    this.Collection.Add(host);
            //}

            this.Collection = GetWindowHosts()?.ToObservable();

            if (this.Value == null)
            {
                this.Value = this.Collection?.FirstOrDefault();
            }
            else
            {
                if (this.Value.Transitions == null || this.Value.Transitions.Count == 0)
                {
                    this.Value.Transitions = this.Collection?.FirstOrDefault(l => l.Name == this.Value.Name)?.Transitions;

                }
                this.Collection.Replace(this.Value, l => l.Name == this.Value.Name);
            }
        }


        public static IEnumerable<TransitionsHost> GetWindowHosts()
        {
            List<Type> easingTypes = new List<Type>();
            easingTypes.Add(typeof(PowerEase));
            //easingTypes.Add(typeof(BackEase));

            foreach (Type item in easingTypes)
            {
                EasingFunctionBase functionBase = item == null ? null : Activator.CreateInstance(item) as EasingFunctionBase;

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Geomotry" };
                    ScaleGeomotryTransition transition = new ScaleGeomotryTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Angle" };
                    AngleTransition transition = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 360, EndAngle = 360 };
                    host.Transitions.Add(transition);
                    yield return host;
                }


                {
                    TransitionsHost host = new TransitionsHost() { Name = "Geomotry-Angle" };
                    ScaleGeomotryTransition transition1 = new ScaleGeomotryTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 360, EndAngle = 360 };
                    host.Transitions.Add(transition2);

                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Geomotry-Scale-Angle" };
                    ScaleGeomotryTransition transition1 = new ScaleGeomotryTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 360, EndAngle = 360 };
                    host.Transitions.Add(transition2);

                    ScaleTransition transition3 = new ScaleTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition3);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Horizontal" };
                    HorizontalTransition transition = new HorizontalTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Horizontal-Angle" };
                    HorizontalTransition transition1 = new HorizontalTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 360, EndAngle = 360 };
                    host.Transitions.Add(transition2);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Vertical" };
                    VerticalTransition transition = new VerticalTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition);

                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Opacity" };
                    OpacityTransition transition = new OpacityTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Scale" };
                    ScaleTransition transition = new ScaleTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Scale-Angle" };
                    ScaleTransition transition1 = new ScaleTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 720, EndAngle = 720 };
                    host.Transitions.Add(transition2);

                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Horizontal-Scale-Angle" };
                    ScaleTransition transition1 = new ScaleTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 720, EndAngle = 720 };
                    host.Transitions.Add(transition2);

                    HorizontalTransition transition3 = new HorizontalTransition() { VisibleEasing = functionBase, HideEasing = functionBase };
                    host.Transitions.Add(transition3);

                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Path-1" };
                    PathTransition transition = new PathTransition();
                    PathGeometry path = new PathGeometry();
                    PathFigureCollectionConverter converter = new PathFigureCollectionConverter();
                    PathFigureCollection figures = converter.ConvertFromInvariantString("M3,3.76 L45.5,11.02 91.459302,20.623944 157.5553,34.599936 218.87885,49.764679 293.98877,66.090991 351.09407,76.042453 407.12632,98.978642 464,111.76 505.5,139.02 529.97565,170.34021 533,200.76 541.5,251.01999 516.02365,289.32026 466.07004,321.64367 399.13219,340.95737 292.23145,338.2076 250.27041,301.07575 236.2834,248.89044 235.28432,199.71583 264.25742,165.59466 321.20455,155.55903 389.14147,164.5911 412.12013,195.70157 C412.12013,195.70157 404.12755,233.83699 403.12848,236.84768 402.12941,239.85837 382.14796,263.9439 382.14796,263.9439 L353,260.76 303.22124,254.16657 322,220.76 350.17764,210.1408") as PathFigureCollection;
                    path.Figures = figures;
                    transition.PathGeometry = path;
                    host.Transitions.Add(transition);

                    ScaleTransition transition1 = new ScaleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, StartX = 0.1, StartY = 0.1, EndX = 0.1, EndY = 0.1 };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 720, EndAngle = 720 };
                    host.Transitions.Add(transition2);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Path-2" };
                    PathTransition transition = new PathTransition() { Duration = 10000 };
                    PathGeometry path = new PathGeometry();
                    PathFigureCollectionConverter converter = new PathFigureCollectionConverter();
                    PathFigureCollection figures = converter.ConvertFromInvariantString("M1,3.76 L72.079466,37.64042 150.5,75.02 66.719799,137.50799 C66.719799,137.50799 142,167.76 147,166.76 152,165.76 232.27349,48.299837 232.27349,48.299837 L130,26.76 37.543213,101.03447 81.595305,242.05546 241.78473,245.3195 33.47097,56.141326 308,87.76 132.5,278.51896 50,400.76 31.5,247.02001 90,204.76 231,330.76 263.5,183.64 252.5,37.640001 116.5,6.6400001 99.5,94.640002 215.5,130.64 135.5,208.64001 25.5,177.64 147.5,127.64 273,55.38 262.5,285.02001 4,15.76") as PathFigureCollection;
                    path.Figures = figures;
                    transition.PathGeometry = path;
                    host.Transitions.Add(transition);

                    ScaleTransition transition1 = new ScaleTransition() { VisibleEasing = functionBase, Duration = 10000, HideEasing = functionBase, StartX = 0.1, StartY = 0.1, EndX = 0.1, EndY = 0.1 };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, Duration = 10000, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 720, EndAngle = 720 };
                    host.Transitions.Add(transition2);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Path-3" };
                    PathTransition transition = new PathTransition();
                    PathGeometry path = new PathGeometry();
                    PathFigureCollectionConverter converter = new PathFigureCollectionConverter();
                    PathFigureCollection figures = converter.ConvertFromInvariantString("M2,2.76 L240.5,145.02 20,162.76 223.03661,239.01999 31.438285,259.30231 135,308.76") as PathFigureCollection;
                    path.Figures = figures;
                    transition.PathGeometry = path;
                    host.Transitions.Add(transition);

                    ScaleTransition transition1 = new ScaleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, StartX = 0.1, StartY = 0.1, EndX = 0.1, EndY = 0.1 };
                    host.Transitions.Add(transition1);

                    AngleTransition transition2 = new AngleTransition() { VisibleEasing = functionBase, HideEasing = functionBase, RenderTransformOrigin = new System.Windows.Point(0.5, 0.5), StartAngle = 720, EndAngle = 720 };
                    host.Transitions.Add(transition2);
                    yield return host;
                }
            }

            {
                TransitionsHost host = new TransitionsHost() { Name = "Radial" };
                RadialOpacityMaskTransition transition = new RadialOpacityMaskTransition();
                host.Transitions.Add(transition);
                yield return host;
            }

            {
                TransitionsHost host = new TransitionsHost() { Name = "Linear Left-Right" };
                LinearOpacityMaskTransition transition = new LinearOpacityMaskTransition();
                transition.Viewport = new System.Windows.Rect(1, 1, 1, 1);
                transition.StartPoint = new System.Windows.Point(-1, 1);
                transition.EndPoint = new System.Windows.Point(1, 1);
                host.Transitions.Add(transition);
                yield return host;
            }

            {
                TransitionsHost host = new TransitionsHost() { Name = "Linear Top-Bottom" };
                LinearOpacityMaskTransition transition = new LinearOpacityMaskTransition();
                transition.Viewport = new System.Windows.Rect(1, 1, 1, 1);
                transition.StartPoint = new System.Windows.Point(1, -1);
                transition.EndPoint = new System.Windows.Point(1, 1);
                host.Transitions.Add(transition);
                yield return host;
            }


            {
                TransitionsHost host = new TransitionsHost() { Name = "Linear" };
                LinearOpacityMaskTransition transition = new LinearOpacityMaskTransition();
                host.Transitions.Add(transition);
                yield return host;
            }

            {
                TransitionsHost host = new TransitionsHost() { Name = "Image" };
                ImageOpacityMaskTransition transition = new ImageOpacityMaskTransition();
                host.Transitions.Add(transition);
                yield return host;
            }

            //{
            //    TransitionsHost host = new TransitionsHost() { Name = "AngleScale" };
            //    AngleTransition transition1 = new AngleTransition() { StartAngle = 720, EndAngle = 720 };
            //    ScaleTransition transition2 = new ScaleTransition();
            //    host.Transitions.Add(transition1);
            //    host.Transitions.Add(transition2);
            //    yield return host;
            //}
            //{
            //    TransitionsHost host = new TransitionsHost();
            //    OpacityTransition transition = new OpacityTransition();
            //    host.Transitions.Add(transition);
            //    yield return host;
            //}
            //{
            //    TransitionsHost host = new TransitionsHost();
            //    OpacityTransition transition = new OpacityTransition();
            //    host.Transitions.Add(transition);
            //    yield return host;
            //}
            //{
            //    TransitionsHost host = new TransitionsHost();
            //    OpacityTransition transition = new OpacityTransition();
            //    host.Transitions.Add(transition);
            //    yield return host;
            //}
            //{
            //    TransitionsHost host = new TransitionsHost();
            //    OpacityTransition transition = new OpacityTransition();
            //    host.Transitions.Add(transition);
            //    yield return host;
            //}
            //{
            //    TransitionsHost host = new TransitionsHost();
            //    OpacityTransition transition = new OpacityTransition();
            //    host.Transitions.Add(transition);
            //    yield return host;
            //}
        }

        public static IEnumerable<TransitionsHost> GetTest()
        {

            List<Type> easingTypes = new List<Type>();

            easingTypes.Add(null);
            easingTypes.Add(typeof(PowerEase));
            easingTypes.Add(typeof(BackEase));
            easingTypes.Add(typeof(ElasticEase));
            easingTypes.Add(typeof(BounceEase));
            //easingTypes.Add(typeof(CircleEase));
            //easingTypes.Add(typeof(QuadraticEase));
            //easingTypes.Add(typeof(CubicEase));
            //easingTypes.Add(typeof(QuarticEase));
            //easingTypes.Add(typeof(QuinticEase));
            //easingTypes.Add(typeof(ExponentialEase));
            //easingTypes.Add(typeof(SineEase));

            foreach (Type item in easingTypes)
            {
                EasingFunctionBase functionBase = item == null ? null : Activator.CreateInstance(item) as EasingFunctionBase;

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Angle" + item?.Name };
                    AngleTransition transition = new AngleTransition() { VisibleEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Horizontal" + item?.Name };
                    HorizontalTransition transition = new HorizontalTransition() { VisibleEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }


                {
                    TransitionsHost host = new TransitionsHost() { Name = "Vertical" + item?.Name };
                    VerticalTransition transition = new VerticalTransition() { VisibleEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "Opacity" + item?.Name };
                    OpacityTransition transition = new OpacityTransition() { VisibleEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }



                {
                    TransitionsHost host = new TransitionsHost() { Name = "Scale" + item?.Name };
                    ScaleTransition transition = new ScaleTransition() { VisibleEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }


                {
                    TransitionsHost host = new TransitionsHost() { Name = "RadialOpacityMask" + item?.Name };
                    RadialOpacityMaskTransition transition = new RadialOpacityMaskTransition() { VisibleEasing = functionBase };
                    host.Transitions.Add(transition);
                    yield return host;
                }

                {
                    TransitionsHost host = new TransitionsHost() { Name = "RadialOpacityMask" + item?.Name };
                    RadialOpacityMaskTransition transition = new RadialOpacityMaskTransition() { VisibleEasing = functionBase, Reverse = true };
                    host.Transitions.Add(transition);
                    yield return host;
                }
            }
        }
    }
}
