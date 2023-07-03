using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TurnoMedDto
    {
        public string Fecha { get; set; }
        public List<Turno> Turnos { get; set; }
    }
}
