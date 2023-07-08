using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class TurnosPaciente : Page
    {
        Paciente paciente;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Usuario)Session["Usuario"]).TipoUsuario != TipoUsuario.Paciente)
            {
                Response.Redirect("Default.aspx");
            }
            paciente = (Paciente)Session["Usuario"];
            if(!IsPostBack)
            {
                DataTable dtTurnosViejos = new DataTable();
                dtTurnosViejos.Columns.Add("IdTurno");
                dtTurnosViejos.Columns.Add("Dia");
                dtTurnosViejos.Columns.Add("Horario");
                dtTurnosViejos.Columns.Add("Especialidad");
                dtTurnosViejos.Columns.Add("Estado");
                foreach (var turno in paciente.Turnos)
                {
                    if(turno.Horario < DateTime.Now)
                    {
                        if(turno.Estado == Estado.Nuevo || turno.Estado == Estado.Reprogramado)
                        {
                            turno.Estado = Estado.Cerrado;
                            TurnoNegocio.ActualizarEstado(turno.IdTurno, Estado.Cerrado);
                            var index = paciente.Turnos.FindIndex(x => x.IdTurno == turno.IdTurno);
                            paciente.Turnos[index].Estado = Estado.Cerrado;
                        }
                        dtTurnosViejos.Rows.Add(turno.IdTurno
                            , CultureInfo.CurrentCulture.TextInfo.ToTitleCase(turno.Horario.ToString("dddd dd/MM"))
                            , $"{turno.Horario:HH:mm}hs"
                            , turno.Especialidad
                            , turno.Estado);
                    }
                }
                dgvTurnosViejos.DataSource = dtTurnosViejos;
                dgvTurnosViejos.DataBind();
                btnCancelarTurno.Enabled = false;
                ActualizarTabla();
            }
        }

        protected void dgvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Elegir";
                e.Row.Cells[1].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void btnCancelarTurno_Click(object sender, EventArgs e)
        {
            var idTurno = Convert.ToInt32(Session["IdTurnoSeleccionadoPaciente"]);
            if(idTurno > 0)
            {
                if(TurnoNegocio.ActualizarEstado(idTurno, Estado.Cancelado))
                {
                    var index = paciente.Turnos.FindIndex(x => x.IdTurno == idTurno);
                    paciente.Turnos[index].Estado = Estado.Cancelado;
                    ActualizarTabla();
                    btnCancelarTurno.Enabled = false;
                }
            }
        }

        private void ActualizarTabla()
        {
            DataTable dtTurnos = new DataTable();
            dtTurnos.Columns.Add("IdTurno");
            dtTurnos.Columns.Add("Dia");
            dtTurnos.Columns.Add("Horario");
            dtTurnos.Columns.Add("Especialidad");
            dtTurnos.Columns.Add("Estado");

            foreach (var turno in paciente.Turnos)
            {
                if(turno.Horario > DateTime.Now)
                {
                    dtTurnos.Rows.Add(turno.IdTurno
                        , CultureInfo.CurrentCulture.TextInfo.ToTitleCase(turno.Horario.ToString("dddd dd/MM"))
                        , $"{turno.Horario:HH:mm}hs"
                        , turno.Especialidad
                        , turno.Estado);
                }
            }

            dgvTurnos.DataSource = dtTurnos;
            dgvTurnos.DataBind();
        }

        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridView dgv = sender as GridView;
                int indexRow = Convert.ToInt32(e.CommandArgument);
                var idTurno = Convert.ToInt32(dgv.Rows[indexRow].Cells[1].Text);
                Session.Add("IdTurnoSeleccionadoPaciente", idTurno);
                var turno = paciente.Turnos.Find(x => x.IdTurno == idTurno);
                txtCausas.InnerText = turno.Causas;
                txtObservaciones.InnerText = turno.Observaciones;
                if (turno.Estado == Estado.Reprogramado || turno.Estado == Estado.Nuevo)
                {
                    btnCancelarTurno.Enabled = true;
                }
                else
                {
                    btnCancelarTurno.Enabled = false;
                }
            }
        }
    }
}