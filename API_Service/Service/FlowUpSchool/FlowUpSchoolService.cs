using AdminDashboard.Server.API_Service.Interface.FlowUpSchool;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.FlowUpSchool;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.FlowUpSchool
{
    public class FlowUpSchoolService: IFlowUpSchoolService
    {

        private readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorageService;
        public FlowUpSchoolService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_FlowUpSchoolDetails(FlowUpSchoolInputModel flowUpSchoolInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SchoolFlowUp/FlowUpSchoolDetails", flowUpSchoolInputModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_FlowUpStatusDetails(FlowUpStatusDetailsInputModel flowUpStatusDetailsInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SchoolFlowUp/FlowUpDetails", flowUpStatusDetailsInputModel);
            return response;
        }

        public async Task<IEnumerable<FlowUpSchoolOutPutModel>> GET_FlowUp_SchoolList(FlowUpSchool_Input_Para_Model flowUpSchool_Input_Para_Model)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SchoolFlowUp/GET_FlowUp_SchoolList?SchoolCode={flowUpSchool_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={flowUpSchool_Input_Para_Model.financialYear}" +
                    $"&userID={flowUpSchool_Input_Para_Model.UserID}" +
                    $"&reportType={flowUpSchool_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<FlowUpSchoolOutPutModel[]>(el.GetRawText());
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

        public async Task<IEnumerable<FlowUpDetailsOutPutModel>> GET_FlowUp_Details(FlowUpDetails_Input_Para_Model flowUpDetails_Input_Para_Model)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SchoolFlowUp/GET_FlowUp_Details?SchoolCode={flowUpDetails_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={flowUpDetails_Input_Para_Model.financialYear}" +
                    $"&holidayTypeId={flowUpDetails_Input_Para_Model.schoolID}" +
                    $"&reportType={flowUpDetails_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<FlowUpDetailsOutPutModel[]>(el.GetRawText());
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
