using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Comparadores
{
    public class UsuarioPorNombre : IEqualityComparer<Usuario>
    {
        public bool Equals(Usuario x, Usuario y)
        {
            return x.Nombre == y.Nombre;
        }
        public int GetHashCode(Usuario codeh)
        {
            return codeh.GetHashCode();
        }
    }
}
