using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores.Excepciones
{
    public class UnknownObject : Exception
    {
        public UnknownObject(string _mensaje , string _object)
            :base (string.Format("Mensaje: {0} - Object: {1}",_mensaje, _object))
        {
        }
    }
}
