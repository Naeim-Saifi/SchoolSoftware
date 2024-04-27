using AIS.Model;
using AIS.Model.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterOccupationService
    {
        Task<IEnumerable<MasterOccupationListModel>> GetMasterOccupationList(int SchoolId, int MasterOccupationId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterOccupation(MasterOccupationModel masterOccupationModel);

    }
}
