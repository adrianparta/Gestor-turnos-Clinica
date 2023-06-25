using Dapper;
using Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class EspecialidadNegocio : AccesoDatos
    {
        public static List<Especialidad> ObtenerEspecialidades()
        {
            var sql = @"
                SELECT IdEspecialidad
                    , Especialidad AS Nombre
                FROM Especialidades
                ORDER BY Especialidad
            ";

            using(var db = Coneccion())
            {
                return db.Query<Especialidad>(sql).ToList();
            }
        }
        public static Especialidad ObtenerEspecialidad(int idEspecialidad)
        {
            var sql = @"
                SELECT IdEspecialidad
                    , Especialidad AS Nombre
                FROM Especialidades
                WHERE IdEspecialidad = @IdEspecialidad
            ";

            using (var db = Coneccion())
            {
                return db.QueryFirstOrDefault<Especialidad>(sql, new { IdEspecialidad = idEspecialidad });
            }
        }
        public static int NuevaEspecialidad(Especialidad especialidad)
        {
            var sql = @"
                IF NOT EXISTS ( 
                    SELECT Especialidad 
                    FROM Especialidades
                    WHERE Especialidad = @Especialidad
                    )
                BEGIN
                    INSERT INTO Especialidades 
                        (Especialidad)
                    VALUES
                        (@Nombre)

                    SELECT SCOPE_IDENTITY()
                END
                ELSE
                BEGIN
	                SELECT -1
                END
            ";

            using (var db = Coneccion())
            {

                return db.ExecuteScalar<int>(sql, new { especialidad.Nombre });
            }
        }
        public static bool ModificarEspecialidad(Especialidad especialidad)
        {
            var sql = @"
                UPDATE Especialidades SET
                    Especialidad = @Nombre
                WHERE IdEspecialidad = @IdEspecialidad
            ";

            using (var db = Coneccion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Nombre", especialidad.Nombre);
                parametros.Add("@IdEspecialidad", especialidad.IdEspecialidad);

                return db.Execute(sql, parametros) == 1;
            }
        }
        public static bool BorrarEspecialidad(int idEspecialidad)
        {
            var sql = @"
                DELETE FROM Especialidades 
                WHERE IdEspecialidad = @IdEspecialidad
            ";

            using (var db = Coneccion())
            {
                return db.Execute(sql, new { IdEspecialidad = idEspecialidad }) == 1;
            }
        }
    }
}
