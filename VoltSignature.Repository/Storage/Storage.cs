using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.Repository.Interface;
using VoltSignature.Repository.Repository;

namespace VoltSignature.Repository.Storage
{ 

    public class Storage : IStorage
    {
        private ConcurrentBag<BaseRepository> _repositorys = new ConcurrentBag<BaseRepository>();

        public Storage(DbSignatureContext context)
        { 
            foreach (Type mytype in this.GetType().Assembly.GetTypes().Where(mytype => mytype.IsSubclassOf(typeof(BaseRepository))))
            {
                BaseRepository repository = Activator.CreateInstance(mytype, context) as BaseRepository;
                _repositorys.Add(repository);  
            }
        }

        public IRepository<T> Get<T>()
        {
            var repository = _repositorys.FirstOrDefault(x => x.GetEntityType() == typeof(T)); 
            return repository as IRepository<T>;
        }
    }
}
