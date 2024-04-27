using AdminDashboard.Server.API_Service.Interface.Dashboard;
using AIS.Data.RequestResponseModel.Dashboard;
using AIS.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.Dashboard
{
    public class DashboardService:IDashboardService
    {
        private readonly HttpClient httpClient;
        public DashboardService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
       
        public async Task<StudentDashBoardOutPutModel> GET_StudentDashBoard_LIST(StudentDashboard_Input_Para_Model studentDashboard_Input_Para_Model)
        {
            
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/Dashboard/GET_StudentDashBoard_LIST?SchoolCode={studentDashboard_Input_Para_Model.schoolCode}" +
                $"&FinancialYear={studentDashboard_Input_Para_Model.financialYear}" +
                $"&fromdate={studentDashboard_Input_Para_Model.startDate}" +
                $"&endate={studentDashboard_Input_Para_Model.endDate}" +
                $"&userId={studentDashboard_Input_Para_Model.userId}" +
                $"&ReportType={studentDashboard_Input_Para_Model.reportType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                 return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentDashBoardOutPutModel)) as StudentDashBoardOutPutModel;

            }
            else
            {
                return null;
            }

        }
    }
}
