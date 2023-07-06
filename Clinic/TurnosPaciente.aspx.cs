using Dominio;
using System;
using System.Collections.Generic;
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
                lbTurnos.DataSource = paciente.Turnos;
                lbTurnos.DataValueField = "IdTurno";
                lbTurnos.DataTextField = "TurnoTextoPaciente";
                lbTurnos.DataBind();
                lbTurnos.Rows = paciente.Turnos.Count;
            }
        }

        protected void lbTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}