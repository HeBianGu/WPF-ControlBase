using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeBianGu.Base.WpfBase
{
    public interface IEntityBase<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
    }

    /// <summary>
    /// 泛型实体基类
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class EntityBase<TPrimaryKey> : IEntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [Column("id", Order = 0)]
        public virtual TPrimaryKey ID { get; set; }
    }
}