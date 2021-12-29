namespace HeBianGu.Base.WpfBase
{
    public interface IModelViewModel<T>
    {
        T Model { get; set; }
        int Value { get; set; }
        bool Visible { get; set; }
    }
}