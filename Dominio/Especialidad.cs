using System.Collections.Generic;

namespace Dominio
{
    public class Especialidad
    {
        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; }
        public static List<Especialidad> EspecialidadAux = new List<Especialidad>();
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Especialidad aux = obj as Especialidad;

            return this.IdEspecialidad == aux.IdEspecialidad;
        }
    }
}
