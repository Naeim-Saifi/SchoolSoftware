using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.UserLogin;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
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
    public class MasterBatchService : IMasterBatchService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public MasterBatchService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<MasterBatchListModel>> GetMasterBatchList(string SchoolCode,int SchoolId, int MasterBatchId, int Status, string OperationType)
        {
            //MasterClassList[] masterClassList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");

            int SID = user.schoolId;

            var r = await httpClient.GetJsonAsync<Response>($"api/MasterBatch/GetMasterBatchList?SchoolCode={SchoolCode}&SchoolId={SchoolId}&MasterBatchId={MasterBatchId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterBatchListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterBatch(MasterBatchModel masterBatchModel)
        {
           
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("api/MasterBatch/AddUpdateMasterBatch", masterBatchModel);
            return response;

        }

        public async Task<MasterBatchViewModel> GetMasterBatchByID(int BatchId)
        {

            MasterBatchViewModel response = await httpClient.PostJsonAsync<MasterBatchViewModel>("api/MasterBatch/AddUpdateMasterBatch", BatchId);
            return response;

        }
    }
}
