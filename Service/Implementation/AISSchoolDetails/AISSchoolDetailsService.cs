using AdminDashboard.Server.Service.Interface.AISSchoolDetails;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.RequestResponseModel.AISSchoolDetails;
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

namespace AdminDashboard.Server.Service.Implementation.AISSchoolDetails
{
    public class AISSchoolDetailsService: IAISSchoolDetailsService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;

        public AISSchoolDetailsService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<APIReturnModel> AddUpdateSchoolDetails(SchoolDetailsModel enquiryModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/AISSchoolDetails/AddUpdateSchoolDetails", enquiryModel);
            return response;
        }
        public async Task<IEnumerable<SchoolDetailsListModel>> GetSchoolDetailsList(InputModel  inputModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                // var r = await httpClient.GetJsonAsync<Response>($"/api/ExamSchedule/GetExamScheduleList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&UserId={UserId}&OperationType={OperationType}");

                var r = await httpClient.GetJsonAsync<Response>($"/api/AISSchoolDetails/GetSchoolListDetails?SchoolCode={inputModel.SchoolCode}&FinancialYear={inputModel.FinancialYear}&FromDate={inputModel.fromDate}" +
                    $"&ToDate={inputModel.toDate}&userID={inputModel.UserId}&ReportType={inputModel.OperationType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
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
 
