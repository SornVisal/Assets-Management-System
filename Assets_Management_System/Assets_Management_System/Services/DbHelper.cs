using System.Data.SqlClient;

namespace Assets_Management_System.Services
{
    public static class DbHelper
    {
        // Connect directly to LocalDB database by NAME (not file)
        // This avoids bin/Debug copy problems forever
        private static string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;
              Initial Catalog=AssetDB;
              Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}