using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.School;
using AIS.Data.RequestResponseModel.Syllabus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.SchoolDetails
{
    public interface ISchoolDetailsService
    { 
            Task<APIReturnModel> CRUD_SchoolDetails(SchoolDetailsAPIModel schoolDetailsAPIModel);
            Task<IEnumerable<SchoolDetailsListModel>> GET_School_List(SchoolDetails_Input_Para_Model schoolDetails_Input_Para_Model);
         
    }
}
