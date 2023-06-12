using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class Doctor : Usuario
    {
        public int IdDoctor { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public int HorarioEntrada { get; set; }
        public int HorarioSalida { get; set; }
        public List<Turno> Turnos { get; set; }
        public List<Paciente> Pacientes {
            get
            {
                return Turnos.Select(t => t.Paciente).ToList();
            }
        }
    }
}