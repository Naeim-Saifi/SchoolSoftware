using AdminDashboard.Server.Models.UserRegistraion;
using AdminDashboard.Server.Service.Interface.AdminUserRegistration;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace AdminDashboard.Server.Service.Implementation.AdminUserRegistration
{
    public class AdminUserRegistrationService : IAdminUserRegistrationService
    {
        private readonly HttpClient httpClient;

        public AdminUserRegistrationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<APIReturnModel> AddUpdateAdminUserRegistration(AdminUserRegistrationModel userRegistrationModel)
        {
            try
            {
                string baseAddress = httpClient.BaseAddress.ToString();
                APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>(baseAddress+"api/AdminUserRegistration/AddUpdateAdminUserRegistration", userRegistrationModel);
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public async Task<IEnumerable<AdminUserRegistrationListModel>> GetAdminUserRegistrationList(string SchoolCode, int SchoolId, int Id, int Status, string OperationType)
        {
            //PaymentGatewayDetailList[] PaymentGatewayDetailList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string baseAddress = httpClient.BaseAddress.ToString();
            baseAddress += $"api/AdminUserRegistration/GetAdminUserAccountList?SchoolCode={SchoolCode}&SchoolId={SchoolId}&UserAccountId={Id}&Status={Status}&OperationType={OperationType}";
            var r = await httpClient.GetJsonAsync<Response>(baseAddress);
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<AdminUserRegistrationListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }
    }
}
