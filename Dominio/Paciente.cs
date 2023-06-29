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

        /// <summary>
        /// Se le agrega al paciente sus datos de usuario.
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static Paciente operator + (Paciente paciente, Usuario usuario)
        {
            paciente.IdUsuario = usuario.IdUsuario;
            paciente.Nombre = usuario.Nombre;
            paciente.Apellido = usuario.Apellido;
            paciente.Email = usuario.Email;
            paciente.TipoUsuario = usuario.TipoUsuario;

            return paciente;
        }
    }
}