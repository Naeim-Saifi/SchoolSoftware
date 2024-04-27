using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Dashboard.Student
{
    public class PaymentDetailsListModel
    {
        public int receiptId { get; set; }
        public string paymentMode { get; set; }
        public string monthName { get; set; }
        public int? totalAmount { get; set; }
        public int? discount { get; set; }
        public int? netAmount { get; set; }

        public int? paidAmount { get; set; }
        public int? due { get; set; }
        public int? advance { get; set; }

        public string receiptBy { get; set; }
        public string remark { get; set; }
        public string paymentDate { get; set; }
    }
}
