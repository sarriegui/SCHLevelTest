using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable()]
    public class Usuario : IEquatable<Usuario>
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Home { get; set; }
        [XmlArray("Roles")]
        [XmlArrayItem("RolId")]
        public List<int> Roles { get; set; }

        public Usuario() {}

        public bool Equals(Usuario other)
        {
            if (other == null)
                return false;

            return this.Id.Equals(other.Id) &&
                    this.Nombre == other.Nombre &&
                    this.Password == this.Password &&
                    this.Home.Equals(other.Home) &&
                    this.Roles.Except(other.Roles).ToList().Count() == 0;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
