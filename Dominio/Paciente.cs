using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Paciente : Usuario
    {
        public int IdPaciente { get; set; }
        public int Dni { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Sexo Sexo { get; set; }
        public string ObraSocial { get; set;}
        public List<Turno> Turnos { get; set; }
    }
}