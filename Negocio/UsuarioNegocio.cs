using Dapper;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio : AccesoDatos
    {
        public static Usuario Login(string email, string contraseña)
        {
            var sqlUsuario = @"
                SELECT Id
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
                
                if (!(usuario is null))
                {
                    if(usuario.TipoUsuario == TipoUsuario.Doctor)
                    {
                        return DoctorNegocio.ObtenerDoctor(usuario);
                    }
                }

                return usuario;
            }
        }
    }
}
