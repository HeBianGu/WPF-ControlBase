namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 带有日志的泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ILogRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>
    {
        ///// <summary>
        ///// 运行日志
        ///// </summary>
        ///// <param name="message"></param>
        //void LogInfo(params string[] message);

        ///// <summary>
        ///// Degbug日志
        ///// </summary>
        ///// <param name="message"></param>
        //void LogDebug(params string[] message);

        ///// <summary>
        ///// 错误日志
        ///// </summary>
        ///// <param name="exception"></param>
        ///// <param name="message"></param>
        //void LogError(Exception exception, string message);
    }
}
