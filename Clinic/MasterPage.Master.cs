using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Clinic
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!EstaLogeado() && !(Page is Login))
            {
                Response.Redirect("Login.aspx");
            }
        }

        private bool EstaLogeado()
        {
            return !(Session["Usuario"] is null);
        }
    }
}