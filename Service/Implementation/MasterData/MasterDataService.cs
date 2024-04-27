using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Service.Interface.MasterData;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.MasterData
{
    public class MasterDataService: IMasterDataService
    {
        private readonly HttpClient httpClient;

        public MasterDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<APIReturnModel> AddUpdateDeleteMasterClass(MasterClassAPIInputModel masterClassAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterClass", masterClassAPIInputModel);
            return response;

        }
        public async Task<IEnumerable<MasterClassListModel>> GetMasterClassList(DefaultInputParameterModel defaultInputModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterClassList?SchoolCode={defaultInputModel.SchoolCode}&FinancialYear={defaultInputModel.FinancialYear}&OperationType={defaultInputModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterClassListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteSectionMaster(MasterSectionAPIInputModel masterSectionAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterSection", masterSectionAPIInputModel);
            return response;

        }
       
        public async Task<IEnumerable<MasterSectionListModel>> GetSectionList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterSectionList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterSectionListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteBatchMaster(MasterBatchAPIInputModel masterBatchAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterBatch", masterBatchAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterBatchListModel>> GetBatchList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterBatchList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterBatchListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteDepartmentMaster(MasterDepartmentAPIInputModel masterDepartmentAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterDepartment", masterDepartmentAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterDepartmentListModel>> GetDepartmentList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterDepartmentList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterDepartmentListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }
        public async Task<APIReturnModel> AddUpdateDeleteDocumentMaster(MasterDocumentAPIInputModel masterDocumentAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterDocument", masterDocumentAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterDocumentListModel>> GetDocumentList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterDocumentList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterDocumentListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }
        public async Task<APIReturnModel> AddUpdateDeleteEmpolyeeMaster(MasterEmployeeAPIInputModel masterEmployeeAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterEmployee", masterEmployeeAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterEmployeeListModel>> GetEmployeeList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterEmployeeList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterEmployeeListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }
        public async Task<APIReturnModel> AddUpdateDeleteOccuptionMaster(MasterOccuptionAPIInputModel masterOccuptionAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterOccupation", masterOccuptionAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterOccuptionListModel>> GetOccuptionList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterOccuptionList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterOccuptionListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }
        public async Task<APIReturnModel> AddUpdateDeleteStateMaster(MasterStateAPIInputModel masterStateAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterState", masterStateAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterStateListModel>> GetStateList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterStateList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterStateListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }
        public async Task<APIReturnModel> AddUpdateDeleteSubjectMaster(MasterSubjectAPIInputModel masterSubjectAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterSubject", masterSubjectAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MastersSubjectListModel>> GetSubjectList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterSubjectList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MastersSubjectListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }
        //11-04-23 farhat
        public async Task<APIReturnModel> AddUpdateDeleteMasterBusStopWithRouteDetails(MasterBusStopWithRouteDetailsAPIInputModel masterBusStopWithRouteDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterBusStopWithRouteDetails", masterBusStopWithRouteDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterBusStopWithRouteDetailsListModel>> GetMasterBusStopWithRouteDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterBusStopWithRouteDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterBusStopWithRouteDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterConcessionDetails(MasterConcessionDetailsAPIInputModel masterConcessionDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterConcessionDetails", masterConcessionDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterConcessionDetailsListModel>> GetMasterConcessionDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterConcessionDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterConcessionDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterDiscountDetails(MasterDiscountDetailsAPIInputModel masterDiscountDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterDiscountDetails", masterDiscountDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterDiscountDetailsListModel>> GetMasterDiscountDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterDiscountDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterDiscountDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterFeeDetails(MasterFeeDetailsAPIInputModel masterFeeDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterFeeDetails", masterFeeDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterFeeDetailsListModel>> GetMasterFeeDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterFeeDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterFeeDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterExamTypeDetails(MasterExamTypeDetailsAPIInputModel masterExamTypeDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterExamTypeDetails", masterExamTypeDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterExamTypeDetailsListModel>> GetMasterExamTypeDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterExamTypeDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterExamTypeDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterItemDetails(MasterItemDetailsAPIInputModel masterItemDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterItemDetails", masterItemDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterItemDetailsListModel>> GetMasterItemDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterItemDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterItemDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterStudentGatePassDetails(MasterStudentGatePassDetailsAPIInputModel masterStudentGatePassDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterStudentGatePassDetails", masterStudentGatePassDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterStudentGatePassDetailsListModel>> GetMasterStudentGatePassDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterStudentGatePassDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterStudentGatePassDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterVehicleDetails(MasterVehicleDetailsAPIInputModel masterVehicleDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterVehicleDetails", masterVehicleDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterVehicleDetailsListModel>> GetMasterVehicleDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterVehicleDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterVehicleDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterVenderDetails(MasterVenderDetailsAPIInputModel masterVenderDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterVenderDetails", masterVenderDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterVenderDetailsListModel>> GetMasterVenderDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterVenderDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterVenderDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateDeleteMasterFeeTermDetails(MasterFeeTermDetailsAPIInputModel masterFeeTermDetailsAPIInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterData/AddUpdateDeleteMasterFeeTermDetails", masterFeeTermDetailsAPIInputModel);
            return response;

        }

        public async Task<IEnumerable<MasterFeeTermDetailsListModel>> GetMasterFeeTermDetailsList(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterFeeTermDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterFeeTermDetailsListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }






    }
}
