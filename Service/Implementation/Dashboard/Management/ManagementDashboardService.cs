using AdminDashboard.Server.Models.Dashboard.Management;
using AdminDashboard.Server.Service.Interface.Dashboard.Management;
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

namespace AdminDashboard.Server.Service.Implementation.Dashboard.Management
{
    public class ManagementDashboardService: IManagementDashboardService
    {
        private readonly HttpClient httpClient;
        public ManagementDashboardService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ManagementDashboardModel> ManagementDashboard(string SchoolCode, string FinancialYear)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {//
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/StudentDashboard/GetManagementDashboardList?SchoolCode={SchoolCode}&FinancialYear={ FinancialYear}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                //return System.Text.Json.JsonSerializer.Deserialize<StudentDashboardModel[]>(el.GetRawText());
                //return System.Text.Json.JsonSerializer.Deserialize<List<StudentDashboardModel>>(el.GetRawText());
                // return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(List<StudentDashboardModel>)) as List<StudentDashboardModel>;
                //var x = System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentDashboardModel));
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(ManagementDashboardModel)) as ManagementDashboardModel;

            }
            else
            {
                return null;
            }

        }
    }
}
