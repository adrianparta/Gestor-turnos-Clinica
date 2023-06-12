using Dapper;
using Dominio;
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
                    FROM EspecialidadesDoctores
                    WHERE IdDoctor = @IdDoctor
                ";

                doctor.Especialidades = db.Query<Especialidad>(sqlEspecialidad, new { IdDoctor = doctor.IdDoctor }).ToList();

                doctor.Turnos = TurnoNegocio.ObtenerTurnosDeDoctor(doctor);

                return doctor;
            }
        }
    }
}
