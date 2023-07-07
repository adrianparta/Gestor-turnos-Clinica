using Dapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
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
                    AND T.Horario >= @DiaActual
            ";

            using(var db = Coneccion())
            {
                return db.Query<Turno,Especialidad,Paciente,Turno>(sql, (turno, especialidad, paciente) =>
                {
                    turno.Especialidad = especialidad;
                    turno.Doctor = doctor;
                    turno.Paciente = paciente;
                    return turno;
                }, new { doctor.IdDoctor, DiaActual = DateTime.Today }, splitOn: "IdEspecialidad,IdPaciente").ToList();
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
                ORDER BY T.Horario
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

        public static List<Turno> ObtenerTurnosDeDoctor(Doctor doctor, DateTime fechaInicio, DateTime fechaFin)
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
                    AND T.Horario >= @FechaInicio
                    AND T.Horario < @FechaFin
                ORDER BY T.Horario
            ";

            using (var db = Coneccion())
            {
                return db.Query<Turno, Especialidad, Paciente, Turno>(sql, (turno, especialidad, paciente) =>
                {
                    turno.Especialidad = especialidad;
                    turno.Doctor = doctor;
                    turno.Paciente = paciente;
                    return turno;
                }, new { doctor.IdDoctor, FechaInicio = fechaInicio.Date, FechaFin = fechaFin.Date }, splitOn: "IdEspecialidad,IdPaciente").ToList();
            }
        }

        public static bool ActualizarObservaciones(int turnoId, string observaciones)
        {
            var sql = @"
                UPDATE Turnos SET
                    Observaciones = @Observaciones
                WHERE IdTurno = @TurnoId
                ";
            using (var db = Coneccion())
            {
                return db.Execute(sql, new{ TurnoId = turnoId, Observaciones = observaciones }, commandType:CommandType.Text) == 1;
            }
        }
        public static bool ActualizarFecha(int turnoId, DateTime fecha)
        {
            var sql = @"
                UPDATE Turnos SET
                    Horario = @Fecha
                WHERE IdTurno = @TurnoId
                ";
            using (var db = Coneccion())
            {
                return db.Execute(sql, new { TurnoId = turnoId, Fecha = fecha }, commandType: CommandType.Text) == 1;
            }
        }
        public static bool ActualizarEstado(int turnoId, Estado estado)
        {
            var sql = @"
                UPDATE Turnos SET
                    Estado = @Estado
                WHERE IdTurno = @TurnoId
                ";
            using (var db = Coneccion())
            {
                return db.Execute(sql, new { TurnoId = turnoId, Estado = estado }, commandType: CommandType.Text) == 1;
            }
        }
        public static int AgregarTurno(Turno turno)
        {
            var sql = @"
                INSERT INTO Turnos
                    (IdEspecialidad,IdDoctor,IdPaciente,Horario,Causas,Estado)
                VALUES
                    (@IdEspecialidad,@IdDoctor,@IdPaciente,@Horario,@Causas,1)
            ";

            using (var db = Coneccion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@IdEspecialidad", turno.Especialidad.IdEspecialidad);
                parametros.Add("@IdDoctor", turno.Doctor.IdDoctor);
                parametros.Add("@IdPaciente", turno.Paciente.IdPaciente);
                parametros.Add("@Horario", turno.Horario);
                parametros.Add("@Causas", turno.Causas);


                return db.ExecuteScalar<int>(sql, parametros);

            }
        }
    }
}
