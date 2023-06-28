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
            ddlAux.DataSource = UsuarioNegocio.ListarUsuario(ddlTipoUsuario.SelectedIndex);
            ddlAux.DataTextField = "Nombre";
            ddlAux.DataValueField = "IdUsuario";
            ddlAux.DataBind();
        }
    }
}