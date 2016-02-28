using Controladores;
using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCHLevelTest
{
    public partial class login : PageBase
    {
        new protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                UsuarioController _controller = new UsuarioController();
                try
                {
                    Usuario _user = _controller.Login(txtUsuario.Value, txtPassword.Value);

                    if (_user != null)
                    {
                        Session["User"] = _user;
                        Session["expire"] = DateTime.Now.AddMinutes(5);
                        Session["home"] = _user.Home;

                        Crypt _crypt = new Crypt(ConfigurationManager.AppSettings["SecretKey"].ToString());
                        Session["cryptuser"] = _crypt.EncryptString(_user.Nombre);
                        Session["cryptpwd"] = _crypt.EncryptString(_user.Password);

                        string returnUrl = Request.QueryString["ReturnUrl"];
                        if (string.IsNullOrEmpty(returnUrl)) returnUrl = _user.Home;

                        Response.Redirect(returnUrl);
                    }
                }
                catch (Exception) { }
            }
        }

        private bool ValidateForm()
        {
            return !String.IsNullOrEmpty(txtUsuario.Value) && !String.IsNullOrEmpty(txtPassword.Value);
        }
    }
}