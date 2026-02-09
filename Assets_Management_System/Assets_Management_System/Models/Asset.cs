using System;

namespace Assets_Management_System.Models
{
    public class Asset
    {
        private static int _nextId = 1;

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string EmployeeName { get; set; } = "None";
        public string Status { get; set; } = "Available";
        public string Notes { get; set; } = "";
        public string ImagePath { get; set; } = "";

        public Asset()
        {
            Id = _nextId++;
            PurchaseDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} - {Category} (${Price:F2})";
        }
    }
}