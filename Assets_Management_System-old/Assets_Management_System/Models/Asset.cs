using System;

namespace Assets_Management_System.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }   // auto later
        public string Name { get; set; }
        public string Category { get; set; }
        public string SerialNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }       // Available, Assigned, Repair, Retired
        public string ImagePath { get; set; }
        public string Notes { get; set; }
    }
}
