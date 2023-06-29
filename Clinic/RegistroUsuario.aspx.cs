using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class RegistroUsuario : Page
    {
        public bool esAdmin;
        public int idUsuarioModificar = 0;
        public int idUsuarioActual = -1;
        public TipoUsuario tipoUsuarioRegistro;
        protected void Page_Load(object sender, EventArgs e)
        {
            idUsuarioModificar = Convert.ToInt32(Request.QueryString["IdUsuario"]);
            if(!IsPostBack)
            {

                HorarioLaboral.HorarioLaboralAux = new List<HorarioLaboral>();
                Especialidad.EspecialidadAux = new List<Especialidad>();

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

                lbEspecialidad.DataTextField = "Nombre";
                lbEspecialidad.DataValueField = "IdEspecialidad";
                lbEspecialidad.DataBind();

                if (idUsuarioModificar > 0)
                {
                    var usuario = UsuarioNegocio.ObtenerUsuario(idUsuarioModificar);
                    if(!(usuario is null) && !(Session["Usuario"] is null))
                    {
                        btnRegistrar.Text = "Guardar Cambios";
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        txtEmail.Text = usuario.Email;
                        ddlTipoUsuario.SelectedIndex = (int)usuario.TipoUsuario - 1;
                        switch (usuario.TipoUsuario)
                        {
                            case TipoUsuario.Doctor:
                                foreach(var especialidad in ((Doctor)usuario).Especialidades)
                                {
                                    lbEspecialidad.Items.Add(especialidad.Nombre);
                                }
                                Especialidad.EspecialidadAux = ((Doctor)usuario).Especialidades;
                                foreach (var horario in ((Doctor)usuario).HorarioLaborales)
                                {
                                    lbHorario.Items.Add(horario.ToString());
                                }
                                HorarioLaboral.HorarioLaboralAux = ((Doctor)usuario).HorarioLaborales;
                                break;

                            case TipoUsuario.Paciente:
                                txtDniAdmin.Text = ((Paciente)usuario).Dni.ToString();
                                txtDireccionAdmin.Text = ((Paciente)usuario).Direccion;
                                txtObraSocialAdmin.Text = ((Paciente)usuario).ObraSocial;
                                txtFechaNacimientoAdmin.Text = ((Paciente)usuario).FechaNacimiento.ToString("yyyy-MM-dd");
                                ddlSexoAdmin.SelectedIndex = (int)((Paciente)usuario).Sexo - 1;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            if (!(Session["Usuario"] is null))
            {
                esAdmin = ((Usuario)Session["Usuario"]).TipoUsuario == TipoUsuario.Admin;
                idUsuarioActual = ((Usuario)Session["Usuario"]).IdUsuario;
                tipoUsuarioRegistro = (TipoUsuario)(ddlTipoUsuario.SelectedIndex + 1);                 
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

                int idUsuario = UsuarioNegocio.AltaUsuario(nuevoUsuario, txtPassword.Text);
                if(idUsuario > 0)
                {
                    nuevoUsuario.IdUsuario = idUsuario;
                    switch (nuevoUsuario.TipoUsuario)
                    {
                        case TipoUsuario.Doctor:
                            var nuevoDoctor = new Doctor()
                            {
                                IdUsuario = idUsuario,
                                HorarioLaborales = HorarioLaboral.HorarioLaboralAux,
                                Especialidades = Especialidad.EspecialidadAux
                            };
                            DoctorNegocio.AltaDoctor(nuevoDoctor);
                            break;
                        case TipoUsuario.Paciente:
                            var nuevoPaciente = new Paciente()
                            {
                                IdUsuario = idUsuario,
                                Dni = Convert.ToInt32(txtDniAdmin.Text),
                                Direccion = txtDireccionAdmin.Text,
                                ObraSocial = txtObraSocialAdmin.Text,
                                FechaNacimiento = DateTime.Parse(txtFechaNacimientoAdmin.Text),
                                Sexo = (Sexo)ddlSexoAdmin.SelectedIndex + 1,
                            };
                            PacienteNegocio.AltaPaciente(nuevoPaciente);
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
            else
            {
                nuevoUsuario.TipoUsuario = TipoUsuario.Paciente;
                int idUsuario = UsuarioNegocio.AltaUsuario(nuevoUsuario, txtPassword.Text);
                if (idUsuario > 0)
                {
                    var nuevoPaciente = new Paciente()
                    {
                        IdUsuario = idUsuario,
                        Dni = Convert.ToInt32(txtDni.Text),
                        Direccion = txtDireccion.Text,
                        ObraSocial = txtObraSocial.Text,
                        FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                        Sexo = (Sexo)ddlSexo.SelectedIndex + 1,
                    };
                    PacienteNegocio.AltaPaciente(nuevoPaciente);
                    Session.Add("Usuario", nuevoPaciente);
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
            var horarioLaboral = new HorarioLaboral(){
                Dia = (Dia)ddlDia.SelectedIndex + 1,
                HorarioEntrada = Convert.ToInt32(txtHorarioEntrada.Text),
                HorarioSalida = Convert.ToInt32(txtHorarioSalida.Text)
            };
            if(horarioLaboral.HorarioEntrada < horarioLaboral.HorarioSalida)
            {
                if(!HorarioLaboral.HorarioLaboralAux.Any(x => x.ToString() == horarioLaboral.ToString()))
                {
                    if(HorarioLaboral.HorarioLaboralAux.Any(x => x.Dia == horarioLaboral.Dia 
                    && (((horarioLaboral.HorarioEntrada >= x.HorarioEntrada && horarioLaboral.HorarioEntrada < x.HorarioSalida)
                        || (horarioLaboral.HorarioSalida > x.HorarioEntrada && horarioLaboral.HorarioSalida <= x.HorarioSalida))
                            || (horarioLaboral.HorarioEntrada < x.HorarioEntrada && horarioLaboral.HorarioSalida > x.HorarioSalida))))
                    {
                        return;
                    }
                    HorarioLaboral.HorarioLaboralAux.Add(horarioLaboral);          
                    lbHorario.Items.Add(horarioLaboral.ToString());
                }
            }
        }

        protected void btnAgregarEspecialidad_Click(object sender, EventArgs e)
        {
            if(!Especialidad.EspecialidadAux.Any(x => x.IdEspecialidad == Convert.ToInt32(ddlEspecialidad.SelectedValue) ))
            {
                lbEspecialidad.Items.Add(ddlEspecialidad.SelectedItem);
                Especialidad.EspecialidadAux.Add(new Especialidad { IdEspecialidad = Convert.ToInt32(ddlEspecialidad.SelectedValue) });
            }
        }

        protected void btnEliminarEspecialidad_Click(object sender, EventArgs e)
        {
            if (Especialidad.EspecialidadAux.Any(x => x.IdEspecialidad.ToString() == lbEspecialidad.SelectedValue))
            {
                Especialidad.EspecialidadAux.Remove(new Especialidad { IdEspecialidad = Convert.ToInt32(lbEspecialidad.SelectedValue) });
                lbEspecialidad.Items.RemoveAt(lbEspecialidad.SelectedIndex);
            }
        }

        protected void btnEliminarHorario_Click(object sender, EventArgs e)
        {
            if (HorarioLaboral.HorarioLaboralAux.Any(x => x.ToString() == lbHorario.SelectedValue))
            {
                var index = HorarioLaboral.HorarioLaboralAux.FindIndex(x => x.ToString() == lbHorario.SelectedValue);
                HorarioLaboral.HorarioLaboralAux.RemoveAt(index);
                lbHorario.Items.RemoveAt(lbHorario.SelectedIndex);
            }
        }
    }
}