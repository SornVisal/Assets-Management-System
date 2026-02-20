using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Assets_Management_System.Models;

namespace Assets_Management_System.Services
{
    public class DataSeeder
    {
        private AssetService _service;

        public DataSeeder()
        {
            _service = new AssetService();
        }

        public void Seed()
        {
            try
            {
                SeedUser();
                SeedEmployees();
                SeedAssets();
                SeedTransactions();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seeding failed: " + ex.Message);
            }
        }

        private void SeedEmployees()
        {
            var empService = new EmployeeService();
            empService.EnsureTableExists();

            if (empService.GetAll().Count > 0) return;

            empService.Add(new Employee { FullName = "Sorn Visal", Department = "IT", Position = "System Admin", Email = "visal@assetpro.com" });
            empService.Add(new Employee { FullName = "Keo Pich", Department = "Finance", Position = "Manager", Email = "pich@assetpro.com" });
            empService.Add(new Employee { FullName = "Chan Sok", Department = "HR", Position = "Officer", Email = "sok@assetpro.com" });
            empService.Add(new Employee { FullName = "Reth Borey", Department = "Sales", Position = "Lead", Email = "borey@assetpro.com" });
            empService.Add(new Employee { FullName = "Lim Heng", Department = "IT", Position = "Developer", Email = "heng@assetpro.com" });

            Console.WriteLine("Employees seeded.");
        }

        private void SeedTransactions()
        {
            var txService = new TransactionService();
            var assets = _service.GetAll();

            // 1. One-time Sync for existing "Assigned" transactions
            // This ensures that if the transactions exist but the dashboard says 0, they get synced.
            var existingTx = txService.GetAll().Where(t => t.TransactionType == "Assign").Select(t => t.AssetId).Distinct();
            foreach (var assetId in existingTx)
            {
                // Sync status only, no new transaction needed here if it already exists
                _service.UpdateStatus(assetId, AssetStatus.Assigned);
            }

            // 2. Normal Seeding if empty
            if (txService.GetAll().Count > 0) return;
            if (assets.Count == 0) return;

            // Seed a few transactions and update statuses
            for (int i = 0; i < Math.Min(5, assets.Count); i++)
            {
                var asset = assets[i];
                // Use the new atomic method to seed both transaction and status
                _service.AssignAsset(asset.Id, "Sorn Visal", "Seeded Transaction", DateTime.Today.AddDays(-i));
            }
            Console.WriteLine("Transactions seeded and Assets updated.");
        }

        private void SeedUser()
        {
            // Check if any user exists
            string sql = "SELECT COUNT(*) FROM Users";
            DataTable dt = DbHelper.GetData(sql);
            long count = dt.Rows.Count > 0 ? Convert.ToInt64(dt.Rows[0][0]) : 0;

            if (count == 0)
            {
                // Add default admin user
                string insertSql = "INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin123', 'Admin')";
                DbHelper.Execute(insertSql);
                Console.WriteLine("Default user seeded: admin / admin123");
            }
        }

        private void SeedAssets()
        {
            // Only seed if database is empty
            if (_service.GetAll().Count > 0) return;

            var assets = new List<Asset>();

                // 1. Laptops (10 items)
                assets.Add(Create("Dell XPS 15", "Laptop", "SN-DELL-001", 1899.99m));
                assets.Add(Create("MacBook Pro 16 M2", "Laptop", "SN-MAC-002", 2499.00m));
                assets.Add(Create("Lenovo ThinkPad X1 Carbon", "Laptop", "SN-LEN-003", 1450.50m));
                assets.Add(Create("HP Spectre x360", "Laptop", "SN-HP-004", 1299.99m));
                assets.Add(Create("Asus ROG Zephyrus G14", "Laptop", "SN-ASUS-005", 1599.00m));
                assets.Add(Create("Microsoft Surface Laptop 5", "Laptop", "SN-SUR-006", 999.99m));
                assets.Add(Create("Razer Blade 15", "Laptop", "SN-RAZ-007", 2199.99m));
                assets.Add(Create("Acer Swift 5", "Laptop", "SN-ACER-008", 899.00m));
                assets.Add(Create("MacBook Air M2", "Laptop", "SN-MAC-009", 1199.00m));
                assets.Add(Create("Dell Latitude 7420", "Laptop", "SN-DELL-010", 1350.00m));

                // 2. PCs / Workstations (10 items)
                assets.Add(Create("Dell OptiPlex 7090", "PC", "SN-OPT-101", 850.00m));
                assets.Add(Create("HP EliteDesk 800 G6", "PC", "SN-HP-102", 920.00m));
                assets.Add(Create("Lenovo ThinkCentre M70q", "PC", "SN-LEN-103", 650.00m));
                assets.Add(Create("Custom Ryzen 9 Workstation", "PC", "SN-CUST-104", 2500.00m));
                assets.Add(Create("Mac Mini M2 Pro", "PC", "SN-MAC-105", 1299.00m));
                assets.Add(Create("Intel NUC 12 Extreme", "PC", "SN-NUC-106", 1400.00m));
                assets.Add(Create("Corsair One i300", "PC", "SN-COR-107", 3200.00m));
                assets.Add(Create("HP Z2 Mini G9", "PC", "SN-HP-108", 1100.00m));
                assets.Add(Create("Dell Precision 3660", "PC", "SN-DELL-109", 1750.00m));
                assets.Add(Create("Falcon Northwest Tiki", "PC", "SN-FAL-110", 2800.00m));

                // 3. Monitors (10 items)
                assets.Add(Create("Dell UltraSharp U2723QE", "Monitor", "SN-MON-201", 580.00m));
                assets.Add(Create("LG 27UP850-W", "Monitor", "SN-MON-202", 450.00m));
                assets.Add(Create("BenQ PD3220U", "Monitor", "SN-MON-203", 1199.00m));
                assets.Add(Create("Asus ProArt PA278CV", "Monitor", "SN-MON-204", 399.00m));
                assets.Add(Create("Samsung Smart Monitor M8", "Monitor", "SN-MON-205", 699.00m));
                assets.Add(Create("Dell P2422H", "Monitor", "SN-MON-206", 210.00m));
                assets.Add(Create("HP Z27k G3", "Monitor", "SN-MON-207", 520.00m));
                assets.Add(Create("ViewSonic VP2768a", "Monitor", "SN-MON-208", 430.00m));
                assets.Add(Create("Alienware AW3423DW", "Monitor", "SN-MON-209", 1099.00m));
                assets.Add(Create("LG DualUp 28MQ780", "Monitor", "SN-MON-210", 699.00m));

                // 4. Furniture (10 items)
                assets.Add(Create("Herman Miller Aeron Chair", "Furniture", "SN-FUR-301", 1450.00m));
                assets.Add(Create("Steelcase Leap V2", "Furniture", "SN-FUR-302", 1100.00m));
                assets.Add(Create("Secretlab Titan Evo", "Furniture", "SN-FUR-303", 549.00m));
                assets.Add(Create("IKEA Markus Chair", "Furniture", "SN-FUR-304", 229.00m));
                assets.Add(Create("Uplift V2 Standing Desk", "Furniture", "SN-FUR-305", 850.00m));
                assets.Add(Create("Fully Jarvis Desk", "Furniture", "SN-FUR-306", 780.00m));
                assets.Add(Create("Office Filing Cabinet", "Furniture", "SN-FUR-307", 150.00m));
                assets.Add(Create("Whiteboard 6x4", "Furniture", "SN-FUR-308", 120.00m));
                assets.Add(Create("Conference Table", "Furniture", "SN-FUR-309", 899.00m));
                assets.Add(Create("Lounge Sofa", "Furniture", "SN-FUR-310", 650.00m));

                // 5. Peripherals / Accessories (10 items)
                assets.Add(Create("Logitech MX Master 3S", "Other", "SN-ACC-401", 99.99m));
                assets.Add(Create("Keychron K2 Pro Keyboard", "Other", "SN-ACC-402", 110.00m));
                assets.Add(Create("Apple Magic Trackpad", "Other", "SN-ACC-403", 129.00m));
                assets.Add(Create("CalDigit TS4 Dock", "Other", "SN-ACC-404", 399.00m));
                assets.Add(Create("Sony WH-1000XM5 Headphones", "Other", "SN-ACC-405", 348.00m));
                assets.Add(Create("Bose QC45", "Other", "SN-ACC-406", 329.00m));
                assets.Add(Create("Blue Yeti X Microphone", "Other", "SN-ACC-407", 169.00m));
                assets.Add(Create("Elgato FaceCam", "Other", "SN-ACC-408", 149.00m));
                assets.Add(Create("Logitech C920 Webcam", "Other", "SN-ACC-409", 79.99m));
                assets.Add(Create("Samsung T7 SSD 1TB", "Other", "SN-ACC-410", 119.00m));

                // Insert all
                foreach (var asset in assets)
                {
                    _service.Add(asset);
                }
        }

        private Asset Create(string name, string cat, string serial, decimal price)
        {
            return new Asset
            {
                Name = name,
                Category = cat,
                SerialNumber = serial,
                Price = price,
                PurchaseDate = DateTime.Today.AddDays(-new Random().Next(1, 365)), // Random date within last year
                Status = AssetStatus.Available,
                Notes = "Seeded Data"
            };
        }
    }
}
