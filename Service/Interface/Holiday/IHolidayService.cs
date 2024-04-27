using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  AdminDashboard.Server.Models.Holiday;

namespace AdminDashboard.Server.Service.Interface.Holiday
{     
        public interface IHolidayService
        {
        Task<APIReturnModel> AddUpdateHolidayDetails(HolidayDetailsModel holidayDetailsModel);
        Task<IEnumerable<HolidayDetailsListModel>> GetHolidayDetails(string SchoolCode, string FinancialYear, int userId, string OperationType);
        Task<APIReturnModel> AddUpdateHolidayTypeMaster(HolidayTypeMasterModel holidayTypeMaster);
        Task<IEnumerable<HolidayTypeListMaster>> GetHolidayTypeMasterDetails(string SchoolCode, string FinancialYear, int userId, string OperationType);
        Task<List<MonthNameList>> GetMonthNameList();

    }    
}
