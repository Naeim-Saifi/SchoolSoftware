using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.Exam
{
    public class  ExamMasterSetupService : IExamMasterSetupService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorageService;
        public ExamMasterSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_ExamTypeMaster(Exam_Type_Master_Model exam_Type_Master_Model)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/ExamMasterSetUp/CRUD_ExamTypeMaster", exam_Type_Master_Model);
            return response;
        }

        public async Task<APIReturnModel> CRUD_UnitWiseMarksEntry(List<UnitWiseMarksEntryAPIModel> unitWiseMarksEntryAPIModel)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/ExamMasterSetUp/CRUD_UnitWiseMarksEntry", unitWiseMarksEntryAPIModel);
            return response;
        }

        public async Task<APIReturnModel> CRUD_TermWiseExamSchedule(List<Exam_schedule_Details_Model> exam_schedule_Details_Model)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/ExamMasterSetUp/CRUD_TermWiseExamSchedule", exam_schedule_Details_Model);
            return response;
        }
        public async Task<APIReturnModel> CRUD_ExamScheduleDetails(Exam_schedule_Details_Model exam_schedule_Details_Model)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Holiday/AddUpdateHolidayDetails", exam_schedule_Details_Model);
            return response;
        }

        public async Task<IEnumerable<Exam_Type_List_Output_Model>> GET_Exam_Type_MasterLIST(Exam_Type_List_Input_Model exam_Type_List_Input_Model)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/ExamMasterSetUp/GET_Exam_Type_MasterLIST?SchoolCode={exam_Type_List_Input_Model.schoolCode}" +
                    $"&FinancialYear={exam_Type_List_Input_Model.financialYear}" +
                    $"&examTypeId={exam_Type_List_Input_Model.examTypeId}" +
                    $"&reportType={exam_Type_List_Input_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Exam_Type_List_Output_Model[]>(el.GetRawText());
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
        public async Task<UnitWiseMarksDetails> GET_UnitWiseMarksEntry(UnitWiseMasrks_Input_Model unitWiseMasrks_Input_Model)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };             

                var r = await httpClient.GetJsonAsync<Response>($"/api/ExamMasterSetUp/GET_UnitWiseMarksEntry_List?SchoolCode={unitWiseMasrks_Input_Model.schoolCode}" +
                    $"&FinancialYear={unitWiseMasrks_Input_Model.financialYear}" +
                    $"&examTypeId={unitWiseMasrks_Input_Model.examTypeId}" +
                    $"&classId={unitWiseMasrks_Input_Model.classId}" +
                    $"&sectionId={unitWiseMasrks_Input_Model.sectionId}" +
                    $"&monthId={unitWiseMasrks_Input_Model.monthId}" +
                    $"&subjectId={unitWiseMasrks_Input_Model.subjectId}" +
                    $"&unitId={unitWiseMasrks_Input_Model.unitId}" +
                    $"&chapterId={unitWiseMasrks_Input_Model.chapterId}" +
                    $"&topicID={unitWiseMasrks_Input_Model.topicID}" +
                    $"&userRoleId={unitWiseMasrks_Input_Model.userRoleId}" +
                   $"&userID={unitWiseMasrks_Input_Model.userID}" +
                   $"&reportType={unitWiseMasrks_Input_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {


                    //return System.Text.Json.JsonSerializer.Deserialize<UnitWiseMarksDetails[]>(el.GetRawText());
                    return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(UnitWiseMarksDetails)) as UnitWiseMarksDetails;
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
        public async Task<IEnumerable<Exam_Schedule_Details_List_Output_Model>> GET_Exam_Schedule_Details_LIST(Exam_Schedule_Details_List_Input_Model exam_Schedule_Details_List_Input_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/ExamMasterSetUp/GET_Exam_Schedule_Details_LIST?SchoolCode={exam_Schedule_Details_List_Input_Model.schoolCode}" +
                    $"&FinancialYear={exam_Schedule_Details_List_Input_Model.financialYear}" +
                    $"&ExamScheduleID={exam_Schedule_Details_List_Input_Model.examScheduleID}" +
                    $"&ExamTypeId={exam_Schedule_Details_List_Input_Model.examTypeId}" +                     
                    $"&classId={exam_Schedule_Details_List_Input_Model.classId}" +
                    $"&sectionId={exam_Schedule_Details_List_Input_Model.sectionId}" +
                    $"&subjectId={exam_Schedule_Details_List_Input_Model.subjectId}" +
                    $"&unitId={exam_Schedule_Details_List_Input_Model.unitId}" +
                    $"&chapterId={exam_Schedule_Details_List_Input_Model.chapterId}" +
                    $"&topicID={exam_Schedule_Details_List_Input_Model.topicID}" +
                    $"&userId={exam_Schedule_Details_List_Input_Model.userId}" +
                    $"&reportType={exam_Schedule_Details_List_Input_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Exam_Schedule_Details_List_Output_Model[]>(el.GetRawText());
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

        public async Task<IEnumerable<TermWiseExamSchedule_List_Output_Model>> GET_TermWiseExamSchedule_Details_LIST(Exam_Schedule_Details_List_Input_Model exam_Schedule_Details_List_Input_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/ExamMasterSetUp/GET_Exam_Schedule_Details_LIST?SchoolCode={exam_Schedule_Details_List_Input_Model.schoolCode}" +
                    $"&FinancialYear={exam_Schedule_Details_List_Input_Model.financialYear}" +
                    $"&ExamScheduleID={exam_Schedule_Details_List_Input_Model.examScheduleID}" +
                    $"&ExamTypeId={exam_Schedule_Details_List_Input_Model.examTypeId}" +
                    $"&classId={exam_Schedule_Details_List_Input_Model.classId}" +
                    $"&sectionId={exam_Schedule_Details_List_Input_Model.sectionId}" +
                    $"&subjectId={exam_Schedule_Details_List_Input_Model.subjectId}" +
                    $"&unitId={exam_Schedule_Details_List_Input_Model.unitId}" +
                    $"&chapterId={exam_Schedule_Details_List_Input_Model.chapterId}" +
                    $"&topicID={exam_Schedule_Details_List_Input_Model.topicID}" +
                    $"&userId={exam_Schedule_Details_List_Input_Model.userId}" +
                    $"&reportType={exam_Schedule_Details_List_Input_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<TermWiseExamSchedule_List_Output_Model[]>(el.GetRawText());
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

        public async Task<string> GetGradeDetails(decimal total_Obtain)
        {
            string vGrade1 = "";
            try
            {
                if (Math.Round(total_Obtain) >= 91 & Math.Round(total_Obtain) <= 100)
                {
                    vGrade1 = "A1";

                }
                else if (Math.Round(total_Obtain) >= 81 & Math.Round(total_Obtain) <= 90)
                {
                    vGrade1 = "A2";
                }
                else if (Math.Round(total_Obtain) >= 71 & Math.Round(total_Obtain) <= 80)
                {
                    vGrade1 = "B1";
                }
                else if (Math.Round(total_Obtain) >= 61 & Math.Round(total_Obtain) <= 70)
                {
                    vGrade1 = "B2";
                }
                else if (Math.Round(total_Obtain) >= 51 & Math.Round(total_Obtain) <= 60)
                {
                    vGrade1 = "C1";
                }
                else if (Math.Round(total_Obtain) >= 41 & Math.Round(total_Obtain) <= 50)
                {
                    vGrade1 = "C2";
                }

                else if (Math.Round(total_Obtain) >= 33 & Math.Round(total_Obtain) <= 40)
                {
                    vGrade1 = "D";
                }
                else if (Math.Round(total_Obtain) >= 21 & Math.Round(total_Obtain) <= 32)
                {
                    vGrade1 = "E1";
                }
                else if (Math.Round(total_Obtain) >= 0 & Math.Round(total_Obtain) <= 20)
                {
                    vGrade1 = "E2";
                }
            }
            
            catch(Exception e) { }
            return vGrade1;
        }
    }
}
