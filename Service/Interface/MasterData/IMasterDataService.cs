using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.MasterData
{
    public interface IMasterDataService
    {
        Task<APIReturnModel> AddUpdateDeleteMasterClass(MasterClassAPIInputModel  masterClassAPIInputModel);
        Task<IEnumerable<MasterClassListModel>> GetMasterClassList(DefaultInputParameterModel defaultInputModel);

        Task<APIReturnModel> AddUpdateDeleteSectionMaster(MasterSectionAPIInputModel masterSectionAPIInputModel);
        Task<IEnumerable<MasterSectionListModel>> GetSectionList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteBatchMaster(MasterBatchAPIInputModel masterBatchAPIInputModel);
        Task<IEnumerable<MasterBatchListModel>> GetBatchList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteDepartmentMaster(MasterDepartmentAPIInputModel masterDepartmentAPIInputModel);
        Task<IEnumerable<MasterDepartmentListModel>> GetDepartmentList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteDocumentMaster(MasterDocumentAPIInputModel masterDocumentAPIInputModel);
        Task<IEnumerable<MasterDocumentListModel>> GetDocumentList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteEmpolyeeMaster(MasterEmployeeAPIInputModel masterEmployeeAPIInputModel);
        Task<IEnumerable<MasterEmployeeListModel>> GetEmployeeList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteOccuptionMaster(MasterOccuptionAPIInputModel masterOccuptionAPIInputModel);
        Task<IEnumerable<MasterOccuptionListModel>> GetOccuptionList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteStateMaster(MasterStateAPIInputModel masterStateAPIInputModel);
        Task<IEnumerable<MasterStateListModel>> GetStateList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteSubjectMaster(MasterSubjectAPIInputModel masterSubjectAPIInputModel);
        Task<IEnumerable<MastersSubjectListModel>> GetSubjectList(DefaultInputParameterModel defaultInputParameterModel);
        //11-04-23 farhat
        Task<APIReturnModel> AddUpdateDeleteMasterBusStopWithRouteDetails(MasterBusStopWithRouteDetailsAPIInputModel masterBusStopWithRouteDetailsAPIInputModel);
        Task<IEnumerable<MasterBusStopWithRouteDetailsListModel>> GetMasterBusStopWithRouteDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterConcessionDetails(MasterConcessionDetailsAPIInputModel masterConcessionDetailsAPIInputModel);
        Task<IEnumerable<MasterConcessionDetailsListModel>> GetMasterConcessionDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterDiscountDetails(MasterDiscountDetailsAPIInputModel masterDiscountDetailsAPIInputModel);
        Task<IEnumerable<MasterDiscountDetailsListModel>> GetMasterDiscountDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterFeeDetails(MasterFeeDetailsAPIInputModel masterFeeDetailsAPIInputModel);
        Task<IEnumerable<MasterFeeDetailsListModel>> GetMasterFeeDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterExamTypeDetails(MasterExamTypeDetailsAPIInputModel masterExamTypeDetailsAPIInputModel);
        Task<IEnumerable<MasterExamTypeDetailsListModel>> GetMasterExamTypeDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterItemDetails(MasterItemDetailsAPIInputModel masterItemDetailsAPIInputModel);
        Task<IEnumerable<MasterItemDetailsListModel>> GetMasterItemDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterStudentGatePassDetails(MasterStudentGatePassDetailsAPIInputModel masterStudentGatePassDetailsAPIInputModel);
        Task<IEnumerable<MasterStudentGatePassDetailsListModel>> GetMasterStudentGatePassDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterVehicleDetails(MasterVehicleDetailsAPIInputModel masterVehicleDetailsAPIInputModel);
        Task<IEnumerable<MasterVehicleDetailsListModel>> GetMasterVehicleDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterVenderDetails(MasterVenderDetailsAPIInputModel masterVenderDetailsAPIInputModel);
        Task<IEnumerable<MasterVenderDetailsListModel>> GetMasterVenderDetailsList(DefaultInputParameterModel defaultInputParameterModel);
        Task<APIReturnModel> AddUpdateDeleteMasterFeeTermDetails(MasterFeeTermDetailsAPIInputModel masterFeeTermDetailsAPIInputModel);
        Task<IEnumerable<MasterFeeTermDetailsListModel>> GetMasterFeeTermDetailsList(DefaultInputParameterModel defaultInputParameterModel);
    }
}
