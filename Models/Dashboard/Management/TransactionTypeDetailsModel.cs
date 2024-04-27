using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Dashboard.Management
{
    public class TransactionTypeDetailsModel
    {
        public string? paymentMode { get; set; }
        public long? amount { get; set; }
    }
}
