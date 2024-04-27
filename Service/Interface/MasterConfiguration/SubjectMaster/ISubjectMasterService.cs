using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster
{
    
    public interface ISubjectMasterService
    {
        Task<SubjectDetailsModel> SubjectDetails(string SchoolCode, string FinancialYear, int userID, string OperationType);
    }
}
