using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        public bool esAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlTipoUsuario.DataSource = TipoUsuarioNegocio.ObtenerTiposUsuarios();
            ddlTipoUsuario.DataBind();
            if(!(Session["Usuario"] is null))
            {
                esAdmin = ((Usuario)Session["Usuario"]).TipoUsuario == TipoUsuario.Admin;
            }
            esAdmin = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(esAdmin 
                ? ""
                : "Login.aspx");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario nuevoUsuario = new Usuario()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Email = txtEmail.Text,
            };
            if (esAdmin)
            {
                nuevoUsuario.TipoUsuario = (TipoUsuario) (ddlTipoUsuario.SelectedIndex + 1);
                int idUsuario = UsuarioNegocio.UsuarioAlta(nuevoUsuario, txtPassword.Text);
                if(idUsuario > 0)
                {
                    nuevoUsuario.IdUsuario = idUsuario;
                    if (nuevoUsuario.TipoUsuario == TipoUsuario.Doctor)
                    {
                        // Response.Redirect("RegistroDoctor.aspx"); 
                    }
                }
            }
            else
            {
                nuevoUsuario.TipoUsuario = TipoUsuario.Paciente;
                int idUsuario = UsuarioNegocio.UsuarioAlta(nuevoUsuario, txtPassword.Text);
                if (idUsuario > 0)
                {
                    nuevoUsuario.IdUsuario = idUsuario;
                    Session.Add("Usuario", nuevoUsuario);
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}