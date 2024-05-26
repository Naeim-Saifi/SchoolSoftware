using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AdminDashboard.Server.API_Service.Interface.StudentSetUp;

namespace AdminDashboard.Server.API_Service.Service.StudentSetUp
{
    public class StudentSetupService: IStudentSetupService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorageService;
        public StudentSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<Student_List_Output_Model>> GET_Student_List(Student_List_Input_Para_Model student_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/StudentSetUp/GET_Student_List?SchoolCode={student_List_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={student_List_Input_Para_Model.financialYear}" +
                    $"&ClassId={student_List_Input_Para_Model.classID}" +
                    $"&SectionId={student_List_Input_Para_Model.sectionID}" +
                    $"&BusStopId={student_List_Input_Para_Model.busStopID}" +
                    $"&userId={student_List_Input_Para_Model.userId}" +                   

                    $"&reportType={student_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Student_List_Output_Model[]>(el.GetRawText());
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

        public async Task<StudentGeneric_Output_Model> get_StudentGenericData_List(StudentGeneric_Input_Para_Model studentGeneric_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/StudentSetUp/Get_StudentGenericData_List?SchoolCode={studentGeneric_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={studentGeneric_Input_Para_Model.financialYear}" +
                    $"&id={studentGeneric_Input_Para_Model.iD}" +                    
                    $"&reportType={studentGeneric_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    //return System.Text.Json.JsonSerializer.Deserialize<StudentGeneric_Output_Model[]>(el.GetRawText());
                    return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(StudentGeneric_Output_Model)) as StudentGeneric_Output_Model;
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
