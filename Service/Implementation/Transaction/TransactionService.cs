using AdminDashboard.Server.Models.Transaction;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AdminDashboard.Server.Service.Interface.Transaction;
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

namespace AdminDashboard.Server.Service.Implementation.Transaction
{
    public class TransactionService:ITransactionService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public TransactionService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

       
        public async Task<TransactionDetailsModel> GetStudentTransactionDateRange(string SchoolCode, string FinancialYear, string fromDate, string toDate, string StudentId, string OperationType)
        {
            
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {//
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
             
           
            var r = await httpClient.GetJsonAsync<Response>($"/api/Transaction/GetStudentTransactionListdateRange?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&fromDate={fromDate}&toDate={toDate}&StudentId={StudentId}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                //return System.Text.Json.JsonSerializer.Deserialize<StudentDashboardModel[]>(el.GetRawText());
                //return System.Text.Json.JsonSerializer.Deserialize<List<StudentDashboardModel>>(el.GetRawText());
                // return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(List<StudentDashboardModel>)) as List<StudentDashboardModel>;
                //var x = System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentDashboardModel));
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(TransactionDetailsModel)) as TransactionDetailsModel;

            }
            else
            {
                return null;
            }
        }
    }
}
