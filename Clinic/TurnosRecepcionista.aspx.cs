using Dominio;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class TurnosRecepcionista : System.Web.UI.Page
    {
    protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListEspecialidad.DataSource = EspecialidadNegocio.ObtenerEspecialidades();
                DropDownListEspecialidad.DataTextField = "Nombre";
                DropDownListEspecialidad.DataValueField = "IdEspecialidad";
                DropDownListEspecialidad.DataBind();
                DropDownListEspecialidad.ClearSelection();
                DropDownListEspecialidad.Items.Insert(0, new ListItem("-- Seleccione --", ""));

                DropDownListPacientes.DataSource = PacienteNegocio.ObtenerPacientes();
                DropDownListPacientes.DataTextField = "Nombre";
                DropDownListPacientes.DataValueField = "IdPaciente";
                DropDownListPacientes.DataBind();
                DropDownListPacientes.ClearSelection();
                DropDownListPacientes.Items.Insert(0, new ListItem("-- Seleccione --", ""));

                if (!(Request.QueryString["x"] is null))
                {
                    DropDownListPacientes.SelectedValue = PacienteNegocio.ObtenerPacientes().Max(item => item.IdPaciente).ToString();
                    Session["paciente"] = PacienteNegocio.ObtenerPaciente(int.Parse(DropDownListPacientes.SelectedValue));

                }
            }
        }

        protected void DropDownListEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListMedicos.DataSource = DoctorNegocio.ObtenerDoctores(DropDownListEspecialidad.SelectedIndex);
            DropDownListMedicos.DataTextField = "NombreCompleto";
            DropDownListMedicos.DataValueField = "IdDoctor";
            DropDownListMedicos.DataBind();
            DropDownListMedicos.ClearSelection();
            DropDownListMedicos.Items.Insert(0, new ListItem("-- Seleccione --", ""));

            Session["Especialidad"] = EspecialidadNegocio.ObtenerEspecialidad(int.Parse(DropDownListEspecialidad.SelectedValue));

        }

        protected void DropDownListMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Doctor"] = DoctorNegocio.ObtenerDoctor(int.Parse(DropDownListMedicos.SelectedValue));
            Session["horariolaboral"] = DoctorNegocio.ObtenerTurnosEspecialidadesHorarios((Doctor)Session["doctor"]).HorarioLaborales;
        }

        protected void DropDownListPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["paciente"] = PacienteNegocio.ObtenerPaciente(int.Parse(DropDownListPacientes.SelectedValue));
        }

        protected void Calendario_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsWeekend)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = Color.LightGray;
            }
            if (!(Session["horariolaboral"] is null))
            {
                for (int i = 0; i < ((List<HorarioLaboral>)Session["horariolaboral"]).Count; i++)
                {
                    if ((int)e.Day.Date.DayOfWeek == ((int)((List<HorarioLaboral>)Session["horariolaboral"])[i].Dia))
                    {
                        e.Day.IsSelectable = true;
                        e.Cell.BackColor = Color.LightGreen;
                    }
                    if (e.Day.Date == Calendario.SelectedDate)
                    {
                        e.Cell.BackColor = Color.Green;
                    }
                }
            }
        }

        protected void DropDownListHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Calendario_SelectionChanged(object sender, EventArgs e)
        {
            HorarioLaboral h = ((List<HorarioLaboral>)Session["horariolaboral"]).Find(item => (int)item.Dia == (int)Calendario.SelectedDate.DayOfWeek);

            List<int> horarios = new List<int>();
            for (int i = h.HorarioEntrada; i <= h.HorarioSalida -1; i++)
            {
                horarios.Add(i);
            }
            DropDownListHorarios.DataSource = horarios;
            DropDownListHorarios.DataBind();
            DropDownListHorarios.ClearSelection();
            DropDownListHorarios.Items.Insert(0, new ListItem("-- Seleccione --", ""));
        }

        protected void DropDownListHorarios_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in DropDownListHorarios.Items)
            {
                List<Turno> turnosdoctor = TurnoNegocio.ObtenerTurnosDeDoctor((Doctor)Session["doctor"], Calendario.SelectedDate, Calendario.SelectedDate.AddDays(1));
                List<Turno> turnospaciente = TurnoNegocio.ObtenerTurnosDePaciente((Paciente)Session["paciente"]);

                int.TryParse(item.Text, out int i);
                
                bool doctorocupado = turnosdoctor.Any(aux => aux.Horario.Hour == i);
                bool pacienteocupado = turnospaciente.Any(aux => aux.Horario.Hour == i  && aux.Horario.Date == Calendario.SelectedDate);

                item.Text = item.Text + ":00 - " + (i+1) + ":00";
                if (doctorocupado || pacienteocupado)
                {
                    item.Attributes["disabled"] = "disabled";
                    item.Attributes["style"] = "color: lightgray";
                }
            }
        }
        protected void Aceptar_Click(object sender, EventArgs e)
        {
            Turno aux = new Turno();
            aux.Especialidad = (Especialidad)Session["Especialidad"];
            aux.Doctor = ((Doctor)Session["Doctor"]);
            aux.Paciente = (Paciente)Session["paciente"];
            aux.Horario = Calendario.SelectedDate;
            aux.Horario = aux.Horario.AddHours(int.Parse(DropDownListHorarios.SelectedValue));
            aux.Causas = TextBoxCausas.Text;

            TurnoNegocio.AgregarTurno(aux);
            Response.Redirect("Default.aspx");
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void BotonAgregarPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroUsuario.aspx?x=1");
        }
    }
}