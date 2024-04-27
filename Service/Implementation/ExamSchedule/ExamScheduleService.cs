using AdminDashboard.Server.Models.ExamSchedule;
using AdminDashboard.Server.Service.Interface.ExamSchedule;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.ExamSchedule
{
    public class ExamScheduleService:IExamScheduleService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;

        public ExamScheduleService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<APIReturnModel> AddUpdateExamSchedule(ExamScheduleModel examScheduleModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/ExamSchedule/AddUpdateExamSchedule", examScheduleModel);
            return response;
        }

        public async Task<IEnumerable<ExamScheduleListModel>> GetExamScheduleList(string SchoolCode, string FinancialYear, int UserId, string OperationType)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                               
                var r = await httpClient.GetJsonAsync<Response>($"/api/ExamSchedule/GetExamScheduleList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&UserId={UserId}&OperationType={OperationType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<ExamScheduleListModel[]>(el.GetRawText());
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
