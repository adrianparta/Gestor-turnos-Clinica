using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class TurnosMed : Page
    {
        Doctor doctor;
        protected void Page_Load(object sender, EventArgs e)
        {
            doctor = (Doctor)Session["Usuario"];
            if(!IsPostBack)
            {
                var turnoPorDia = new List<TurnoMedDto>();
                for(int i = 0; i < 7; i++)
                {
                    turnoPorDia.Add(new TurnoMedDto()
                    {
                        Fecha = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.AddDays(i).ToString("dddd dd/MM")),
                        Turnos = new List<Turno>()
                    });
                }
                var hoy = DateTime.Today;
                foreach (var turno in doctor.Turnos)
                {
                    if(turno.Horario == hoy)
                    {
                        turnoPorDia[0].Turnos.Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(1))
                    {
                        turnoPorDia[1].Turnos.Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(2))
                    {
                        turnoPorDia[2].Turnos.Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(3))
                    {
                        turnoPorDia[3].Turnos.Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(4))
                    {
                        turnoPorDia[4].Turnos.Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(5))
                    {
                        turnoPorDia[5].Turnos.Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(6))
                    {
                        turnoPorDia[6].Turnos.Add(turno);
                    }
                    else
                    {
                        break;
                    }
                }
                repeaterTurnos.DataSource = turnoPorDia;
                repeaterTurnos.DataBind();
            }
        }

        protected void lbTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var turnoId = Convert.ToInt32(((ListBox)sender).SelectedValue);
            var turno = doctor.Turnos.Find(x => x.IdTurno == turnoId);
            txtCausas.InnerText = turno.Causas;
            txtObservaciones.InnerText = turno.Observaciones;
            txtDia.Text = turno.Horario.ToString("yyyy-MM-dd");
            txtDia_TextChanged(txtDia, new EventArgs());
        }

        protected void txtDia_TextChanged(object sender, EventArgs e)
        {
            var diaSeleccionado = DateTime.Parse(txtDia.Text);
            var dia = (Dia)Enum.Parse(typeof(Dia), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(diaSeleccionado.ToString("dddd")));
            var lstHorariosLaborales = DoctorNegocio.ObtenerHorariosDelDia(doctor, dia);
            var lstHorariosDisponibles = new List<int>();
            if (lstHorariosLaborales.Count > 0)
            {
                var lstHorariosOcupados = TurnoNegocio.ObtenerTurnosDeDoctor(doctor, diaSeleccionado, diaSeleccionado.AddDays(1)).Select(x => x.Horario.Hour);
                foreach(var horario in lstHorariosLaborales)
                {
                    for(int i = horario.HorarioEntrada; i <  horario.HorarioSalida; i++)
                    {
                        lstHorariosDisponibles.Add(i);
                    }
                }
                lstHorariosDisponibles.RemoveAll(x => lstHorariosOcupados.Contains(x));
                ddlHorario.Items.Clear();
                ddlHorario.DataSource = lstHorariosDisponibles;
                ddlHorario.DataBind();
            }
        }

        protected void btnReasignarTurno_Click(object sender, EventArgs e)
        {

        }

        protected void btnSemanaSiguiente_Click(object sender, EventArgs e)
        {

        }

        protected void btnSemanaAnterior_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarObservacion_Click(object sender, EventArgs e)
        {

        }
    }
}