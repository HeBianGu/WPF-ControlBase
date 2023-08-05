// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Repository
{
    public static class RepositoryExtention
    {
        public static IRepository GetRepository(this Type modelType, out string error)
        {
            error = null;
            if (modelType == null)
            {
                error = "参数不能为空";
                return null;
            }

            Type type = typeof(IRepository<>);

            Type genericType = type.MakeGenericType(modelType);

            IRepository result = ServiceRegistry.Instance.GetInstance(genericType) as IRepository;

            if (result == null)
            {
                error = $"请先注入接口IRepository<{modelType.Name}>";
            }
            return result;
        }

        public static async Task<T> InvokeMethod<T>(this Type type, string methodName, params object[] parameters)
        {
            IRepository repository = type.GetRepository(out string error);

            if (repository == null)
            {
                await MessageProxy.Messager.ShowSumit(error);
                return default(T);
            }

            return await MessageProxy.Messager.ShowWaitter(() =>
            {
                return (T)repository.GetType().GetMethod(methodName)?.Invoke(repository, parameters);
            });

        }
    }
}
