using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VoltSignature.Core.Services;

namespace VoltSignature.Test
{
    [TestClass]
    public class UserTest : TestBase
    {
        [TestMethod]
        public void TestFindUser()
        {
            UserService service = new UserService(_mapper, storage);
            var list = service.FindUsers("Eric",null,null).GetAwaiter().GetResult();
        }
    }
}
