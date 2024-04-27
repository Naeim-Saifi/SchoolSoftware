using AdminDashboard.Server.API_Service.Interface.Attendance;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Attendance;
using AIS.Data.RequestResponseModel.SyllabusVideo;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AIS.Data.RequestResponseModel.HolidaySetup;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Serialization;
using AIS.Data.Model;

namespace AdminDashboard.Server.API_Service.Service.Attendance
{
    public class AttendanceService: IAttendanceService
    {
        private readonly HttpClient httpClient;
        public AttendanceService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<APIReturnModel> CRUD_StudentAttendance(List<AttendanceAPIModel> attendanceAPIModels)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Attendance/CRUD_Student_Attendance", attendanceAPIModels);
            return response;
        }
        public async Task<IEnumerable<TodayAttendanceList>> GET_StudentAttendanceList(AttendanceInputParaModel attendanceInputParaModel)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Attendance/GET_StudentAttendanceList?SchoolCode={attendanceInputParaModel.schoolCode}" +
                    $"&FinancialYear={attendanceInputParaModel.financialYear}" +
                    $"&classId={attendanceInputParaModel.classId}" +
                    $"&sectionId={attendanceInputParaModel.sectionId}" +
                    $"&userid={attendanceInputParaModel.userid}" +
                    $"&userRoleId={attendanceInputParaModel.userRoleId}" +
                    $"&fromDate={attendanceInputParaModel.fromDate}" +
                    $"&toDate={attendanceInputParaModel.toDate}" +
                    $"&reportType={attendanceInputParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<TodayAttendanceList[]>(el.GetRawText());
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

        public async Task<IEnumerable<StudentAttendanceList_UserIDWise>> StudentAttendanceList_UserIDWise(AttendanceInputParaModel attendanceInputParaModel)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Attendance/GET_StudentAttendanceList?SchoolCode={attendanceInputParaModel.schoolCode}" +
                    $"&FinancialYear={attendanceInputParaModel.financialYear}" +
                    $"&classId={attendanceInputParaModel.classId}" +
                    $"&sectionId={attendanceInputParaModel.sectionId}" +
                    $"&userid={attendanceInputParaModel.userid}" +
                    $"&userRoleId={attendanceInputParaModel.userRoleId}" +
                    $"&fromDate={attendanceInputParaModel.fromDate}" +
                    $"&toDate={attendanceInputParaModel.toDate}" +
                    $"&reportType={attendanceInputParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<StudentAttendanceList_UserIDWise[]>(el.GetRawText());
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

        public async Task<IEnumerable<StudentAttendanceList_MonthWise>> StudentAttendanceList_MonthWise(AttendanceInputParaModel attendanceInputParaModel)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Attendance/GET_StudentAttendanceList?SchoolCode={attendanceInputParaModel.schoolCode}" +
                    $"&FinancialYear={attendanceInputParaModel.financialYear}" +
                    $"&classId={attendanceInputParaModel.classId}" +
                    $"&sectionId={attendanceInputParaModel.sectionId}" +
                    $"&userid={attendanceInputParaModel.userid}" +
                    $"&userRoleId={attendanceInputParaModel.userRoleId}" +
                    $"&fromDate={attendanceInputParaModel.fromDate}" +
                    $"&toDate={attendanceInputParaModel.toDate}" +
                    $"&reportType={attendanceInputParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<StudentAttendanceList_MonthWise[]>(el.GetRawText());
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