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
                btnLogin.Text = "Editar Perfil";
            }
            else
            {
                btnRegistrarCerrarSesion.Text = "Registrarse";
                btnLogin.Text = "Log In";

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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (EstaLogeado())
            {
                Response.Redirect($"RegistroUsuario.aspx?IdUsuario={((Usuario)Session["Usuario"]).IdUsuario}");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}