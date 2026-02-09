using System;

namespace Assets_Management_System.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string SerialNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }

        // Calculated property
        public decimal TotalValue => Quantity * Price;
    }
}