using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.FlowUpSchool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.FlowUpSchool
{
    public interface IFlowUpSchoolService
    {
        
          
            Task<APIReturnModel> CRUD_FlowUpSchoolDetails(FlowUpSchoolInputModel flowUpSchoolInputModel);
            Task<APIReturnModel> CRUD_FlowUpStatusDetails(FlowUpStatusDetailsInputModel flowUpStatusDetailsInputModel);
            Task<IEnumerable<FlowUpSchoolOutPutModel>> GET_FlowUp_SchoolList(FlowUpSchool_Input_Para_Model flowUpSchool_Input_Para_Model);
            Task<IEnumerable<FlowUpDetailsOutPutModel>> GET_FlowUp_Details(FlowUpDetails_Input_Para_Model flowUpDetails_Input_Para_Model);
         
    }
}
