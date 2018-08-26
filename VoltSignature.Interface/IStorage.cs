using VoltSignature.DbCore.Entity.Base;
using VoltSignature.DbCore.Repository;

namespace VoltSignature.Interface
{
    public interface IStorage
    {
        IRepository<T> GetRepository<T>() where T : IEntity<string>;
    }
}