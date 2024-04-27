using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Transaction
{
    public class StudentTransactionDetailsModel
    {
        public string userId { get; set; }
        public string admissionId { get; set; }
        public string classSection { get; set; }
        public string studentName { get; set; }
        public string fatherFullName { get; set; }
        public int receiptId { get; set; }
        public string paymentMode { get; set; }
        public string monthName { get; set; }
        public long totalAmount { get; set; }
        public long discount { get; set; }
        public long netAmount { get; set; }
        public long paidAmount { get; set; }
        public long due { get; set; }
        public long advance { get; set; }

        public string receiptBy { get; set; }
        public string remark { get; set; }
        public string paymentDate { get; set; }
    }
    public class StudentTransactionHeaderDetailsModel
    {
        // public string userId { get; set; }
        public int receiptId { get; set; }
        public string feeHeaderName { get; set; }
        public int amount { get; set; }
        public int discountAmount { get; set; }
        public int receivedAmount { get; set; }
        public string feeReceivedDate { get; set; }

    }   
    public class ClasswiseFeeCollectionListModel
    {

        public int classId { get; set; }
        public string className { get; set; }
        public string payCount { get; set; }
        public long paidAmount { get; set; }
        public string paymentDate { get; set; }


    }
    public class ModewiseTransactionDetails
    {

        public string paymentMode { get; set; }
        public long amount { get; set; }
        public int paymentModeCount { get; set; }
        public DateTime paymentDate { get; set; }



    }
}
