using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores.Excepciones
{
    public class IncorrectPassword : Exception
    {
        public IncorrectPassword(string _mensaje)
            :base (_mensaje)
        {
        }
    }
}
