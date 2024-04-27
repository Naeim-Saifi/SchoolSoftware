using AIS.Model;
using AIS.Model.SchoolSetup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface ISchoolSetupService
    {
        Task<IEnumerable<SchoolListModel>> GetMasterSchoolList(int SchoolId, int MasterSchoolId, int Status, string SchoolCode,
            string OperationType);
        Task<APIReturnModel> AddUpdateMasterSchool(SchoolModel masterSchoolModel);

    }
}
