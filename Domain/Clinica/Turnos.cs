using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica
{
    public class Turnos
    {
        public int id { get; set; }
        public string especialidad {  get; set; }
        public Doctor doctor { get; set; }
        public Pacient paciente { get; set; }
        public DateTime horario { get; set; }
        public string causas { get; set; }
        public enum estado 
        {
            Nuevo, Reprogramado, Cancelado, NoAsistio, Cerrado
        }


    }
}