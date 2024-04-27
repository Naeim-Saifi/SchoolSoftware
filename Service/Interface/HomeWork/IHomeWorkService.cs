using AdminDashboard.Server.Models.HomeWork;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.HomeWork
{
    public interface IHomeWorkService
    {
        Task<APIReturnModel> AddUpdateHomeWork(HomeWorkModel  homeWorkModel);

        Task<IEnumerable<HomeworkListModel>> GetHomeWorkList(string SchoolCode, string FinancialYear,string fromDate,string toDate, int userId, string OperationType);
    }
}
