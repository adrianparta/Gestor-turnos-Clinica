using Dapper;
using Dominio;
using System.Collections.Generic;

namespace Negocio
{
    public class TipoUsuarioNegocio : AccesoDatos
    {
        public static IEnumerable<TipoUsuario> ObtenerTiposUsuarios()
        {
            using(var db = Coneccion())
            {
                var sql = @"
                    SELECT IdTipoUsuario
                    FROM TipoUsuario";

                return db.Query<TipoUsuario>(sql);
            }
        }
    }
}
