using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TransactionClass
{
    public int Id { get; set; }                      // ✅ PRIMARY KEY
    public int AssetId { get; set; }                 // ✅ FOREIGN KEY (NEW)

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