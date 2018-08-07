using System;
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
        private List<BaseRepository> list = new List<BaseRepository>();

        public Storage(DbSignatureContext context)
        { 
            foreach (Type mytype in this.GetType().Assembly.GetTypes().Where(mytype => mytype.IsSubclassOf(typeof(BaseRepository))))
            {
                BaseRepository repository = Activator.CreateInstance(mytype, context) as BaseRepository;
                list.Add(repository);  
            }
        }

        public IRepository<T> Get<T>()
        {
            var repository = list.FirstOrDefault(x => x.GetEntityType() == typeof(T)); 
            return repository as IRepository<T>;
        }
    }
}
