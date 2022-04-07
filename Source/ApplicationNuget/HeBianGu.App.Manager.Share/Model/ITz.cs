using System.Collections.Generic;

namespace HeBianGu.App.Manager
{
    public interface ITz
    {

    }

    public abstract class TZType : ITz
    {
        public double OutFre { get; set; }
    }


    public class CW : TZType

    {

    }

    public class AM : TZType
    {
        public double TzFre { get; set; }

        public double TzDeep { get; set; }
    }


    public class ASK : TZType
    {
        public double XhSpeed { get; set; }

        public double TzDeep { get; set; }

        public string SendType { get; set; }
        public double Xzyz { get; set; }

        public string SelectType { get; set; }

        public double Age { get; set; }

        public double Height { get; set; }

        public string SavePath { get; set; }

        public double Weight { get; set; }

        public bool Online { get; set; }

        public List<int> Mls { get; set; }

    }
}
