using AdminDashboard.Server.Models.Dashboard.Student;
using AdminDashboard.Server.Service.Interface.Dashboard.Student;
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

namespace AdminDashboard.Server.Service.Implementation.Dashboard.Student
{
    public class StudentDashboardService: IStudentDashboardService
    {

        private readonly HttpClient httpClient;

        public StudentDashboardService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

       

        public async Task <StudentDashboardModel> StudentDashboard(string SchoolCode, string FinancialYear ,int UserID, string OperationType)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {//
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/StudentDashboard/GetStudentDashboardList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&userId={UserID}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                //return System.Text.Json.JsonSerializer.Deserialize<StudentDashboardModel[]>(el.GetRawText());
                //return System.Text.Json.JsonSerializer.Deserialize<List<StudentDashboardModel>>(el.GetRawText());
                // return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(List<StudentDashboardModel>)) as List<StudentDashboardModel>;
                //var x = System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentDashboardModel));
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentDashboardModel)) as StudentDashboardModel;
               
            }
            else
            {
                return null;
            }
            
        }
    }
}
