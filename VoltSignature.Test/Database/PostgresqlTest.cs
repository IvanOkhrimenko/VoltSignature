using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Repository;

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
            CompanyRepository _companyRepository = new CompanyRepository(context);
            UserRepository _userRepository = new UserRepository(context);
            var u = _userRepository.Get(x => x.CompanyId == 4).Result;
            //Role r1 = new Role()
            //{
            //    Name = "User"
            //};
            //Role r2 = new Role()
            //{
            //    Name = "CompanyAdmin"
            //};
            //Role r3 = new Role()
            //{
            //    Name = "Admin"
            //};
            //context.Roles.AddRange(r1, r2, r3);
            //context.SaveChanges();

            User u1 = new User()
            {
                CompanyId = 4,
                Email = "test@email.com",
                FirstName = "Test1",
                LastName = "test2",
                Password = "test",
                Phone = "21321321",
                RegisterDate = DateTime.UtcNow,
                RoleId = 1,
                Position = "Manger"
            };
            _userRepository.Create(u1).Wait();

            //Company c = new Company()
            //{
            //    Name = "ABC Motors",
            //    RegisterDate = DateTime.UtcNow,
            //    //OwnerId = 0
            //};
            //_companyRepository.Create(c).Wait();
            var company = context.Companies.Include(x=>x.Owner).FirstOrDefault();
            Company c = _companyRepository.Get(x => x.Id == 4,include=>include.Include(x=>x.Owner)).Result; 


            context.Dispose();
        }
    }
}
