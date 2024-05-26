using AdminDashboard.Server.API_Service.Interface.RemarksTypeMaster;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.RemarksTypeMaster;
using AIS.Data.RequestResponseModel.StudentSetUp;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using AIS.Data.Model;

namespace AdminDashboard.Server.API_Service.Service.RemarksTypeMaster
{
    public class RemarksTypeMasterService: IRemarksTypeMasterService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorageService;
        public RemarksTypeMasterService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_RemarksType(RemarksTypeAPIModel remarksTypeAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/RemarksTypeMaster/CRUD_RemarksType", remarksTypeAPIModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_RemarksTypeDescription(RemarksTypeDescriptionAPIModel remarksTypeDescriptionAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/RemarksTypeMaster/CRUD_RemarksTypeDescription", remarksTypeDescriptionAPIModel);
            return response;
        }
        public async Task<IEnumerable<RemarksTypeListModel>> Get_RemarksTypeList_List(RemarksType_Input_Para_Model remarksType_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/RemarksTypeMaster/Get_RemarksTypeList_List?SchoolCode={remarksType_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={remarksType_Input_Para_Model.financialYear}" +
                    $"&remarkstypeID={remarksType_Input_Para_Model.remarksTypeID}" +
                    $"&reportType={remarksType_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<RemarksTypeListModel[]>(el.GetRawText());
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
        public async Task<IEnumerable<RemarksTypeDescriptionListModel>> Get_RemarksTypeDescription(RemarksTypeDescription_Input_Para_Model remarksTypeDescription_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/RemarksTypeMaster/Get_RemarksTypeDescription?SchoolCode={remarksTypeDescription_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={remarksTypeDescription_Input_Para_Model.financialYear}" +
                    $"&remarkstypeDescriptionID={remarksTypeDescription_Input_Para_Model.RemarksTypeDescriptionID}" +
                    $"&reportType={remarksTypeDescription_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<RemarksTypeDescriptionListModel[]>(el.GetRawText());
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
