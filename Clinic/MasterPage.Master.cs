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
            if(!EstaLogeado() && !(Page is Login) && !(Page is RegistroUsuario))
            {
                Response.Redirect("Login.aspx");
            }
            if (EstaLogeado())
            {
                btnRegistrarCerrarSesion.Text = "Cerrar Sesión";
            }
            else
            {
                btnRegistrarCerrarSesion.Text = "Registrarse";
            }
        }

        private bool EstaLogeado()
        {
            return !(Session["Usuario"] is null);
        }

        protected void btnRegistrarCerrarSesion_Click(object sender, EventArgs e)
        {
            if (EstaLogeado())
            {
                Session["Usuario"] = null;
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("RegistroUsuario.aspx");
            }
        }
    }
}