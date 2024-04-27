
using AIS.Model;
using AIS.Model.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IClassMasterService
    {
        Task<IEnumerable<ClassMasterListModel>> GetClassMasterList(string SchoolCode,string FinancialYear, int SchoolId, int ClassMasterId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateClassMaster(ClassMasterModel ClassMasterModel);

    }
}
