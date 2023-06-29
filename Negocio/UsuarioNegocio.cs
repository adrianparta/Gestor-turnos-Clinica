using Dapper;
using Dominio;
using System.Collections.Generic;

namespace Negocio
{
    public class UsuarioNegocio : AccesoDatos
    {
        public static Usuario Login(string email, string contraseña)
        {
            var sqlUsuario = @"
                SELECT IdUsuario
                    , Email
                    , TipoUsuario
                    , Nombre
                    , Apellido
                FROM Usuarios
                WHERE Email = @Email
                    AND Contraseña = @Contraseña
            ";

            using (var db = Coneccion())
            {
                var usuario = db.QueryFirstOrDefault<Usuario>(sqlUsuario, new { Email = email, Contraseña = contraseña});

                return ObtenerDatosComplementarios(usuario);
            }
        }

        public static int AltaUsuario(Usuario usuario, string contraseña)
        {
            var sqlUsuario = @"
                IF NOT EXISTS ( 
                    SELECT IdUsuario 
                    FROM Usuarios
                    WHERE Email = @Email
                    )
                BEGIN
                    INSERT INTO Usuarios 
                        (Email, TipoUsuario, Nombre, Apellido, Contraseña)
                    VALUES
                        (@Email, @TipoUsuario, @Nombre, @Apellido, @Contraseña)

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
                parametros.Add("@Email", usuario.Email);
                parametros.Add("@TipoUsuario", usuario.TipoUsuario);
                parametros.Add("@Nombre", usuario.Nombre);
                parametros.Add("@Apellido", usuario.Apellido);
                parametros.Add("@Contraseña", contraseña);

                return db.ExecuteScalar<int>(sqlUsuario, parametros);
            }
        }
        public static Usuario ObtenerUsuario(int idUsuario)
        {
            var sqlUsuario = @"
                SELECT IdUsuario
                    , Email
                    , TipoUsuario
                    , Nombre
                    , Apellido
                FROM Usuarios
                WHERE IdUsuario = @IdUsuario
            ";

            using (var db = Coneccion())
            {
                var usuario = db.QueryFirstOrDefault<Usuario>(sqlUsuario, new { IdUsuario = idUsuario });

                return ObtenerDatosComplementarios(usuario);
            }
        }
        public static List<Usuario> ListarUsuario( int tipoUsuario)
        {
            using (var db = Coneccion())
            {
                List<Usuario> UsuarioLista;

                var sql = @"
                    SELECT U.IdUsuario
                        , U.Email
                        , U.Nombre
                        , U.Apellido
                    FROM Usuarios u 
                    Where u.TipoUsuario = @TipoUsuario
                "
                ;

                UsuarioLista = db.Query<Usuario>(sql, new { TipoUsuario = tipoUsuario }).AsList();
                return UsuarioLista;
            }
        }

        public static bool ModificarUsuario(Usuario usuario)
        {
            var sqlUsuario = @"
                UPDATE Usuarios SET
                    Email = @Email
                    , TipoUsuario = @TipoUsuario
                    , Nombre = @Nombre
                    , Apellido = @Apellido
                WHERE IdUsuario = @IdUsuario
            ";

            using (var db = Coneccion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Email", usuario.Email);
                parametros.Add("@TipoUsuario", usuario.TipoUsuario);
                parametros.Add("@Nombre", usuario.Nombre);
                parametros.Add("@Apellido", usuario.Apellido);
                parametros.Add("@IdUsuario", usuario.IdUsuario);

                return db.Execute(sqlUsuario, parametros) == 1;
            }
        }

        public static bool BorrarUsuario(int idUsuario)
        {
            var sqlUsuario = @"
                DELETE FROM Usuarios 
                WHERE IdUsuario = @IdUsuario
            ";

            using (var db = Coneccion())
            {
                return db.Execute(sqlUsuario, new { IdUsuario = idUsuario }) == 1;
            }
        }

        private static Usuario ObtenerDatosComplementarios(Usuario usuario)
        {
            if (!(usuario is null))
            {
                if (usuario.TipoUsuario == TipoUsuario.Doctor)
                {
                    return DoctorNegocio.ObtenerDoctor(usuario);
                }
                if (usuario.TipoUsuario == TipoUsuario.Paciente)
                {
                    return PacienteNegocio.ObtenerPaciente(usuario);
                }
            }
            return usuario;
        }
    }
}
