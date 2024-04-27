using AIS.Model;
using AIS.Model.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterSectionService
    {
        Task<IEnumerable<MasterSectionListModel>> GetMasterSectionList(string SchoolCode, string FinancialYear, int SchoolId, int MasterSectionId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterSection(MasterSectionModel masterSectionModel);

    }
}
