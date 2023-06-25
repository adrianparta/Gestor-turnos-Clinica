﻿using System;
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

        public override string ToString()
        {
            return $"{Dia} Desde {HorarioEntrada}:00 hasta {HorarioSalida}:00";
        }
    }
}
