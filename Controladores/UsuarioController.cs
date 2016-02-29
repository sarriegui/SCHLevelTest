using Controladores.Excepciones;
using Entidades;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Controladores
{
    public class UsuarioController : Controller
    {
        public UsuarioController()
        { }
        
        public Usuario Login(string _user, string _password)
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            Usuario _GoodUser = _modelo.Get<Usuario>("Nombre", _user);

            if (_GoodUser == null)
                throw new UnknownObject(_user, "user");

            if (_GoodUser.Password.Equals(_password))
                return _GoodUser;
            else
                throw new IncorrectPassword(_user);
        }

        public bool UserHasAccess(Usuario usuario, string _ressource)
        {
            RolController _rolController = new RolController();

            foreach (int _rolId in usuario.Roles)
            {
                Rol _rol = _rolController.Get(_rolId);

                if (_rol == null)
                    throw new UnknownObject(_rolId.ToString(), "rol");

                foreach (string _permiso in _rol.Permisos)
                {
                    if (_permiso.Equals("all"))
                        return true;
                    else if (_ressource.Equals(string.Format("/{0}.aspx", _permiso)))
                        return true;   
                }
            }

            return false;
        }

        public IEnumerable<Usuario> GetAll()
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            List<Usuario> _users = _modelo.GetAll<Usuario>();

            if (_users == null)
                throw new EmptyList("Usuarios");

            return _users;
        }

        public Usuario Get(int _id)
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            Usuario _user = _modelo.Get<Usuario>("Id", _id);

            if (_user == null)
                throw new UnknownObject(_id.ToString(), "user");

            return _user;
        }

        public void Add(Usuario usuario)
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            List<Usuario> _users = _modelo.GetAll<Usuario>();

            if (_users.Where(_u => _u.Id == usuario.Id).Count() == 0)
            {
                usuario.Password = usuario.Nombre;
                _users.Add(usuario);

                _modelo.Save<Usuario>(_users);
            }
            else
                throw new DuplicateKeyException(string.Format("usuario con clave {0} ya existe",usuario.Id));
        }

        public void Update(int id, Usuario usuario)
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            List<Usuario> _users = _modelo.GetAll<Usuario>();

            Usuario _replaced = _users.Where(_u => _u.Id == usuario.Id).FirstOrDefault();

            if (_replaced != null)
            {
                usuario.Password = _replaced.Password;
                int index = _users.IndexOf(_replaced);
                if (index != -1)
                    _users[index] = usuario;

                _modelo.Save<Usuario>(_users);
            }
            else
                throw new DuplicateKeyException(string.Format("usuario con clave {0} no existe", usuario.Id));
        }

        public void Remove(int id)
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            List<Usuario> _users = _modelo.GetAll<Usuario>();

            Usuario _user = _users.Where(_u => _u.Id == id).Select(_u => _u).FirstOrDefault();

            if (_user == null)
                throw new UnknownObject(id.ToString(), "user");

            _users.Remove(_user);

            _modelo.Save<Usuario>(_users);
        }
    }
}
