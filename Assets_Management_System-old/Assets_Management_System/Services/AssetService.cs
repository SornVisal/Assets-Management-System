using Assets_Management_System.Models;
using System;
using System.Collections.Generic;
using Npgsql;

namespace Assets_Management_System.Services
{
    public class AssetService
    {
        // Connection string is managed by DbHelper
        
        // ================= ADD =================
        public void Add(Asset asset)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // conn.Open(); // Connection is already open from DbHelper

                // 1. Insert asset and get new ID (Postgres syntax)
                // Using unquoted identifiers allows Postgres to match regardless of casing (defaults to lowercase)
                string insertSql = @"
INSERT INTO Assets
(Name, Category, SerialNumber, PurchaseDate, Price, Status, ImagePath, Notes)
VALUES
(@Name, @Category, @SerialNumber, @PurchaseDate, @Price, @Status, @ImagePath, @Notes)
RETURNING Id";

                int newId;
                using (var cmd = new NpgsqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("Name", asset.Name);
                    cmd.Parameters.AddWithValue("Category", asset.Category);
                    cmd.Parameters.AddWithValue("SerialNumber", asset.SerialNumber ?? "");
                    cmd.Parameters.AddWithValue("PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("Price", asset.Price);
                    cmd.Parameters.AddWithValue("Status", asset.Status);
                    cmd.Parameters.AddWithValue("ImagePath", asset.ImagePath ?? "");
                    cmd.Parameters.AddWithValue("Notes", asset.Notes ?? "");

                    newId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. Generate AssetCode
                string assetCode = $"AST-{newId:0000}";

                // 3. Update AssetCode
                string updateSql = "UPDATE Assets SET AssetCode=@Code WHERE Id=@Id";

                using (var cmd = new NpgsqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("Code", assetCode);
                    cmd.Parameters.AddWithValue("Id", newId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ================= UPDATE =================
        public void Update(Asset asset)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // conn.Open();

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

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Id", asset.Id);
                    cmd.Parameters.AddWithValue("Name", asset.Name);
                    cmd.Parameters.AddWithValue("Category", asset.Category);
                    cmd.Parameters.AddWithValue("SerialNumber", asset.SerialNumber ?? "");
                    cmd.Parameters.AddWithValue("PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("Price", asset.Price);
                    cmd.Parameters.AddWithValue("ImagePath", asset.ImagePath ?? "");
                    cmd.Parameters.AddWithValue("Notes", asset.Notes ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ================= DELETE =================
        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // conn.Open();

                string sql = "DELETE FROM Assets WHERE Id=@Id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ================= GET ALL =================
        public List<Asset> GetAll()
        {
            List<Asset> list = new List<Asset>();

            using (var conn = DbHelper.GetConnection())
            {
                // conn.Open();

                string sql = "SELECT * FROM Assets ORDER BY Id DESC";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Asset
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            AssetCode = r["AssetCode"] as string,
                            Name = r["Name"].ToString(),
                            Category = r["Category"].ToString(),
                            SerialNumber = r["SerialNumber"].ToString(),
                            PurchaseDate = Convert.ToDateTime(r["PurchaseDate"]),
                            Price = Convert.ToDecimal(r["Price"]),
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
            UpdateStatus(assetId, "Assigned");
        }

        public void UpdateStatus(int assetId, string status)
        {
            using (var conn = DbHelper.GetConnection())
            {
                string sql = "UPDATE Assets SET Status=@Status WHERE Id=@Id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Status", status);
                    cmd.Parameters.AddWithValue("Id", assetId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

