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

        // ================= ADD =================
        public void Add(Asset asset)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1️⃣ Insert asset and get new ID
                string insertSql = @"
INSERT INTO Assets
(Name, Category, SerialNumber, PurchaseDate, Price, Status, ImagePath, Notes)
OUTPUT INSERTED.Id
VALUES
(@Name, @Category, @SerialNumber, @PurchaseDate, @Price, @Status, @ImagePath, @Notes);
";

                int newId;
                using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", asset.Name);
                    cmd.Parameters.AddWithValue("@Category", asset.Category);
                    cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber ?? "");
                    cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("@Price", asset.Price);
                    cmd.Parameters.AddWithValue("@Status", asset.Status);
                    cmd.Parameters.AddWithValue("@ImagePath", asset.ImagePath ?? "");
                    cmd.Parameters.AddWithValue("@Notes", asset.Notes ?? "");

                    newId = (int)cmd.ExecuteScalar();
                }

                // 2️⃣ Generate AssetCode
                string assetCode = $"AST-{newId:0000}";

                // 3️⃣ Update AssetCode
                string updateSql = "UPDATE Assets SET AssetCode=@Code WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", assetCode);
                    cmd.Parameters.AddWithValue("@Id", newId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ================= UPDATE =================
        public void Update(Asset asset)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
UPDATE Assets SET
Name=@Name,
Category=@Category,
SerialNumber=@SerialNumber,
PurchaseDate=@PurchaseDate,
Price=@Price,
ImagePath=@ImagePath,
Notes=@Notes
WHERE Id=@Id;
";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", asset.Id);
                    cmd.Parameters.AddWithValue("@Name", asset.Name);
                    cmd.Parameters.AddWithValue("@Category", asset.Category);
                    cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber ?? "");
                    cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("@Price", asset.Price);
                    cmd.Parameters.AddWithValue("@ImagePath", asset.ImagePath ?? "");
                    cmd.Parameters.AddWithValue("@Notes", asset.Notes ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ================= DELETE =================
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM Assets WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ================= GET ALL =================
        public List<Asset> GetAll()
        {
            List<Asset> list = new List<Asset>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Assets ORDER BY Id DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Asset
                        {
                            Id = (int)r["Id"],
                            AssetCode = r["AssetCode"] as string,
                            Name = r["Name"].ToString(),
                            Category = r["Category"].ToString(),
                            SerialNumber = r["SerialNumber"].ToString(),
                            PurchaseDate = (DateTime)r["PurchaseDate"],
                            Price = (decimal)r["Price"],
                            Status = r["Status"].ToString(),
                            ImagePath = r["ImagePath"].ToString(),
                            Notes = r["Notes"].ToString()
                        });
                    }
                }
            }

            return list;
        }
        public void AssignAsset(int assetId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "UPDATE Assets SET Status='Assigned' WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", assetId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
