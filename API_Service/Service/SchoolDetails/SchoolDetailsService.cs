using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.School;
using AIS.Data.RequestResponseModel.SyllabusVideo;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using AIS.Data.Model;
using AdminDashboard.Server.API_Service.Interface.SchoolDetails;

namespace AdminDashboard.Server.API_Service.Service.SchoolDetails
{
    public class SchoolDetailsService:ISchoolDetailsService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public SchoolDetailsService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_SchoolDetails(SchoolDetailsAPIModel schoolDetailsAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SchoolDetails/CRUD_SchoolDetails", schoolDetailsAPIModel);
            return response;
        }
        public async Task<IEnumerable<SchoolDetailsListModel>> GET_School_List(SchoolDetails_Input_Para_Model schoolDetails_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SchoolDetails/Get_School_List?SchoolCode={schoolDetails_Input_Para_Model.schoolCode}" +
                   
                    $"&schoolID={schoolDetails_Input_Para_Model.schoolID}" +
                    $"&FinancialYear={schoolDetails_Input_Para_Model.financialYear}" +
                    $"&reportType={schoolDetails_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<SchoolDetailsListModel[]>(el.GetRawText());
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
