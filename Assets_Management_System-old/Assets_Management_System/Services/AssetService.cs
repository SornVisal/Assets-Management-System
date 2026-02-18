using Assets_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Assets_Management_System.Services
{
    public class AssetService
    {
        private readonly string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AssetDB;Integrated Security=True";

        // =========================
        // ADD
        // =========================
        public void Add(Asset asset)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
INSERT INTO Assets
(Name, Category, SerialNumber, PurchaseDate, Price, Status, ImagePath, Notes)
VALUES
(@Name, @Category, @SerialNumber, @PurchaseDate, @Price, @Status, @ImagePath, @Notes);
";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", asset.Name);
                    cmd.Parameters.AddWithValue("@Category", (object)asset.Category ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SerialNumber", (object)asset.SerialNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("@Price", asset.Price);
                    cmd.Parameters.AddWithValue("@Status", asset.Status);
                    cmd.Parameters.AddWithValue("@ImagePath", (object)asset.ImagePath ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", (object)asset.Notes ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Asset asset)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
UPDATE Assets SET
    Name = @Name,
    Category = @Category,
    SerialNumber = @SerialNumber,
    PurchaseDate = @PurchaseDate,
    Price = @Price,
    Notes = @Notes
WHERE Id = @Id;
";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", asset.Id);
                    cmd.Parameters.AddWithValue("@Name", asset.Name);
                    cmd.Parameters.AddWithValue("@Category", asset.Category);
                    cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);
                    cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("@Price", asset.Price);
                    cmd.Parameters.AddWithValue("@Notes", asset.Notes ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // GET ALL
        // =========================
        public List<Asset> GetAll()
        {
            var list = new List<Asset>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Assets ORDER BY Id DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Asset
                        {
                            Id = (int)reader["Id"],
                            AssetCode = reader["AssetCode"] as string,
                            Name = reader["Name"].ToString(),
                            Category = reader["Category"] as string,
                            SerialNumber = reader["SerialNumber"] as string,
                            PurchaseDate = reader["PurchaseDate"] == DBNull.Value
                                ? DateTime.MinValue
                                : (DateTime)reader["PurchaseDate"],
                            Price = reader["Price"] == DBNull.Value
                                ? 0
                                : (decimal)reader["Price"],
                            Status = reader["Status"].ToString(),
                            ImagePath = reader["ImagePath"] as string,
                            Notes = reader["Notes"] as string
                        });
                    }
                }
            }

            return list;
        }

        // =========================
        // DELETE
        // =========================
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM Assets WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
