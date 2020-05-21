using System.Collections.ObjectModel;

namespace HeBianGu.Application.ToolWindow
{
    public interface IAssemblyDomain
    {
        ObservableCollection<FileBindModel> GetCommons();

        void SaveCommons(ObservableCollection<FileBindModel> collection);

        FileBindModel GetClipBoardFile();

        ObservableCollection<FileBindModel> GetFavorites();
    }
}