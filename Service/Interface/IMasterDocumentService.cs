using AIS.Model;
using AIS.Model.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterDocumentService
    {
        Task<IEnumerable<MasterDocumentListModel>> GetMasterDocumentList(int SchoolId, int MasterDocumentId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterDocument(MasterDocumentModel masterDocumentModel);

    }
}
