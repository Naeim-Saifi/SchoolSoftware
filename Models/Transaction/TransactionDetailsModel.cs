using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Transaction
{
    public class TransactionDetailsModel
    {
        public List<StudentTransactionDetailsModel> studentTransactionDetailsModels { get; set; }
        public List<StudentTransactionHeaderDetailsModel> studentTransactionHeaderDetailsModels { get; set; }
        public List<ClasswiseFeeCollectionListModel> classwiseFeeCollectionListModels { get; set; }
        public List<ModewiseTransactionDetails> modewiseTransactions { get; set; }
       

    }
}
