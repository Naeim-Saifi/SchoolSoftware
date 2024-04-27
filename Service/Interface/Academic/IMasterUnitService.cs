using AIS.Model;
using AIS.Model.Academic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterUnitService
    {
        Task<IEnumerable<MasterUnitListModel>> GetMasterUnitList(int SchoolId, int MasterUnitId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterUnit(MasterUnitModel MasterUnitModel);

    }
}
