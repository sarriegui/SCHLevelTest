using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ControllerTest
{
    [TestClass]
    public class UsuarioController
    {
        [ClassInitialize()]
        public static void Init_TestUsuarioController(TestContext testContext)
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user2 = new Entidades.Usuario();
            _user2.Id = -2;
            _user2.Nombre = "adminc";
            _user2.Password = "adminc";
            _user2.Home = "userlistb.aspx";

            _userC.Add(_user2);

            _user2 = new Entidades.Usuario();
            _user2.Id = -3;
            _user2.Nombre = "adminc";
            _user2.Password = "adminc";
            _user2.Home = "userlistb.aspx";

            _userC.Add(_user2);
        }

        [ClassCleanup]
        public static void End_TestUsuarioController()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            _userC.Remove(-1);
            _userC.Remove(-2);
        }

        [TestMethod]
        public void Get()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user = _userC.Get(1);
            Entidades.Usuario _user2 = new Entidades.Usuario();
            _user2.Id = 1;
            _user2.Nombre = "admin";
            _user2.Password = "admin";
            _user2.Home = "userlist.aspx";
            _user2.Roles = new System.Collections.Generic.List<int>() { 1 };

            Assert.IsTrue(_user.Equals(_user2));
        }

        [TestMethod]
        [ExpectedException(typeof(Controladores.Excepciones.UnknownObject))]
        public void GetWrongUser()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user = _userC.Get(-4);
        }

        [TestMethod]
        public void Login()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user = _userC.Login("admin","admin");

            Assert.IsTrue(_user != null);
        }

        [TestMethod]
        [ExpectedException(typeof(Controladores.Excepciones.UnknownObject))]
        public void WrongLogin()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user = _userC.Login("aadmin", "admin");

            Assert.IsTrue(_user == null);
        }

        [TestMethod]
        public void UserHasAccess()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user = _userC.Get(1);
            Assert.IsTrue(_userC.UserHasAccess(_user, "/pagA.aspx"));
        }

        [TestMethod]
        public void UserNotHasAccess()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user = _userC.Get(3);
            Assert.IsTrue(!_userC.UserHasAccess(_user, "/pagA.aspx"));
        }

        [TestMethod]
        public void GetAll()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            IEnumerable<Entidades.Usuario> _lista = _userC.GetAll();
            Assert.IsTrue(_lista != null);
        }

        [TestMethod]
        public void Add()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user2 = new Entidades.Usuario();
            _user2.Id = -1;
            _user2.Nombre = "adminb";
            _user2.Password = "adminb";
            _user2.Home = "userlistb.aspx";

            _userC.Add(_user2);

            Entidades.Usuario _user = _userC.Get(1);
            Assert.IsTrue(_user != null);
        }

        [TestMethod]
        [ExpectedException(typeof(Controladores.Excepciones.DuplicateKeyException))]
        public void AddDuplicate()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user2 = new Entidades.Usuario();
            _user2.Id =- 2;
            _user2.Nombre = "adminb";
            _user2.Password = "adminb";
            _user2.Home = "userlistb.aspx";

            _userC.Add(_user2);
        }

        [TestMethod]
        public void Update()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            Entidades.Usuario _user2 = new Entidades.Usuario();
            _user2.Id = -2;
            _user2.Nombre = "adminbbb";
            _user2.Password = "adminbbb";
            _user2.Home = "userlistbbb.aspx";

            _userC.Update(-2, _user2);

            Entidades.Usuario _user = _userC.Get(-2);
            Assert.IsTrue(_user.Home.Equals("userlistbbb.aspx"));
        }

        [TestMethod]
        [ExpectedException(typeof(Controladores.Excepciones.UnknownObject))]
        public void Remove()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            _userC.Remove(-3);

            Entidades.Usuario _user = _userC.Get(-3);
            Assert.IsTrue(_user == null);
        }

        [TestMethod]
        [ExpectedException(typeof(Controladores.Excepciones.UnknownObject))]
        public void WrongRemove()
        {
            Controladores.UsuarioController _userC = new Controladores.UsuarioController();
            _userC.Remove(-4);
        }
    }
}
