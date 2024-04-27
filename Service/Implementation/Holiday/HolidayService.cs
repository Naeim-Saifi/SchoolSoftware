using AdminDashboard.Server.Service.Interface.Holiday;
using AdminDashboard.Server.Service.Interface.LocalStorage;
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
using AdminDashboard.Server.Models.Holiday;
namespace AdminDashboard.Server.Service.Implementation.Holiday
{
    public class HolidayService:IHolidayService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public HolidayService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<HolidayDetailsListModel>> GetHolidayDetails(string SchoolCode, string FinancialYear, int UserId, string OperationType)
        {
            
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                 
                var r = await httpClient.GetJsonAsync<Response>($"/api/Holiday/GetHolidayDetails?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&UserId={UserId}&OperationType={OperationType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<HolidayDetailsListModel[]>(el.GetRawText());
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

        public async Task<IEnumerable<HolidayTypeListMaster>> GetHolidayTypeMasterDetails(string SchoolCode, string FinancialYear, int UserId, string OperationType)
        {

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Holiday/GetHolidayTypeMasterDetails?SchoolCode={SchoolCode}&FinancialYear={FinancialYear}&UserId={UserId}&OperationType={OperationType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                if (r.isError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<HolidayTypeListMaster[]>(el.GetRawText());
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

        public async Task<APIReturnModel> AddUpdateHolidayDetails(HolidayDetailsModel holidayDetailsModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Holiday/AddUpdateHolidayDetails", holidayDetailsModel);
            return response;
        }
        public async Task<APIReturnModel> AddUpdateHolidayTypeMaster(HolidayTypeMasterModel holidayTypeMaster)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Holiday/AddUpdateHolidayTypeMaster", holidayTypeMaster);
            return response;
        }

        public async Task<List<MonthNameList>> GetMonthNameList()
        {
            var dayList = new List<MonthNameList>() {
                new MonthNameList(){ MonthId = "1", MonthName="January"},
                new MonthNameList(){ MonthId = "2", MonthName="February"},
                new MonthNameList(){ MonthId = "3", MonthName="March"},
                new MonthNameList(){ MonthId = "4", MonthName="April"},
                new MonthNameList(){ MonthId = "5", MonthName="May"},
                new MonthNameList(){ MonthId = "6", MonthName="June"},
                new MonthNameList(){ MonthId = "7", MonthName="July"},
                new MonthNameList(){ MonthId = "8", MonthName="August"},
                new MonthNameList(){ MonthId = "9", MonthName="September"},
                new MonthNameList(){ MonthId = "10", MonthName="October"},
                new MonthNameList(){ MonthId = "11", MonthName="November"},
                new MonthNameList(){ MonthId = "12", MonthName="December"},
            };

            return dayList;
        }

    }
}
