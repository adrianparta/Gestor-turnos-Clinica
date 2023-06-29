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
                    SELECT P.IdPaciente
                        , P.Dni 
                        , P.Direccion 
                        , P.FechaNacimiento 
                        , P.Sexo 
                        , P.ObraSocial                                
                        , U.Email
                        , U.TipoUsuario
                        , U.Nombre
                        , U.Apellido
                    FROM Pacientes P
                    INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario
                    WHERE IdPaciente = @IdPaciente
                    ";

                var paciente = db.QueryFirstOrDefault<Paciente>(sql, new { IdPaciente = idPaciente });
                if (paciente != null)
                {
                    paciente.Turnos = TurnoNegocio.ObtenerTurnosDePaciente(paciente);
                }
                return paciente;
            }
        }
        public static Paciente ObtenerPaciente(Usuario usuario)
        {
            using (var db = Coneccion())
            {
                var sql = @"                       
                    SELECT P.IdPaciente
                        , P.Dni 
                        , P.Direccion 
                        , P.FechaNacimiento 
                        , P.Sexo 
                        , P.ObraSocial                                
                    FROM Pacientes P
                    WHERE IdUsuario = @IdUsuario
                    ";

                var paciente = db.QueryFirstOrDefault<Paciente>(sql, new { usuario.IdUsuario });
                if (paciente != null)
                {
                    paciente += usuario;
                    paciente.Turnos = TurnoNegocio.ObtenerTurnosDePaciente(paciente);
                    return paciente;
                }
                return null;
            }
        }
        public static int AltaPaciente(Paciente paciente)
        {
            var sql = @"
                IF NOT EXISTS ( 
                    SELECT IdPaciente 
                    FROM Pacientes
                    WHERE IdUsuario = @IdUsuario
                    )
                BEGIN
                    INSERT INTO Pacientes 
                        (Dni, Direccion, FechaNacimiento, Sexo, ObraSocial, IdUsuario)
                    VALUES
                        (@Dni, @Direccion, @FechaNacimiento, @Sexo, @ObraSocial, @IdUsuario)

                    SELECT SCOPE_IDENTITY()
                END
                ELSE
                BEGIN
	                SELECT -1
                END
            ";

            using (var db = Coneccion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Dni", paciente.Dni);
                parametros.Add("@Direccion", paciente.Direccion);
                parametros.Add("@FechaNacimiento", paciente.FechaNacimiento);
                parametros.Add("@Sexo", paciente.Sexo);
                parametros.Add("@ObraSocial", paciente.ObraSocial);
                parametros.Add("@IdUsuario", paciente.IdUsuario);

                return db.ExecuteScalar<int>(sql, parametros);
            }
        }
        public static bool ModificarPaciente(Paciente paciente)
        {
            var sql = @"
                UPDATE Usuarios SET
                    Dni = @Dni
                    , Direccion = @Direccion
                    , FechaNacimiento = @FechaNacimiento
                    , Sexo = @Sexo
                    , ObraSocial = @ObraSocial
                WHERE IdPaciente = @IdPaciente
            ";

            using (var db = Coneccion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@IdPaciente", paciente.IdPaciente);
                parametros.Add("@Dni", paciente.Dni);
                parametros.Add("@Direccion", paciente.Direccion);
                parametros.Add("@FechaNacimiento", paciente.FechaNacimiento);
                parametros.Add("@Sexo", paciente.Sexo);
                parametros.Add("@ObraSocial", paciente.ObraSocial);

                return db.Execute(sql, parametros) == 1;
            }
        }
        public static bool BorrarPaciente(int idPaciente)
        {
            var sql = @"
                DELETE FROM Pacientes 
                WHERE IdPaciente = @IdPaciente
            ";

            using (var db = Coneccion())
            {
                return db.Execute(sql, new { IdPaciente = idPaciente }) == 1;
            }
        }
    }
}
