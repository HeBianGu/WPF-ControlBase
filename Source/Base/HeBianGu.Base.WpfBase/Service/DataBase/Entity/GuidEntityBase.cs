using System;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 定义默认主键类型为Guid的实体基类
    /// </summary>
    public abstract class GuidEntityBase : EntityBase<Guid>
    {
        public GuidEntityBase()
        {
            this.ID = Guid.NewGuid();
        }
    }

}
