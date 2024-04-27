using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusDetails;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AIS.Data.BaseClass;
using AIS.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.MasterConfiguration.Syllabus
{
    public class UnitMasterService: IUnitMasterService
    {
        private readonly HttpClient httpClient;

        public UnitMasterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
       
        public async Task<APIReturnModel> AddUpdateUnitMaster(MasterUnitAPIModel  unitMasterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Syllabus/AddUpdate_UnitMaster", unitMasterModel);
            return response;

        }

        public async Task<APIReturnModel> AddUpdateChapterMaster(MasterChapterAPIModel chapterMasterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Syllabus/AddUpdate_ChapterMaster", chapterMasterModel);
            return response;

        }
        public async Task<APIReturnModel> AddUpdateTopicMaster(MasterTopicAPIModel topicMasterModel1)
        {
            APIReturnModel response;
            try
            {
                response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Syllabus/AddUpdate_TopicMaster", topicMasterModel1);
                return response;
            }
            catch(Exception ex)
            {
               Console.WriteLine(ex.Message);
                //return response.responseCode = ex.Message;
            }
            response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Syllabus/AddUpdate_TopicMaster", topicMasterModel1);
            return response;
            //return response.status=true;

        }

        public async Task<IEnumerable<MasterUnitListModel>> GetUnitMasterDetails(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/Syllabus/Get_UnitMasterList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
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
        public async Task<UnitMasterListDetailsModel> GetUnitMasterDetailsList(string SchoolCode, string FinancialYear, string OperationType)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {//
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/Syllabus/Get_UnitMasterDetailsList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(UnitMasterListDetailsModel)) as UnitMasterListDetailsModel;
                
            }
            else
            {
                return null;
            }

        }

        public async Task<IEnumerable<MasterChapterListModel>> GetChapterMasterDetails(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/Syllabus/Get_ChapterMasterList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterChapterListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<IEnumerable<MasterTopicListModel>> GetTopicMasterDetails(DefaultInputParameterModel defaultInputParameterModel)
        {
            //ClassMasterList[] ClassMasterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/Syllabus/Get_topicMasterList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&OperationType={defaultInputParameterModel.OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterTopicListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

    }
}
