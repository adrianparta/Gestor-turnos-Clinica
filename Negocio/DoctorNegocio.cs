using Dapper;
using Dominio;
using System.Collections.Generic;
using System.Linq;

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
                        , D.HorarioEntrada
                        , D.HorarioSalida
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
                
                return ObtenerTurnosYEspecialidades(doctor);
            }
        }
        public static Doctor ObtenerDoctor(Usuario usuario)
        {
            using (var db = Coneccion())
            {
                var sql = @"
                    SELECT IdDoctor
                        , HorarioEntrada
                        , HorarioSalida
                    FROM Doctores 
                    WHERE IdUsuario = @IdUsuario
                ";

                var docAux = db.QueryFirstOrDefault<Doctor>(sql, new { IdUsuario = usuario.IdUsuario });

                Doctor doctor = (Doctor)usuario;
                doctor.IdDoctor = docAux.IdDoctor;
                doctor.HorarioEntrada = docAux.HorarioEntrada;
                doctor.HorarioSalida = docAux.HorarioSalida;
                
                return ObtenerTurnosYEspecialidades(doctor);
            }
        }
        private static Doctor ObtenerTurnosYEspecialidades(Doctor doctor)
        {
            using (var db = Coneccion())
            {
                var sqlEspecialidad = @"
                    SELECT IdEspecialidad
                        , Especialidad AS Nombre
                    FROM EspecialidadesDoctores
                    WHERE IdDoctor = @IdDoctor
                ";

                doctor.Especialidades = db.Query<Especialidad>(sqlEspecialidad, new { IdDoctor = doctor.IdDoctor }).ToList();

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
                    INNER JOIN EspecialidadesDoctores ED ON ED.IdEspecialidad = E.IdEspecialidad
                    WHERE IdDoctor = @IdDoctor
                ";

                return db.Query<Especialidad>(sqlEspecialidad, new { IdDoctor = idDoctor }).ToList();
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
    }
}
