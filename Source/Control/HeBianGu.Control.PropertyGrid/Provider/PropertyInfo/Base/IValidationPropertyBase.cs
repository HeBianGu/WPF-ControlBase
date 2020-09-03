namespace HeBianGu.Control.PropertyGrid
{
    public interface IValidationProperty
    {
        bool IsValidation(Property source);

        void LoadDefault();

        void LoadProperty(Property config, string defaultName = null);

        void LoadUnit(UnitGroup unit);

        void LoadValue(object obj);
    }
}