using AdminDashboard.Server.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.Transaction
{
    public interface ITransactionService
    {
        Task<TransactionDetailsModel> GetStudentTransactionDateRange(string SchoolCode, string FinancialYear, string fromDate, string toDate, string StudentId, string OperationType);
    }
}
