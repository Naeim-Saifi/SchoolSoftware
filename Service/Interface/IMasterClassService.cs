
using AIS.Model;
using AIS.Model.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterClassService
    {
        Task<IEnumerable<MasterClassListModel>> GetMasterClassList(string SchoolCode, string FinancialYear,int SchoolId, int MasterClassId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterClass(MasterClassModel masterClassModel);

    }
}
