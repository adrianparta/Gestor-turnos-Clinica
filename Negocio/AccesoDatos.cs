using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private static string stringConeccion = @"Server=.;Database=ClinicaDB;Trusted_Connection=True;";
        public static SqlConnection Coneccion ()
        {
            return new SqlConnection(stringConeccion);
        }
    }
}
