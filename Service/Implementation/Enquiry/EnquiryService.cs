using AdminDashboard.Server.Models.Enquiry;
using AdminDashboard.Server.Models.ExamSchedule;
using AdminDashboard.Server.Service.Interface.Enquiry;
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

namespace AdminDashboard.Server.Service.Implementation.Enquiry
{
     
        public class EnquiryService : IEnquiryService
    {
            private readonly HttpClient httpClient;
            private ILocalStorageService _localStorageService;

            public EnquiryService(HttpClient httpClient, ILocalStorageService localStorageService)
            {
                this.httpClient = httpClient;
                _localStorageService = localStorageService;
            }

            public async Task<APIReturnModel> AddUpdateEnquiry(EnquiryModel enquiryModel)
        {

                APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Enquiry/AddUpdateEnquiry", enquiryModel);
                return response;
            }

            public async Task<IEnumerable<EnquiryListModel>> GetEnquiryList(InputEnquiryModel inputEnquiryModel)
        {
                try
                {
                    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                // var r = await httpClient.GetJsonAsync<Response>($"/api/ExamSchedule/GetExamScheduleList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&UserId={UserId}&OperationType={OperationType}");
                 
                  var r = await httpClient.GetJsonAsync<Response>($"/api/Enquiry/GetEnquiryDetails?SchoolCode={inputEnquiryModel.schoolCode}&FinancialYear={inputEnquiryModel.financiyalYear}&FromDate={inputEnquiryModel.fromDate}" +
                      $"&ToDate={inputEnquiryModel.todate}&userID={inputEnquiryModel.userId}&ReportType={inputEnquiryModel.reportType}");
                JsonElement el;
                    var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                    if (r.isError == false)
                    {
                        return System.Text.Json.JsonSerializer.Deserialize<EnquiryListModel[]>(el.GetRawText());
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
