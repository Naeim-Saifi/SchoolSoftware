using AdminDashboard.Server;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.MasterData;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AIS.FrontEnd_07062021.Service.Implementation
{
    public class MasterClassService : IMasterClassService
    {
        private readonly HttpClient httpClient;

        public MasterClassService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterClassListModel>> GetMasterClassList(string SchoolCode, string FinancialYear, int SchoolId, int MasterClassId, int Status, string OperationType)
        {
            //MasterClassList[] masterClassList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            //var r = await httpClient.GetJsonAsync<Response>($"/api/MasterClass/GetMasterClassList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&SchoolId={SchoolId}&MasterClassId={MasterClassId}&Status={Status}&OperationType={OperationType}");
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterData/GetMasterClassList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&OperationType={OperationType}");

            
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterClassListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterClass(MasterClassModel masterClassModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterClass/AddUpdateMasterClass", masterClassModel);
            return response;

        }
    }
}
