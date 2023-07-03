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
                if (Session["DiferenciaConHoy"] is null)
                {
                    Session.Add("DiferenciaConHoy", 0);
                }

                if (Session["TurnoElegido"] is null)
                {
                    Session.Add("TurnoElegido", -1);
                }
                ObtenerTurnosPorFecha();
            }
        }

        protected void lbTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var turnoId = Convert.ToInt32(((ListBox)sender).SelectedValue);
            Session["TurnoElegido"] = turnoId;
            var turno = doctor.Turnos.Find(x => x.IdTurno == turnoId);
            ((ListBox)sender).SelectedItem.Selected = false;
            txtPacienteSeleccionado.Text = $"Turno de: {turno.Paciente} para la especialidad: {turno.Especialidad}";
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
            var lstHorariosDisponibles = new List<HorarioLaboral>();
            if (lstHorariosLaborales.Count > 0)
            {
                var lstHorariosOcupados = TurnoNegocio.ObtenerTurnosDeDoctor(doctor, diaSeleccionado, diaSeleccionado.AddDays(1)).Select(x => x.Horario.Hour);
                foreach(var horario in lstHorariosLaborales)
                {
                    for(int i = horario.HorarioEntrada; i <  horario.HorarioSalida; i++)
                    {
                        lstHorariosDisponibles.Add(new HorarioLaboral(){ HorarioEntrada = i });
                    }
                }
                lstHorariosDisponibles.RemoveAll(x => lstHorariosOcupados.Contains(x.HorarioEntrada));
                ddlHorario.Items.Clear();
                ddlHorario.DataSource = lstHorariosDisponibles;
                ddlHorario.DataTextField = "HorarioEntradaString";
                ddlHorario.DataValueField = "HorarioEntrada";
                ddlHorario.DataBind();
            }
        }

        private void ObtenerTurnosPorFecha()
        {
            var turnoPorDia = new List<TurnoMedDto>();
            var diaReferencia = DateTime.Today.AddDays(Convert.ToInt32(Session["DiferenciaConHoy"]));
            for (int i = 0; i < 7; i++)
            {
                turnoPorDia.Add(new TurnoMedDto()
                {
                    Fecha = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(diaReferencia.AddDays(i).ToString("dddd dd/MM")),
                    Turnos = new List<Turno>()
                });
            }

            var turnos = TurnoNegocio.ObtenerTurnosDeDoctor(doctor, diaReferencia, diaReferencia.AddDays(7));
            doctor.Turnos = turnos;
            foreach (var turno in turnos)
            {
                if (turno.Horario.Date == diaReferencia)
                {
                    turnoPorDia[0].Turnos.Add(turno);
                }
                else if (turno.Horario.Date == diaReferencia.AddDays(1))
                {
                    turnoPorDia[1].Turnos.Add(turno);
                }
                else if (turno.Horario.Date == diaReferencia.AddDays(2))
                {
                    turnoPorDia[2].Turnos.Add(turno);
                }
                else if (turno.Horario.Date == diaReferencia.AddDays(3))
                {
                    turnoPorDia[3].Turnos.Add(turno);
                }
                else if (turno.Horario.Date == diaReferencia.AddDays(4))
                {
                    turnoPorDia[4].Turnos.Add(turno);
                }
                else if (turno.Horario.Date == diaReferencia.AddDays(5))
                {
                    turnoPorDia[5].Turnos.Add(turno);
                }
                else if (turno.Horario.Date == diaReferencia.AddDays(6))
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
        protected void btnSemanaSiguiente_Click(object sender, EventArgs e)
        {
            Session["DiferenciaConHoy"] = (int)Session["DiferenciaConHoy"] + 7;
            LimpiarCamposPaciente();
            ObtenerTurnosPorFecha();
        }
        protected void btnSemanaAnterior_Click(object sender, EventArgs e)
        {
            Session["DiferenciaConHoy"] = (int)Session["DiferenciaConHoy"] - 7;
            LimpiarCamposPaciente();
            ObtenerTurnosPorFecha();
        }
        protected void btnHoy_OnClick(object sender, EventArgs e)
        {
            Session["DiferenciaConHoy"] = 0;
            LimpiarCamposPaciente();
            ObtenerTurnosPorFecha();
        }

        private void LimpiarCamposPaciente()
        {
            txtCausas.InnerText = string.Empty;
            txtObservaciones.InnerText = string.Empty;
            txtDia.Text = string.Empty;
            ddlHorario.Items.Clear();
            txtPacienteSeleccionado.Text = "Seleccione un paciente por favor";
        }

        protected void btnGuardarObservacion_Click(object sender, EventArgs e)
        {
            TurnoNegocio.ActualizarObservaciones((int)Session["TurnoElegido"], txtObservaciones.InnerText);
            ObtenerTurnosPorFecha();
        }
        protected void btnReasignarTurno_Click(object sender, EventArgs e)
        {
            var fecha = DateTime.Parse(txtDia.Text);
            fecha = fecha.AddHours(Convert.ToInt32(ddlHorario.SelectedValue));
            TurnoNegocio.ActualizarFecha((int)Session["TurnoElegido"], fecha);
            ObtenerTurnosPorFecha();
        }
    }
}