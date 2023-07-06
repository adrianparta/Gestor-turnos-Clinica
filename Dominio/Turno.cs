using System;
using System.Globalization;

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
        public string TurnoTextoPaciente { get => $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Horario.ToString("dddd dd/MM"))} a las {Horario:HH:mm}hs - {Especialidad}"; }
        public override string ToString()
        {
            return $"{Horario:t} {Paciente}";
        }
        public static bool operator ==(Turno a, Turno b)
        {
            return a.IdTurno == b.IdTurno;
        }
        public static bool operator !=(Turno a, Turno b)
        {
            return !(a == b);
        }
    }
}