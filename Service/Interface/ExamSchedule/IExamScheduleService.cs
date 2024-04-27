using AdminDashboard.Server.Models.ExamSchedule;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.ExamSchedule
{
    public interface IExamScheduleService
    {
            Task<IEnumerable<ExamScheduleListModel>> GetExamScheduleList(string SchoolCode, string FinancialYear, int UserId, string OperationType);
            Task<APIReturnModel> AddUpdateExamSchedule(ExamScheduleModel  examScheduleModel);
         
    }
}
