using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controladores;

namespace ControllerTest
{
    [TestClass]
    public class CryptTest
    {
        [TestMethod]
        public void DecryptString()
        {
            Crypt _crypt = new Crypt("clavequequeramos");
            Assert.IsTrue(_crypt.DecryptString("AE14CB0EA7D6C55A") == "admin");
        }

        [TestMethod]
        public void EncryptString()
        {
            Crypt _crypt = new Crypt("clavequequeramos");
            Assert.IsTrue(_crypt.EncryptString("admin") == "AE14CB0EA7D6C55A");
        }
    }
}
