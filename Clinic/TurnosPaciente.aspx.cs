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

        protected void dgvTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idTurno = Convert.ToInt32(dgvTurnos.SelectedRow.DataItem);
            var turno = paciente.Turnos.Find(x => x.IdTurno == idTurno);
            txtCausas.InnerText = turno.Causas;
            txtObservaciones.InnerText = turno.Observaciones;
            if(turno.Estado == Estado.Cancelado)
            {
                btnCancelarTurno.Enabled = false;
            }
            else
            {
                btnCancelarTurno.Enabled = true;
            }
        }

        protected void btnCancelarTurno_Click(object sender, EventArgs e)
        {
            var idTurno = Convert.ToInt32(dgvTurnos.SelectedRow.DataItem);
            if(idTurno > 0)
            {
                if(TurnoNegocio.ActualizarEstado(idTurno, Estado.Cancelado))
                {
                    ActualizarTabla();
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
                dtTurnos.Rows.Add(turno.IdTurno
                    , CultureInfo.CurrentCulture.TextInfo.ToTitleCase(turno.Horario.ToString("dddd dd/MM"))
                    , $"{turno.Horario:HH:mm}hs"
                    , turno.Especialidad
                    , turno.Estado);
            }

            dgvTurnos.DataSource = dtTurnos;
            dgvTurnos.DataBind();
        }
    }
}