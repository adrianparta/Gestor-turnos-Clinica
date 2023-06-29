using Dapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Negocio
{
    public class DoctorNegocio : AccesoDatos
    {
        public static Doctor ObtenerDoctor(int IdDoctor)
        {
            using (var db = Coneccion())
            {
                Doctor doctor;

                var sql= @"
                    SELECT D.IdDoctor
                        , D.IdUsuario
                        , U.Email
                        , U.TipoUsuario
                        , U.Nombre
                        , U.Apellido
                    FROM Doctores D
                    INNER JOIN Usuarios U ON D.IdUsuario = U.IdUsuario
                    WHERE IdDoctor = @IdDoctor
                ";

                doctor = db.QueryFirstOrDefault<Doctor>(sql, new { IdDoctor = IdDoctor });
                
                return ObtenerTurnosEspecialidadesHorarios(doctor);
            }
        }
        
        public static Doctor ObtenerDoctor(Usuario usuario)
        {
            using (var db = Coneccion())
            {
                var sql = @"
                    SELECT IdDoctor
                    FROM Doctores 
                    WHERE IdUsuario = @IdUsuario
                ";

                var doctor = db.QueryFirstOrDefault<Doctor>(sql, new { usuario.IdUsuario });
                doctor += usuario;
                
                return ObtenerTurnosEspecialidadesHorarios(doctor);
            }
        }
        public static Doctor ObtenerTurnosEspecialidadesHorarios(Doctor doctor)
        {
            using (var db = Coneccion())
            {
                doctor.Especialidades = ObtenerEspecialidades(doctor.IdDoctor);
                doctor.HorarioLaborales = ObtenerHorarios(doctor.IdDoctor);
                doctor.Turnos = TurnoNegocio.ObtenerTurnosDeDoctor(doctor);

                return doctor;
            }
        } 
        internal static List<Especialidad> ObtenerEspecialidades(int idDoctor)
        {
            using (var db = Coneccion())
            {
                var sqlEspecialidad = @"
                    SELECT E.IdEspecialidad
                        , E.Especialidad AS Nombre
                    FROM Especialidades E 
                    INNER JOIN EspecialidadesDoctores ED ON E.IdEspecialidad = ED.IdEspecialidad
                    WHERE IdDoctor = @IdDoctor
                ";

                return db.Query<Especialidad>(sqlEspecialidad, new { IdDoctor = idDoctor }).ToList();
            }
        }
        internal static List<HorarioLaboral> ObtenerHorarios(int idDoctor)
        {
            using (var db = Coneccion())
            {
                var sqlHorario = @"
                    SELECT IdDia AS Dia
                        , HorarioEntrada 
                        , HorarioSalida
                    FROM Horarios  
                    WHERE IdDoctor = @IdDoctor
                ";

                return db.Query<HorarioLaboral>(sqlHorario, new { IdDoctor = idDoctor }).ToList();
            }
        }
        public static bool AgregarEspecialidad(Doctor doctor, Especialidad especialidad)
        {
            var sql = @"
                IF NOT EXISTS ( 
                    SELECT IdEspecialidad 
                    FROM EspecialidadesDoctores
                    WHERE IdEspecialidad = @IdEspecialidad
                        AND IdDoctor = @IdDoctor
                    )
                BEGIN
                    INSERT INTO EspecialidadesDoctores 
                        (IdEspecialidad,IdDoctor)
                    VALUES
                        (@IdEspecialidad,@IdDoctor)

                    SELECT SCOPE_IDENTITY()
                END
                ELSE
                BEGIN
	                SELECT -1
                END
            ";

            using (var db = Coneccion())
            {
                return db.Execute(sql, new { especialidad.IdEspecialidad, doctor.IdDoctor }) > 0;
            }
        }
        public static bool AgregarHorario(Doctor doctor, HorarioLaboral horario)
        {
            var sql = @"
                IF NOT EXISTS ( 
                    SELECT IdHorario 
                    FROM Horarios
                    WHERE IdDoctor = @IdDoctor
                        AND IdDia = @Dia
                        AND (HorarioEntrada BETWEEN @HorarioEntrada AND @HorarioSalida 
                            OR HorarioSalida BETWEEN @HorarioEntrada AND @HorarioSalida)
                    )
                BEGIN
                    INSERT INTO Horarios 
                        (IdDoctor, IdDia, HorarioEntrada, HorarioSalida)
                    VALUES
                        (@IdDoctor, @Dia, @HorarioEntrada, @HorarioSalida)

                    SELECT SCOPE_IDENTITY()
                END
                ELSE
                BEGIN
	                SELECT -1
                END
            ";

            using (var db = Coneccion())
            {
                return db.Execute(sql, new { doctor.IdDoctor, horario.Dia, horario.HorarioEntrada, horario.HorarioSalida }) > 0;
            }
        }
        public static int AltaDoctor(Doctor doctor)
        {
            var sql = @"
                IF NOT EXISTS (
                    SELECT IdDoctor
                    FROM DOCTORES
                    WHERE IdUsuario = @IdUsuario
                    )
                BEGIN
                    INSERT INTO Doctores
                        (IdUsuario)
                    VALUES
                        (@IdUsuario)
                    SELECT SCOPE_IDENTITY()
                END
                ELSE
                BEGIN
	                SELECT -1
                END
            ";

            using ( var db = Coneccion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@IdUsuario", doctor.IdUsuario);

                var idDoctor = db.ExecuteScalar<int>(sql, parametros);
                
                if(idDoctor > 0)
                {
                    doctor.IdDoctor = idDoctor;
                    foreach(var especialidad in doctor.Especialidades)
                    {
                        AgregarEspecialidad(doctor, especialidad);
                    }
                    foreach(var horario in doctor.HorarioLaborales)
                    {
                        AgregarHorario(doctor, horario);
                    }
                }

                return idDoctor;
            }
        }
    }
}
