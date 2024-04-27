using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AIS.Data.Model;
using AdminDashboard.Server.API_Service.Interface.QuestionSetup;

namespace AdminDashboard.Server.API_Service.Service.QuestionSetup
{
    public class QuestionSetupService : IQuestionsetup
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public QuestionSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_QuestionBank(QuestionBankAPIModel questionBankAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/QuestionSetUp/CRUD_QuestionBank", questionBankAPIModel);
            return response;
        }
        public async Task<IEnumerable<QuestionBankListModel>> GET_QuestionList(QuestionBankList_ParaModel questionBankList_ParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/QuestionSetUp/GET_QuestionList?SchoolCode={questionBankList_ParaModel.schoolCode}" +
                    $"&FinancialYear={questionBankList_ParaModel.financialYear}" +
                    $"&ClassId={questionBankList_ParaModel.classId}" +
                    $"&subjectId={questionBankList_ParaModel.subjectId}" +
                    $"&UnitId={questionBankList_ParaModel.unitId}" +
                    $"&ChapterId={questionBankList_ParaModel.chapterId}" +
                    $"&topicId={questionBankList_ParaModel.topicId}" +
                    $"&questiontypeID={questionBankList_ParaModel.questionTypeId}" +
                    $"&userRoleId={questionBankList_ParaModel.userRoleId}" +
                    $"&ReportType={questionBankList_ParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<QuestionBankListModel[]>(el.GetRawText());
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

        public async Task<APIReturnModel> CRUD_SelectedQuestionPaper(List<SelectedQuestionListAPIModel> selectedQuestionListModels)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/QuestionSetUp/CRUD_QuestionPaperSetup", selectedQuestionListModels);
            return response;
        }

        public async Task<IEnumerable<QuestionPaperListOutPutModel>> GET_QuestionPaperList(QuestionPaperList_ParaModel questionPaperList_ParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/QuestionSetUp/GET_QuestionPaperList?SchoolCode={questionPaperList_ParaModel.schoolCode}" +
                    $"&FinancialYear={questionPaperList_ParaModel.financialYear}" +
                    $"&ClassId={questionPaperList_ParaModel.classId}" +
                    $"&subjectId={questionPaperList_ParaModel.subjectId}" +
                    $"&useriD={questionPaperList_ParaModel.useriD}" +
                    $"&userRoleId={questionPaperList_ParaModel.userRoleId}" + 
                    $"&ReportType={questionPaperList_ParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<QuestionPaperListOutPutModel[]>(el.GetRawText());
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

        public async Task<IEnumerable<QuestionPaperDetailsListOutPutModel>> GET_QuestionPaperDetailsList(QuestionPaperList_ParaModel questionPaperList_ParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/QuestionSetUp/GET_QuestionPaperDetailsList?SchoolCode={questionPaperList_ParaModel.schoolCode}" +
                    $"&FinancialYear={questionPaperList_ParaModel.financialYear}" +
                    $"&ClassId={questionPaperList_ParaModel.classId}" +
                    $"&subjectId={questionPaperList_ParaModel.subjectId}" +
                    $"&useriD={questionPaperList_ParaModel.useriD}" +
                    $"&userRoleId={questionPaperList_ParaModel.userRoleId}" +
                    $"&ReportType={questionPaperList_ParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<QuestionPaperDetailsListOutPutModel[]>(el.GetRawText());
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
    }
}
         