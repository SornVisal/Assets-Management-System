using Npgsql;
using System.Data;

namespace Assets_Management_System.Services
{
    public static class DbHelper
    {
        private static readonly string connectionString =
            

        public static NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static DataTable GetData(string sql)
        {
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int Execute(string sql, params NpgsqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}