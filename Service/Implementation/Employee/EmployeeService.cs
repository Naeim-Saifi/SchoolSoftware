using AdminDashboard.Server.Service.Interface.Employee;
using AIS.Data.BaseClass;
using AIS.Model;
using AIS.Model.Employee;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.Employee
{
    public class EmployeeService: IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<APIReturnModel> AddUpdateEmployee(EmployeeApiModel  employeeModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Employee/AddUpdateEmployee", employeeModel);
            return response;

        }

        public async Task<IEnumerable<EmployeeDetailsModel>> GetemployeeDetails(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"api/Employee/GetEmployeeDetailsList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&userId={defaultInputParameterModel.Userid}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<EmployeeDetailsModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        //public Task<IEnumerable<EmployeeDetailsModel>> GetemployeeDetails(string SchoolCode, string userId, string OperationType)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
