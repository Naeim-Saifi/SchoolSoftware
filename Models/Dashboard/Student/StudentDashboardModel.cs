using AdminDashboard.Server.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Dashboard.Student
{
    public class StudentDashboardModel
    {
        public List<PaymentDetailsListModel> paymentDetailsListModel { get; set; }
        public List<PendingFeeDetailsModel> pendingFee { get; set; }

        public List<StudentTransactionHeaderDetailsModel> studentTransactionHeaderDetailsModels { get; set; }
        //public PaymentDetailsListModel paymentDetailsListModel { get; set; }
        // public PendingFeeDetailsModel pendingFee { get; set; }
    }
}
