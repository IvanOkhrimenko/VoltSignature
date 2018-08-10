using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using VoltSignature.MongoDb.Context;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;
using VoltSignature.Repository.Repository;
using VoltSignature.Repository.Storage;

namespace VoltSignature.Test.Database
{
    [TestClass]
    public class MongoDbTest
    {
        SignatureImageContext BuildContext()
        {
            string mongoDbConnectionString = "mongodb://localhost:27017/SignatureDb";
            //DbContextOptions<DbSignatureContext> option = new DbContextOptionsBuilder<DbSignatureContext>().UseNpgsql(connectionString).Options;
            SignatureImageContext context = new SignatureImageContext(mongoDbConnectionString);
            return context;
        }

        [TestMethod]
        public void TestImage()
        {
            Debugger.Launch();

            
            SignatureImageContext context = BuildContext();
            string path = "C:/test.jpg";
            var fs = File.Open(path, FileMode.Open);
            string id = context.SaveImage(fs, fs.Name).Result;
            var bytes = context.GetImage(id).Result;
            File.WriteAllBytes("C:/vm/result.jpg", bytes);
            context.RemoveImage(id).Wait();
            

        }
    }
}
