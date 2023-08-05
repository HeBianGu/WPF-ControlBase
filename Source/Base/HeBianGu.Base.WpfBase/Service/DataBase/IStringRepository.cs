namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 默认string主键类型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IStringRepository<TEntity> : ILogRepository<TEntity, string> where TEntity : StringEntityBase
    {

    }
}
