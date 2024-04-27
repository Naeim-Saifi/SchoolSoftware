using AdminDashboard.Server.Models.HomeWork;
using AdminDashboard.Server.Service.Interface.HomeWork;
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

namespace AdminDashboard.Server.Service.Implementation.HomeWork
{
    public class HomeWorkService : IHomeWorkService
    {
        private readonly HttpClient httpClient;

        public HomeWorkService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<APIReturnModel> AddUpdateHomeWork(HomeWorkModel homeWorkModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/HomeWork/AddUpdateHomeWork", homeWorkModel);
            return response;

        }
        public async Task<IEnumerable<HomeworkListModel>> GetHomeWorkList(string SchoolCode, string FinancialYear, string fromDate, string toDate, int userId, string OperationType)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/HomeWork/GetHomeWorkList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&fromDate={fromDate}&toDate={toDate}&UserId={userId}&OperationType={OperationType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<HomeworkListModel[]>(el.GetRawText());
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
