using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;
using VoltSignature.Repository.Repository;
using VoltSignature.Repository.Storage;

namespace VoltSignature.Test.Database
{
    [TestClass]
    public class PostgresqlTest
    {
        DbSignatureContext BuildContext()
        {
            string connectionString = "Host=localhost;Port=5432;Database=SignatureDb;Username=postgres;Password=fsgpsa";
            DbContextOptions<DbSignatureContext> option = new DbContextOptionsBuilder<DbSignatureContext>().UseNpgsql(connectionString).Options;
            DbSignatureContext context = new DbSignatureContext(option);
            return context;
        }

        [TestMethod]
        public void TestCompanyCRUD()
        {
            Debugger.Launch();

      
            DbSignatureContext context = BuildContext();
            Storage storage = new Storage(context);
            IRepository<Company> repo = storage.Get<Company>();

            CompanyRepository _companyRepository = new CompanyRepository(context);
            UserRepository _userRepository = new UserRepository(context);

           // Role r1 = new Role()
           // {
           //     Name = "User"
           // };
           // Role r2 = new Role()
           // {
           //     Name = "CompanyAdmin"
           // };
           // Role r3 = new Role()
           // {
           //     Name = "Admin"
           // };
           // context.Roles.AddRange(r1, r2, r3);
           // context.SaveChanges();

           // Company c = new Company()
           // {
           //     Name = "ABC motors",
           //     RegisterDate = DateTime.UtcNow,
           // };
           // _companyRepository.Create(c).Wait();
 
   
           // //var company = context.Companies.FirstOrDefault();
           // User u1 = new User()
           // {
           //     CompanyId = c.Id,
           //     Email = "test@email.com",
           //     FirstName = "Test1",
           //     LastName = "test2",
           //     Password = "test",
           //     Phone = "21321321",
           //     RegisterDate = DateTime.UtcNow,
           //     RoleId = 1,
           //     Position = "Manger"
           // };
           // _userRepository.Create(u1).Wait();
           // c.OwnerId = u1.Id;
           // _companyRepository.Update(c).Wait();

           // Signature s = new Signature()
           // {
           //     FileId = "asdaa",
           //     CreateDate = DateTime.UtcNow
           // };
             
           // context.Signatures.Add(s);
           // context.SaveChanges();
           // UserSignature us = new UserSignature()
           // {
           //     SignatureId = s.Id,
           //     UserId = u1.Id
           // };
           // context.UserSignatures.Add(us);
           // context.SaveChanges();
           // Company cc = _companyRepository.Get(x => x.Id == 4,include=>include.Include(x=>x.Owner)).Result; 
           // var t1 = context.Signatures.FirstOrDefault();
           // var t2 = context.UserSignatures.FirstOrDefault();

           //var t12 = context.Signatures.Include(x => x.UserSignatures).FirstOrDefault();
           // var asd = context.Companies.FirstOrDefault(x => x.Name == "BSI");
           // var user = context.Users.FirstOrDefault();
           //var company = context.Companies.FirstOrDefault();

            context.Dispose();
        }
    }
}
