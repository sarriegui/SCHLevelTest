using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Entidades;
using System.Collections.Generic;

namespace ControllerTest
{
    [TestClass]
    public class RolController
    {
        [TestMethod]
        public void GetRol()
        {
            Controladores.RolController _rolC = new Controladores.RolController();
            Entidades.Rol _rol = _rolC.Get(1);
            Entidades.Rol _rol2 = new Entidades.Rol();
            _rol2.Id = 1;
            _rol2.Nombre = "admin";
            _rol2.Permisos = new System.Collections.Generic.List<string>() { "all" };
            _rol2.WebPI_Access = (int)Controladores.RolController.Permissions.ReadWrite;

            Assert.IsTrue(_rol.Equals(_rol2));
        }

        [TestMethod]
        [ExpectedException(typeof(Controladores.Excepciones.UnknownObject))]
        public void GetWrongRol()
        {
            Controladores.RolController _rolC = new Controladores.RolController();
            Entidades.Rol _rol = _rolC.Get(-1);
        }

        [TestMethod]
        public void UserHasAccessToWEBAPI()
        {
            Controladores.RolController _rolC = new Controladores.RolController();
            Entidades.Rol _rol2 = new Entidades.Rol();
            _rol2.Id = 1;
            _rol2.Nombre = "admin";
            _rol2.Permisos = new System.Collections.Generic.List<string>() { "all" };
            _rol2.WebPI_Access = (int)Controladores.RolController.Permissions.ReadWrite;

            if (!_rolC.RolHasAccessToWEBAPI(new System.Collections.Generic.List<int>() { 1 }, HttpMethod.Get))
                Assert.Fail();

            if (!_rolC.RolHasAccessToWEBAPI(new System.Collections.Generic.List<int>() { 1 }, HttpMethod.Put))
                Assert.Fail();

            if (!_rolC.RolHasAccessToWEBAPI(new System.Collections.Generic.List<int>() { 2 }, HttpMethod.Get))
                Assert.Fail();

            if (_rolC.RolHasAccessToWEBAPI(new System.Collections.Generic.List<int>() { 2 }, HttpMethod.Put))
                Assert.Fail();
        }

        [TestMethod]
        public void GetAll()
        {
            Controladores.RolController _rolC = new Controladores.RolController();
            IEnumerable<Rol> _lista = _rolC.GetAll();
            Assert.IsTrue(_lista != null);
        }
    }
}
