using AIS.Model;
using AIS.Model.Academic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterSubjectService
    {
        Task<IEnumerable<MasterSubjectListModel>> GetMasterSubjectList(int SchoolId, int MasterSubjectId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterSubject(MasterSubjectModel masterSubjectModel);

    }
}
