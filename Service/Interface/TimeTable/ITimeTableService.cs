using AdminDashboard.Server.Models.TimeTable;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.TimeTable
{
    public interface ITimeTableService
    {
        Task<IEnumerable<TimeTableListModel>> GetTimeTableList(string SchoolCode, string FinancialYear, int UserId, string OperationType);
        Task<IEnumerable<PeriodListModel>> GetPeriodList(string SchoolCode, string FinancialYear, int UserId, string OperationType);
        Task<IEnumerable<DayModelList>> GetDaysNameList();
        Task<APIReturnModel> AddUpdatePeriodDetails(PeriodModel  periodModel);
        Task<APIReturnModel> AddUpdateTimeTable(ViewTimeTableModel timeTableModel);
    }
    
}
