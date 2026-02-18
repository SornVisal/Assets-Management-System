using System.Data.SqlClient;
using Assets_Management_System.Models;

namespace Assets_Management_System.Services
{
    public class TransactionService
    {
        private readonly string connectionString =
            System.Configuration.ConfigurationManager
            .ConnectionStrings["AssetDB"].ConnectionString;

        public void Add(TransactionClass t)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
INSERT INTO Transactions
(AssetId, TransactionType, EmployeeName, TransactionDate, Note)
VALUES
(@AssetId, @TransactionType, @EmployeeName, @TransactionDate, @Note)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AssetId", t.AssetId);
                    cmd.Parameters.AddWithValue("@TransactionType", t.TransactionType);
                    cmd.Parameters.AddWithValue("@EmployeeName", t.EmployeeName);
                    cmd.Parameters.AddWithValue("@TransactionDate", t.TransactionDate);
                    cmd.Parameters.AddWithValue("@Note", t.Note);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}