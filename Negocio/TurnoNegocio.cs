using Dapper;
using Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class TurnoNegocio : AccesoDatos
    {
        public static List<Turno> ObtenerTurnosDeDoctor(Doctor doctor)
        {
            var sql = @"
                SELECT  T.IdTurno
                    , T.Especialidad 
                    , T.Horario 
                    , T.Causas 
                    , T.Observaciones 
                    , T.Estado 
                    , T.IdPaciente 
                    , P.Nombre  
                    , P.Apellido  
                    , P.Dni  
                    , P.Direccion  
                    , P.FechaNacimiento  
                    , P.Sexo  
                    , P.ObraSocial 
                    , P.Email
                FROM Turnos T
                INNER JOIN Pacientes P ON P.IdPaciente = T.IdPaciente
                WHERE IdDoctor = @IdDoctor
            ";

            using(var db = Coneccion())
            {
                return db.Query<Turno,Paciente,Turno>(sql, (turno, paciente) =>
                {
                    turno.Doctor = doctor;
                    turno.Paciente = paciente;
                    return turno;
                }, new { IdDoctor = doctor.IdDoctor }, splitOn: "IdPaciente").ToList();
            }
        }
        public static List<Turno> ObtenerTurnosDePaciente(Paciente paciente)
        {
            var sql = @"
                SELECT  T.IdTurno
                    , T.Especialidad 
                    , T.Horario 
                    , T.Causas 
                    , T.Observaciones 
                    , T.Estado 
                    , T.IdDoctor
                    , D.HorarioEntrada
                    , D.HorarioSalida
                    , D.IdUsuario
                    , U.Email
                    , U.TipoUsuario
                    , U.Nombre
                    , U.Apellido
                FROM Turnos T
                INNER JOIN Doctores D ON T.IdDoctor = D.IdDoctor
                INNER JOIN Usuarios U ON D.IdUsuario = U.IdUsuario
                WHERE IdPaciente = @IdPaciente
            ";

            using (var db = Coneccion())
            {
                return db.Query<Turno, Doctor, Turno>(sql, (turno, doctor) =>
                {
                    turno.Doctor = doctor;
                    turno.Paciente = paciente;
                    return turno;
                }, new { IdPaciente = paciente.IdPaciente }, splitOn: "IdDoctor").ToList();
            }
        }
    }
}
