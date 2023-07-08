using System;
using System.Web.UI;
using Dominio;

namespace Clinic
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = (Usuario)Session["Usuario"];
            if (!(usuario is null))
            {
                switch(usuario.TipoUsuario)
                {
                    case TipoUsuario.Paciente:
                        Response.Redirect("TurnosPaciente.aspx");
                        break;
                    case TipoUsuario.Doctor:
                        Response.Redirect("TurnosDoctor.aspx");
                        break;
                    case TipoUsuario.Recepcionista:
                        Response.Redirect("TurnosRecepcionista.aspx");
                        break;
                    case TipoUsuario.Admin:
                        Response.Redirect("ListarUsuarios");
                        break;
                }
            }
        }
    }
}