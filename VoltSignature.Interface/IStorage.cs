using VoltSignature.DbCore.Entity.Base;
using VoltSignature.DbCore.Repository;
using VoltSignature.Files.Model;
using VoltSignature.Files.Repository;

namespace VoltSignature.Interface
{
    public interface IStorage
    {
        IRepository<T> GetRepository<T>() where T : IEntity<string>;
        IFileRepository GetFileRepository(FileTypeEnum fileType);
    }
}