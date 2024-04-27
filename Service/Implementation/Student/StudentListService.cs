using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.UserLogin;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.MasterData;
using AIS.Model.Student;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AIS.FrontEnd_07062021.Service.Implementation
{
    public class StudentListService : IStudentListService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public StudentListService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<StudentDetailsListModel> GetStudentDetailsList(string SchoolCode, string FinancialYear, int ClassID, int SectionID, int StudentId, string OperationType)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {//
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/Student/GetStudentDetailsList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&ClassID={ClassID}&SectionID={SectionID}&StudentId={StudentId}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                //return System.Text.Json.JsonSerializer.Deserialize<StudentDashboardModel[]>(el.GetRawText());
                //return System.Text.Json.JsonSerializer.Deserialize<List<StudentDashboardModel>>(el.GetRawText());
                // return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(List<StudentDashboardModel>)) as List<StudentDashboardModel>;
                //var x = System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentDashboardModel));
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentDetailsListModel)) as StudentDetailsListModel;

            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<StudentListModel>> GetStudentList(int ClassId, int SectionId, int SchoolId, int StudentId, int StatusId, string OperationType)
        {
            //MasterClassList[] masterClassList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");

            int SID = user.schoolId;

           // var r = await httpClient.GetJsonAsync<Response>($"/api/Student/GetStudentList?SchoolId={SchoolId}&StudentListId={StudentListId}&Status={Status}&OperationType={OperationType}");
            var r = await httpClient.GetJsonAsync<Response>($"/api/Student/GetStudentList?ClassId={ClassId}&SectionId={SectionId}&SchoolId={SID}&StudentId={StudentId}&StatusId=={StatusId}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<StudentListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

       
    }
}
