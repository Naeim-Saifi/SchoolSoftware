using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.UserLogin;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.Mapping;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AIS.FrontEnd_07062021.Service.Implementation
{
    public class MapClassWithSubjectService : IMapClassWithSubjectService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public MapClassWithSubjectService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<MapClassWithSubjectList>> GetMapClassWithSubjectList( int MapClassWithSubjectId, int Status, string OperationType)
        {
            //MasterClassList[] masterClassList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");

            int SchoolId = user.schoolId;

            var r = await httpClient.GetJsonAsync<Response>($"/api/MapClassWithSubject/GetMapClassWithSubjectList?SchoolId={SchoolId}&MapClassWithSubjectId={MapClassWithSubjectId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MapClassWithSubjectList[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMapClassWithSubject(MapClassWithSubjectModel mapClassWithSubjectModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("api/MasterBatch/AddUpdateMasterBatch", mapClassWithSubjectModel);
            return response;

        }

        
    }
}
