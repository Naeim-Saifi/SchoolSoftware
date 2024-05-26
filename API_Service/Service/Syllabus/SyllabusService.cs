using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.SyllabusVideo;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AdminDashboard.Server.API_Service.Service.Syllabus
{
    public class SyllabusService: ISyllabusService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorageService;
        public SyllabusService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_MasterUnit(UnitMasterModel unitMasterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SyllabusDetails/Syllabus_UnitDetails", unitMasterModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_MasterChapter(ChapterMasterModel chapterMasterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SyllabusDetails/Syllabus_Chapter", chapterMasterModel);
            return response;
        }

        public async Task<APIReturnModel> CRUD_MasterTopic(TopicMasterModel topicMasterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SyllabusDetails/Syllabus_Topic", topicMasterModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_SyllabusSetup(SyllabusSetupModel syllabusSetupModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SyllabusDetails/Syllabus_SyllabusSetUp", syllabusSetupModel);
            return response;
        }

        public async Task<APIReturnModel> CRUD_SyllabusVideoDetails(SyllabusVideoAPIModel syllabusVideoAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/SyllabusDetails/CRUD_SyllabusVideoDetails", syllabusVideoAPIModel);
            return response;
        }

        public async Task<IEnumerable<Unit_Output_Model>> Get_Unit_List(Unit_Input_Para_Model unit_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SyllabusDetails/Get_Unit_List?SchoolCode={unit_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={unit_Input_Para_Model.financialYear}" +
                    $"&ClasssId={unit_Input_Para_Model.classId}" +
                    $"&userRoleID={unit_Input_Para_Model.userRoleId}" +
                    $"&userID={unit_Input_Para_Model.userId}" +
                    $"&subjectId={unit_Input_Para_Model.subjectId}" +
                    $"&reportType={unit_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Unit_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<IEnumerable<Chapter_Output_Model>> Get_Chapter_List(Chapter_Input_Para_Model chapter_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SyllabusDetails/Get_Chapter_List?SchoolCode={chapter_Input_Para_Model.schoolCode}" +
                    
                    $"&ClassID={chapter_Input_Para_Model.classId}" +
                    $"&SubjectID={chapter_Input_Para_Model.subjectId}" +
                    $"&UnitID={chapter_Input_Para_Model.unitID}" +
                    $"&FinancialYear={chapter_Input_Para_Model.financialYear}" +
                    $"&reportType={chapter_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Chapter_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<IEnumerable<Topic_Output_Model>> Get_Topic_List(Topic_Input_Para_Model topic_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SyllabusDetails/Get_Topic_List?SchoolCode={topic_Input_Para_Model.schoolCode}" +
                    $"&ClassId={topic_Input_Para_Model.classId}" +
                    $"&subjectId={topic_Input_Para_Model.subjectId}" +
                    $"&unitID={topic_Input_Para_Model.chapterID}" +
                    $"&chapterID={topic_Input_Para_Model.chapterID}" +
                    $"&FinancialYear={topic_Input_Para_Model.financialYear}" +
                    $"&reportType={topic_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Topic_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<IEnumerable<Syllabus_Output_Model>> Get_Syllabus_List(Syllabus_Input_Para_Model syllabus_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SyllabusDetails/Get_Syllabus_List?SchoolCode={syllabus_Input_Para_Model.schoolCode}" +
                    $"&classId={syllabus_Input_Para_Model.classId}" +
                    $"&subjectId={syllabus_Input_Para_Model.subjectId}" +
                    $"&unitID={syllabus_Input_Para_Model.unitID}" +
                    $"&chapterID={syllabus_Input_Para_Model.chapterID}" +
                    $"&monthID={syllabus_Input_Para_Model.monthID}" +
                    $"&FinancialYear={syllabus_Input_Para_Model.financialYear}" +
                    $"&reportType={syllabus_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Syllabus_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<SyllabusStatus_Output_Model>> Get_SyllabusStatus_List(SyllabusStatus_Input_Para_Model syllabus_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SyllabusDetails/Get_Syllabus_List?SchoolCode={syllabus_Input_Para_Model.schoolCode}" +
                    $"&classId={syllabus_Input_Para_Model.classId}" +
                    $"&subjectId={syllabus_Input_Para_Model.subjectId}" +
                    $"&unitID={syllabus_Input_Para_Model.unitID}" +
                    $"&chapterID={syllabus_Input_Para_Model.chapterID}" +
                    $"&monthID={syllabus_Input_Para_Model.topicID}" +
                    $"&FinancialYear={syllabus_Input_Para_Model.financialYear}" +
                    $"&reportType={syllabus_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<SyllabusStatus_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<SyllabusVideo_Output_Model>> Get_SyllabusVideo_List(SyllabusVideo_Input_Para_Model syllabusVideo_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/SyllabusDetails/Get_SyllabusVideo_List?SchoolCode={syllabusVideo_Input_Para_Model.schoolCode}" +
                    $"&classId={syllabusVideo_Input_Para_Model.classId}" +
                    $"&subjectId={syllabusVideo_Input_Para_Model.subjectId}" +
                    $"&unitID={syllabusVideo_Input_Para_Model.unitID}" +
                    $"&chapterID={syllabusVideo_Input_Para_Model.chapterID}" +
                    $"&topicID={syllabusVideo_Input_Para_Model.topicID}" +
                    $"&userID={syllabusVideo_Input_Para_Model.userID}" +
                    $"&userRoleId={syllabusVideo_Input_Para_Model.userRoleId}" +
                    $"&FinancialYear={syllabusVideo_Input_Para_Model.financialYear}" +
                    $"&reportType={syllabusVideo_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<SyllabusVideo_Output_Model[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<List<MonthNameListModel>> GetMonthNameList()
        {
            var dayList = new List<MonthNameListModel>() {
                new MonthNameListModel(){ MonthId = "4", MonthName="April"},
                new MonthNameListModel(){ MonthId = "5", MonthName="May"},
                new MonthNameListModel(){ MonthId = "6", MonthName="June"},
                new MonthNameListModel(){ MonthId = "7", MonthName="July"},
                new MonthNameListModel(){ MonthId = "8", MonthName="August"},
                new MonthNameListModel(){ MonthId = "9", MonthName="September"},
                new MonthNameListModel(){ MonthId = "10", MonthName="October"},
                new MonthNameListModel(){ MonthId = "11", MonthName="November"},
                new MonthNameListModel(){ MonthId = "12", MonthName="December"},
                new MonthNameListModel(){ MonthId = "1", MonthName="January"},
                new MonthNameListModel(){ MonthId = "2", MonthName="February"},
                new MonthNameListModel(){ MonthId = "3", MonthName="March"},
               
            };

            return dayList;
        }

         
    }
}
