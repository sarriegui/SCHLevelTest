using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores.Excepciones
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string _mensaje)
            :base (_mensaje)
        {
        }
    }
}
