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
    public class ClassMasterService : IClassMasterService
    {
        private readonly HttpClient httpClient;

        public ClassMasterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ClassMasterListModel>> GetClassMasterList(string SchoolCode, string FinancialYear, int SchoolId, int ClassMasterId, int Status, string OperationType)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/ClassMaster/GetClassMasterList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&&SchoolId={SchoolId}&ClassMasterId={ClassMasterId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<ClassMasterListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateClassMaster(ClassMasterModel ClassMasterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/ClassMaster/AddUpdateClassMaster", ClassMasterModel);
            return response;

        }
    }
}
