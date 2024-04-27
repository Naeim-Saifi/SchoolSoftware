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
    public class MasterTopicService : IMasterTopicService
    {
        private readonly HttpClient httpClient;

        public MasterTopicService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterTopicList>> GetMasterTopicList(int SchoolId, int MasterTopicId, int Status, string OperationType)
        {
            //MasterTopicList[] MasterTopicList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterTopic/GetMasterTopicList?SchoolId={SchoolId}&MasterTopicId={MasterTopicId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterTopicList[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterTopic(MasterTopicModel MasterTopicModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterTopic/AddUpdateMasterTopic", MasterTopicModel);
            return response;

        }
    }
}
