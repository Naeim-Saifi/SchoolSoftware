using AdminDashboard.Server.API_Service.Interface.Employee;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.Employee;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.Employee
{
    public class EmployeeSetupService: IEmployeeSetupService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public EmployeeSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_UserDetails(Employee_API_Model employee_API_Model)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Employee/CRUD_UserDetails", employee_API_Model);
            return response;
        }


        public async Task<IEnumerable<Employee_User_List_Output_Model>> GET_USER_LIST(Employee_User_List_Input_Model employee_User_List_Input_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Employee/GET_USER_LIST?SchoolCode={employee_User_List_Input_Model.schoolCode}" +
                    $"&FinancialYear={employee_User_List_Input_Model.financialYear}" +
                    $"&ExamScheduleID={employee_User_List_Input_Model.userAccountID}" +
                    $"&ExamTypeId={employee_User_List_Input_Model.userRoleID}" + 
                    $"&reportType={employee_User_List_Input_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Employee_User_List_Output_Model[]>(el.GetRawText());
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
