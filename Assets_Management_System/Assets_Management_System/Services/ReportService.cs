using System;
using System.Data;
using Assets_Management_System.Services;

namespace Assets_Management_System.Services
{
    public class ReportService
    {
        public DataTable GetCurrentAssignments()
        {
            // Now using the PostgreSQL View for cleaner app code
            return DbHelper.GetData("SELECT * FROM v_current_assignments");
        }

        public DataTable GetTransactionHistory(DateTime? fromDate = null, DateTime? toDate = null)
        {
            // Now using the PostgreSQL View
            string sql = "SELECT * FROM v_transaction_history WHERE 1=1";

            if (fromDate.HasValue)
                sql += $" AND TransactionDate >= '{fromDate.Value:yyyy-MM-dd HH:mm:ss}'";
            
            if (toDate.HasValue)
                sql += $" AND TransactionDate <= '{toDate.Value:yyyy-MM-dd 23:59:59}'";

            sql += " ORDER BY TransactionDate DESC";

            return DbHelper.GetData(sql);
        }

        public DataTable GetAssetsByStatus(string status = null)
        {
            string sql = "SELECT * FROM Assets";
            if (!string.IsNullOrEmpty(status))
            {
                sql += $" WHERE Status = '{status}'";
            }
            return DbHelper.GetData(sql);
        }
    }
}
