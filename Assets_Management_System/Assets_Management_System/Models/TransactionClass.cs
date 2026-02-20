using System;

namespace Assets_Management_System.Models
{
    public class TransactionClass
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; } // For display
        public string TransactionType { get; set; }
        public string EmployeeName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
    }
}
