using AdminDashboard.Server.API_Service.Interface.Enquiry;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.Enquiry;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.Enquiry
{
    public class EnquiryService: IEnquiryService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public EnquiryService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<APIReturnModel> CRUD_EnquiryDetails(EnquiryAPIModel enquiryAPIModel)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Enquiry/CRUD_EnquiryDetails", enquiryAPIModel);
            return response;
        }
       public async Task<IEnumerable<EnquiryListModel>> GET_EnquiryDetails_List(EnquiryParaModel enquiryParaModel)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Enquiry/GET_EnquiryDetails_List?SchoolCode={enquiryParaModel.schoolCode}" +
                    $"&FinancialYear={enquiryParaModel.financialYear}" +                    
                    $"&EnquiryID={enquiryParaModel.EnquiryId}" +
                    $"&RoleID={enquiryParaModel.userRoleId}" +
                    $"&ReportType={enquiryParaModel.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<EnquiryListModel[]>(el.GetRawText());
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
