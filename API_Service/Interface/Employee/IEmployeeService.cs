using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Employee;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.Employee
{
    public interface IEmployeeSetupService
    {
        Task<APIReturnModel> CRUD_UserDetails(Employee_API_Model employee_API_Model);

        Task<IEnumerable<Employee_User_List_Output_Model>> GET_USER_LIST(Employee_User_List_Input_Model employee_User_List_Input_Model);
    }
}
