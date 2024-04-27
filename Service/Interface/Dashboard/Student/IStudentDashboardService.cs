using AdminDashboard.Server.Models.Dashboard.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.Dashboard.Student
{
    public interface IStudentDashboardService
    {
        Task<StudentDashboardModel> StudentDashboard(string SchoolCode, string FinancialYear, int UserID, string OperationType);
    }
}
