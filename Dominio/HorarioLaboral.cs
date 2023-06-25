using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HorarioLaboral
    {
        public Dia Dia { get; set; }
        public int HorarioEntrada { get; set; }
        public int HorarioSalida { get; set;}
        public static List<HorarioLaboral> HorarioLaboralAux = new List<HorarioLaboral>();

        public override string ToString()
        {
            return $"{Dia}: Desde: {HorarioEntrada}:00 - Hasta: {HorarioSalida}:00";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            HorarioLaboral aux = obj as HorarioLaboral;

            return this.Dia == aux.Dia && this.HorarioEntrada == aux.HorarioEntrada && this.HorarioSalida == aux.HorarioSalida;
        }
    }
}
