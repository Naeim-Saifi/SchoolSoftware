using AdminDashboard.Server.Models.FeeConfigurationList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.FeeConfiguration
{
    public interface IFeeConfigurationListService
    {
        Task<FeeConfigurationListModel> GetFeeConfigurationList(string SchoolCode, string FinancialYear, int UserId, string OperationType);
    }
}
