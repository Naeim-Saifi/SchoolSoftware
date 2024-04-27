using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.Academic;
using AIS.Model.MasterData;
using AIS.Model.SchoolSetup;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AIS.FrontEnd_07062021.Service.Implementation
{
    public class SchoolSetupService : ISchoolSetupService
    {
        private readonly HttpClient httpClient;

        public SchoolSetupService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

         

        public async Task<IEnumerable<SchoolListModel>> GetMasterSchoolList(int SchoolId, int MasterSchoolId, int Status, string SchoolCode,string OperationType)
        {
            //MasterSchoolSetupList[] MasterSchoolSetupList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            };
            //var r = await httpClient.GetJsonAsync<Response>($"/api/SchoolAPI/GetSchoolList?SchoolId={SchoolId}&MasterSchoolSetupId={MasterSchoolId}&Status={Status}&OperationType={OperationType}");
            var r = await httpClient.GetJsonAsync<Response>($"/api/SchoolAPI/GetSchoolList?SchoolId={SchoolId}&Status={Status}&SchoolCode={SchoolCode}&&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<SchoolListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }
        }

        public async Task<APIReturnModel> AddUpdateMasterSchool(SchoolModel masterSchoolModel)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SchoolAPI/AddUpdateSchool", masterSchoolModel);
            return response;
        }
    }
}
