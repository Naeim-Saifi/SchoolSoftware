using AdminDashboard.Server.Models.Dashboard.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.Dashboard.Management
{
    public interface IManagementDashboardService
    {
        Task<ManagementDashboardModel> ManagementDashboard(string SchoolCode,string FinancialYear);
    }
}
