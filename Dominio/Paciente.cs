using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Paciente 
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Sexo { get; set; }
        public string ObraSocial { get; set;}
        public List<Turno> Turnos { get; set; }
        public string Email { get; set; }
    }
}