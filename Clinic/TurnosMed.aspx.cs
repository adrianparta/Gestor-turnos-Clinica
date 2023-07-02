using Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                List<List<Turno>> turnoPorDia = new List<List<Turno>>();
                for(int i = 0; i < 7; i++)
                {
                    turnoPorDia.Add(new List<Turno>());
                }
                var hoy = DateTime.Today;
                foreach (var turno in doctor.Turnos)
                {
                    if(turno.Horario == hoy)
                    {
                        turnoPorDia[0].Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(1))
                    {
                        turnoPorDia[1].Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(2))
                    {
                        turnoPorDia[2].Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(3))
                    {
                        turnoPorDia[3].Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(4))
                    {
                        turnoPorDia[4].Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(5))
                    {
                        turnoPorDia[5].Add(turno);
                    }
                    else if (turno.Horario.Date == hoy.AddDays(6))
                    {
                        turnoPorDia[6].Add(turno);
                    }
                    else
                    {
                        break;
                    }
                }
                lblTurnos0.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.ToString("dddd dd/MM"));
                lblTurnos1.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.AddDays(1).ToString("dddd dd/MM"));
                lblTurnos2.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.AddDays(2).ToString("dddd dd/MM"));
                lblTurnos3.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.AddDays(3).ToString("dddd dd/MM"));
                lblTurnos4.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.AddDays(4).ToString("dddd dd/MM"));
                lblTurnos5.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.AddDays(5).ToString("dddd dd/MM"));
                lblTurnos6.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Today.AddDays(6).ToString("dddd dd/MM"));

                lbTurnos0.DataSource = turnoPorDia[0];
                lbTurnos0.DataBind();
                lbTurnos1.DataSource = turnoPorDia[1];
                lbTurnos1.DataBind();
                lbTurnos2.DataSource = turnoPorDia[2];
                lbTurnos2.DataBind();
                lbTurnos3.DataSource = turnoPorDia[3];
                lbTurnos3.DataBind();
                lbTurnos4.DataSource = turnoPorDia[4];
                lbTurnos4.DataBind();
                lbTurnos5.DataSource = turnoPorDia[5];
                lbTurnos5.DataBind();
                lbTurnos6.DataSource = turnoPorDia[6];
                lbTurnos6.DataBind();
            }
        }
    }
}