using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class Doctor : Usuario
    {
        public int IdDoctor { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public List<HorarioLaboral> HorarioLaborales { get; set; }
        public List<Turno> Turnos { get; set; }
        public List<Paciente> Pacientes {
            get
            {
                return Turnos.Select(t => t.Paciente).ToList();
            }
        }
        /// <summary>
        /// Se le agregan al Doctor sus datos de usuario.
        /// </summary>
        /// <param name="doctor"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static Doctor operator +(Doctor doctor, Usuario usuario)
        {
            doctor.IdUsuario = usuario.IdUsuario;
            doctor.Nombre = usuario.Nombre;
            doctor.Apellido = usuario.Apellido;
            doctor.Email = usuario.Email;
            doctor.TipoUsuario = usuario.TipoUsuario;

            return doctor;
        }
    }
}