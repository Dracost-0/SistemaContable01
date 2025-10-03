using Microsoft.Data.SqlClient;

namespace SistemaContable01.conexion
{
    public class DatabaseConnection
    {
        // Cadena de conexión centralizada
        private readonly string connectionString =
            @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        // Método para obtener una conexión lista
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
