using AIS.Model;
using AIS.Model.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterEmployeeService
    {
        Task<IEnumerable<MasterEmployeeListModel>> GetMasterEmployeeList(int SchoolId, int MasterEmployeeId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterEmployee(MasterEmployeeModel masterEmployeeModel);

    }
}
