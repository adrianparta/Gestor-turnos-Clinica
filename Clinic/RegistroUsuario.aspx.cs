using Dominio;
using Negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class RegistroUsuario : Page
    {
        public bool esAdmin;
        public int idUsuarioModificar = 0;
        public TipoUsuario tipoUsuarioRegistro;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session.Add("Usuario", new Usuario() { TipoUsuario = TipoUsuario.Admin});
                ddlTipoUsuario.DataSource = Enum.GetValues(typeof(TipoUsuario));
                ddlTipoUsuario.DataBind();
                ddlSexo.DataSource = Enum.GetValues(typeof(Sexo));
                ddlSexo.DataBind();
                ddlSexoAdmin.DataSource = Enum.GetValues(typeof(Sexo));
                ddlSexoAdmin.DataBind();
                ddlDia.DataSource = Enum.GetValues(typeof(Dia));
                ddlDia.DataBind();
                ddlEspecialidad.DataSource = EspecialidadNegocio.ObtenerEspecialidades();
                ddlEspecialidad.DataTextField = "Nombre";
                ddlEspecialidad.DataValueField = "IdEspecialidad";
                ddlEspecialidad.DataBind();

                idUsuarioModificar = Convert.ToInt32(Request.QueryString["idUsuarioModificar"]);        
                if(idUsuarioModificar > 0)
                {
                    var usuario = UsuarioNegocio.ObtenerUsuario(idUsuarioModificar);
                    txtApellido.Text = usuario.Apellido;
                    txtEmail.Text = usuario.Email;
                    txtNombre.Text = usuario.Nombre;
                    ddlTipoUsuario.SelectedIndex = (int) (usuario.TipoUsuario -1);
                }                   
            }
            if (!(Session["Usuario"] is null))
            {
                esAdmin = ((Usuario)Session["Usuario"]).TipoUsuario == TipoUsuario.Admin;
                if(esAdmin)
                {
                    tipoUsuarioRegistro = (TipoUsuario)(ddlTipoUsuario.SelectedIndex + 1);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(esAdmin 
                ? ""
                : "Login.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
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
                if (idUsuarioModificar > 0)
                {
                    if(UsuarioNegocio.ModificarUsuario(nuevoUsuario))
                    {

                    }
                    else
                    {
                        //Error
                    }
                }
                else
                {
                    int idUsuario = UsuarioNegocio.AltaUsuario(nuevoUsuario, txtPassword.Text);
                    if(idUsuario > 0)
                    {
                        Response.Redirect("Default.aspx");
                        nuevoUsuario.IdUsuario = idUsuario;
                        switch (nuevoUsuario.TipoUsuario)
                        {
                            case TipoUsuario.Doctor:
                                // Response.Redirect("RegistroDoctor.aspx"); 
                                break;
                            case TipoUsuario.Paciente:
                                // Response.Redirect("RegistroPaciente.aspx"); 
                                break;
                            default:
                                // Registro Exitoso 
                                break;
                        }
                    }
                    else
                    {
                        //Error
                    }
                }
            }
            else
            {
                nuevoUsuario.TipoUsuario = TipoUsuario.Paciente;
                int idUsuario = UsuarioNegocio.AltaUsuario(nuevoUsuario, txtPassword.Text);
                if (idUsuario > 0)
                {
                    nuevoUsuario.IdUsuario = idUsuario;
                    Session.Add("Usuario", nuevoUsuario);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    //Error
                }
            }
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoUsuarioRegistro = (TipoUsuario)(ddlTipoUsuario.SelectedIndex + 1);
        }

        protected void btnAgregarHorario_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarEspecialidad_Click(object sender, EventArgs e)
        {
            lbEspecialidad.Items.Add(ddlEspecialidad.SelectedItem);
        }
    }
}