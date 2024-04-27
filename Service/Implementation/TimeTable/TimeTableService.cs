using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AIS.Model;
using AIS.Model.Mapping;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.TimeTable
{
    public class TimeTableService:ITimeTableService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public TimeTableService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<TimeTableListModel>> GetTimeTableList(string SchoolCode, string FinancialYear, int UserId, string OperationType)
        {
            //MasterClassList[] masterClassList;

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                //var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");

                //int SchoolId = user.schoolId;

                var r = await httpClient.GetJsonAsync<Response>($"/api/TimeTable/GetTimeTableList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&UserId={UserId}&OperationType={OperationType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<TimeTableListModel[]>(el.GetRawText());
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

        public async Task<IEnumerable<PeriodListModel>> GetPeriodList(string SchoolCode, string FinancialYear, int UserId, string OperationType)
        {
            //MasterClassList[] masterClassList;

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                //var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");

                //int SchoolId = user.schoolId;

                var r = await httpClient.GetJsonAsync<Response>($"/api/TimeTable/GetPeriodList?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&UserId={UserId}&OperationType={OperationType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<PeriodListModel[]>(el.GetRawText());
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

        public async Task<IEnumerable<DayModelList>> GetDaysNameList()
        {
           var dayList = new List<DayModelList>() {
                new DayModelList(){ DayId = 1, DayName="Sunday"},
                new DayModelList(){ DayId = 2, DayName="Monday"},
                new DayModelList(){ DayId = 3, DayName="Tuesday"},
                new DayModelList(){ DayId = 4, DayName="Wednesday"},
                new DayModelList(){ DayId = 5, DayName="Thursday"},
                new DayModelList(){ DayId = 6, DayName="Friday"},
                new DayModelList(){ DayId = 7, DayName="Saturday"},

            };
            
            return dayList;
        }
        public async Task<APIReturnModel> AddUpdateTimeTable(ViewTimeTableModel timeTableModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/TimeTable/AddUpdateTimeTable", timeTableModel);
            return response;

        }
        public async Task<APIReturnModel> AddUpdatePeriodDetails(PeriodModel periodModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/TimeTable/AddUpdatePeriodMaster", periodModel);
            return response;

        }
    }
}
