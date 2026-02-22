using Npgsql;
using System;
using System.Configuration;
using System.Data;

namespace Assets_Management_System.Services
{
    public static class DbHelper
    {
        private static readonly string connectionString = 
            ConfigurationManager.ConnectionStrings["PostgresDB"]?.ConnectionString 
            ?? throw new ConfigurationErrorsException("PostgresDB connection string not found in App.config");

        public static NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static DataTable GetData(string sql)
        {
            try
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database Error in GetData: {ex.Message}");
                throw new Exception($"Failed to retrieve data from database. Please check your connection.", ex);
            }
        }

        public static int Execute(string sql, params NpgsqlParameter[] parameters)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database Error in Execute: {ex.Message}");
                throw new Exception($"Failed to execute database operation. Please check your data and try again.", ex);
            }
        }
    }
}