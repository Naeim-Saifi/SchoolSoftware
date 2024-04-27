using AdminDashboard.Server.API_Service.Interface.HolidaySetUp;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.HolidaySetup;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.HolidaySetUp
{
    public class HolidaySetupService:IHolidaySetUpService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public HolidaySetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_HolidaySetup(HolidaySetUpModel holidaySetUpModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/HolidaySetUp/CRUD_HolidaySetup", holidaySetUpModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_HolidayTypeList(HolidayModel holidayModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/HolidaySetUp/CRUD_HolidayTypeList", holidayModel);
            return response;
        }
        public async Task<IEnumerable<Holiday_SetUp_List_Output_Model>> GET_HolidaySetup_LIST(Holiday_SetUp_List_Input_Para_Model holiday_SetUp_List_Input_Para_Model)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/HolidaySetUp/GET_HolidaySetup_LIST?SchoolCode={holiday_SetUp_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={holiday_SetUp_List_Input_Para_Model.financialYear}" +
                    $"&holidayTypeId={holiday_SetUp_List_Input_Para_Model.HolidayDetailsId }" +
                    $"&reportType={ holiday_SetUp_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Holiday_SetUp_List_Output_Model[]>(el.GetRawText());
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
        public async Task<IEnumerable<Holiday_Type_List_Output_Model>> GET_HolidayType_LIST(Holiday_Type_List_Input_Para_Model holiday_Type_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/HolidaySetUp/GET_HolidayType_LIST?SchoolCode={holiday_Type_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={holiday_Type_List_Input_Para_Model.financialYear}" +
                    $"&holidayTypeId={holiday_Type_List_Input_Para_Model.holidayTypeId}" +
                    $"&reportType={holiday_Type_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Holiday_Type_List_Output_Model[]>(el.GetRawText());
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
