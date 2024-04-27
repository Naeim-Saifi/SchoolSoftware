using AdminDashboard.Server.Models.PendingFee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.PendingFee
{
    public interface IPendingFeeService
    {
        Task<StudentPendingFeeModel> GetStudentPendingFee(string SchoolCode, string FinancialYear, int UserId, string OperationType);
    }
}
