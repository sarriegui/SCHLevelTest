using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable()]
    public class Rol : IEquatable<Rol>
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [XmlArray("Permisos")]
        [XmlArrayItem("Permiso")]
        public List<string> Permisos { get; set; }
        public int WebPI_Access { get; set; }

        public bool Equals(Rol other)
        {
            if (other == null)
                return false;

            return  this.Id.Equals(other.Id) &&
                    this.Nombre == other.Nombre &&
                    this.WebPI_Access   == this.WebPI_Access &&
                    this.Permisos.Except(other.Permisos).ToList().Count() == 0;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
