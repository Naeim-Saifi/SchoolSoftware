using AdminDashboard.Server.Models.FeeConfigurationList;
using AdminDashboard.Server.Service.Interface.FeeConfiguration;
using AdminDashboard.Server.Service.Interface.LocalStorage;
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

namespace AdminDashboard.Server.Service.Implementation.FeeConfiguration
{
    public class FeeConfigurationListService: IFeeConfigurationListService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public FeeConfigurationListService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }


        public async Task<FeeConfigurationListModel> GetFeeConfigurationList(string SchoolCode, string FinancialYear, int Userid, string OperationType)
        {

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {//
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var r = await httpClient.GetJsonAsync<Response>($"api/FeeConfiguration/GetFeeConfigurationList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear }& UserId={Userid}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(FeeConfigurationListModel)) as FeeConfigurationListModel;
            }
            else
            {
                return null;
            }
        }

    }
}
