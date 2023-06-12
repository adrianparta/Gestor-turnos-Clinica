using Dapper;
using Dominio;

namespace Negocio
{
    public class PacienteNegocio : AccesoDatos
    {
        public static Paciente ObtenerPaciente(int idPaciente)
        {
            using (var db = Coneccion())
            {
                var sql = @"                       
                    SELECT IdPaciente
                        , Nombre 
                        , Apellido 
                        , Dni 
                        , Direccion 
                        , FechaNacimiento 
                        , Sexo 
                        , ObraSocial                                
                        , Email 
                    FROM Pacientes
                    WHERE IdPaciente = @IdPaciente
                    ";

                var paciente = db.QueryFirstOrDefault<Paciente>(sql);
                if (paciente != null)
                {
                    paciente.Turnos = TurnoNegocio.ObtenerTurnosDePaciente(paciente);
                }
                return paciente;
            }
        }
    }
}
