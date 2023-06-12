using System;

namespace Dominio
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public Especialidad Especialidad {  get; set; }
        public Doctor Doctor { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime Horario { get; set; }
        public string Causas { get; set; }
        public string Observaciones { get; set; }
        public Estado Estado { get; set; }
    }
}