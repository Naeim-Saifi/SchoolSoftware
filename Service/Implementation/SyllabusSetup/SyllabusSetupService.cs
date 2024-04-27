using AdminDashboard.Server.Models.SyllabusSetup;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AdminDashboard.Server.Service.Interface.SyllabusSetup;
using AIS.Data.BaseClass;
using AIS.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.SyllabusSetup
{
    public class SyllabusSetupService : ISyllabusSetupService
        {
            private readonly HttpClient httpClient;
            private ILocalStorageService _localStorageService;
            public SyllabusSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
            {
                this.httpClient = httpClient;
                _localStorageService = localStorageService;
            }

            public async Task<IEnumerable<SyllabusSetupListModel>> GetSyllabusSetupDetails(DefaultInputParameterModel defaultInputParameterModel)
            {

                try
                {
                    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    var r = await httpClient.GetJsonAsync<Response>($"/api/SyllabusSetup/GetSyllabusList?SchoolCode={defaultInputParameterModel.SchoolCode}&FinancialYear={defaultInputParameterModel.FinancialYear}&UserId={defaultInputParameterModel.Userid}&OperationType={defaultInputParameterModel.OperationType}");
                    JsonElement el;
                    var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

                    if (r.isError == false)
                    {
                        return System.Text.Json.JsonSerializer.Deserialize<SyllabusSetupListModel[]>(el.GetRawText());
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

            public async Task<APIReturnModel> AddUpdateSyllabusSetup(SyllabusSetupAPIModel syllabusSetupModel)
            {
                APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>($"/api/SyllabusSetup/AddUpdateSyllabusSetup", syllabusSetupModel);
                return response;
            }

        }
    }

 
