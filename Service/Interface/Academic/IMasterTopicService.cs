using AIS.Model;
using AIS.Model.Academic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterTopicService
    {
        Task<IEnumerable<MasterTopicList>> GetMasterTopicList(int SchoolId, int MasterTopicId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterTopic(MasterTopicModel MasterTopicModel);

    }
}
