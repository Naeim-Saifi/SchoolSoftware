using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.MasterDataSetUp;

using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.MasterDataSetUp
{
    public class MasterDataSetupService: IMasterDataSetupService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorageService;
        public MasterDataSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<IEnumerable<Master_CLass_List_Output_Model>> GET_Master_ClassLIST(Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Master_ClassLIST?SchoolCode={master_CLass_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={master_CLass_List_Input_Para_Model.financialYear}" +
                    $"&classId={master_CLass_List_Input_Para_Model.classId}" +
                    $"&userId={master_CLass_List_Input_Para_Model.userId}" +
                    $"&reportType={master_CLass_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Master_CLass_List_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<IEnumerable<Master_Section_List_Output_Model>> GET_Master_SectionLIST(Master_Section_List_Input_Para_Model master_Section_List_Input_Para_Model)
        
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Master_SectionLIST?SchoolCode={master_Section_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={master_Section_List_Input_Para_Model.financialYear}" +
                    $"&SectionId={master_Section_List_Input_Para_Model.sectionId}" +
                    $"&userID={master_Section_List_Input_Para_Model.userId}" +
                    $"&reportType={master_Section_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Master_Section_List_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<IEnumerable<Master_Map_Subject_With_Class_List_Output_Model>> GET_Mapp_SubjectwithClassLIST(Master_Map_Subject_With_ClassList_Input_Para_Model master_Map_Subject_With_ClassList_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Mapp_SubjectwithClassLIST?SchoolCode={master_Map_Subject_With_ClassList_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={master_Map_Subject_With_ClassList_Input_Para_Model.financialYear}" +
                    $"&classId={master_Map_Subject_With_ClassList_Input_Para_Model.classID}" +
                     $"&userId={master_Map_Subject_With_ClassList_Input_Para_Model.userId}" +
                    $"&subjectID={master_Map_Subject_With_ClassList_Input_Para_Model.subjectID}" +
                    $"&reportType={master_Map_Subject_With_ClassList_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Master_Map_Subject_With_Class_List_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<Master_Subject_List_Output_Model>> GET_Master_SubjectLIST(Master_Subject_List_Input_Para_Model master_Subject_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Master_SubjectLIST?SchoolCode={master_Subject_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={master_Subject_List_Input_Para_Model.financialYear}" +
                    $"&subjectID={master_Subject_List_Input_Para_Model.subjectID}" +
                    $"&reportType={master_Subject_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Master_Subject_List_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<IEnumerable<Master_School_List_Output_Model>> GET_School_List(Master_School_List_Input_Para_Model master_School_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_School_List?SchoolCode={master_School_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={master_School_List_Input_Para_Model.financialYear}" +
                    $"&reportType={master_School_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Master_School_List_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<Master_Role_List_Output_Model>> GET_Master_RoleList(Master_Role_List_Input_Para_Model master_Role_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Master_RoleList?SchoolCode={master_Role_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={master_Role_List_Input_Para_Model.financialYear}" +
                    $"&reportType={master_Role_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Master_Role_List_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<APIReturnModel> CRUD_MasterRole(MasterRoleAPIModel masterRoleSetUpModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/CRUD_MasterRole", masterRoleSetUpModel);
            return response;
        }

        
        //add 24-05-24 imran
       
        
        public async Task<APIReturnModel> CRUD_Transport_RouteMaster(RouteDetailsApiModel routeDetailsApiModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/CRUD_Transport_RouteMaster", routeDetailsApiModel);
            return response;
        }
        public async Task<IEnumerable<RouteDetailsOutputModel>> GET_Transport_RouteMaster(RouteDetailsParaModel routeDetailsParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Transport_RouteMaster?SchoolCode={routeDetailsParaModel.schoolCode}" +
                    $"&FinancialYear={routeDetailsParaModel.financialYear}" +
                    $"&RouteId={routeDetailsParaModel.RouteDetailsId}" +
                    $"&reportType={routeDetailsParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<RouteDetailsOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<APIReturnModel> CRUD_Transport_TransportMaster(TransportDetailsApiModel transportDetailsApiModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/CRUD_Transport_TransportMaster", transportDetailsApiModel);
            return response;
        }
        public async Task<IEnumerable<TransportDetailsOutputModel>> GET_Transport_TransportMaster(TransportDetailsParaModel transportDetailsParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Transport_TransportMaster?SchoolCode={transportDetailsParaModel.schoolCode}" +
                    $"&FinancialYear={transportDetailsParaModel.financialYear}" +
                    $"&TransportID={transportDetailsParaModel.TransportDetailsId}" +
                    $"&reportType={transportDetailsParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<TransportDetailsOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<APIReturnModel> CRUD_Transport_BusStopMaster(BusStopApiModel busStopApiModel)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/CRUD_Transport_BusStopMaster", busStopApiModel);
            return response;            
        }
        public async Task<IEnumerable<BusStopOutputModel>> GET_Transport_BusStopMaster(BusStopParaModel busStopParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Transport_BusStopMaster?SchoolCode={busStopParaModel.schoolCode}" +
                    $"&FinancialYear={busStopParaModel.financialYear}" +
                    $"&BusStopID={busStopParaModel.BusStopId}" +
                    $"&reportType={busStopParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<BusStopOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<APIReturnModel> CRUD_Transport_RouteWithBusStop(MapBusWithRouteApiModel mapBusWithRouteApiModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/CRUD_Transport_RouteWithBusStop", mapBusWithRouteApiModel);
            return response;
        }
        public async Task<IEnumerable<MapBusWithRouteOutputModel>> GET_Transport_RouteWithBusStop(MapBusWithRouteParaModel mapBusWithRouteParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Transport_RouteWithBusStop?SchoolCode={mapBusWithRouteParaModel.schoolCode}" +
                    $"&FinancialYear={mapBusWithRouteParaModel.financialYear}" 
                    +$"&mapRouteId={mapBusWithRouteParaModel.mapRouteID}" +                    
                    $"&reportType={mapBusWithRouteParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<MapBusWithRouteOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        // add Fee Master.

        public async Task<APIReturnModel> CRUD_Fee_FeeHeaderMaster(FeeHeaderApiModel feeHeaderApiModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/CRUD_Fee_FeeHeaderMaster", feeHeaderApiModel);
            return response;
        }

        public async Task<IEnumerable<FeeHeaderOutputModel>> GET_Fee_FeeHeaderList(FeeHeaderParaModel feeHeaderParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Fee_FeeHeaderList?SchoolCode={feeHeaderParaModel.schoolCode}" +
                    $"&FinancialYear={feeHeaderParaModel.financialYear}"
                    + $"&feeHeaderID={feeHeaderParaModel.feeHeaderID}" +
                    $"&reportType={feeHeaderParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<FeeHeaderOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
         
        public async Task<APIReturnModel> CRUD_Fee_ConcessionCategory(FeeConcessionCategoryApiModel feeConcessionCategoryApiModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/CRUD_Fee_ConcessionCategory", feeConcessionCategoryApiModel);
            return response;
        }
        public async Task<IEnumerable<FeeConcessionCategoryOutputModel>> GET_Fee_ConcessionCategoryList(FeeConcessionCategoryParaModel feeConcessionCategoryParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Fee_ConcessionCategoryList?SchoolCode={feeConcessionCategoryParaModel.schoolCode}" +
                    $"&FinancialYear={feeConcessionCategoryParaModel.financialYear}"
                    + $"&FeeCategoryID={feeConcessionCategoryParaModel.feeCategoryID}" +
                    $"&reportType={feeConcessionCategoryParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<FeeConcessionCategoryOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<APIReturnModel> CRUD_Fee_DiscountRemarks(FeeDiscountRemarksApiModel feeDiscountRemarksApiModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterDataSetUp/feeDiscountRemarksApiModel", feeDiscountRemarksApiModel);
            return response;
        }
        public async Task<IEnumerable<FeeDiscountRemarksOutputModel>> GET_Fee_DiscountRemarksList(FeeDiscountRemarksParaModel feeDiscountRemarksParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/MasterDataSetUp/GET_Fee_DiscountRemarksList?SchoolCode={feeDiscountRemarksParaModel.schoolCode}" +
                    $"&FinancialYear={feeDiscountRemarksParaModel.financialYear}"
                    + $"&FeeDiscountID={feeDiscountRemarksParaModel.feeDiscountID}" +
                    $"&reportType={feeDiscountRemarksParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<FeeDiscountRemarksOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        
    }
}
