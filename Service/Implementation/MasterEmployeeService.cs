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
    public class MasterEmployeeService : IMasterEmployeeService
    {
        private readonly HttpClient httpClient;

        public MasterEmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterEmployeeListModel>> GetMasterEmployeeList(int SchoolId, int MasterEmployeeId, int Status, string OperationType)
        {
            //MasterEmployeeList[] MasterEmployeeList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterEmployee/GetMasterEmployeeList?SchoolId={SchoolId}&MasterEmployeeId={MasterEmployeeId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterEmployeeListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterEmployee(MasterEmployeeModel MasterEmployeeModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterEmployee/AddUpdateMasterEmployee", MasterEmployeeModel);
            return response;

        }
    }
}
