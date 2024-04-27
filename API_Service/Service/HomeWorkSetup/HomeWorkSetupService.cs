using AdminDashboard.Server.API_Service.Interface.HomeWork;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.HomeWorkSetUp;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.HomeWork
{
    public class HomeWorkSetupService: IHomeWorkSetupService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public HomeWorkSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_HomeWorkSetup(HomeWorkSetUpModel homeWorkSetUpModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/HomeWorkSetUp/CRUD_HomeWorkSetup", homeWorkSetUpModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_HomeWorkType(HomeWorkTypeAPIModel homeWorkTypeAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/HomeWorkSetUp/CRUD_HomeWork_Type", homeWorkTypeAPIModel);
            return response;
        }
        public async Task<IEnumerable<HomeWork_List_Output_Model>> GET_HOMEWORK_LIST(HomeWork_List_Input_Para_Model homeWork_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/HomeWorkSetUp/GET_HOMEWORK_LIST?SchoolCode={homeWork_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={homeWork_List_Input_Para_Model.financialYear}" +
                    $"&ClassId={homeWork_List_Input_Para_Model.classId}" +
                    $"&SubjectId={homeWork_List_Input_Para_Model.subjectId}" +
                    $"&UserId={homeWork_List_Input_Para_Model.userId}" +
                    $"&FromDate={homeWork_List_Input_Para_Model.fromDate}" +
                    $"&toDate={homeWork_List_Input_Para_Model.toDate}" +
                    $"&reportType={homeWork_List_Input_Para_Model.reportType}");

                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<HomeWork_List_Output_Model[]>(el.GetRawText());
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

        public async Task<IEnumerable<HomeWorkType_OutPut_Model>> GET_HomeWorkType_LIST(HomeWork_Type_Input_Para_Model homeWork_Type_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/HomeWorkSetUp/GET_HomeWork_Type_LIST?SchoolCode={homeWork_Type_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={homeWork_Type_Input_Para_Model.financialYear}" +
                    $"&homeWorkTypeID={homeWork_Type_Input_Para_Model.HomeWorkTypeId}" +                  
                    $"&reportType={homeWork_Type_Input_Para_Model.reportType}");

                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<HomeWorkType_OutPut_Model[]>(el.GetRawText());
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
