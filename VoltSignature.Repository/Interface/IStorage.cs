namespace VoltSignature.Repository.Interface
{
    public interface IStorage
    {
        IRepository<T> Get<T>();
    }
}