using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets_Management_System
{
    public class TransactionClass
    {
        // The name of the asset being transacted
        public string AssetName { get; set; }

        // Type of transaction: "IN" (Add Stock) or "OUT" (Remove Stock)
        public string TransactionType { get; set; }

        // How many items were moved
        public int Quantity { get; set; }

        // When did this happen
        public DateTime TransactionDate { get; set; }

        // Any extra notes (optional)
        public string Remarks { get; set; }

        // Constructor to set the date automatically
        public TransactionClass()
        {
            TransactionDate = DateTime.Now;
        }
    }
}