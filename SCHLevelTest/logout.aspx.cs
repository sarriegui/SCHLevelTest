using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCHLevelTest
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["User"] = null;
            Session["expire"] = null;
            Session["cryptuser"] = null;
            Session["cryptpwd"] = null;
            Session["home"] = null;

            Session.Abandon();
            Session.Clear();

            FormsAuthentication.SignOut();

            Response.Redirect("default.aspx");
        }
    }
}