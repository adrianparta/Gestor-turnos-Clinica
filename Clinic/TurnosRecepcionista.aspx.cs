using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
	public partial class TurnosRecepcionista : System.Web.UI.Page
	{
        public List<HorarioLaboral> horarioLaboral;
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

            }
        }

        protected void DropDownListEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListMedicos.DataSource = DoctorNegocio.ObtenerDoctores(DropDownListEspecialidad.SelectedIndex);
            DropDownListMedicos.DataTextField = "NombreCompleto";
            DropDownListMedicos.DataValueField = "IdDoctor";
            DropDownListMedicos.DataBind();

            
            
        }

        protected void DropDownListMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            horarioLaboral = DoctorNegocio.ObtenerTurnosEspecialidadesHorarios((DoctorNegocio.ObtenerDoctor(int.Parse(DropDownListMedicos.SelectedValue)))).HorarioLaborales;
        }

        protected void DropDownListPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}