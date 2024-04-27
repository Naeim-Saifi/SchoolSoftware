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
    public class MasterSubjectService : IMasterSubjectService
    {
        private readonly HttpClient httpClient;

        public MasterSubjectService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterSubjectListModel>> GetMasterSubjectList(int SchoolId, int MasterSubjectId, int Status, string OperationType)
        {
            //MasterSubjectList[] MasterSubjectList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterSubject/GetMasterSubjectList?SchoolId={SchoolId}&MasterSubjectId={MasterSubjectId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterSubjectListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterSubject(MasterSubjectModel MasterSubjectModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterSubject/AddUpdateMasterSubject", MasterSubjectModel);
            return response;

        }
    }
}
