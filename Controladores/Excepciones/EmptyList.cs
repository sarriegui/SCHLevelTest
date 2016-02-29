using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores.Excepciones
{
    public class EmptyList : Exception
    {
        public EmptyList(string _mensaje)
            :base (_mensaje)
        {
        }
    }
}
