using Controladores;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCHLevelTest
{
    public partial class userlist : PageBase
    {
        new protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            LoadRoles();
        }

        private void LoadRoles()
        {
            RolController _rolController = new RolController();

            IEnumerable<Rol> _roles = _rolController.GetAll();

            StringBuilder _ss = new StringBuilder();

            foreach (Rol _rol in _roles) 
                _ss.AppendFormat("<option value='{0}'>{1}</option>", _rol.Id, _rol.Nombre);

            litRoles.Text = _ss.ToString();
        }
    }
}