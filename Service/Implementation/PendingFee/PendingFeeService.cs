using AdminDashboard.Server.Models.PendingFee;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AdminDashboard.Server.Service.Interface.PendingFee;
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

namespace AdminDashboard.Server.Service.Implementation.PendingFee
{
    public class PendingFeeService: IPendingFeeService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public PendingFeeService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }


        public async Task<StudentPendingFeeModel> GetStudentPendingFee(string SchoolCode, string FinancialYear,  int Userid, string OperationType)
        {
             
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {//
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            var r = await httpClient.GetJsonAsync<Response>($"/api/PendingFee/GetPendingFeeDetails?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&StudentId={Userid}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            { 
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentPendingFeeModel)) as StudentPendingFeeModel;
            }
            else
            {
                return null;
            }
        }

        
    }
}
