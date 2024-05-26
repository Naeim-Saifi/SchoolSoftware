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



        //add 24-05-24

        Task<APIReturnModel> CRUD_Transport_RouteMaster(RouteDetailsApiModel routeDetailsApiModel);
        Task<IEnumerable<RouteDetailsOutputModel>> GET_Transport_RouteMaster(RouteDetailsParaModel routeDetailsParaModel);

        Task<APIReturnModel> CRUD_Transport_TransportMaster(TransportDetailsApiModel transportDetailsApiModel);
        Task<IEnumerable<TransportDetailsOutputModel>> GET_Transport_TransportMaster(TransportDetailsParaModel transportDetailsParaModel);

        Task<APIReturnModel> CRUD_Transport_BusStopMaster(BusStopApiModel busStopApiModel);
        Task<IEnumerable<BusStopOutputModel>> GET_Transport_BusStopMaster(BusStopParaModel busStopParaModel);

        Task<APIReturnModel> CRUD_Transport_RouteWithBusStop(MapBusWithRouteApiModel mapBusWithRouteApiModel);
        Task<IEnumerable<MapBusWithRouteOutputModel>> GET_Transport_RouteWithBusStop(MapBusWithRouteParaModel mapBusWithRouteParaModel);


        //Fee Master.

        Task<APIReturnModel> CRUD_Fee_FeeHeaderMaster(FeeHeaderApiModel feeHeaderApiModel);
        Task<IEnumerable<FeeHeaderOutputModel>> GET_Fee_FeeHeaderList(FeeHeaderParaModel feeHeaderParaModel);

        Task<APIReturnModel> CRUD_Fee_ConcessionCategory(FeeConcessionCategoryApiModel feeConcessionCategoryApiModel);
        Task<IEnumerable<FeeConcessionCategoryOutputModel>> GET_Fee_ConcessionCategoryList(FeeConcessionCategoryParaModel feeConcessionCategoryParaModel);

        Task<APIReturnModel> CRUD_Fee_DiscountRemarks(FeeDiscountRemarksApiModel feeDiscountRemarksApiModel);
        Task<IEnumerable<FeeDiscountRemarksOutputModel>> GET_Fee_DiscountRemarksList(FeeDiscountRemarksParaModel feeDiscountRemarksParaModel);

         
    }
}
