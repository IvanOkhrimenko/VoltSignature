using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VoltSignature.Core.Mapper;
using VoltSignature.Core.Storage;
using VoltSignature.DbCore.Context;
using VoltSignature.DbCore.Entity;
using VoltSignature.DbCore.Repository;

namespace VoltSignature.Test
{
    public class TestBase
    {
        public IMapper _mapper;
        public const string ConnectionString = "mongodb://localhost:27017/SignatureDb";
        public MongoContext context;
        public MongoFileContext mongoFileContext;
        public Storage storage;
        public IRepository<User> userRepo;

        public TestBase()
        {
            var profile = new MappingProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = config.CreateMapper();
             context = new MongoContext(ConnectionString);
             mongoFileContext = new MongoFileContext(ConnectionString);
             storage = new Storage(context, mongoFileContext);
            userRepo = storage.GetRepository<User>();
        }
    }
}
