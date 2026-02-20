using System;
using Npgsql;
using Assets_Management_System.Models;

namespace Assets_Management_System.Services
{
    public class TransactionService
    {
        // Connection string managed by DbHelper

        public void Add(TransactionClass t)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // conn.Open();

                string sql = @"
INSERT INTO Transactions
(AssetId, TransactionType, EmployeeName, TransactionDate, Note)
VALUES
(@AssetId, @TransactionType, @EmployeeName, @TransactionDate, @Note)";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("AssetId", t.AssetId);
                    cmd.Parameters.AddWithValue("TransactionType", t.TransactionType);
                    cmd.Parameters.AddWithValue("EmployeeName", t.EmployeeName);
                    cmd.Parameters.AddWithValue("TransactionDate", t.TransactionDate);
                    cmd.Parameters.AddWithValue("Note", t.Note);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public System.Collections.Generic.List<TransactionClass> GetAll()
        {
            var list = new System.Collections.Generic.List<TransactionClass>();

            using (var conn = DbHelper.GetConnection())
            {
                // conn.Open();

                string sql = @"
SELECT t.Id, t.AssetId, t.TransactionType, t.EmployeeName, t.TransactionDate, t.Note, COALESCE(a.Name, 'Unknown Asset') as AssetName
FROM Transactions t
LEFT JOIN Assets a ON t.AssetId = a.Id
ORDER BY t.TransactionDate DESC";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new TransactionClass
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            AssetId = Convert.ToInt32(r["AssetId"]),
                            AssetName = r["AssetName"].ToString(),
                            TransactionType = r["TransactionType"].ToString(),
                            EmployeeName = r["EmployeeName"] != System.DBNull.Value ? r["EmployeeName"].ToString() : "-",
                            TransactionDate = Convert.ToDateTime(r["TransactionDate"]),
                            Note = r["Note"] != System.DBNull.Value ? r["Note"].ToString() : ""
                        });
                    }
                }
            }

            return list;
        }
    }
}