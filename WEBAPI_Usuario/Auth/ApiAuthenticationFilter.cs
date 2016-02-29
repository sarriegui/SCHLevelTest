using Controladores;
using Entidades;
using System;
using System.Configuration;
using System.Threading;
using System.Web.Http.Controllers;

namespace WEBAPI_Usuario
{
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        public ApiAuthenticationFilter()
        {
        }

        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            Controladores.UsuarioController _controller = new Controladores.UsuarioController();
            Usuario _user = null;
            try
            {
                Crypt _crypt = new Crypt(ConfigurationManager.AppSettings["SecretKey"].ToString());
                _user = _controller.Login(_crypt.DecryptString(username), _crypt.DecryptString(password));

                var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null) 
                {
                    Controladores.RolController _rolController = new Controladores.RolController();
                    if (!_rolController.RolHasAccessToWEBAPI(_user.Roles, actionContext.Request.Method))
                        return false;

                    basicAuthenticationIdentity.UserId = _user.Id;
                }
            }
            catch (Exception) { }
            
            return _user != null;
        }
    }
}