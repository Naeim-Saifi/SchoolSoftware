//using AdminDashboard.Server.API_Service.Interface.MasterData;
//using AdminDashboard.Server.Service.Interface.LocalStorage;
//using AIS.Data.APIReturnModel;
//using AIS.Data.Model;
//using AIS.Data.RequestResponseModel.Enquiry;
//using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Class;
//using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Section;
//using Microsoft.AspNetCore.Components;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using Syncfusion.Blazor.Charts.Chart.Internal;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace AdminDashboard.Server.API_Service.Service.MasterData
//{
//    public class MasterDataService : IMasterDataService
//    {
//        private readonly HttpClient httpClient;
//        private ILocalStorageService _localStorageService;

//        public MasterDataService(HttpClient httpClient, ILocalStorageService localStorageService)
//        {
//            this.httpClient = httpClient;
//            _localStorageService = localStorageService;
//        }

//        public async Task<APIReturnModel> CRUD_ClassDetails(ClassApiModel classApiModel)
//        {
//            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("", classApiModel);
//            return response;
//        }

//        public async Task<IEnumerable<ClassOutputModel>> GET_ClassDetails_List(ClassParaModel classParaModel)
//        {
//            try
//            {
//                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
//                {
//                    ContractResolver = new CamelCasePropertyNamesContractResolver()
//                };

//                var r = await httpClient.GetJsonAsync<Response>($"  ?SchoolCode={classParaModel.schoolCode}" +
//                    $"&FinancialYear={classParaModel.financialYear}" +
//                    $"&Classid={classParaModel.ClassId}" +
//                    $"&RoleID={classParaModel.userRoleId}" +
//                    $"&ReportType={classParaModel.reportType}");
//                JsonElement el;
//                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

//                if (r.IsError == false)
//                {
//                    return System.Text.Json.JsonSerializer.Deserialize<ClassOutputModel[]>(el.GetRawText());
//                }
//                else
//                {
//                    return null;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//            return null;

//        }




//        public async Task<APIReturnModel> CRUD_SectionDetails(SectionApiModel sectionApiModel)
//        {
//            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("", sectionApiModel);
//            return response;
//        }



//        public async Task<IEnumerable<SectionOutputModel>> GET_SectionDetails_List(SectionParaModel sectionParaModel)
//        {
//            try
//            {
//                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
//                {
//                    ContractResolver = new CamelCasePropertyNamesContractResolver()
//                };

//                var r = await httpClient.GetJsonAsync<Response>($"  ?SchoolCode={sectionParaModel.schoolCode}" +
//                    $"&FinancialYear={sectionParaModel.financialYear}" +
//                    $"&Classid={sectionParaModel.SectionId}" +
//                    $"&RoleID={sectionParaModel.userRoleId}" +
//                    $"&ReportType={sectionParaModel.reportType}");
//                JsonElement el;
//                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

//                if (r.IsError == false)
//                {
//                    return System.Text.Json.JsonSerializer.Deserialize<SectionOutputModel[]>(el.GetRawText());
//                }
//                else
//                {
//                    return null;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//            return null;

//        }

//    }
//}
