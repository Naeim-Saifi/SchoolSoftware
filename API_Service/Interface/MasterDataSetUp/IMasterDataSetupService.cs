using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.HolidaySetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.MasterDataSetUp
{
    
    public interface IMasterDataSetupService
    {
        Task<IEnumerable<Master_CLass_List_Output_Model>> GET_Master_ClassLIST(Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model);
        Task<IEnumerable<Master_Section_List_Output_Model>> GET_Master_SectionLIST(Master_Section_List_Input_Para_Model master_Section_List_Input_Para_Model);
        Task<IEnumerable<Master_Map_Subject_With_Class_List_Output_Model>> GET_Mapp_SubjectwithClassLIST(Master_Map_Subject_With_ClassList_Input_Para_Model master_Map_Subject_With_ClassList_Input_Para_Model);
        Task<IEnumerable<Master_Subject_List_Output_Model>> GET_Master_SubjectLIST(Master_Subject_List_Input_Para_Model master_Subject_List_Input_Para_Model);
        Task<IEnumerable<Master_School_List_Output_Model>> GET_School_List(Master_School_List_Input_Para_Model master_School_List_Input_Para_Model);
        Task<IEnumerable<Master_Role_List_Output_Model>> GET_Master_RoleList(Master_Role_List_Input_Para_Model master_Role_List_Input_Para_Model);
        Task<APIReturnModel> CRUD_MasterRole(MasterRoleAPIModel masterRoleSetUpModel);
    }
}
