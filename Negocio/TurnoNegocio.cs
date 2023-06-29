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
                    , T.Horario 
                    , T.Causas 
                    , T.Observaciones 
                    , T.Estado 
                    , T.IdEspecialidad 
                    , E.Especialidad AS Nombre
                    , T.IdPaciente 
                    , U.Nombre  
                    , U.Apellido  
                    , U.Email
                    , U.TipoUsuario
                    , P.Dni  
                    , P.Direccion  
                    , P.FechaNacimiento  
                    , P.Sexo  
                    , P.ObraSocial 
                FROM Turnos T
                INNER JOIN Pacientes P ON P.IdPaciente = T.IdPaciente
                INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario
                INNER JOIN Especialidades E ON T.IdEspecialidad = E.IdEspecialidad
                WHERE IdDoctor = @IdDoctor
            ";

            using(var db = Coneccion())
            {
                return db.Query<Turno,Especialidad,Paciente,Turno>(sql, (turno, especialidad, paciente) =>
                {
                    turno.Especialidad = especialidad;
                    turno.Doctor = doctor;
                    turno.Paciente = paciente;
                    return turno;
                }, new { doctor.IdDoctor }, splitOn: "IdEspecialidad,IdPaciente").ToList();
            }
        }
        public static List<Turno> ObtenerTurnosDePaciente(Paciente paciente)
        {
            var sql = @"
                SELECT  T.IdTurno
                    , T.Horario 
                    , T.Causas 
                    , T.Observaciones 
                    , T.Estado 
                    , T.IdEspecialidad 
                    , E.Especialidad AS Nombre
                    , T.IdDoctor
                    , D.IdUsuario
                    , U.Email
                    , U.TipoUsuario
                    , U.Nombre
                    , U.Apellido
                FROM Turnos T
                INNER JOIN Doctores D ON T.IdDoctor = D.IdDoctor
                INNER JOIN Usuarios U ON D.IdUsuario = U.IdUsuario
                INNER JOIN Especialidades E ON T.IdEspecialidad = E.IdEspecialidad
                WHERE IdPaciente = @IdPaciente
            ";

            using (var db = Coneccion())
            {
                return db.Query<Turno, Especialidad, Doctor, Turno>(sql, (turno, especialidad, doctor) =>
                {
                    turno.Especialidad = especialidad;
                    turno.Doctor = DoctorNegocio.ObtenerTurnosEspecialidadesHorarios(doctor);
                    turno.Paciente = paciente;
                    return turno;
                }, new { paciente.IdPaciente }, splitOn: "IdEspecialidad,IdDoctor").ToList();
            }
        }
    }
}
