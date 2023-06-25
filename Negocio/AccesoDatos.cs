using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private static string stringConeccion = @"Server=tcp:lucasanche-server.database.windows.net,1433;Initial Catalog=clinica;Persist Security Info=False;User ID=ls-admin;Password=Grupo13prog;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static SqlConnection Coneccion ()
        {
            return new SqlConnection(stringConeccion);
        }
    }
}
