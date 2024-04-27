using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.Academic;
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
    public class MasterUnitService : IMasterUnitService
    {
        private readonly HttpClient httpClient;

        public MasterUnitService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterUnitListModel>> GetMasterUnitList(int SchoolId, int MasterUnitId, int Status, string OperationType)
        {
            //MasterUnitList[] MasterUnitList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterUnit/GetMasterUnitList?SchoolId={SchoolId}&MasterUnitId={MasterUnitId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterUnitListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterUnit(MasterUnitModel MasterUnitModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterUnit/AddUpdateMasterUnit", MasterUnitModel);
            return response;

        }
    }
}
