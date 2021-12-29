using System.Collections.ObjectModel;

namespace HeBianGu.App.Tool
{
    public interface IAssemblyDomain
    {
        ObservableCollection<FileBindModel> GetCommons();

        void SaveCommons(ObservableCollection<FileBindModel> collection);

        FileBindModel GetClipBoardFile();

        ObservableCollection<FileBindModel> GetFavorites();
    }
}