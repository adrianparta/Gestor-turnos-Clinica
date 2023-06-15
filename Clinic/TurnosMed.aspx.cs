using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class TurnosMed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbTurnos.Rows = 10;

            ddlHorario.Items.Add("13:00");
            ddlHorario.Items.Add("15:00");
            ddlHorario.Items.Add("20:00");
            ddlFecha.Items.Add("Junio - Semana 1");
            ddlFecha.Items.Add("Junio - Semana 2");
            ddlFecha.Items.Add("Junio - Semana 3");
            ddlFecha.Items.Add("Junio - Semana 4");
        }
    }
}