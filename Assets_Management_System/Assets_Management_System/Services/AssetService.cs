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

            try
            {
                using (var conn = DbHelper.GetConnection())
                {
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
                    
                    System.Diagnostics.Debug.WriteLine($"GetAll() returned {list.Count} assets");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AssetService.GetAll() Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }

            return list;
        }
        // ================= GET BY ID =================
        public Asset GetById(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                string sql = "SELECT * FROM Assets WHERE Id=@Id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new Asset
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
                            };
                        }
                    }
                }
            }
            return null;
        }

        // ================= LIFECYCLE OPERATIONS =================

        public void AssignAsset(int assetId, string employeeName, string note, DateTime date)
        {
            ExecuteLifecycleTransition(assetId, AssetStatus.Assigned, "Assign", employeeName, note, date);
        }

        public void ReturnAsset(int assetId, string employeeName, string condition, string note, DateTime date)
        {
            string targetStatus = condition == "Good" ? AssetStatus.Available : AssetStatus.Repair;
            ExecuteLifecycleTransition(assetId, targetStatus, "Return", employeeName, note, date);
        }

        public void SendToRepair(int assetId, string note)
        {
            ExecuteLifecycleTransition(assetId, AssetStatus.Repair, "Repair", "-", note, DateTime.Now);
        }

        public void RetireAsset(int assetId, string note)
        {
            ExecuteLifecycleTransition(assetId, AssetStatus.Retired, "Retire", "-", note, DateTime.Now);
        }

        private void ExecuteLifecycleTransition(int assetId, string targetStatus, string transactionType, string employee, string note, DateTime date)
        {
            var asset = GetById(assetId);
            if (asset == null) throw new Exception("Asset not found.");

            ValidateTransition(asset.Status, targetStatus);

            using (var conn = DbHelper.GetConnection())
            {
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Update Asset Status
                        string updateSql = "UPDATE Assets SET Status=@Status WHERE Id=@Id";
                        using (var cmd = new NpgsqlCommand(updateSql, conn))
                        {
                            cmd.Parameters.AddWithValue("Status", targetStatus);
                            cmd.Parameters.AddWithValue("Id", assetId);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Log Transaction
                        string logSql = @"
                            INSERT INTO Transactions (AssetId, TransactionType, EmployeeName, TransactionDate, Note)
                            VALUES (@AssetId, @TransactionType, @EmployeeName, @TransactionDate, @Note)";
                        using (var cmd = new NpgsqlCommand(logSql, conn))
                        {
                            cmd.Parameters.AddWithValue("AssetId", assetId);
                            cmd.Parameters.AddWithValue("TransactionType", transactionType);
                            cmd.Parameters.AddWithValue("EmployeeName", employee ?? "-");
                            cmd.Parameters.AddWithValue("TransactionDate", date);
                            cmd.Parameters.AddWithValue("Note", note ?? "");
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        private void ValidateTransition(string currentStatus, string targetStatus)
        {
            // 1. Retired is final
            if (currentStatus == AssetStatus.Retired)
            {
                throw new InvalidOperationException("This asset is Retired and cannot perform any further actions.");
            }

            // 2. Already in target status
            if (currentStatus == targetStatus)
            {
                throw new InvalidOperationException($"Asset is already in {targetStatus} status.");
            }

            // 3. Business Rules
            if (targetStatus == AssetStatus.Assigned && currentStatus != AssetStatus.Available)
            {
                throw new InvalidOperationException("Only Available assets can be assigned.");
            }

            if (targetStatus == AssetStatus.Available && currentStatus != AssetStatus.Assigned && currentStatus != AssetStatus.Repair)
            {
                // This covers Return or Fixed
                throw new InvalidOperationException("Invalid transition to Available.");
            }
            
            // Allow transitions to Retired from anything except Retired (handled above)
            // Allow transitions to Repair from Available or Assigned
        }

        // Deprecated simple update
        public void UpdateStatus(int assetId, string status)
        {
            var asset = GetById(assetId);
            if (asset != null)
            {
                ValidateTransition(asset.Status, status);
            }

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

