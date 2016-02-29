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
    public class RolController : Controller
    {
        [Flags]
        public enum Permissions
        {
            None = 0,
            Read = 1,
            Write = 2,
            ReadWrite = Read | Write
        }

        public RolController()
        { }

        public Rol Get(int _id)
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            Rol _rol = _modelo.Get<Rol>("Id", _id);

            if (_rol == null)
                throw new UnknownObject(_id.ToString(), "user");

            return _rol;
        }

        public IEnumerable<Rol> GetAll()
        {
            Modelo.Modelo _modelo = new Modelo.Modelo(_dataPath);
            List<Rol> _roles = _modelo.GetAll<Rol>();

            if (_roles == null)
                throw new EmptyList("Roles");

            return _roles;
        }

        public bool RolHasAccessToWEBAPI(List<int> _iRoles, HttpMethod _method)
        {
            RolController _rolController = new RolController();

            Permissions _perm = Permissions.ReadWrite;
            List<Rol> _roles = new List<Rol>();

            _iRoles.ForEach(_r =>
            {
                _roles.Add(Get(_r));
                _perm = _perm & (Permissions)_roles.Last().WebPI_Access;
            });

            if (_method == HttpMethod.Get)
                return ((Permissions)_perm & Permissions.Read) == Permissions.Read;
            else
                return ((Permissions)_perm & Permissions.Write) == Permissions.Write;
        }
    }
}
