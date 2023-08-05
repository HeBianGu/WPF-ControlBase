using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HeBianGu.Systems.Design
{
    public interface IDoublesData
    {
        void RefreshData(IEnumerable<double> data);
    }

    public interface IDisplayDoublesData : IDoublesData
    {
        ObservableCollection<string> xDisplay { get; set; }
    }
}
