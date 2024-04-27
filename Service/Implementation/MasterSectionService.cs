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
    public class MasterSectionService : IMasterSectionService
    {
        private readonly HttpClient httpClient;

        public MasterSectionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterSectionListModel>> GetMasterSectionList(string SchoolCode, string FinancialYear, int SchoolId, int MasterSectionId, int Status, string OperationType)
        {
            //MasterSectionList[] MasterSectionList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterSection/GetMasterSectionList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&SchoolId={SchoolId}&MasterSectionId={MasterSectionId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterSectionListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterSection(MasterSectionModel MasterSectionModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterSection/AddUpdateMasterSection", MasterSectionModel);
            return response;

        }
    }
}
