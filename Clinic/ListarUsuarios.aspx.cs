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
    public partial class ListarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTipoUsuario.DataSource = TipoUsuarioNegocio.ObtenerTiposUsuarios();
                ddlTipoUsuario.DataBind();
                ddlTipoUsuario.ClearSelection();
                ddlTipoUsuario.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
        }
       
        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            repeaterLista.DataSource = UsuarioNegocio.ListarUsuario(ddlTipoUsuario.SelectedIndex);
            repeaterLista.DataBind();
        }

        protected void BotonModificar_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Response.Redirect("RegistroUsuario.aspx?IdUsuario=" + id, false);
        }

        protected void BotonELiminar_Click(object sender, EventArgs e)
        {
            //poner usuario.activo = 0
        }
    }
}