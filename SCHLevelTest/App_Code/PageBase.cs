using Controladores;
using Entidades;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace SCHLevelTest
{
    public class PageBase : System.Web.UI.Page
    {
        public string nombreUsuario;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            CheckLogin();
        }

        private void CheckLogin()
        {
            if (Session["User"] == null || Session["expire"] == null )
                LogOut();
            else
            {
                DateTime _dt;
                DateTime.TryParse(Session["expire"].ToString(), out _dt);

                if (_dt < DateTime.Now)
                    LogOut();

                nombreUsuario = ((Usuario)Session["User"]).Nombre;

                UsuarioController _controller = new UsuarioController();

                if (!_controller.UserHasAccess((Usuario)Session["User"], Request.RawUrl))
                    LogOut();
            }
        }

        private void LogOut()
        {
            Session["User"] = null;
            Response.Redirect("default.aspx?ReturnUrl="+ Request.RawUrl ,true);
        }

        private string GetQueryString()
        {
            string queryString = "";

            NameValueCollection qs = Request.QueryString;

            foreach (string key in qs.AllKeys)
                foreach (string value in qs.GetValues(key))
                    queryString += Server.UrlEncode(key) + "=" + Server.UrlEncode(value) + "&";

            return queryString.TrimEnd('&');
        }
    }
}
