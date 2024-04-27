using AIS.Model;
using AIS.Model.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterDepartmentService
    {
        Task<IEnumerable<MasterDepartmentListModel>> GetMasterDepartmentList(int SchoolId, int MasterDepartmentId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterDepartment(MasterDepartmentModel masterDepartmentModel);

    }
}
