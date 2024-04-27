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
    public class MasterOccupationService : IMasterOccupationService
    {
        private readonly HttpClient httpClient;

        public MasterOccupationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterOccupationListModel>> GetMasterOccupationList(int SchoolId, int MasterOccupationId, int Status, string OperationType)
        {
            //MasterOccupationList[] MasterOccupationList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterOccupation/GetMasterOccupationList?SchoolId={SchoolId}&MasterOccupationId={MasterOccupationId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterOccupationListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterOccupation(MasterOccupationModel MasterOccupationModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterOccupation/AddUpdateMasterOccupation", MasterOccupationModel);
            return response;

        }
    }
}
