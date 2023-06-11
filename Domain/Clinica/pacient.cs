using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica
{
    public class Pacient
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int documento { get; set; }
        public string direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Sexo { get; set; }
        public string ObraSocial { get; set;}
        public List<DateTime> turnos { get; set; }
        public string correo { get; set; }


    }
}